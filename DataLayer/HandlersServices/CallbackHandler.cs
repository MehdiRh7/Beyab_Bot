using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using DataLayer;
using DataLayer.Utilities;
using DataLayer.Handlers;

namespace DataLayer.HandlersServices
{
    public class CallbackHandler : ICallbackHandler
    {
        private readonly IPersonRepository _personRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly IOnlinesRepository _onlinesRepository;
        private readonly IBlockRepository _blockRepository;
        private readonly IValidationRepository _validationRepository;
        private readonly KeyboardProvider _keyboardProvider;

        public CallbackHandler(IPersonRepository personRepository, IFriendRepository friendRepository, IOnlinesRepository onlinesRepository, IBlockRepository blockRepository, IValidationRepository validationRepository, KeyboardProvider keyboardProvider)
        {
            _personRepository = personRepository;
            _friend_repository_check(personRepository);
            _friendRepository = friendRepository;
            _onlinesRepository = onlinesRepository;
            _blockRepository = block_repository_check(blockRepository);
            _validationRepository = validationRepository;
            _keyboardProvider = keyboardProvider ?? throw new ArgumentNullException(nameof(keyboardProvider));
        }

        // small guards to keep constructor consistent with older code style
        private void _friend_repository_check(IPersonRepository r) { /* no-op for readability */ }
        private IBlockRepository block_repository_check(IBlockRepository r) { return r; }

        public async Task HandleCallbackAsync(Update update, TelegramBotClient bot)
        {
            if (update?.CallbackQuery == null) return;

            var cb = update.CallbackQuery;
            var fromId = cb.From.Id;
            var data = (cb.Data ?? string.Empty).Trim();

            // Always acknowledge the callback quickly
            try
            {
                await bot.AnswerCallbackQueryAsync(cb.Id).ConfigureAwait(false);
            }
            catch { /* ignore */ }

            // Extract leading numeric id (chat id) then suffix command
            var targetIdStr = new string(data.TakeWhile(char.IsDigit).ToArray());
            if (!long.TryParse(targetIdStr, out var targetChatId))
            {
                // If can't parse numeric prefix try alternative: if data equals plain commands, just return
                return;
            }

            var suffix = data.Substring(targetIdStr.Length).Trim();

            // Load current state once
            var persons = _personRepository.GetAllPerson().ToList();
            var friends = _friendRepository.GetAllFriendList().ToList();
            var onlines = _onlinesRepository.GetAllOnlines().ToList();
            var blocks = _block_repository_getall();
            var validations = _validationRepository.GetAllValidations().ToList();

            // Helper local functions
            Func<long, Person> getPersonByChat = chat => persons.FirstOrDefault(p => p.chatid == chat);

            // Case: send a talk request: "{id}درخواست صحبت"
            if (suffix == "درخواست صحبت")
            {
                // Caller is fromId, target is targetChatId
                // Check block: if target has blocked caller, notify caller
                if (blocks.Any(b => b.P1Chatid == targetChatId && b.P2Chatid == fromId))
                {
                    var sbBlocked = new StringBuilder();
                    sbBlocked.AppendLine("<b>کاربر گرامی متاسفانه دوستت شمارو بلاک کرده و نمیتونین بهش درخواست بفرستین.</b>");
                    await bot.SendTextMessageAsync(fromId, sbBlocked.ToString(), ParseMode.Html, replyMarkup: _keyboard_provider_safe(_keyboardProvider?.SearchKeyboard)).ConfigureAwait(false);
                    return;
                }

                // If already a pending validation from caller to target with valid date, notify caller
                if (validations.Any(v => v.P1Chatid == fromId && v.P2Chatid == targetChatId && v.Date.HasValue && v.Date.Value > DateTime.Now))
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("<b>کاربر گرامی به این دوستت تازگی درخواست دادی ، در هر روز فقط یکبار میتونی به هر شخصی درخواست بدی.</b>");
                    await bot.SendTextMessageAsync(fromId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboard_provider_safe(_keyboardProvider?.SearchKeyboard)).ConfigureAwait(false);
                    return;
                }

                // Create validation and send request to target with inline accept/reject/block options
                var validation = new RequestValidation
                {
                    P1Chatid = fromId,
                    P2Chatid = targetChatId,
                    Date = DateTime.Now.AddDays(1),
                    PersonId = getPersonByChat(fromId)?.PersonId ?? 0
                };

                _validationRepository.InsertValidation(validation);
                await _validationRepository.Save().ConfigureAwait(false);

                try
                {
                    var accept = new InlineKeyboardButton { Text = "قبول درخواست", CallbackData = fromId + "قبول درخواست", Pay = true };
                    var reject = new InlineKeyboardButton { Text = "رد درخواست", CallbackData = fromId + "رد درخواست", Pay = true };
                    var block = new InlineKeyboardButton { Text = "بلاک", CallbackData = fromId + "بلاک", Pay = true };

                    var inline = new InlineKeyboardMarkup(new[] { accept, reject, block });

                    var sbSend = new StringBuilder();
                    sbSend.AppendLine("<b>یکی از کابرا بهت درخواست صحبت داده . میتونی با گزینه های پایین درخواستشو قبول کنی یا رد کنی.</b>");
                    await bot.SendTextMessageAsync(targetChatId, sbSend.ToString(), ParseMode.Html, replyMarkup: inline).ConfigureAwait(false);

                    var sbAck = new StringBuilder();
                    sbAck.AppendLine("<b>درخواست برای دوستت ارسال شد.</b>");
                    sbAck.AppendLine("<b>حالا چه کاری برات انجام بدم؟</b>");
                    await bot.SendTextMessageAsync(fromId, sbAck.ToString(), ParseMode.Html, replyMarkup: _keyboard_provider_safe(_keyboardProvider?.SearchKeyboard)).ConfigureAwait(false);

                    // update caller command state
                    var callerPerson = getPersonByChat(fromId);
                    if (callerPerson != null)
                    {
                        callerPerson.CommandName = "SearchKeyboard";
                        _personRepository.UpdatePerson(callerPerson);
                        await _personRepository.Save().ConfigureAwait(false);
                    }
                }
                catch
                {
                    var sbErr = new StringBuilder();
                    sbErr.AppendLine("<b>کاربر گرامی اشکالی در ارتباط بوجود آمده و ارسال پیام امکان پذیر نیست.</b>");
                    await bot.SendTextMessageAsync(fromId, sbErr.ToString(), ParseMode.Html).ConfigureAwait(false);
                }

                return;
            }

