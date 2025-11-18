using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using DataLayer;
using DataLayer.Handlers;
using DataLayer.Utilities;

namespace DataLayer.HandlersServices
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IPersonRepository _personRepository;
        private readonly IOnlinesRepository _onlinesRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly IValidationRepository _validationRepository;
        private readonly KeyboardProvider _keyboardProvider;

        public MessageHandler(IPersonRepository personRepository, IOnlinesRepository onlinesRepository, IValidationRepository validationRepository, KeyboardProvider keyboardProvider, IFriendRepository friendRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _onlinesRepository = onlinesRepository ?? throw new ArgumentNullException(nameof(onlinesRepository));
            _validation_repository_check(validationRepository);
            _validationRepository = validationRepository;
            _keyboardProvider = keyboardProvider ?? throw new ArgumentNullException(nameof(keyboardProvider));
            _friendRepository = friendRepository;
        }

        // small no-op to keep constructor compatible with some styles
        private void _validation_repository_check(IValidationRepository r) { /*noop*/ }

        public async Task HandleMessageAsync(Update update, TelegramBotClient bot)
        {
            if (update?.Message == null) return;

            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text?.Trim();
            if (text == null) return;

            var persons = _personRepository.GetAllPerson().ToList();
            var me = persons.FirstOrDefault(p => p.chatid == chatId);

            // route by incoming text
            if (text == "/start")
            {
                await HandleStartAsync(update, bot, me).ConfigureAwait(false);
                return;
            }

            // Registration start
            if (text == "ثبت نام \U0001F63B")
            {
                await StartRegistrationAsync(chatId, bot, me, update).ConfigureAwait(false);
                return;
            }

            // About bot
            if (text == "درباره ربات \U00002764")
            {
                await SendAboutAsync(chatId, bot, me).ConfigureAwait(false);
                return;
            }

            // Gender selection
            if (text == "پسرم \U0001F466" || text == "دخترم \U0001F467")
            {
                await HandleGenderSelectionAsync(chatId, bot, me, text).ConfigureAwait(false);
                return;
            }

            // City selection (uses provider list)
            var allCities = ExtractCityListFromProvider();
            if (allCities.Contains(text) && me != null)
            {
                await HandleCitySelectionAsync(chatId, bot, me, text).ConfigureAwait(false);
                return;
            }

            // Age selection
            if ((text == "18 تا 20 سال" || text == "20 تا 25 سال" || text == "25 تا 30 سال" || text == "30+ سال") && me != null)
            {
                if (me.CommandName == "AgeKeyboard" || me.CommandName == "AgeFilterKeyboard")
                {
                    await HandleAgeSelectionAsync(chatId, bot, me, text).ConfigureAwait(false);
                }
                
                else
                {
                    await SendStatePromptAsync(chatId, bot, me).ConfigureAwait(false);
                }
                return;
            }

            // Start search (the label used in your keyboard)
            if (text == "\U0001F50D" + "برام یه هم صحبت پیدا کن!")
            {
                await HandleGenderFilterAsync(chatId, bot, me).ConfigureAwait(false);
                return;
            }
            if (text == "دختر باشه" + " \U0001F467" || text == "پسر باشه" + " \U0001F466")
            {
                await HandleGenderSelectionAsync(chatId, bot, me,text).ConfigureAwait(false);
                return;
            }
            if (text == "اتمام صحبت")
            {
                if (me != null)
                {
                    if (me.CommandName == "StopKeyboard")
                    {
                        me.CommandName = "BeCancelledKeyboard";
                        _personRepository.UpdatePerson(me);
                        await _personRepository.Save().ConfigureAwait(false);

                        var sbBC = new StringBuilder();
                        sbBC.AppendLine("<b>آیا مطمئنی میخوای صحبت رو ببندی؟\U0001F628</b>");
                        await bot.SendTextMessageAsync(chatId, sbBC.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.BeCancelledKeyboard).ConfigureAwait(false);
                        return;
                    }

                    if (me.CommandName == "CancellKeyboard")
                    {
                        var sbRepeat = new StringBuilder();
                        sbRepeat.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
                        sbRepeat.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                        // show cancell keyboard
                        await bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.CancellKeyboard).ConfigureAwait(false);
                        return;
                    }

                    if (me.CommandName == "ComeBackKeyboard")
                    {
                        // This branch expects user's message to be the new friend name
                        var friends = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId).ToList();

                        // if there is no friend with P2Chatid == me.LastUser, we are adding a new friend
                        if (!friends.Any(f => f.P2Chatid == me.LastUser))
                        {
                            // validate name uniqueness
                            if (!friends.Any(f => f.name == text))
                            {
                                if (friends.Count == 1)
                                {
                                    // update single existing friend
                                    var friend = friends.Single();
                                    friend.P2Chatid = me.LastUser;
                                    friend.name = text;
                                    _friendRepository.UpdateFriend(friend);
                                    await _friendRepository.Save().ConfigureAwait(false);
                                }
                                else
                                {
                                    var fr = new FriendsList()
                                    {
                                        P1Chatid = chatId,
                                        P2Chatid = me.LastUser,
                                        name = text,
                                        PersonId = me.PersonId
                                    };
                                    _friendRepository.InsertFriend(fr);
                                    await _friendRepository.Save().ConfigureAwait(false);
                                }

                                me.CommandName = "SearchKeyboard";
                                _personRepository.UpdatePerson(me);
                                await _personRepository.Save().ConfigureAwait(false);

                                var sbtext = new StringBuilder();
                                sbtext.AppendLine("<b>کاربر با موفقیت به لیست دوستات اضافه شد\U00002705</b>");
                                sbtext.AppendLine("<b>خب حالا چه کاری برات انجام بدم؟</b>");
                                await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.SearchKeyboard).ConfigureAwait(false);
                            }
                            else
                            {
                                var sbtext = new StringBuilder();
                                sbtext.AppendLine("<b>کاربر گرامی متاسفانه این اسم رو قبلا برای دوست دیگه ای انتخاب کردی لطفا یه اسم دیگه مشخض کن\U0001F64F</b>");
                                await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, replyMarkup: _keyboard_provider_safe(_keyboardProvider.ComeBackKeyboard)).ConfigureAwait(false);
                            }
                        }
                        else
                        {
                            // friend exists: update its name
                            foreach (var fr in friends.Where(f => f.P2Chatid == me.LastUser))
                            {
                                fr.name = text;
                                _friendRepository.UpdateFriend(fr);
                                await _friendRepository.Save().ConfigureAwait(false);

                                me.CommandName = "SearchKeyboard";
                                _personRepository.UpdatePerson(me);
                                await _personRepository.Save().ConfigureAwait(false);

                                var sbtext = new StringBuilder();
                                sbtext.AppendLine("<b>نام دوستت با موفقیت تغییر کرد\U00002705</b>");
                                await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.SearchKeyboard).ConfigureAwait(false);
                            }
                        }

                        return;
                    }

                    // fallback
                    var sbUnknown = new StringBuilder();
                    sbUnknown.AppendLine("<b>متوجه نشدم!</b>");
                    sbUnknown.AppendLine("<b>لطفا از گزینه های زیر استفاده کنید</b>");
                    await bot.SendTextMessageAsync(chatId, sbUnknown.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.MainKeyboard).ConfigureAwait(false);
                }

                return;
            }
            if (text == "اتمام")
            {
                if (me == null) return;

                if (me.CommandName != "BeCancelledKeyboard")
                {
                    // Not in expected state - show state keyboard
                    await SendStatePromptAsync(chatId, bot, me).ConfigureAwait(false);
                    return;
                }

                try
                {
                    // Mark person as AfterCancelledKeyboard
                    me.CommandName = "AfterCancelledKeyboard";
                    _personRepository.UpdatePerson(me);
                    await _personRepository.Save().ConfigureAwait(false);

                    // Find online entries for this person
                    var onlines = _onlinesRepository.GetAllOnlines().ToList();

                    long chatid1 = chatId;
                    long chatid2 = chatId;

                    // Delete onlines belonging to this person
                    var myOnlines = onlines.Where(o => o.PersonId == me.PersonId).ToList();
                    foreach (var o in myOnlines)
                    {
                        chatid1 = o.chatid;
                        _onlinesRepository.DeleteOnline(o);
                    }

                    // Find partner entries where this person was User2 (i.e., partner.User2 == me.chatid)
                    var partnerOnlineEntries = onlines.Where(o => o.User2 == me.chatid).ToList();
                    foreach (var o in partnerOnlineEntries)
                    {
                        // set partner's person state to SearchKeyboard and LastUser
                        var partnerPerson = o.Person;
                        if (partnerPerson != null)
                        {
                            partnerPerson.CommandName = "SearchKeyboard";
                            partnerPerson.LastUser = chatId;
                            _personRepository.UpdatePerson(partnerPerson);
                            await _personRepository.Save().ConfigureAwait(false);

                            chatid2 = o.chatid;
                        }

                        // delete partner's online row
                        _onlinesRepository.DeleteOnline(o);
                    }

                    // Persist deletions
                    await _onlines_repository_save_safe().ConfigureAwait(false);

                    // Notify both sides
                    var sb1 = new StringBuilder();
                    sb1.AppendLine("<b>صحبت رو قطع کردی حالا چه کاری برات انجام بدم؟😆</b>");
                    await bot.SendTextMessageAsync(chatid1, sb1.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.AfterCancelledKeyboard).ConfigureAwait(false);

                    var sb2 = new StringBuilder();
                    sb2.AppendLine("<b>کاربر از چت خارج شد حالا چه کاری برات انجام بدم؟</b>");
                    await bot.SendTextMessageAsync(chatid2, sb2.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.SearchKeyboard).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    // In original code there was a DB transaction; here we do best-effort and log
                    Console.WriteLine($"Error finishing conversation: {ex.Message}");
                }

                return;
            }
            if (text == "ادامه صحبت")
            {
                if (me != null && me.CommandName == "BeCancelledKeyboard")
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("<b>شما در حال چت با هم صحبت خود هستید\U0001F60A</b>");
                    await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.StopKeyboard).ConfigureAwait(false);

                    me.CommandName = "StopKeyboard";
                    _personRepository.UpdatePerson(me);
                    await _personRepository.Save().ConfigureAwait(false);
                }
                else if (me != null)
                {
                    await SendStatePromptAsync(chatId, bot, me).ConfigureAwait(false);
                }

                return;
            }
            // If chatting (StopKeyboard) forward text to partner
            if (me != null && me.CommandName == "StopKeyboard")
            {
                await ForwardTextToPartnerAsync(chatId, bot, update).ConfigureAwait(false);
                return;
            }
            
            // Default fallback: prompt with the correct keyboard according to state
            await SendStatePromptAsync(chatId, bot, me).ConfigureAwait(false);
        }
        private ReplyKeyboardMarkup _keyboard_provider_safe(ReplyKeyboardMarkup kb)
        {
            return kb ?? new ReplyKeyboardMarkup() { ResizeKeyboard = true };
        }

        private async Task _onlines_repository_save_safe()
        {
            try { await _onlinesRepository.Save().ConfigureAwait(false); }
            catch { /* ignore */ }
        }
        private async Task HandleStartAsync(Update update, TelegramBotClient bot, Person me)
        {
            var chatId = update.Message.Chat.Id;
            if (me == null)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"<b>سلام {update.Message.From.FirstName} ، خوش اومدی \U00002764</b>");
                sb.AppendLine("<b>میتونی با گزینه های پایین بگی برات چیکار کنم \U0001F603 </b>");
                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.MainKeyboard).ConfigureAwait(false);

                var pr = new Person { chatid = chatId, Username = update.Message.From.Username, CommandName = "mainKeyboard" };
                _personRepository.InsertPerson(pr);
                await _personRepository.Save().ConfigureAwait(false);
            }
            else
            {
                // user exists: show appropriate keyboard depending on stored state
                await SendStatePromptAsync(chatId, bot, me).ConfigureAwait(false);
            }
        }

        private async Task StartRegistrationAsync(long chatId, TelegramBotClient bot, Person me, Update update)
        {
            if (me == null) return;

            if (me.CommandName == "mainKeyboard" || string.IsNullOrEmpty(me.CommandName))
            {
                me.CommandName = "GenderKeyboard";
                // ensure PersonName isn't empty (mirrors prior approach)
                if (string.IsNullOrEmpty(me.PersonName))
                {
                    me.PersonName = Guid.NewGuid().ToString();
                }
                _personRepository.UpdatePerson(me);
                await _personRepository.Save().ConfigureAwait(false);

                var sb = new StringBuilder();
                sb.AppendLine("<b>لطفا جنسیت خود را انتخاب کنید\U00002049</b>");
                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.GenderKeyboard).ConfigureAwait(false);
            }
            else
            {
                await SendStatePromptAsync(chatId, bot, me).ConfigureAwait(false);
            }
        }

        private async Task SendAboutAsync(long chatId, TelegramBotClient bot, Person me)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<b>'بیاب'</b> یک ربات دوستیابی و چت ناشناس هستش ...");
            sb.AppendLine("برای ادامه از کیبورد استفاده کنید.");
            if (me.CommandName == "mainKeyboard")
            {
                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.MainKeyboard).ConfigureAwait(false);
            }
            else if(me.CommandName == "SearchKeyboard")
            {
                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.SearchKeyboard).ConfigureAwait(false);
            }
        }

        private async Task HandleGenderSelectionAsync(long chatId, TelegramBotClient bot, Person me, string text)
        {
            if (me == null) return;

            if (me.CommandName == "GenderKeyboard")
            {
                me.PersonGender = text == "پسرم \U0001F466" ? "پسر" : "دختر";
                me.CommandName = "CityKeyboard";
                _personRepository.UpdatePerson(me);
                await _personRepository.Save().ConfigureAwait(false);

                var sb = new StringBuilder();
                sb.AppendLine("<b>لطفا استان خود را انتخاب کنید\U00002049</b>");
                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.CityKeyboard).ConfigureAwait(false);
            }
            else if (me.CommandName == "GenderFilterKeyboard")
            {
                // This branch is used when user is setting a city filter
                me.FilterGender = text == "پسر باشه" + " \U0001F466" ? "پسر" : "دختر";
                me.CommandName = "CityFilterKeyboard";
                _personRepository.UpdatePerson(me);
                await _personRepository.Save().ConfigureAwait(false);

                var sb = new StringBuilder();
                sb.AppendLine("<b>هم صحبتت از چه استانی باشه\U00002049</b>");
                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.CityFilterKeyboard).ConfigureAwait(false);

                // in case of filter change, automatically try to matchmaking if already registered
                if (!string.IsNullOrEmpty(me.PersonGender))
                {
                    await EnsureOnlinesRecordAsync(me).ConfigureAwait(false);
                    //await TryMatchmakeAsync(me, bot).ConfigureAwait(false);
                }
            }
            else
            {
                await SendStatePromptAsync(chatId, bot, me).ConfigureAwait(false);
            }
        }

        private async Task HandleCitySelectionAsync(long chatId, TelegramBotClient bot, Person me, string city)
        {
            if (me == null) return;

            if (me.CommandName == "CityKeyboard")
            {
                me.PersonCity = city;
                me.CommandName = "AgeKeyboard";
                _personRepository.UpdatePerson(me);
                await _personRepository.Save().ConfigureAwait(false);

                var sb = new StringBuilder();
                sb.AppendLine("<b>لطفا سن خود را انتخاب کنید\U00002049</b>");
                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.AgeKeyboard).ConfigureAwait(false);
            }
            else if (me.CommandName == "CityFilterKeyboard")
            {
                // This branch is used when user is setting a city filter
                me.FilterCity = city;
                me.CommandName = "AgeFilterKeyboard";
                _personRepository.UpdatePerson(me);
                await _personRepository.Save().ConfigureAwait(false);

                var sb = new StringBuilder();
                sb.AppendLine("<b>هم صحبتت چند ساله باشه\U00002049</b>");
                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.AgeFilterKeyboard).ConfigureAwait(false);

                // in case of filter change, automatically try to matchmaking if already registered
                //if (!string.IsNullOrEmpty(me.PersonGender) && !string.IsNullOrEmpty(me.PersonAge))
                //{
                //    await EnsureOnlinesRecordAsync(me).ConfigureAwait(false);
                //    await TryMatchmakeAsync(me, bot).ConfigureAwait(false);
                //}
            }
            else
            {
                await SendStatePromptAsync(chatId, bot, me).ConfigureAwait(false);
            }
        }

        private async Task HandleAgeSelectionAsync(long chatId, TelegramBotClient bot, Person me, string age)
        {
            if (me == null) return;

            if (me.CommandName == "AgeKeyboard")
            {
                me.PersonAge = age;
                me.CommandName = "SearchKeyboard";
                _personRepository.UpdatePerson(me);
                await _personRepository.Save().ConfigureAwait(false);

                var sb = new StringBuilder();
                sb.AppendLine("<b>ثبت نام با موفقیت انجام شد\U0000263A</b>");
                sb.AppendLine("<b>حالا برات چه کاری انجام بدم؟</b>");
                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.SearchKeyboard).ConfigureAwait(false);
            }
            else if (me.CommandName == "AgeFilterKeyboard")
            {
                // user started a filtered search
                me.FilterAge = age;
                me.CommandName = "CancellKeyboard";
                _personRepository.UpdatePerson(me);
                await _personRepository.Save().ConfigureAwait(false);

                var sb = new StringBuilder();
                sb.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
                sb.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.CancellKeyboard).ConfigureAwait(false);

                // ensure an Onlines record exists for this user so matchmaking can find them
                await EnsureOnlinesRecordAsync(me).ConfigureAwait(false);

                // Attempt matchmaking synchronously here (simple approach)
                await TryMatchmakeAsync(me, bot).ConfigureAwait(false);
            }
            else
            {
                await SendStatePromptAsync(chatId, bot, me).ConfigureAwait(false);
            }
        }

        private async Task StartSearchAsync(long chatId, TelegramBotClient bot, Person me)
        {
            if (me == null) return;

            // Start simple search: mark user as searching and attempt to match
            me.CommandName = "CancellKeyboard";
            _personRepository.UpdatePerson(me);
            await _personRepository.Save().ConfigureAwait(false);

            await EnsureOnlinesRecordAsync(me).ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
            sb.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
            await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.CancellKeyboard).ConfigureAwait(false);

            await TryMatchmakeAsync(me, bot).ConfigureAwait(false);
        }
        private async Task HandleGenderFilterAsync(long chatId, TelegramBotClient bot, Person me)
        {
            if (me == null) return;

            me.CommandName = "GenderFilterKeyboard";
            _personRepository.UpdatePerson(me);
            await _personRepository.Save().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("<b>هم صحبتت پسر باشه یا دختر باشه؟\U00002049</b>");
            await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, replyMarkup: _keyboardProvider.GenderFilterKeyboard).ConfigureAwait(false);

            //await TryMatchmakeAsync(me, bot).ConfigureAwait(false);
        }

        private async Task EnsureOnlinesRecordAsync(Person me)
        {
            if (me == null) return;

            var existing = _onlines_repository_getall().FirstOrDefault(o => o.PersonId == me.PersonId);
            if (existing == null)
            {
                var o = new Onlines
                {
                    Username = me.PersonName,
                    PersonId = me.PersonId,
                    chatid = me.chatid,
                    User2 = 0
                };
                _onlinesRepository.InsertOnline(o);
                await _onlinesRepository.Save().ConfigureAwait(false);
            }
        }

        private async Task TryMatchmakeAsync(Person me, TelegramBotClient bot)
        {
            if (me == null) return;

            // get available onlines with User2 == 0 excluding self
            var available = _onlines_repository_getall().Where(o => o.User2 == 0 && o.PersonId != me.PersonId).ToList();

            // apply simple filter matching using Person's filters
            var candidate = available.FirstOrDefault(o =>
            {
                var other = o.Person;
                if (other == null) return false;
                if (!string.IsNullOrEmpty(me.FilterGender) && other.PersonGender != me.FilterGender) return false;
                if (!string.IsNullOrEmpty(me.FilterCity) && other.PersonCity != me.FilterCity) return false;
                if (!string.IsNullOrEmpty(me.FilterAge) && other.PersonAge != me.FilterAge) return false;
                if (!string.IsNullOrEmpty(other.FilterGender) && other.FilterGender != me.PersonGender) return false;
                if (!string.IsNullOrEmpty(other.FilterCity) && other.FilterCity != me.PersonCity) return false;
                if (!string.IsNullOrEmpty(other.FilterAge) && other.FilterAge != me.PersonAge) return false;
                return true;
            });

            if (candidate == null)
            {
                // no match right now — leave the user waiting (could queue)
                return;
            }

            // pair them
            try
            {
                var otherPerson = candidate.Person;
                // update candidate.User2 to me.chatid
                candidate.User2 = me.chatid;
                _onlinesRepository.UpdateOnline(candidate);
                await _onlinesRepository.Save().ConfigureAwait(false);

                // update or create me's online entry and set User2 to candidate.chatid
                var myOnline = _onlines_repository_getall().FirstOrDefault(o => o.PersonId == me.PersonId);
                if (myOnline == null)
                {
                    myOnline = new Onlines { Username = me.PersonName, PersonId = me.PersonId, chatid = me.chatid, User2 = candidate.chatid };
                    _onlinesRepository.InsertOnline(myOnline);
                    await _onlinesRepository.Save().ConfigureAwait(false);
                }
                else
                {
                    myOnline.User2 = candidate.chatid;
                    _onlinesRepository.UpdateOnline(myOnline);
                    await _onlinesRepository.Save().ConfigureAwait(false);
                }

                // update both people's CommandName to StopKeyboard
                me.CommandName = "StopKeyboard";
                _personRepository.UpdatePerson(me);
                await _personRepository.Save().ConfigureAwait(false);

                if (otherPerson != null)
                {
                    otherPerson.CommandName = "StopKeyboard";
                    _personRepository.UpdatePerson(otherPerson);
                    await _personRepository.Save().ConfigureAwait(false);
                }

                var msg = "<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>";

                await bot.SendTextMessageAsync(me.chatid, msg, ParseMode.Html, replyMarkup: _keyboardProvider.StopKeyboard).ConfigureAwait(false);
                await bot.SendTextMessageAsync(candidate.chatid, msg, ParseMode.Html, replyMarkup: _keyboardProvider.StopKeyboard).ConfigureAwait(false);
            }
            catch
            {
                // swallow errors here; in production log them
            }
        }

        private async Task ForwardTextToPartnerAsync(long chatId, TelegramBotClient bot, Update update)
        {
            var texts = update.Message.Text;
            if (string.IsNullOrEmpty(texts)) return;

            var onlines = _onlines_repository_getall();
            var myOnline = onlines.FirstOrDefault(o => o.chatid == chatId && o.User2 != 0);
            if (myOnline == null) return;

            var partnerId = myOnline.User2;
            try
            {
                // forward plain text
                await bot.SendTextMessageAsync(partnerId, texts, ParseMode.Default, replyMarkup: _keyboardProvider.StopKeyboard).ConfigureAwait(false);
            }
            catch
            {
                // ignoring send errors here
            }
        }

        private async Task SendStatePromptAsync(long chatId, TelegramBotClient bot, Person me)
        {
            // Decide which keyboard to show based on person's CommandName
            var kb = _keyboardProvider.MainKeyboard;
            if (me != null)
            {
                switch (me.CommandName)
                {
                    case "GenderKeyboard": kb = _keyboardProvider.GenderKeyboard; break;
                    case "CityKeyboard": kb = _keyboardProvider.CityKeyboard; break;
                    case "AgeKeyboard": kb = _keyboardProvider.AgeKeyboard; break;
                    case "SearchKeyboard": kb = _keyboardProvider.SearchKeyboard; break;
                    case "StopKeyboard": kb = _keyboardProvider.StopKeyboard; break;
                    case "CancellKeyboard": kb = _keyboardProvider.CancellKeyboard; break;
                    case "ComeBackKeyboard": kb = _keyboardProvider.ComeBackKeyboard; break;
                    case "DeleteFriendKeyboard": kb = _keyboardProvider.DeleteFriendKeyboard; break;
                    default: kb = _keyboardProvider.MainKeyboard; break;
                }
            }

            await bot.SendTextMessageAsync(chatId, "<b>خب چه کاری برات انجام بدم؟</b>", ParseMode.Html, replyMarkup: kb).ConfigureAwait(false);
        }

        // repository helpers
        private List<Onlines> _onlines_repository_getall()
        {
            try { return _onlinesRepository.GetAllOnlines().ToList(); }
            catch { return new List<Onlines>(); }
        }

        private static List<string> ExtractCityListFromProvider()
        {
            // keep in sync with KeyboardProvider city list
            return new List<string>
            {
                "اردبیل","آذربایجان غربی","آذربایجان شرقی","ایلام","البرز","اصفهان",
                "چهارمحال و بختیاری","تهران","بوشهر","خراسان شمالی","خراسان رضوی","خراسان جنوبی",
                "سمنان","زنجان","خوزستان","قزوین","فارس","سیستان بلوچستان","کرمان","کردستان",
                "قم","لرستان","کهگیلویه و بویراحمد","کرمانشاه","گلستان","مازندران","گیلان",
                "همدان","هرمزگان","مرکزی","یزد"
            };
        }
    }
}