            // Case: friend accepted the request: "{id}قبول درخواست"
            if (suffix == "قبول درخواست")
            {
                // targetChatId here is the original requester (P1), fromId is the friend (P2)
                // Ask original requester to final-confirm (تایید / عدم تایید)
                try
                {
                    var confirm = new InlineKeyboardButton { Text = "تایید", CallbackData = fromId + "تایید", Pay = true };
                    var deny = new InlineKeyboardButton { Text = "عدم تایید", CallbackData = fromId + "عدم تایید", Pay = true };
                    var inline = new InlineKeyboardMarkup(new[] { confirm, deny });

                    var sbNotify = new StringBuilder();
                    sbNotify.AppendLine($"<b>کاربر گرامی دوستت درخواست چتت رو پذیرفت لطفا تایید کن تا وصلتون کنم.</b>");
                    await bot.SendTextMessageAsync(targetChatId, sbNotify.ToString(), ParseMode.Html, replyMarkup: inline).ConfigureAwait(false);

                    // set requester's CommandName to QuitKeyboard and LastUser to friend
                    var requester = getPersonByChat(targetChatId);
                    if (requester != null)
                    {
                        requester.CommandName = "QuitKeyboard";
                        requester.LastUser = fromId;
                        _personRepository.UpdatePerson(requester);
                        await _personRepository.Save().ConfigureAwait(false);
                    }
                }
                catch
                {
                    var sbErr = new StringBuilder();
                    sbErr.AppendLine("<b>کاربر گرامی اشکالی در ارتباط بوجود آمده و ارسال پیام امکان پذیر نیست.</b>");
                    await bot.SendTextMessageAsync(fromId, sbErr.ToString(), ParseMode.Html).ConfigureAwait(false);
                }
                return;
            }

            // Case: final approval to connect: "{id}تایید"
            if (suffix == "تایید")
            {
                // fromId == confirmer (original requester), targetChatId == friend who initially accepted
                // Ensure there is a validation entry and neither user is currently in an online chat
                var validation = validations.FirstOrDefault(v => v.P1Chatid == fromId && v.P2Chatid == targetChatId);
                if (validation == null || (validation.Date.HasValue && validation.Date.Value < DateTime.Now))
                {
                    await bot.SendTextMessageAsync(fromId, "<b>کاربر گرامی متاسفانه این درخواست دیگه اعتباری نداره!</b>", ParseMode.Html).ConfigureAwait(false);
                    return;
                }

                // Check if either is already in an active Onlines pair
                if (onlines.Any(o => (o.chatid == fromId || o.chatid == targetChatId) && o.User2 != 0))
                {
                    await bot.SendTextMessageAsync(fromId, "<b>کاربر گرامی شما در حال صحبت با شخص دیگری هستی.</b>", ParseMode.Html).ConfigureAwait(false);
                    return;
                }

                try
                {
                    // Create Onlines for both participants (if not exist)
                    var personA = getPersonByChat(fromId);
                    var personB = getPersonByChat(targetChatId);

                    if (personA != null && personB != null)
                    {
                        var sa = new StringBuilder();
                        sa.AppendLine("<b>به دوستت وصل شدی بهش سلام کن :</b>)");
                        await bot.SendTextMessageAsync(fromId, sa.ToString(), ParseMode.Html, replyMarkup: _keyboard_provider_safe(_keyboardProvider?.StopKeyboard)).ConfigureAwait(false);
                        var onA = new Onlines { Username = personA.PersonName, PersonId = personA.PersonId, chatid = personA.chatid, User2 = targetChatId };
                        _onlinesRepository.InsertOnline(onA);
                        await _onlinesRepository.Save().ConfigureAwait(false);
                        personA.CommandName = "StopKeyboard";
                        _personRepository.UpdatePerson(personA);
                        await _personRepository.Save().ConfigureAwait(false);

                        var sb = new StringBuilder();
                        sb.AppendLine("<b>به دوستت وصل شدی بهش سلام کن :</b>)");
                        await bot.SendTextMessageAsync(targetChatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboard_provider_safe(_keyboardProvider?.StopKeyboard)).ConfigureAwait(false);
                        var onB = new Onlines { Username = personB.PersonName, PersonId = personB.PersonId, chatid = personB.chatid, User2 = fromId };
                        _onlinesRepository.InsertOnline(onB);
                        await _onlinesRepository.Save().ConfigureAwait(false);
                        personB.CommandName = "StopKeyboard";
                        _personRepository.UpdatePerson(personB);
                        await _personRepository.Save().ConfigureAwait(false);
                    }

                    // Remove the validation
                    _validationRepository.DeleteValidation(validation);
                    await _validationRepository.Save().ConfigureAwait(false);
                }
                catch
                {
                    await bot.SendTextMessageAsync(fromId, "<b>خطا در برقراری ارتباط</b>", ParseMode.Html).ConfigureAwait(false);
                }

                return;
            }

            // Case: final denial: "{id}عدم تایید"
            if (suffix == "عدم تایید")
            {
                // fromId is the confirmer; targetChatId is the friend who previously accepted
                var validation = validations.FirstOrDefault(v => v.P1Chatid == fromId && v.P2Chatid == targetChatId);
                if (validation != null)
                {
                    _validationRepository.DeleteValidation(validation);
                    await _validationRepository.Save().ConfigureAwait(false);
                    await bot.SendTextMessageAsync(fromId, "<b>درخواست با موفقیت حذف شد</b>", ParseMode.Html).ConfigureAwait(false);

                    // reset friend's CommandName to SearchKeyboard where applicable
                    var friendPerson = persons.FirstOrDefault(p => p.chatid == targetChatId);
                    if (friendPerson != null)
                    {
                        friendPerson.CommandName = "SearchKeyboard";
                        _personRepository.UpdatePerson(friendPerson);
                        await _personRepository.Save().ConfigureAwait(false);
                    }

                    await bot.SendTextMessageAsync(targetChatId, "<b>کاربر مورد نظر تایید نکرد</b>\n<b>حالا چه کاری برات انجام بدم؟</b>", ParseMode.Html).ConfigureAwait(false);
                }
                else
                {
                    await bot.SendTextMessageAsync(fromId, "<b>کاربر گرامی این درخواست دیگه اعتباری نداره!</b>", ParseMode.Html).ConfigureAwait(false);
                }

                return;
            }

            // Case: reject request by target: "{id}رد درخواست"
            if (suffix == "رد درخواست")
            {
                // targetChatId is original sender, fromId is who rejected
                var validation = validations.FirstOrDefault(v => v.P1Chatid == targetChatId && v.P2Chatid == fromId);
                if (validation != null)
                {
                    if (validation.Date.HasValue && validation.Date.Value > DateTime.Now)
                    {
                        _validationRepository.DeleteValidation(validation);
                        await _validationRepository.Save().ConfigureAwait(false);
                        await bot.SendTextMessageAsync(fromId, "<b>درخواست با موفقیت حذف شد</b>", ParseMode.Html).ConfigureAwait(false);
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(fromId, "<b>کاربر گرامی این درخواست دیگه اعتباری نداره!</b>", ParseMode.Html).ConfigureAwait(false);
                    }
                }
                else
                {
                    await bot.SendTextMessageAsync(fromId, "<b>کاربر گرامی این درخواست دیگه اعتباری نداره!</b>", ParseMode.Html).ConfigureAwait(false);
                }
                return;
            }

            // Case: block user: "{id}بلاک"
            if (suffix == "بلاک")
            {
                // fromId wants to block targetChatId (or vice versa depending how you created data)
                // We'll treat fromId as the person performing the block and targetChatId as the to-be-blocked
                // Avoid duplicate blocks
                if (!_blockExists(fromId, targetChatId))
                {
                    var person = getPersonByChat(fromId);
                    var block = new BlockList
                    {
                        PersonId = person?.PersonId ?? 0,
                        P1Chatid = fromId,
                        P2Chatid = targetChatId
                    };

                    _blockRepository.InsertBlock(block);
                    await _blockRepository.Save().ConfigureAwait(false);

                    await bot.SendTextMessageAsync(fromId, "<b>کاربر مورد نظر بلاک شد.</b>", ParseMode.Html).ConfigureAwait(false);
                }
                else
                {
                    await bot.SendTextMessageAsync(fromId, "<b>شما قبلا این شخص رو بلاک کردین.</b>", ParseMode.Html).ConfigureAwait(false);
                }

                return;
            }

            // Case: change friend name: "{id}تغییر اسم"
            if (suffix == "تغییر اسم")
            {
                var person = getPersonByChat(fromId);
                if (person != null)
                {
                    person.CommandName = "ComeBackKeyboard";
                    person.LastUser = targetChatId;
                    _personRepository.UpdatePerson(person);
                    await _personRepository.Save().ConfigureAwait(false);

                    await bot.SendTextMessageAsync(fromId, "<b>بسیار خب! حالا یه اسم دیگه برای دوستت انتخاب کن</b>", ParseMode.Html, replyMarkup: _keyboard_provider_safe(_keyboardProvider?.ComeBackKeyboard)).ConfigureAwait(false);
                }
                return;
            }

            // Case: delete friend: "{id}حذف"
            if (suffix == "حذف")
            {
                var person = getPersonByChat(fromId);
                if (person != null)
                {
                    person.LastUser = targetChatId;
                    person.CommandName = "DeleteFriendKeyboard";
                    _personRepository.UpdatePerson(person);
                    await _personRepository.Save().ConfigureAwait(false);

                    await bot.SendTextMessageAsync(fromId, "<b>آیا مطمئنی میخوای حذفش کنی؟</b>", ParseMode.Html, replyMarkup: _keyboard_provider_safe(_keyboardProvider?.DeleteFriendKeyboard)).ConfigureAwait(false);
                }
                return;
            }

            // Unhandled suffix: ignore
        }

        // small helpers to avoid null keyboard provider issues
        private ReplyKeyboardMarkup _keyboard_provider_safe(ReplyKeyboardMarkup kb)
        {
            return kb ?? new ReplyKeyboardMarkup() { ResizeKeyboard = true };
        }

        private bool _blockExists(long who, long whom)
        {
            try
            {
                return _blockRepository.GetAllBlockList().Any(b => b.P1Chatid == who && b.P2Chatid == whom);
            }
            catch { return false; }
        }

        private System.Collections.Generic.List<BlockList> _block_repository_getall()
        {
            try { return _blockRepository.GetAllBlockList().ToList(); }
            catch { return new System.Collections.Generic.List<BlockList>(); }
        }
    }
}