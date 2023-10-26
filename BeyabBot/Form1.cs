using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using DataLayer;
using Telegram.Bot;
using System.IO;
using System.Security.Policy;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.InputFiles;
using File = System.IO.File;
using Timer = System.Windows.Forms.Timer;
using System.Timers;

namespace BeyabBot
{
    public partial class Form1 : Form
    {
        int refresh = 0;
        private string urlname = " ";
        private static int id;
        private Telegram.Bot.TelegramBotClient bot;






        //6276187744:AAE5jfo04l10BA4ABAcPHBDgekW1ByrHHl8
        private static string Token = "5597459849:AAEKNQx4TN_mEXIy-ziYXSfKAeWsSJ0w284";
        private Thread botThread;
        private ReplyKeyboardMarkup mainKeyboard;
        private ReplyKeyboardMarkup GenderKeyboard;
        private ReplyKeyboardMarkup AgeKeyboard;
        private ReplyKeyboardMarkup CityKeyboard;
        private ReplyKeyboardMarkup SearchKeyboard;
        private ReplyKeyboardMarkup StopKeyboard;
        private ReplyKeyboardMarkup CancellKeyboard;
        private ReplyKeyboardMarkup BeCancelledKeyboard;
        private ReplyKeyboardMarkup AfterCancelledKeyboard;
        private ReplyKeyboardMarkup GenderFilterKeyboard;
        private ReplyKeyboardMarkup CityFilterKeyboard;
        private ReplyKeyboardMarkup AgeFilterKeyboard;
        private ReplyKeyboardMarkup BlockKeyboard;
        private ReplyKeyboardMarkup ComeBackKeyboard;
        private ReplyKeyboardMarkup DeleteFriendKeyboard;
        private ReplyKeyboardMarkup QuitKeyboard;

        BeyabContext db = new BeyabContext();
        IOnlinesRepository _onlinesRepository;
        IPersonRepository _personRepository;
        IBlockRepository _blockRepository;
        private IFriendRepository _friendRepository;
        private IValidationRepository _validationRepository;
        public Form1()
        {

            _onlinesRepository = new OnlinesRepository(db);
            _personRepository = new PersonRepository(db);
            _blockRepository = new BlockRepository(db);
            _friendRepository = new FriendRepository(db);
            _validationRepository = new ValidationRepository(db);
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {


            mainKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] row1 =
            {
                new KeyboardButton("ثبت نام" + " \U0001F63B")
            };
            KeyboardButton[] row2 =
            {
                new KeyboardButton("درباره ربات" + " \U00002764")
            };
            mainKeyboard.Keyboard = new KeyboardButton[][]
            {
                row1,row2
            };
            mainKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************

            GenderKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] Grow1 =
            {
                new KeyboardButton("پسرم" + " \U0001F466")
            };
            KeyboardButton[] Grow2 =
            {
                new KeyboardButton("دخترم" + " \U0001F467")
            };
            GenderKeyboard.Keyboard = new KeyboardButton[][]
            {
                Grow1,Grow2
            };
            GenderKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************

            AgeKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] Arow1 =
            {
                new KeyboardButton("18 تا 20 سال")
            };
            KeyboardButton[] Arow2 =
            {
                new KeyboardButton("20 تا 25 سال")

            };
            KeyboardButton[] Arow3 =
            {
                new KeyboardButton("25 تا 30 سال")
            };
            KeyboardButton[] Arow4 =
            {
                new KeyboardButton("بالای 30 سال")
            };
            AgeKeyboard.Keyboard = new KeyboardButton[][]
            {
                Arow1,Arow2,Arow3,Arow4
            };
            AgeKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************

            CityKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] Crow1 =
            {
                new KeyboardButton("آذربایجان شرقی") , new KeyboardButton("آذربایجان غربی"), new KeyboardButton("اردبیل")
            };
            KeyboardButton[] Crow2 =
            {
                new KeyboardButton("اصفهان") , new KeyboardButton("البرز"), new KeyboardButton("ایلام")
            };
            KeyboardButton[] Crow3 =
            {
                new KeyboardButton("بوشهر") , new KeyboardButton("تهران"), new KeyboardButton("چهارمحال و بختیاری")
            };
            KeyboardButton[] Crow4 =
            {
                new KeyboardButton("خراسان جنوبی") , new KeyboardButton("خراسان رضوی"), new KeyboardButton("خراسان شمالی")
            };
            KeyboardButton[] Crow5 =
            {
                new KeyboardButton("خوزستان") , new KeyboardButton("زنجان"), new KeyboardButton("سمنان")
            };
            KeyboardButton[] Crow6 =
            {
                new KeyboardButton("سیستان بلوچستان") , new KeyboardButton("فارس"), new KeyboardButton("قزوین")
            };
            KeyboardButton[] Crow7 =
            {
                new KeyboardButton("قم") , new KeyboardButton("کردستان"), new KeyboardButton("کرمان")
            };
            KeyboardButton[] Crow8 =
            {
                new KeyboardButton("کرمانشاه") , new KeyboardButton("کهگیلویه و بویراحمد"), new KeyboardButton("لرستان")
            };
            KeyboardButton[] Crow9 =
            {
                new KeyboardButton("گیلان") , new KeyboardButton("مازندران"), new KeyboardButton("گلستان")
            };
            KeyboardButton[] Crow10 =
            {
                new KeyboardButton("مرکزی") , new KeyboardButton("هرمزگان"), new KeyboardButton("همدان")
            };
            KeyboardButton[] Crow11 =
            {
                new KeyboardButton("یزد")
            };
            CityKeyboard.Keyboard = new KeyboardButton[][]
            {
                Crow1,Crow2,Crow3,Crow4,Crow5,Crow6,Crow7,Crow8,Crow9,Crow10,Crow11
            };
            CityKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************

            BeCancelledKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] BCrow1 =
            {
                new KeyboardButton("ادامه صحبت") , new KeyboardButton("اتمام")
            };

            BeCancelledKeyboard.Keyboard = new KeyboardButton[][]
            {
                BCrow1
            };
            BeCancelledKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************

            AfterCancelledKeyboard = new ReplyKeyboardMarkup();

            KeyboardButton[] ACrow1 =
{
                new KeyboardButton("افزودن به دوست"),new KeyboardButton("بلاک")
            };
            KeyboardButton[] ACrow2 =
            {
                new KeyboardButton("منوی اصلی")
            };

            AfterCancelledKeyboard.Keyboard = new KeyboardButton[][]
            {
                ACrow1,ACrow2
            };
            AfterCancelledKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************

            ComeBackKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] CBrow1 =
            {
                new KeyboardButton("بازگشت"+"\U0001F519")
            };

            ComeBackKeyboard.Keyboard = new KeyboardButton[][]
            {
                CBrow1
            };

            ComeBackKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************
            QuitKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] Qrow1 =
            {
                new KeyboardButton("انصراف")
            };

            QuitKeyboard.Keyboard = new KeyboardButton[][]
            {
                Qrow1
            };

            QuitKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************
            DeleteFriendKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] DFrow1 =
            {
                new KeyboardButton("بیخیال"),new KeyboardButton("حذف")
            };

            DeleteFriendKeyboard.Keyboard = new KeyboardButton[][]
            {
                DFrow1
            };

            DeleteFriendKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************

            BlockKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] Blrow1 =
            {
                new KeyboardButton("نه") , new KeyboardButton("آره")
            };

            BlockKeyboard.Keyboard = new KeyboardButton[][]
            {
                Blrow1
            };

            BlockKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************


            SearchKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] Srow1 =
            {
                new KeyboardButton("\U0001F50D"+"برام یه هم صحبت پیدا کن!")
            };

            KeyboardButton[] Srow2 =
            {
                new KeyboardButton("مشخصات من"+"\U0001F4CB"),new KeyboardButton("دوستان من"+"\U0001F465")
            };

            KeyboardButton[] Srow3 =
            {
                new KeyboardButton("درباره ربات" + " \U00002764")
            };
            SearchKeyboard.Keyboard = new KeyboardButton[][]
            {
                Srow1,Srow2,Srow3
            };
            SearchKeyboard.ResizeKeyboard = true;

            //***************************************************************************************************************************************************

            StopKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] Strow1 =
            {
                new KeyboardButton("اتمام صحبت")
            };
            StopKeyboard.Keyboard = new KeyboardButton[][]
            {
                Strow1
            };
            StopKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************

            CancellKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] Cnrow1 =
            {
                new KeyboardButton("لغو جستجو")
            };
            CancellKeyboard.Keyboard = new KeyboardButton[][]
            {
               Cnrow1
            };
            CancellKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************

            GenderFilterKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] GFrow1 =
            {
                new KeyboardButton("مهم نیست"+" \U0001F605")
            };
            KeyboardButton[] GFrow2 =
            {
                new KeyboardButton("دختر باشه"+" \U0001F467") , new KeyboardButton("پسر باشه"+" \U0001F466")
            };
            GenderFilterKeyboard.Keyboard = new KeyboardButton[][]
            {
                GFrow1,GFrow2
            };
            GenderFilterKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************

            CityFilterKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] CFrow1 =
            {
                new KeyboardButton("مهم نیست"+" \U0001F605")
            };
            KeyboardButton[] CFrow2 =
            {
                new KeyboardButton("آذربایجان شرقی") , new KeyboardButton("آذربایجان غربی"), new KeyboardButton("اردبیل")
            };
            KeyboardButton[] CFrow3 =
            {
                new KeyboardButton("اصفهان") , new KeyboardButton("البرز"), new KeyboardButton("ایلام")
            };
            KeyboardButton[] CFrow4 =
            {
                new KeyboardButton("بوشهر") , new KeyboardButton("تهران"), new KeyboardButton("چهارمحال و بختیاری")
            };
            KeyboardButton[] CFrow5 =
            {
                new KeyboardButton("خراسان جنوبی") , new KeyboardButton("خراسان رضوی"), new KeyboardButton("خراسان شمالی")
            };
            KeyboardButton[] CFrow6 =
            {
                new KeyboardButton("خوزستان") , new KeyboardButton("زنجان"), new KeyboardButton("سمنان")
            };
            KeyboardButton[] CFrow7 =
            {
                new KeyboardButton("سیستان بلوچستان") , new KeyboardButton("فارس"), new KeyboardButton("قزوین")
            };
            KeyboardButton[] CFrow8 =
            {
                new KeyboardButton("قم") , new KeyboardButton("کردستان"), new KeyboardButton("کرمان")
            };
            KeyboardButton[] CFrow9 =
            {
                new KeyboardButton("کرمانشاه") , new KeyboardButton("کهگیلویه و بویراحمد"), new KeyboardButton("لرستان")
            };
            KeyboardButton[] CFrow10 =
            {
                new KeyboardButton("گیلان") , new KeyboardButton("مازندران"), new KeyboardButton("گلستان")
            };
            KeyboardButton[] CFrow11 =
            {
                new KeyboardButton("مرکزی") , new KeyboardButton("هرمزگان"), new KeyboardButton("همدان")
            };
            KeyboardButton[] CFrow12 =
            {
                new KeyboardButton("یزد")
            };

            CityFilterKeyboard.Keyboard = new KeyboardButton[][]
            {
                CFrow1,CFrow2,CFrow3,CFrow4,CFrow5,CFrow6,CFrow7,CFrow8,CFrow9,CFrow10,CFrow11,CFrow12
            };
            CityFilterKeyboard.ResizeKeyboard = true;
            //***************************************************************************************************************************************************
            AgeFilterKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] AFrow1 =
            {
                new KeyboardButton("مهم نیست"+" \U0001F605")
            };
            KeyboardButton[] AFrow2 =
            {
                new KeyboardButton("20 تا 25 سال") , new KeyboardButton("18 تا 20 سال")
            };
            KeyboardButton[] AFrow3 =
           {
                new KeyboardButton("بالای 30 سال") , new KeyboardButton("25 تا 30 سال")
            };
            AgeFilterKeyboard.Keyboard = new KeyboardButton[][]
            {
                AFrow1,AFrow2,AFrow3
            };
            AgeFilterKeyboard.ResizeKeyboard = true;


            botThread = new Thread(new ThreadStart(RubBot));
            botThread.Start();

        }


        async void RubBot()
        {


            bot = new Telegram.Bot.TelegramBotClient(Token);

            int offset = 0;

            while (true)
            {

                Telegram.Bot.Types.Update[] updates = bot.GetUpdatesAsync(offset).Result;

                string path = "";
                dynamic file = "";
                dynamic file2 = "";

                foreach (var up in updates)
                {
                    try
                    {
                        int a = 0;
                        var person = _personRepository.GetAllPerson();
                        var online = _onlinesRepository.GetAllOnlines();
                        dynamic command = "";
                        offset = up.Id + 1;
                        if (up.CallbackQuery != null)
                        {
                            var callback = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 12, 12);
                            if (up.CallbackQuery.Data.Contains("تغییر اسم"))
                            {
                                callback = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 9, 9);
                            }
                            if (up.CallbackQuery.Data.Contains("حذف"))
                            {
                                callback = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 3, 3);
                            }
                            if (up.CallbackQuery.Data.Contains("رد درخواست"))
                            {
                                callback = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 10, 10);
                            }
                            if (up.CallbackQuery.Data.Contains("بلاک"))
                            {
                                callback = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 4, 4);
                            }
                            if (up.CallbackQuery.Data.Contains("تایید"))
                            {
                                callback = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 5, 5);
                            }
                            if (up.CallbackQuery.Data.Contains("عدم تایید"))
                            {
                                callback = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 9, 9);
                            }

                            var friends1 = _friendRepository.GetAllFriendList();
                            if (friends1.Any(f =>
                                    f.P1Chatid == up.CallbackQuery.From.Id && f.P2Chatid.ToString() == callback))
                            {
                                foreach (FriendsList fr in friends1)
                                {
                                    if (fr.P1Chatid == up.CallbackQuery.From.Id && fr.P2Chatid.ToString() == callback)
                                    {
                                        var person2 = _personRepository.GetAllPerson();
                                        var online2 = _onlinesRepository.GetAllOnlines();

                                        if (up.CallbackQuery.Data == fr.P2Chatid.ToString() + "درخواست صحبت")
                                        {
                                            if (!online2.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2 == fr.P2Chatid) && !online2.Any(o => o.chatid == fr.P2Chatid && o.User2 == up.CallbackQuery.From.Id))
                                            {
                                                var blocks = _blockRepository.GetAllBlockList();
                                                if (blocks.Any(b =>
                                                        b.P1Chatid == fr.P2Chatid && b.P2Chatid == up.CallbackQuery.From.Id))
                                                {
                                                    StringBuilder sb = new StringBuilder();
                                                    sb.AppendLine(
                                                        "<b>کاربر گرامی متاسفانه دوستت شمارو بلاک کرده و نمیتونین بهش درخواست بفرستین.</b>");
                                                    await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(),
                                                        ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                                }
                                                else
                                                {
                                                    var validation2 = _validationRepository.GetAllValidations();
                                                    if (validation2.Any(v =>
                                                            v.P1Chatid == up.CallbackQuery.From.Id &&
                                                            v.P2Chatid == fr.P2Chatid))
                                                    {
                                                        foreach (RequestValidation val in validation2)
                                                        {
                                                            if (val.P1Chatid == up.CallbackQuery.From.Id &&
                                                                val.P2Chatid == fr.P2Chatid)
                                                            {
                                                                if (val.Date > DateTime.Now)
                                                                {
                                                                    StringBuilder sb = new StringBuilder();
                                                                    sb.AppendLine(
                                                                        "<b>کاربر گرامی به این دوستت تازگی درخواست دادی ، در هر روز فقط یکبار میتونی به هر شخصی درخواست بدی.</b>");
                                                                    await bot.SendTextMessageAsync(up.CallbackQuery.From.Id,
                                                                        sb.ToString(), ParseMode.Html, null, false, false, 0,
                                                                        false,
                                                                        SearchKeyboard);

                                                                }
                                                                else
                                                                {
                                                                    _validationRepository.DeleteValidation(val);
                                                                    _validationRepository.Save();

                                                                    try
                                                                    {
                                                                        InlineKeyboardButton urlButton = new InlineKeyboardButton();
                                                                        InlineKeyboardButton urlButton2 = new InlineKeyboardButton();
                                                                        InlineKeyboardButton urlButton3 = new InlineKeyboardButton();

                                                                        urlButton.Text = "قبول درخواست";
                                                                        urlButton.CallbackData =
                                                                            fr.P1Chatid.ToString() + "قبول درخواست";
                                                                        urlButton.Pay = true;

                                                                        urlButton2.Text = "رد درخواست";
                                                                        urlButton2.CallbackData =
                                                                            fr.P1Chatid.ToString() + "رد درخواست";
                                                                        urlButton2.Pay = true;

                                                                        urlButton3.Text = "بلاک";
                                                                        urlButton3.CallbackData =
                                                                            fr.P1Chatid.ToString() + "بلاک";
                                                                        urlButton3.Pay = true;


                                                                        InlineKeyboardButton[] buttons = new InlineKeyboardButton[]
                                                                            { urlButton, urlButton2,urlButton3 };

                                                                        // Keyboard markup
                                                                        InlineKeyboardMarkup inline = new InlineKeyboardMarkup(buttons);

                                                                        StringBuilder sb = new StringBuilder();
                                                                        sb.AppendLine(
                                                                            "<b>یکی از کابرا بهت درخواست صحبت داده . میتونی با گزینه های پایین درخواستشو قبول کنی یا رد کنی.</b>");
                                                                        await bot.SendTextMessageAsync(fr.P2Chatid, sb.ToString(),
                                                                            ParseMode.Html, null, false, false, 0, false, inline);


                                                                        StringBuilder sb2 = new StringBuilder();
                                                                        sb2.AppendLine("<b>درخواست برای دوستت ارسال شد.</b>");
                                                                        sb2.AppendLine("<b>حالا چه کاری برات انجام بدم؟</b>");
                                                                        await bot.SendTextMessageAsync(up.CallbackQuery.From.Id,
                                                                            sb2.ToString(),
                                                                            ParseMode.Html, null, false, false, 0, false,
                                                                            SearchKeyboard);
                                                                        var validation = _validationRepository.GetAllValidations()
                                                                            .Where(b => b.P1Chatid == up.CallbackQuery.From.Id);
                                                                        foreach (Person per in person2)
                                                                        {
                                                                            if (per.chatid == up.CallbackQuery.From.Id)
                                                                            {
                                                                                per.CommandName = "SearchKeyboard";
                                                                                _personRepository.UpdatePerson(per);
                                                                                _personRepository.Save();
                                                                                foreach (RequestValidation vl in validation)
                                                                                {
                                                                                    RequestValidation val2 = new RequestValidation()
                                                                                    {
                                                                                        P1Chatid = up.CallbackQuery.From.Id,
                                                                                        P2Chatid = fr.P2Chatid,
                                                                                        Date = DateTime.Now.AddDays(1),
                                                                                        PersonId = per.PersonId
                                                                                    };
                                                                                    _validationRepository.InsertValidation(val2);
                                                                                    _validationRepository.Save();

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    catch
                                                                    {
                                                                        StringBuilder sbchat = new StringBuilder();
                                                                        sbchat.AppendLine("<b>کاربر گرامی اشکالی در ارتباط بوجود آمده و ارسال پیام امکان پذیر نیست. ممکن است دلیل اختلال بوجود آمده بلاک کردن ربات از سمت کاربر مقابل باشد </b>");
                                                                        await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sbchat.ToString(), ParseMode.Html, null, false, false, 0, false);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            InlineKeyboardButton urlButton = new InlineKeyboardButton();
                                                            InlineKeyboardButton urlButton2 = new InlineKeyboardButton();
                                                            InlineKeyboardButton urlButton3 = new InlineKeyboardButton();

                                                            urlButton.Text = "قبول درخواست";
                                                            urlButton.CallbackData = fr.P1Chatid.ToString() + "قبول درخواست";
                                                            urlButton.Pay = true;

                                                            urlButton2.Text = "رد درخواست";
                                                            urlButton2.CallbackData = fr.P1Chatid.ToString() + "رد درخواست";
                                                            urlButton2.Pay = true;

                                                            urlButton3.Text = "بلاک";
                                                            urlButton3.CallbackData =
                                                                fr.P1Chatid.ToString() + "بلاک";
                                                            urlButton3.Pay = true;


                                                            InlineKeyboardButton[] buttons = new InlineKeyboardButton[]
                                                                { urlButton, urlButton2, urlButton3 };

                                                            // Keyboard markup
                                                            InlineKeyboardMarkup inline = new InlineKeyboardMarkup(buttons);

                                                            StringBuilder sb = new StringBuilder();
                                                            sb.AppendLine(
                                                                "<b>یکی از کابرا بهت درخواست صحبت داده . میتونی با گزینه های پایین درخواستشو قبول کنی یا رد کنی.</b>");
                                                            await bot.SendTextMessageAsync(fr.P2Chatid, sb.ToString(),
                                                                ParseMode.Html, null, false, false, 0, false, inline);


                                                            StringBuilder sb2 = new StringBuilder();
                                                            sb2.AppendLine("<b>درخواست برای دوستت ارسال شد.</b>");
                                                            sb2.AppendLine("<b>حالا چه کاری برات انجام بدم؟</b>");
                                                            await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb2.ToString(),
                                                                ParseMode.Html, null, false, false, 0, false, SearchKeyboard);
                                                            var validation = _validationRepository.GetAllValidations()
                                                                .Where(b => b.P1Chatid == up.CallbackQuery.From.Id);
                                                            foreach (Person per in person2)
                                                            {
                                                                if (per.chatid == up.CallbackQuery.From.Id)
                                                                {
                                                                    per.CommandName = "SearchKeyboard";
                                                                    _personRepository.UpdatePerson(per);
                                                                    _personRepository.Save();

                                                                    RequestValidation val = new RequestValidation()
                                                                    {
                                                                        P1Chatid = up.CallbackQuery.From.Id,
                                                                        P2Chatid = fr.P2Chatid,
                                                                        Date = DateTime.Now.AddDays(1),
                                                                        PersonId = per.PersonId
                                                                    };
                                                                    _validationRepository.InsertValidation(val);
                                                                    _validationRepository.Save();
                                                                }
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            StringBuilder sbchat = new StringBuilder();
                                                            sbchat.AppendLine("<b>کاربر گرامی اشکالی در ارتباط بوجود آمده و ارسال پیام امکان پذیر نیست. ممکن است دلیل اختلال بوجود آمده بلاک کردن ربات از سمت کاربر مقابل باشد </b>");
                                                            await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sbchat.ToString(), ParseMode.Html, null, false, false, 0, false);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                StringBuilder sb = new StringBuilder();
                                                sb.AppendLine(
                                                    "<b>کاربر گرامی شما در حال صحبت با شخص مورد نظر هستین در این شرایط نمیتونین درخواست مجدد ارسال کنید.</b>.");
                                                await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(),
                                                    ParseMode.Html,
                                                    null, false, false, 0, false);
                                            }
                                        }
                                    }
                                }
                            }

                            if (up.CallbackQuery.Data.Contains("قبول درخواست"))
                            {
                                if (friends1.Any(f =>
                                        f.P2Chatid == up.CallbackQuery.From.Id && f.P1Chatid.ToString() == callback))
                                {
                                    foreach (FriendsList fr in friends1)
                                    {
                                        if (fr.P2Chatid == up.CallbackQuery.From.Id && fr.P1Chatid.ToString() == callback)
                                        {


                                            var callback2 = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 12, 12);
                                            var friends2 = _friendRepository.GetAllFriendList();

                                            if (friends2.Any(f =>
                                                    f.P2Chatid == up.CallbackQuery.From.Id &&
                                                    f.P1Chatid.ToString() == callback2))
                                            {
                                                foreach (FriendsList fr2 in friends2)
                                                {
                                                    if (fr2.P2Chatid == up.CallbackQuery.From.Id &&
                                                        fr2.P1Chatid.ToString() == callback2)
                                                    {
                                                        if (up.CallbackQuery.Data == fr2.P1Chatid + "قبول درخواست")
                                                        {

                                                            var validation2 = _validationRepository.GetAllValidations();
                                                            var onlines = _onlinesRepository.GetAllOnlines();
                                                            if (person.Any(p => p.chatid == up.CallbackQuery.From.Id))
                                                            {
                                                                foreach (Person per in person)
                                                                {
                                                                    if (per.chatid == up.CallbackQuery.From.Id)
                                                                    {
                                                                        if (!onlines.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2.ToString() == callback) && !onlines.Any(o => o.chatid.ToString() == callback && o.User2 == up.CallbackQuery.From.Id))
                                                                        {
                                                                            if (per.CommandName != "QuitKeyboard")
                                                                            {

                                                                                if (onlines.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2 != 0))
                                                                                {
                                                                                    foreach (Onlines on in onlines)
                                                                                    {
                                                                                        if (on.chatid == up.CallbackQuery.From.Id)
                                                                                        {
                                                                                            if (validation2.Any(v =>
                                                                                                       v.P1Chatid == fr2.P1Chatid &&
                                                                                                       v.P1Chatid == fr2.P1Chatid &&
                                                                                                       v.P2Chatid == up.CallbackQuery.From.Id))
                                                                                            {
                                                                                                foreach (RequestValidation rv in validation2)
                                                                                                {
                                                                                                    if (rv.P1Chatid == fr2.P1Chatid &&
                                                                                                        rv.P2Chatid == up.CallbackQuery.From.Id)
                                                                                                    {
                                                                                                        if (rv.Date < DateTime.Now)
                                                                                                        {
                                                                                                            StringBuilder sb =
                                                                                                                new StringBuilder();
                                                                                                            sb.AppendLine(
                                                                                                                "<b>کاربر گرامی متاسفانه این درخواست دیگه اعتباری نداره!</b>");
                                                                                                            await bot.SendTextMessageAsync(
                                                                                                                up.CallbackQuery.From.Id,
                                                                                                                sb.ToString(),
                                                                                                                ParseMode.Html,
                                                                                                                null, false,
                                                                                                                false, 0,
                                                                                                                false);
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            StringBuilder sb =
                                                                                                                new StringBuilder();
                                                                                                            sb.AppendLine(
                                                                                                                "<b>کاربر گرامی شما در حال حاضر مشغول به صحبت با شخص دیگری هستی . اگه میخوای درخواست چت رو قبول کنی اول اتمام صحبت رو بزن و این گپ رو ببند.</b>");
                                                                                                            await bot.SendTextMessageAsync(
                                                                                                                up.CallbackQuery.From.Id,
                                                                                                                sb.ToString(),
                                                                                                                ParseMode.Html,
                                                                                                                null, false,
                                                                                                                false, 0,
                                                                                                                false);

                                                                                                        }

                                                                                                    }
                                                                                                }

                                                                                            }


                                                                                        }
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (onlines.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2 == 0))
                                                                                    {
                                                                                        foreach (Onlines on in onlines)
                                                                                        {
                                                                                            if (on.chatid == up.CallbackQuery.From.Id)
                                                                                            {
                                                                                                _onlinesRepository.DeleteOnline(on);
                                                                                                _onlinesRepository.Save();
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    validation2 = _validationRepository.GetAllValidations();
                                                                                    if (validation2.Any(v =>
                                                                                            v.P1Chatid == fr2.P1Chatid &&
                                                                                            v.P2Chatid == up.CallbackQuery.From.Id))
                                                                                    {
                                                                                        foreach (RequestValidation rv in validation2)
                                                                                        {
                                                                                            if (rv.P1Chatid ==
                                                                                                fr2.P1Chatid &&
                                                                                                rv.P2Chatid == up.CallbackQuery.From.Id)
                                                                                            {
                                                                                                if (rv.Date < DateTime.Now)
                                                                                                {
                                                                                                    StringBuilder sb = new StringBuilder();
                                                                                                    sb.AppendLine(
                                                                                                        "<b>کاربر گرامی متاسفانه این درخواست دیگه اعتباری نداره!</b>");
                                                                                                    await bot.SendTextMessageAsync(
                                                                                                        up.CallbackQuery.From.Id,
                                                                                                        sb.ToString(),
                                                                                                        ParseMode.Html, null, false, false, 0,
                                                                                                        false);
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    StringBuilder sb = new StringBuilder();
                                                                                                    sb.AppendLine(
                                                                                                        "<b>کاربر گرامی لطفا منتظر تایید از سمت مقابل باشین تا بهم وصلتون کنم.</b>");
                                                                                                    await bot.SendTextMessageAsync(
                                                                                                        up.CallbackQuery.From.Id,
                                                                                                        sb.ToString(),
                                                                                                        ParseMode.Html, null, false, false, 0,
                                                                                                        false, QuitKeyboard);

                                                                                                    try
                                                                                                    {
                                                                                                        InlineKeyboardButton urlButton = new InlineKeyboardButton();
                                                                                                        InlineKeyboardButton urlButton2 = new InlineKeyboardButton();

                                                                                                        urlButton.Text = "تایید";
                                                                                                        urlButton.CallbackData =
                                                                                                            fr.P2Chatid.ToString() + "تایید";
                                                                                                        urlButton.Pay = true;

                                                                                                        urlButton2.Text = "عدم تایید";
                                                                                                        urlButton2.CallbackData =
                                                                                                            fr.P2Chatid.ToString() + "عدم تایید";
                                                                                                        urlButton2.Pay = true;


                                                                                                        InlineKeyboardButton[] buttons = new InlineKeyboardButton[]
                                                                                                            { urlButton, urlButton2 };


                                                                                                        // Keyboard markup
                                                                                                        InlineKeyboardMarkup inline = new InlineKeyboardMarkup(buttons);

                                                                                                        StringBuilder sb2 = new StringBuilder();
                                                                                                        sb2.AppendLine($"<b>کاربر گرامی دوستت '{fr2.name}' درخواست چتت رو پذیرفت لطفا تایید کن تا وصلتون کنم.</b>");
                                                                                                        await bot.SendTextMessageAsync(
                                                                                                            fr.P1Chatid,
                                                                                                            sb2.ToString(),
                                                                                                            ParseMode.Html, null, false, false, 0,
                                                                                                            false, inline);

                                                                                                        per.CommandName = "QuitKeyboard";
                                                                                                        per.LastUser = fr.P1Chatid;
                                                                                                        _personRepository.UpdatePerson(per);
                                                                                                        _personRepository.Save();
                                                                                                    }
                                                                                                    catch
                                                                                                    {
                                                                                                        StringBuilder sbchat = new StringBuilder();
                                                                                                        sbchat.AppendLine("<b>کاربر گرامی اشکالی در ارتباط بوجود آمده و ارسال پیام امکان پذیر نیست. ممکن است دلیل اختلال بوجود آمده بلاک کردن ربات از سمت کاربر مقابل باشد </b>");
                                                                                                        await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sbchat.ToString(), ParseMode.Html, null, false, false, 0, false);
                                                                                                    }


                                                                                                }
                                                                                            }
                                                                                        }

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        StringBuilder sb =
                                                                                            new StringBuilder();
                                                                                        sb.AppendLine(
                                                                                            "<b>کاربر گرامی متاسفانه این درخواست دیگه اعتباری نداره!</b>");
                                                                                        await bot.SendTextMessageAsync(
                                                                                            up.CallbackQuery.From.Id,
                                                                                            sb.ToString(),
                                                                                            ParseMode.Html,
                                                                                            null, false,
                                                                                            false, 0,
                                                                                            false);
                                                                                    }

                                                                                }

                                                                            }

                                                                            else
                                                                            {
                                                                                StringBuilder sb =
                                                                                                new StringBuilder();
                                                                                sb.AppendLine(
                                                                                    "<b>کاربر گرامی لطفا منتظر تایید باشین یا در صورت تمایل گزینه انصراف را بزنید.</b>");
                                                                                await bot.SendTextMessageAsync(
                                                                                    up.CallbackQuery.From.Id,
                                                                                    sb.ToString(),
                                                                                    ParseMode.Html,
                                                                                    null, false,
                                                                                    false, 0,
                                                                                    false);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            StringBuilder sb = new StringBuilder();
                                                                            sb.AppendLine(
                                                                                "<b>کاربر گرامی شما در حال صحبت با شخص مورد نظر هستین بنابراین این دستور منقضی شده.</b>.");
                                                                            await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(),
                                                                                ParseMode.Html,
                                                                                null, false, false, 0, false);
                                                                        }
                                                                    }
                                                                }
                                                            }



                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    StringBuilder sb =
                                                                                        new StringBuilder();
                                    sb.AppendLine(
                                        "<b>کاربر گرامی متاسفانه این درخواست دیگه اعتباری نداره!</b>");
                                    await bot.SendTextMessageAsync(
                                        up.CallbackQuery.From.Id,
                                        sb.ToString(),
                                        ParseMode.Html,
                                        null, false,
                                        false, 0,
                                        false);
                                }
                            }





                            var callback3 = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 10, 10);
                            var friends3 = _friendRepository.GetAllFriendList();
                            if (up.CallbackQuery.Data.Contains("رد درخواست"))
                            {
                                if (friends1.Any(f =>
                                        f.P2Chatid == up.CallbackQuery.From.Id && f.P1Chatid.ToString() == callback))
                                {
                                    if (friends3.Any(f =>
                                    f.P2Chatid == up.CallbackQuery.From.Id &&
                                    f.P1Chatid.ToString() == callback))
                                    {
                                        foreach (FriendsList fr3 in friends3)
                                        {
                                            if (fr3.P2Chatid == up.CallbackQuery.From.Id &&
                                                fr3.P1Chatid.ToString() == callback)
                                            {
                                                if (up.CallbackQuery.Data == fr3.P1Chatid + "رد درخواست")
                                                {
                                                    if (person.Any(p => p.chatid == up.CallbackQuery.From.Id))
                                                    {
                                                        foreach (Person per in person)
                                                        {
                                                            if (per.chatid == up.CallbackQuery.From.Id)
                                                            {
                                                                if (per.CommandName != "QuitKeyboard")
                                                                {
                                                                    var onlines = _onlinesRepository.GetAllOnlines();
                                                                    if (!onlines.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2.ToString() == callback) && !onlines.Any(o => o.chatid.ToString() == callback && o.User2 == up.CallbackQuery.From.Id))
                                                                    {
                                                                        var validation3 = _validationRepository.GetAllValidations();
                                                                        if (validation3.Any(v =>
                                                                                v.P1Chatid == fr3.P1Chatid && v.P2Chatid == up.CallbackQuery.From.Id))
                                                                        {
                                                                            foreach (RequestValidation rv in validation3)
                                                                            {
                                                                                if (rv.P1Chatid == fr3.P1Chatid &&
                                                                                    rv.P2Chatid == up.CallbackQuery.From.Id)
                                                                                {
                                                                                    if (rv.Date > DateTime.Now)
                                                                                    {
                                                                                        var vld = _validationRepository.GetAllValidations().Where(v => v.P1Chatid == fr3.P1Chatid);

                                                                                        _validationRepository.DeleteValidation(rv);
                                                                                        _validationRepository.Save();
                                                                                        StringBuilder sb = new StringBuilder();
                                                                                        sb.AppendLine(
                                                                                            "<b>درخواست با موفقیت حذف شد</b>");
                                                                                        await bot.SendTextMessageAsync(fr3.P2Chatid, sb.ToString(),
                                                                                            ParseMode.Html,
                                                                                            null, false, false, 0, false);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        StringBuilder sb = new StringBuilder();
                                                                                        sb.AppendLine(
                                                                                            "<b>کاربر گرامی این درخواست دیگه اعتباری نداره!</b>");
                                                                                        await bot.SendTextMessageAsync(fr3.P2Chatid, sb.ToString(),
                                                                                            ParseMode.Html,
                                                                                            null, false, false, 0, false);
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            StringBuilder sb = new StringBuilder();
                                                                            sb.AppendLine(
                                                                                "<b>کاربر گرامی این درخواست دیگه اعتباری نداره!</b>");
                                                                            await bot.SendTextMessageAsync(fr3.P2Chatid, sb.ToString(),
                                                                                ParseMode.Html,
                                                                                null, false, false, 0, false);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        StringBuilder sb = new StringBuilder();
                                                                        sb.AppendLine(
                                                                            "<b>کاربر گرامی شما در حال صحبت با شخص مورد نظر هستین بنابراین این دستور منقضی شده.</b>.");
                                                                        await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(),
                                                                            ParseMode.Html,
                                                                            null, false, false, 0, false);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    StringBuilder sb =
                                                                                    new StringBuilder();
                                                                    sb.AppendLine(
                                                                        "<b>کاربر گرامی لطفا منتظر تایید باشین یا در صورت تمایل گزینه انصراف را بزنید.</b>");
                                                                    await bot.SendTextMessageAsync(
                                                                        up.CallbackQuery.From.Id,
                                                                        sb.ToString(),
                                                                        ParseMode.Html,
                                                                        null, false,
                                                                        false, 0,
                                                                        false);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    StringBuilder sb =
                                                                                        new StringBuilder();
                                    sb.AppendLine(
                                        "<b>کاربر گرامی متاسفانه این درخواست دیگه اعتباری نداره!</b>");
                                    await bot.SendTextMessageAsync(
                                        up.CallbackQuery.From.Id,
                                        sb.ToString(),
                                        ParseMode.Html,
                                        null, false,
                                        false, 0,
                                        false);
                                }
                            }


                            var callback4 = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 10, 10);
                            var friends4 = _friendRepository.GetAllFriendList();
                            if (up.CallbackQuery.Data.Contains("بلاک"))
                            {
                                if (friends1.Any(f =>
                                        f.P2Chatid == up.CallbackQuery.From.Id && f.P1Chatid.ToString() == callback))
                                {
                                    if (friends3.Any(f =>
                                    f.P2Chatid == up.CallbackQuery.From.Id &&
                                    f.P1Chatid.ToString() == callback))
                                    {
                                        foreach (FriendsList fr3 in friends3)
                                        {
                                            if (fr3.P2Chatid == up.CallbackQuery.From.Id &&
                                                fr3.P1Chatid.ToString() == callback)
                                            {
                                                if (up.CallbackQuery.Data == fr3.P1Chatid + "بلاک")
                                                {
                                                    if (person.Any(p => p.chatid == up.CallbackQuery.From.Id))
                                                    {
                                                        foreach (Person pe in person)
                                                        {
                                                            if (pe.chatid == up.CallbackQuery.From.Id)
                                                            {
                                                                if (pe.CommandName != "QuitKeyboard")
                                                                {
                                                                    var onlines = _onlinesRepository.GetAllOnlines();
                                                                    if (!onlines.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2.ToString() == callback) && !onlines.Any(o => o.chatid.ToString() == callback && o.User2 == up.CallbackQuery.From.Id))
                                                                    {
                                                                        var blocks = _blockRepository.GetAllBlockList();
                                                                        if (!blocks.Any(b =>
                                                                                b.P1Chatid == up.CallbackQuery.From.Id &&
                                                                                b.P2Chatid.ToString() == callback))
                                                                        {
                                                                            if (person.Any(p => p.chatid == up.CallbackQuery.From.Id))
                                                                            {
                                                                                foreach (Person per in person)
                                                                                {
                                                                                    if (per.chatid == up.CallbackQuery.From.Id)
                                                                                    {
                                                                                        per.Block_Ckeck = per.CommandName;
                                                                                        per.CommandName = "BlockKeyboard";
                                                                                        per.LastUser = fr3.P1Chatid;
                                                                                        _personRepository.UpdatePerson(per);
                                                                                        _personRepository.Save();
                                                                                    }
                                                                                }
                                                                            }

                                                                            StringBuilder sbtext = new StringBuilder();
                                                                            sbtext.AppendLine("<b>آیا میخوای کابر رو بلاک کنی؟</b>");
                                                                            await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sbtext.ToString(),
                                                                                ParseMode.Html, null, false, false, 0, false, BlockKeyboard);
                                                                        }
                                                                        else
                                                                        {
                                                                            StringBuilder sbtext = new StringBuilder();
                                                                            sbtext.AppendLine("<b>کاربر گرامی شما قبلا این شخص رو بلاک کردین.</b>");
                                                                            await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sbtext.ToString(),
                                                                                ParseMode.Html, null, false, false, 0, false);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        StringBuilder sb = new StringBuilder();
                                                                        sb.AppendLine(
                                                                            "<b>کاربر گرامی شما در حال صحبت با شخص مورد نظر هستین بنابراین این دستور منقضی شده.</b>.");
                                                                        await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(),
                                                                            ParseMode.Html,
                                                                            null, false, false, 0, false);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    StringBuilder sb =
                                                                                    new StringBuilder();
                                                                    sb.AppendLine(
                                                                        "<b>کاربر گرامی لطفا منتظر تایید باشین یا در صورت تمایل گزینه انصراف را بزنید.</b>");
                                                                    await bot.SendTextMessageAsync(
                                                                        up.CallbackQuery.From.Id,
                                                                        sb.ToString(),
                                                                        ParseMode.Html,
                                                                        null, false,
                                                                        false, 0,
                                                                        false);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    StringBuilder sb =
                                                                                        new StringBuilder();
                                    sb.AppendLine(
                                        "<b>کاربر گرامی متاسفانه این درخواست دیگه اعتباری نداره!</b>");
                                    await bot.SendTextMessageAsync(
                                        up.CallbackQuery.From.Id,
                                        sb.ToString(),
                                        ParseMode.Html,
                                        null, false,
                                        false, 0,
                                        false);
                                }
                            }




                            var friends = _friendRepository.GetAllFriendList();

                            if (friends.Any(f =>
                                    f.P1Chatid == up.CallbackQuery.From.Id &&
                                    f.P2Chatid.ToString() == callback))
                            {
                                foreach (FriendsList fr in friends)
                                {
                                    if (fr.P1Chatid == up.CallbackQuery.From.Id &&
                                        fr.P2Chatid.ToString() == callback)
                                    {
                                        if (up.CallbackQuery.Data == fr.P2Chatid + "تایید")
                                        {
                                            var online2 = _onlinesRepository.GetAllOnlines();
                                            if (!online2.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2 == fr.P2Chatid) && !online2.Any(o => o.chatid == fr.P2Chatid && o.User2 == up.CallbackQuery.From.Id))
                                            {
                                                var onlines = _onlinesRepository.GetAllOnlines();
                                                if (!onlines.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2.ToString() == callback))
                                                {
                                                    if (person.Any(p => p.chatid == up.CallbackQuery.From.Id))
                                                    {
                                                        foreach (Person per in person)
                                                        {
                                                            if (per.chatid == up.CallbackQuery.From.Id)
                                                            {

                                                                var validation2 = _validationRepository.GetAllValidations();
                                                                if (onlines.Any(o => o.chatid == up.CallbackQuery.From.Id))
                                                                {
                                                                    foreach (Onlines on in onlines)
                                                                    {
                                                                        if (on.chatid == up.CallbackQuery.From.Id)
                                                                        {
                                                                            if (on.User2 != 0)
                                                                            {
                                                                                if (!validation2.Any(v =>
                                                                                        v.P1Chatid == fr.P1Chatid &&
                                                                                        v.P2Chatid == fr.P2Chatid))
                                                                                {
                                                                                    StringBuilder sb =
                                                                                        new StringBuilder();
                                                                                    sb.AppendLine(
                                                                                        "<b>کاربر گرامی متاسفانه درخواست پاک شده اگه میخوای میتونی دوباره به دوستت درخواست بدی!</b>");
                                                                                    await bot.SendTextMessageAsync(
                                                                                        up.CallbackQuery.From.Id,
                                                                                        sb.ToString(),
                                                                                        ParseMode.Html,
                                                                                        null, false,
                                                                                        false, 0,
                                                                                        false);
                                                                                }
                                                                                else
                                                                                {
                                                                                    StringBuilder sb =
                                                                                        new StringBuilder();
                                                                                    sb.AppendLine(
                                                                                        "<b>کاربر گرامی شما در حال حاضر مشغول به صحبت با شخص دیگری هستی . اگه میخوای درخواست رو تایید کنی اول اتمام صحبت رو بزن و این گپ رو ببند.</b>");
                                                                                    await bot.SendTextMessageAsync(
                                                                                        up.CallbackQuery.From.Id,
                                                                                        sb.ToString(),
                                                                                        ParseMode.Html,
                                                                                        null, false,
                                                                                        false, 0,
                                                                                        false);

                                                                                }

                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    validation2 = _validationRepository.GetAllValidations();
                                                                    if (!validation2.Any(v =>
                                                                            v.P1Chatid == fr.P1Chatid &&
                                                                            v.P2Chatid == fr.P2Chatid))
                                                                    {
                                                                        StringBuilder sb =
                                                                                        new StringBuilder();
                                                                        sb.AppendLine(
                                                                            "<b>کاربر گرامی متاسفانه درخواست پاک شده اگه میخوای میتونی دوباره به دوستت درخواست بدی!</b>");
                                                                        await bot.SendTextMessageAsync(
                                                                            up.CallbackQuery.From.Id,
                                                                            sb.ToString(),
                                                                            ParseMode.Html,
                                                                            null, false,
                                                                            false, 0,
                                                                            false);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (person.Any(p =>
                                                                                p.chatid == up.CallbackQuery.From.Id))
                                                                        {
                                                                            foreach (Person per2 in person)
                                                                            {


                                                                                if (fr.P2Chatid.ToString() == callback)
                                                                                {
                                                                                    if (!onlines.Any(o =>
                                                                                        o.chatid == per2.chatid))
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            if (per2.chatid ==
                                                                                             fr.P2Chatid)
                                                                                            {

                                                                                                StringBuilder sb =
                                                                                                    new StringBuilder();
                                                                                                sb.AppendLine(
                                                                                                    "<b>به دوستت وصل شدی بهش سلام کن :</b>)");
                                                                                                await bot
                                                                                                    .SendTextMessageAsync(
                                                                                                        per2.chatid,
                                                                                                        sb.ToString(),
                                                                                                        ParseMode.Html,
                                                                                                        null,
                                                                                                        false, false,
                                                                                                        0, false,
                                                                                                        StopKeyboard);
                                                                                                Onlines o = new Onlines()
                                                                                                {
                                                                                                    Username = per2
                                                                                                        .PersonName,
                                                                                                    PersonId = per2.PersonId,
                                                                                                    chatid = per2.chatid,
                                                                                                    User2 = fr
                                                                                                        .P1Chatid
                                                                                                };
                                                                                                _onlinesRepository
                                                                                                    .InsertOnline(
                                                                                                        o);
                                                                                                _onlinesRepository.Save();
                                                                                                per2.CommandName =
                                                                                                    "StopKeyboard";
                                                                                                _personRepository
                                                                                                    .UpdatePerson(
                                                                                                        per2);
                                                                                                _personRepository.Save();
                                                                                                if (validation2.Any(v =>
                                                                                                    v.P1Chatid ==
                                                                                                    per2.chatid &&
                                                                                                    v.P2Chatid ==
                                                                                                    fr.P1Chatid))
                                                                                                {
                                                                                                    foreach
                                                                                                        (RequestValidation
                                                                                                             vl in
                                                                                                         validation2)
                                                                                                    {
                                                                                                        if (vl.P1Chatid ==
                                                                                                         per2.chatid &&
                                                                                                         vl.P2Chatid ==
                                                                                                         fr.P1Chatid)
                                                                                                        {
                                                                                                            _validationRepository
                                                                                                                .DeleteValidation(
                                                                                                                    vl);
                                                                                                            _validationRepository
                                                                                                                .Save();
                                                                                                        }
                                                                                                    }

                                                                                                }

                                                                                            }

                                                                                            if (per2.chatid ==
                                                                                             fr.P1Chatid)
                                                                                            {
                                                                                                Onlines o2 = new Onlines()
                                                                                                {
                                                                                                    Username = per2
                                                                                                        .PersonName,
                                                                                                    PersonId = per2.PersonId,
                                                                                                    chatid = per2.chatid,
                                                                                                    User2 = fr
                                                                                                        .P2Chatid
                                                                                                };
                                                                                                _onlinesRepository
                                                                                                    .InsertOnline(
                                                                                                        o2);
                                                                                                _onlinesRepository.Save();
                                                                                                per2.CommandName =
                                                                                                    "StopKeyboard";
                                                                                                _personRepository
                                                                                                    .UpdatePerson(
                                                                                                        per2);
                                                                                                _personRepository.Save();
                                                                                                StringBuilder sb =
                                                                                                    new StringBuilder();
                                                                                                sb.AppendLine(
                                                                                                    "<b>به دوستت وصل شدی بهش سلام کن :</b>)");
                                                                                                await bot
                                                                                                    .SendTextMessageAsync(
                                                                                                        per2.chatid,
                                                                                                        sb.ToString(),
                                                                                                        ParseMode.Html,
                                                                                                        null,
                                                                                                        false, false,
                                                                                                        0, false,
                                                                                                        StopKeyboard);
                                                                                                if (validation2.Any(v =>
                                                                                                    v.P1Chatid ==
                                                                                                    per2.chatid &&
                                                                                                    v.P2Chatid ==
                                                                                                    fr.P2Chatid))
                                                                                                {
                                                                                                    foreach
                                                                                                        (RequestValidation
                                                                                                             vl in
                                                                                                         validation2)
                                                                                                    {
                                                                                                        if (vl.P1Chatid ==
                                                                                                         per2.chatid &&
                                                                                                         vl.P2Chatid ==
                                                                                                         fr
                                                                                                             .P2Chatid)
                                                                                                        {
                                                                                                            _validationRepository
                                                                                                                .DeleteValidation(
                                                                                                                    vl);
                                                                                                            _validationRepository
                                                                                                                .Save();
                                                                                                        }
                                                                                                    }

                                                                                                }

                                                                                            }

                                                                                        }
                                                                                        catch
                                                                                        {
                                                                                            StringBuilder sbchat = new StringBuilder();
                                                                                            sbchat.AppendLine("<b>کاربر گرامی اشکالی در ارتباط بوجود آمده و ارسال پیام امکان پذیر نیست. ممکن است دلیل اختلال بوجود آمده بلاک کردن ربات از سمت کاربر مقابل باشد </b>");
                                                                                            await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sbchat.ToString(), ParseMode.Html, null, false, false, 0, false);
                                                                                        }
                                                                                    }



                                                                                }

                                                                            }
                                                                        }

                                                                    }

                                                                }

                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                StringBuilder sb = new StringBuilder();
                                                sb.AppendLine(
                                                    "<b>کاربر گرامی شما در حال صحبت با شخص مورد نظر هستین بنابراین این دستور منقضی شده.</b>.");
                                                await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(),
                                                    ParseMode.Html,
                                                    null, false, false, 0, false);
                                            }
                                        }
                                        else if (up.CallbackQuery.Data == fr.P2Chatid + "عدم تایید")
                                        {
                                            var online2 = _onlinesRepository.GetAllOnlines();
                                            if (!online2.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2 == fr.P2Chatid) && !online2.Any(o => o.chatid == fr.P2Chatid && o.User2 == up.CallbackQuery.From.Id))
                                            {
                                                var validation2 = _validationRepository.GetAllValidations();
                                                if (!validation2.Any(v =>
                                                        v.P1Chatid == fr.P1Chatid &&
                                                        v.P2Chatid == fr.P2Chatid))
                                                {
                                                    StringBuilder sb =
                                                                    new StringBuilder();
                                                    sb.AppendLine(
                                                        "<b>کاربر گرامی درخواست پاک شده اگه میخوای میتونی دوباره به دوستت درخواست بدی!</b>");
                                                    await bot.SendTextMessageAsync(
                                                        up.CallbackQuery.From.Id,
                                                        sb.ToString(),
                                                        ParseMode.Html,
                                                        null, false,
                                                        false, 0,
                                                        false);
                                                }
                                                else
                                                {
                                                    foreach (RequestValidation rv in validation2)
                                                    {
                                                        if (rv.P1Chatid == up.CallbackQuery.From.Id &&
                                                            rv.P2Chatid.ToString() == callback)
                                                        {

                                                            _validationRepository.DeleteValidation(rv);
                                                            _validationRepository.Save();
                                                            StringBuilder sb = new StringBuilder();
                                                            sb.AppendLine(
                                                                "<b>درخواست با موفقیت حذف شد</b>");
                                                            await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(),
                                                                ParseMode.Html,
                                                                null, false, false, 0, false);
                                                            foreach (Person per in person)
                                                            {
                                                                if (per.chatid.ToString() == callback)
                                                                {
                                                                    per.CommandName = "SearchKeyboard";
                                                                    _personRepository.UpdatePerson(per);
                                                                    _personRepository.Save();
                                                                }
                                                            }
                                                            StringBuilder sb2 = new StringBuilder();
                                                            sb2.AppendLine(
                                                                "<b>کاربر مورد نظر تایید نکرد</b>" +
                                                                "<b>حالا چه کاری برات انجام بدم؟</b>");
                                                            await bot.SendTextMessageAsync(callback, sb2.ToString(),
                                                                ParseMode.Html,
                                                                null, false, false, 0, false);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                StringBuilder sb = new StringBuilder();
                                                sb.AppendLine(
                                                    "<b>کاربر گرامی شما در حال صحبت با شخص مورد نظر هستین بنابراین این دستور منقضی شده.</b>.");
                                                await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(),
                                                    ParseMode.Html,
                                                    null, false, false, 0, false);
                                            }
                                        }
                                        var CHangeFNcallback = up.CallbackQuery.Data.Remove(up.CallbackQuery.Data.Length - 9, 9);
                                        var ChangeFriendName = _friendRepository.GetAllFriendList();

                                        if (ChangeFriendName.Any(ch =>
                                                ch.P1Chatid == up.CallbackQuery.From.Id &&
                                                ch.P2Chatid.ToString() == CHangeFNcallback))
                                        {
                                            foreach (FriendsList frch in ChangeFriendName)
                                            {
                                                if (frch.P1Chatid == up.CallbackQuery.From.Id &&
                                                    frch.P2Chatid.ToString() == CHangeFNcallback)
                                                {
                                                    if (up.CallbackQuery.Data == frch.P2Chatid.ToString() + "تغییر اسم")
                                                    {
                                                        var online2 = _onlinesRepository.GetAllOnlines();
                                                        if (!online2.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2 == fr.P2Chatid) && !online2.Any(o => o.chatid == fr.P2Chatid && o.User2 == up.CallbackQuery.From.Id))
                                                        {
                                                            StringBuilder sb = new StringBuilder();
                                                            sb.AppendLine("<b>بسیار خب! حالا یه اسم دیگه برای دوستت انتخاب کن</b>");
                                                            await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(), ParseMode.Html, null, false, false, 0, false, ComeBackKeyboard);
                                                            if (person.Any(p => p.chatid == up.CallbackQuery.From.Id))
                                                            {
                                                                foreach (Person per in person)
                                                                {
                                                                    if (per.chatid == up.CallbackQuery.From.Id)
                                                                    {
                                                                        per.CommandName = "ComeBackKeyboard";
                                                                        per.LastUser = long.Parse(CHangeFNcallback);
                                                                        _personRepository.UpdatePerson(per);
                                                                        _personRepository.Save();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            StringBuilder sb = new StringBuilder();
                                                            sb.AppendLine(
                                                                "<b>کاربر گرامی شما در حال صحبت با شخص مورد نظر هستین در این شرایط نمیتونین اسمش رو تغییر بدین.</b>.");
                                                            await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(),
                                                                ParseMode.Html,
                                                                null, false, false, 0, false);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        if (up.CallbackQuery.Data == fr.P2Chatid.ToString() + "حذف")
                                        {
                                            var online2 = _onlinesRepository.GetAllOnlines();
                                            if (!online2.Any(o => o.chatid == up.CallbackQuery.From.Id && o.User2 == fr.P2Chatid) && !online2.Any(o => o.chatid == fr.P2Chatid && o.User2 == up.CallbackQuery.From.Id))
                                            {
                                                foreach (Person per in person)
                                                {
                                                    if (per.chatid == up.CallbackQuery.From.Id)
                                                    {
                                                        per.LastUser = long.Parse(callback);
                                                        per.CommandName = "DeleteFriendKeyboard";
                                                        _personRepository.UpdatePerson(per);
                                                        _personRepository.Save();
                                                    }
                                                }
                                                StringBuilder sb = new StringBuilder();
                                                sb.AppendLine("<b>آیا مطمئنی میخوای حذفش کنی؟</b>");
                                                await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(), ParseMode.Html, null, false, false, 0, false, DeleteFriendKeyboard);
                                            }
                                            else
                                            {
                                                StringBuilder sb = new StringBuilder();
                                                sb.AppendLine(
                                                    "<b>کاربر گرامی شما در حال صحبت با شخص مورد نظر هستین در این شرایط نمیتونین از لیست دوستاتون حذفش کنین.</b>.");
                                                await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sb.ToString(),
                                                    ParseMode.Html,
                                                    null, false, false, 0, false);
                                            }
                                        }
                                    }
                                }
                            }



                        }

                        if (up.Message == null) continue;

                        var from = up.Message.From;
                        var chatId = up.Message.Chat.Id;
                        //var channel = bot.GetChatMemberAsync(-1001524352586, chatId).Result.Status.ToString();

                        if (up.Message.Type == MessageType.Text)
                        {
                            command = up.Message.Text.ToLower();
                        }
                        //else if (up.Message.Type == MessageType.Photo)
                        //{
                        //    foreach (Person per in person)
                        //    {
                        //        if (per.chatid == chatId)
                        //        {
                        //            if (per.CommandName == "StopKeyboard")
                        //            {
                        //                file = await bot.GetFileAsync(up.Message.Photo[up.Message.Photo.Count() - 1].FileId);
                        //                path = file.FilePath;
                        //                var stream = File.Open(path, FileMode.Create);
                        //                file2 = await bot.GetInfoAndDownloadFileAsync(up.Message.Photo[up.Message.Photo.Count() - 1].FileId, stream);
                        //                stream.Close();
                        //            }
                        //        }
                        //    }
                        //}
                        //else if (up.Message.Type == MessageType.Video)
                        //{
                        //    foreach (Person per in person)
                        //    {
                        //        if (per.chatid == chatId)
                        //        {
                        //            if (per.CommandName == "StopKeyboard")
                        //            {
                        //                if (up.Message.Video.Duration < 61)
                        //                {
                        //                    file = await bot.GetFileAsync(up.Message.Video.FileId);
                        //                    path = file.FilePath;
                        //                    var stream = File.Open(path, FileMode.Create);
                        //                    file2 = await bot.GetInfoAndDownloadFileAsync(up.Message.Video.FileId, stream);
                        //                    stream.Close();
                        //                }
                        //                else
                        //                {
                        //                    StringBuilder sb = new StringBuilder();
                        //                    sb.AppendLine("کاربر گرامی ، حجم فایلی که ارسال کرده اید بیشتر از حد مجاز است لطفا از ویدئو های کمتر از 1 دقیقه استفاده کنید.");
                        //                    await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, null, false, false, 0, false, StopKeyboard);

                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //else if (up.Message.Type == MessageType.Sticker)
                        //{
                        //    foreach (Person per in person)
                        //    {
                        //        if (per.chatid == chatId)
                        //        {
                        //            if (per.CommandName == "StopKeyboard")
                        //            {
                        //                file = await bot.GetFileAsync(up.Message.Sticker.FileId);
                        //                path = file.FilePath;
                        //                var stream = File.Open(path, FileMode.Create);
                        //                file2 = await bot.GetInfoAndDownloadFileAsync(up.Message.Sticker.FileId, stream);
                        //                stream.Close();

                        //            }
                        //        }
                        //    }
                        //}
                        //else if (up.Message.Type == MessageType.Document)
                        //{
                        //    foreach (Person per in person)
                        //    {
                        //        if (per.chatid == chatId)
                        //        {
                        //            if (per.CommandName == "StopKeyboard")
                        //            {
                        //                file = await bot.GetFileAsync(up.Message.Animation.FileId);
                        //                path = file.FilePath;
                        //                var stream = File.Open(path, FileMode.Create);
                        //                file2 = await bot.GetInfoAndDownloadFileAsync(up.Message.Animation.FileId, stream);
                        //                stream.Close();

                        //            }
                        //        }
                        //    }
                        //}
                        //else if (up.Message.Type == MessageType.Voice)
                        //{
                        //    foreach (Person per in person)
                        //    {
                        //        if (per.chatid == chatId)
                        //        {
                        //            if (per.CommandName == "StopKeyboard")
                        //            {
                        //                file = await bot.GetFileAsync(up.Message.Voice.FileId);
                        //                path = file.FilePath;
                        //                var stream = File.Open(path, FileMode.Create);
                        //                file2 = await bot.GetInfoAndDownloadFileAsync(up.Message.Voice.FileId, stream);
                        //                stream.Close();

                        //            }
                        //        }
                        //    }
                        //}
                        //else if (up.Message.Type == MessageType.Audio)
                        //{
                        //    foreach (Person per in person)
                        //    {
                        //        if (per.chatid == chatId)
                        //        {
                        //            if (per.CommandName == "StopKeyboard")
                        //            {
                        //                file = await bot.GetFileAsync(up.Message.Audio.FileId);
                        //                path = file.FilePath;
                        //                var stream = File.Open(path, FileMode.Create);
                        //                file2 = await bot.GetInfoAndDownloadFileAsync(up.Message.Audio.FileId, stream);
                        //                stream.Close();

                        //            }
                        //        }
                        //    }
                        //}
                        //else if (up.Message.Type == MessageType.VideoNote)
                        //{
                        //    foreach (Person per in person)
                        //    {
                        //        if (per.chatid == chatId)
                        //        {
                        //            if (per.CommandName == "StopKeyboard")
                        //            {
                        //                file = await bot.GetFileAsync(up.Message.VideoNote.FileId);
                        //                path = file.FilePath;
                        //                var stream = File.Open(path, FileMode.Create);
                        //                file2 = await bot.GetInfoAndDownloadFileAsync(up.Message.VideoNote.FileId, stream);
                        //                stream.Close();

                        //            }
                        //        }
                        //    }
                        //}

                        //command = "\U0001F50D" + "برام یه هم صحبت پیدا کن!";

                        if (command != null)
                        {
                            if (up.Message.From.IsBot == false)
                            {
                                //if (channel == "Member" || channel == "Creator" || channel == "Administrator" || command == "/start" || command == "درباره ربات" + " \U00002764")
                                //{

                                var friends1 = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId);
                                foreach (FriendsList fr in friends1)
                                {
                                    if (command == $"/start {fr.P2Chatid}")
                                    {
                                        InlineKeyboardButton urlButton = new InlineKeyboardButton();
                                        InlineKeyboardButton urlButton2 = new InlineKeyboardButton();
                                        InlineKeyboardButton urlButton3 = new InlineKeyboardButton();

                                        urlButton.Text = "درخواست صحبت";
                                        urlButton.CallbackData = fr.P2Chatid.ToString() + "درخواست صحبت";
                                        urlButton.Pay = true;

                                        urlButton2.Text = "تغییر اسم";
                                        urlButton2.CallbackData = fr.P2Chatid.ToString() + "تغییر اسم";
                                        urlButton2.Pay = true;

                                        urlButton3.Text = "حذف";
                                        urlButton3.CallbackData = fr.P2Chatid.ToString() + "حذف";
                                        urlButton3.Pay = true;



                                        InlineKeyboardButton[] buttons = new InlineKeyboardButton[] { urlButton, urlButton2, urlButton3 };

                                        // Keyboard markup
                                        InlineKeyboardMarkup inline = new InlineKeyboardMarkup(buttons);
                                        StringBuilder sb = new StringBuilder();
                                        sb.AppendLine($"<b>اسم : {fr.name}</b>");
                                        sb.AppendLine("<b>می خوای بهش درخواست صحبت بفرستی؟</b>");
                                        await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, inline);
                                        command = null;
                                    }
                                }
                                if (command == "/start")
                                {

                                    StringBuilder sb = new StringBuilder();
                                    if (!person.Any(p => p.chatid == chatId))
                                    {
                                        sb.AppendLine("<b>سلام " + from.FirstName + " ، خوش اومدی \U00002764</b>");
                                        sb.AppendLine("<b>میتونی با گزینه های پایین بگی برات چیکار کنم \U0001F603 </b>");
                                        await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, mainKeyboard);

                                        Person pr = new Person()
                                        {
                                            chatid = chatId,
                                            Username = up.Message.From.Username,
                                            CommandName = "mainKeyboard"
                                        };
                                        _personRepository.InsertPerson(pr);
                                        _personRepository.Save();
                                    }
                                    else
                                    {
                                        //if (channel == "Member" || channel == "Creator" || channel == "Administrator")
                                        //{
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == chatId)
                                            {
                                                if (per.CommandName == "CancellKeyboard")
                                                {
                                                    StringBuilder sbRepeat = new StringBuilder();
                                                    sbRepeat.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
                                                    sbRepeat.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                                                    CommandCheck(chatId, sbRepeat);
                                                }
                                                else if (per.CommandName == "StopKeyboard")
                                                {
                                                    if (online.Any(o => o.PersonId == per.PersonId))
                                                    {
                                                        foreach (Onlines on in online)
                                                        {
                                                            if (on.PersonId == per.PersonId)
                                                            {
                                                                StringBuilder sbRepeat = new StringBuilder();
                                                                sbRepeat.AppendLine("<b>پیام سیستم : شما در حال چت با هم صحبت خودتون هستید</b>");
                                                                CommandCheck(chatId, sbRepeat);
                                                            }
                                                        }
                                                    }

                                                }
                                                else
                                                {
                                                    StringBuilder sbRepeat = new StringBuilder();
                                                    sbRepeat.AppendLine("<b>خب چه کاری برات انجام بدم؟</b>");
                                                    CommandCheck(chatId, sbRepeat);
                                                }

                                            }
                                        }
                                        //}
                                        //else
                                        //{
                                        //    var channelID = bot.GetChatAsync(-1001524352586).Result.Username.ToString();
                                        //    StringBuilder sbRepeat = new StringBuilder();
                                        //    sbRepeat.AppendLine("<b>شما هنوز عضو کانال " + "@" + bot.GetChatAsync(-1001524352586).Result.Username + " نشده اید.</b>");
                                        //    sbRepeat.AppendLine("<b>لطفا ابتدا در کانال عضو شده و سپس روی /start بزنید.</b>");
                                        //    bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html);
                                        //}
                                    }



                                }
                                //***************************************************************************************************************************************************


                                else if (command == "ثبت نام" + " \U0001F63B")
                                {
                                    if (person.Any(p => p.chatid == chatId))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "mainKeyboard")
                                                {

                                                    if (per.chatid == from.Id)
                                                    {
                                                        per.CommandName = "GenderKeyboard";
                                                        per.PersonName = Guid.NewGuid().ToString();
                                                        if (person.Any(p => p.PersonName == per.PersonName))
                                                        {
                                                            per.PersonName = Guid.NewGuid().ToString();
                                                        }
                                                        _personRepository.UpdatePerson(per);
                                                        _personRepository.Save();
                                                    }
                                                    id += 1;
                                                    StringBuilder sb = new StringBuilder();
                                                    sb.AppendLine("<b>لطفا جنسیت خود را انتخاب کنید\U00002049</b>");
                                                    await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, GenderKeyboard);
                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }


                                }

                                //***************************************************************************************************************************************************
                                else if (command == "درباره ربات" + " \U00002764")
                                {
                                    if (person.Any(p => p.chatid == chatId))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "mainKeyboard" || per.CommandName == "SearchKeyboard")
                                                {
                                                    StringBuilder sb = new StringBuilder();
                                                    sb.AppendLine("<b>'بیاب'</b> یک ربات دوستیابی و چت ناشناس هستش که باهاش میتونی از هرجای ایران که خواستی دوست پیدا کنی و به صورت ناشناس صحبت کنی\U0001F603");
                                                    sb.AppendLine("");
                                                    sb.AppendLine("در حال حاضر استفاده از بخش های پیشرفته ربات مثل (فیلتر جنسیت ، فیلتر سن ، فیلتر استان) کاملا <b>رایگان</b> هستش و شما عزیزان برای استفاده از ربات فقط کافیه در کانال ما با آیدی @HarHarLand عضو شین و از ما حمایت کنین تا بتونیم امکانات جدید تر و بهتری در اختیارتون بذاریم\U00002764");
                                                    sb.AppendLine("");
                                                    sb.AppendLine("<b>دوستان عزیز از تک تک شما عزیزان خواهش می کنیم هنگام صحبت با کاربران دیگه ادب رو رعایت کنید و از ارسال گیف یا عکس های مستهجن به شدت خودداری کنید ، در غیر اینصورت در کمال تاسف از سیستم حذف خواهید شد ، با تشکر\U0001F64F</b>");
                                                    sb.AppendLine("");
                                                    sb.AppendLine("راستی ، تا چند وقت دیگه به بیاب امکانات جدیدی مثل (ویرایش مشخصات شخص ، بازی با دوستت ، نمایش افراد هم استانی و هم سن بر اساس فیلتر جنسیت و ...) اضافه میشه اما برای اضافه کردن این امکانات ما به حمایت شما که معرفی ربات به دوستانتون هست نیاز داریم.");
                                                    sb.AppendLine("");
                                                    sb.AppendLine("در آخر باز هم از شما دوستان عزیز تقاضا میکنیم که ربات رو به دوستانتون معرفی کنید و با حمایت خودتون از ما به 'بیاب' قدرت بدین تا با امکانات بهتری به شما خدمت کنه\U0001F607");
                                                    sb.AppendLine("");
                                                    sb.AppendLine("خب حالا از طریق کیبورد پایین بگو چه کاری برات انجام بدم؟\U0001F609");
                                                    sb.AppendLine("\U0001F449\U0001F449@Beyab_Bot\U0001F448\U0001F448");
                                                    await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false);
                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }


                                }

                                //***************************************************************************************************************************************************


                                else if (command == "پسرم" + " \U0001F466")
                                {

                                    foreach (Person per in person)
                                    {
                                        if (per.chatid == from.Id)
                                        {
                                            if (per.CommandName == "GenderKeyboard")
                                            {

                                                if (per.chatid == from.Id)
                                                {
                                                    per.CommandName = "CityKeyboard";
                                                    per.PersonGender = "پسر";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();
                                                }

                                                StringBuilder sb = new StringBuilder();
                                                sb.AppendLine("<b>لطفا استان خود را انتخاب کنید\U00002049</b>");
                                                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, CityKeyboard);
                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);
                                            }
                                        }
                                    }

                                }

                                //***************************************************************************************************************************************************


                                else if (command == "دخترم" + " \U0001F467")
                                {

                                    foreach (Person per in person)
                                    {
                                        if (per.chatid == from.Id)
                                        {
                                            if (per.CommandName == "GenderKeyboard")
                                            {

                                                if (per.chatid == from.Id)
                                                {
                                                    per.CommandName = "CityKeyboard";
                                                    per.PersonGender = "دختر";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();
                                                }

                                                StringBuilder sb = new StringBuilder();
                                                sb.AppendLine("<b>لطفا استان خود را انتخاب کنید\U00002049</b>");
                                                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, CityKeyboard);
                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);
                                            }
                                        }
                                    }
                                }

                                //***************************************************************************************************************************************************


                                else if (command == "اردبیل" || command == "آذربایجان غربی" || command == "آذربایجان شرقی" || command == "ایلام" || command == "البرز" || command == "اصفهان" || command == "چهارمحال و بختیاری" || command == "تهران" || command == "بوشهر" || command == "خراسان شمالی" || command == "خراسان رضوی" || command == "خراسان جنوبی" || command == "سمنان" || command == "زنجان" || command == "خوزستان" || command == "قزوین" || command == "فارس" || command == "سیستان بلوچستان" || command == "کرمان" || command == "کردستان" || command == "قم" || command == "لرستان" || command == "کهگیلویه و بویراحمد" || command == "کرمانشاه" || command == "گلستان" || command == "مازندران" || command == "گیلان" || command == "همدان" || command == "هرمزگان" || command == "مرکزی" || command == "یزد")
                                {

                                    foreach (Person per in person)
                                    {
                                        if (per.chatid == from.Id)
                                        {
                                            if (per.CommandName == "CityKeyboard")
                                            {

                                                if (per.chatid == from.Id)
                                                {
                                                    per.CommandName = "AgeKeyboard";
                                                    per.PersonCity = command;
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();
                                                }

                                                StringBuilder sb = new StringBuilder();
                                                sb.AppendLine("<b>لطفا سن خود را انتخاب کنید\U00002049</b>");
                                                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, AgeKeyboard);
                                            }
                                            else if (per.CommandName == "CityFilterKeyboard")
                                            {
                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>هم صحبتت چند ساله باشه\U00002049</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, AgeFilterKeyboard);
                                                per.CommandName = "AgeFilterKeyboard";
                                                per.FilterCity = command;
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();
                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);
                                            }
                                        }
                                    }
                                }

                                //***************************************************************************************************************************************************


                                else if (command == "18 تا 20 سال")
                                {


                                    foreach (Person per in person)
                                    {
                                        if (per.chatid == from.Id)
                                        {
                                            if (per.CommandName == "AgeKeyboard")
                                            {

                                                if (per.chatid == from.Id)
                                                {
                                                    per.CommandName = "SearchKeyboard";
                                                    per.PersonAge = "18 - 20";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();

                                                }

                                                StringBuilder sb = new StringBuilder();

                                                sb.AppendLine("<b>ثبت نام با موفقیت انجام شد\U0000263A</b>");
                                                sb.AppendLine("<b>حالا برات چه کاری انجام بدم؟</b>");
                                                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                            }
                                            else if (per.CommandName == "AgeFilterKeyboard")
                                            {
                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
                                                sbwait.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, CancellKeyboard);
                                                per.CommandName = "CancellKeyboard";
                                                per.FilterAge = "18 - 20";
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();



                                                if (person.Any(p => p.chatid == from.Id))
                                                {
                                                    if (per.chatid == from.Id)
                                                    {
                                                        if (!online.Any(o => o.PersonId == per.PersonId))
                                                        {
                                                            Onlines o = new Onlines()
                                                            {
                                                                Username = per.PersonName,
                                                                PersonId = per.PersonId,
                                                                chatid = per.chatid
                                                            };
                                                            _onlinesRepository.InsertOnline(o);
                                                            _onlinesRepository.Save();
                                                        }
                                                    }


                                                }


                                                if (person.Any(p => p.chatid == from.Id))
                                                {

                                                    if (per.chatid == from.Id)
                                                    {


                                                        var onlines = _onlinesRepository.GetAllOnlines().Where(oo => oo.User2 == 0).ToList();
                                                        var onlines2 = onlines.Where(oo => (per.FilterGender != null) ? oo.Person.PersonGender == per.FilterGender : oo.User2 == 0).ToList();
                                                        var onlines3 = onlines2.Where(oo => (oo.Person.FilterGender != null) ? oo.Person.FilterGender == per.PersonGender : oo.User2 == 0).ToList();
                                                        var onlines4 = onlines3.Where(oo => (per.FilterCity != null) ? oo.Person.PersonCity == per.FilterCity : oo.User2 == 0).ToList();
                                                        var onlines5 = onlines4.Where(oo => (oo.Person.FilterCity != null) ? oo.Person.FilterCity == per.PersonCity : oo.User2 == 0).ToList();
                                                        var onlines6 = onlines5.Where(oo => (per.FilterAge != null) ? oo.Person.PersonAge == per.FilterAge : oo.User2 == 0).ToList();
                                                        var onlines7 = onlines6.Where(oo => (oo.Person.FilterAge != null) ? oo.Person.FilterAge == per.PersonAge : oo.User2 == 0).ToList();
                                                        var me = _onlinesRepository.GetAllOnlines().Where(oo => oo.PersonId == per.PersonId);
                                                        StringBuilder sb2 = new StringBuilder();
                                                        Random rand = new Random();
                                                        if (onlines7.Count() > 0)
                                                        {

                                                            foreach (Onlines on in onlines7)
                                                            {
                                                                foreach (Onlines m in me)
                                                                {
                                                                    var block = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == chatId);
                                                                    if (block.Any(b => b.P1Chatid == chatId))
                                                                    {
                                                                        foreach (BlockList bl in block)
                                                                        {
                                                                            var block2 = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == on.Person.chatid);
                                                                            if (block2.Any(b => b.P1Chatid == on.Person.chatid))
                                                                            {
                                                                                foreach (BlockList bl2 in block2)
                                                                                {
                                                                                    if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && !block2.Any(b => b.P2Chatid == chatId) && on.User2 == 0 && m.User2 == 0)
                                                                                    {
                                                                                        on.User2 = chatId;
                                                                                        on.Person.CommandName = "StopKeyboard";
                                                                                        on.Person.FilterGender = null;
                                                                                        on.Person.FilterCity = null;
                                                                                        on.Person.FilterAge = null;
                                                                                        _onlinesRepository.UpdateOnline(on);
                                                                                        _onlinesRepository.Save();

                                                                                        sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                        await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                        m.User2 = on.Person.chatid;
                                                                                        m.Person.CommandName = "StopKeyboard";
                                                                                        _onlinesRepository.UpdateOnline(m);
                                                                                        _onlinesRepository.Save();
                                                                                        per.FilterGender = null;
                                                                                        per.FilterCity = null;
                                                                                        per.FilterAge = null;
                                                                                        _personRepository.UpdatePerson(per);
                                                                                        _personRepository.Save();

                                                                                        await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && on.User2 == 0 && m.User2 == 0)
                                                                                {
                                                                                    on.User2 = chatId;
                                                                                    on.Person.CommandName = "StopKeyboard";
                                                                                    on.Person.FilterGender = null;
                                                                                    on.Person.FilterCity = null;
                                                                                    on.Person.FilterAge = null;
                                                                                    _onlinesRepository.UpdateOnline(on);
                                                                                    _onlinesRepository.Save();

                                                                                    sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                    await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                    m.User2 = on.Person.chatid;
                                                                                    m.Person.CommandName = "StopKeyboard";
                                                                                    _onlinesRepository.UpdateOnline(m);
                                                                                    _onlinesRepository.Save();
                                                                                    per.FilterGender = null;
                                                                                    per.FilterCity = null;
                                                                                    per.FilterAge = null;
                                                                                    _personRepository.UpdatePerson(per);
                                                                                    _personRepository.Save();

                                                                                    await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        var block2 = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == on.Person.chatid);
                                                                        if (block2.Any(b => b.P1Chatid == on.Person.chatid))
                                                                        {
                                                                            foreach (BlockList bl2 in block2)
                                                                            {
                                                                                if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && !block2.Any(b => b.P2Chatid == chatId) && on.User2 == 0 && m.User2 == 0)
                                                                                {
                                                                                    on.User2 = chatId;
                                                                                    on.Person.CommandName = "StopKeyboard";
                                                                                    on.Person.FilterGender = null;
                                                                                    on.Person.FilterCity = null;
                                                                                    on.Person.FilterAge = null;
                                                                                    _onlinesRepository.UpdateOnline(on);
                                                                                    _onlinesRepository.Save();

                                                                                    sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                    await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                    m.User2 = on.Person.chatid;
                                                                                    m.Person.CommandName = "StopKeyboard";
                                                                                    _onlinesRepository.UpdateOnline(m);
                                                                                    _onlinesRepository.Save();
                                                                                    per.FilterGender = null;
                                                                                    per.FilterCity = null;
                                                                                    per.FilterAge = null;
                                                                                    _personRepository.UpdatePerson(per);
                                                                                    _personRepository.Save();

                                                                                    await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && on.User2 == 0 && m.User2 == 0)
                                                                            {
                                                                                on.User2 = chatId;
                                                                                on.Person.CommandName = "StopKeyboard";
                                                                                on.Person.FilterGender = null;
                                                                                on.Person.FilterCity = null;
                                                                                on.Person.FilterAge = null;
                                                                                _onlinesRepository.UpdateOnline(on);
                                                                                _onlinesRepository.Save();

                                                                                sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                m.User2 = on.Person.chatid;
                                                                                m.Person.CommandName = "StopKeyboard";
                                                                                _onlinesRepository.UpdateOnline(m);
                                                                                _onlinesRepository.Save();
                                                                                per.FilterGender = null;
                                                                                per.FilterCity = null;
                                                                                per.FilterAge = null;
                                                                                _personRepository.UpdatePerson(per);
                                                                                _personRepository.Save();

                                                                                await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }


                                                    }

                                                }
                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);
                                            }
                                        }
                                    }
                                }
                                else if (command == "20 تا 25 سال")
                                {

                                    foreach (Person per in person)
                                    {
                                        if (per.chatid == from.Id)
                                        {
                                            if (per.CommandName == "AgeKeyboard")
                                            {

                                                if (per.chatid == from.Id)
                                                {
                                                    per.CommandName = "SearchKeyboard";
                                                    per.PersonAge = "20 - 25";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();

                                                }

                                                StringBuilder sb = new StringBuilder();

                                                sb.AppendLine("<b>ثبت نام با موفقیت انجام شد\U0000263A</b>");
                                                sb.AppendLine("<b>حالا برات چه کاری انجام بدم؟</b>");
                                                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                            }
                                            else if (per.CommandName == "AgeFilterKeyboard")
                                            {
                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
                                                sbwait.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, CancellKeyboard);
                                                per.CommandName = "CancellKeyboard";
                                                per.FilterAge = "20 - 25";
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();



                                                if (person.Any(p => p.chatid == from.Id))
                                                {
                                                    if (per.chatid == from.Id)
                                                    {
                                                        if (!online.Any(o => o.PersonId == per.PersonId))
                                                        {
                                                            Onlines o = new Onlines()
                                                            {
                                                                Username = per.PersonName,
                                                                PersonId = per.PersonId,
                                                                chatid = per.chatid
                                                            };
                                                            _onlinesRepository.InsertOnline(o);
                                                            _onlinesRepository.Save();
                                                        }
                                                    }


                                                }


                                                if (person.Any(p => p.chatid == from.Id))
                                                {

                                                    if (per.chatid == from.Id)
                                                    {


                                                        var onlines = _onlinesRepository.GetAllOnlines().Where(oo => oo.User2 == 0).ToList();
                                                        var onlines2 = onlines.Where(oo => (per.FilterGender != null) ? oo.Person.PersonGender == per.FilterGender : oo.User2 == 0).ToList();
                                                        var onlines3 = onlines2.Where(oo => (oo.Person.FilterGender != null) ? oo.Person.FilterGender == per.PersonGender : oo.User2 == 0).ToList();
                                                        var onlines4 = onlines3.Where(oo => (per.FilterCity != null) ? oo.Person.PersonCity == per.FilterCity : oo.User2 == 0).ToList();
                                                        var onlines5 = onlines4.Where(oo => (oo.Person.FilterCity != null) ? oo.Person.FilterCity == per.PersonCity : oo.User2 == 0).ToList();
                                                        var onlines6 = onlines5.Where(oo => (per.FilterAge != null) ? oo.Person.PersonAge == per.FilterAge : oo.User2 == 0).ToList();
                                                        var onlines7 = onlines6.Where(oo => (oo.Person.FilterAge != null) ? oo.Person.FilterAge == per.PersonAge : oo.User2 == 0).ToList();
                                                        var me = _onlinesRepository.GetAllOnlines().Where(oo => oo.PersonId == per.PersonId);
                                                        StringBuilder sb2 = new StringBuilder();
                                                        Random rand = new Random();
                                                        if (onlines7.Count() > 0)
                                                        {

                                                            foreach (Onlines on in onlines7)
                                                            {
                                                                foreach (Onlines m in me)
                                                                {
                                                                    var block = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == chatId);
                                                                    if (block.Any(b => b.P1Chatid == chatId))
                                                                    {
                                                                        foreach (BlockList bl in block)
                                                                        {
                                                                            var block2 = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == on.Person.chatid);
                                                                            if (block2.Any(b => b.P1Chatid == on.Person.chatid))
                                                                            {
                                                                                foreach (BlockList bl2 in block2)
                                                                                {
                                                                                    if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && !block2.Any(b => b.P2Chatid == chatId) && on.User2 == 0 && m.User2 == 0)
                                                                                    {
                                                                                        on.User2 = chatId;
                                                                                        on.Person.CommandName = "StopKeyboard";
                                                                                        on.Person.FilterGender = null;
                                                                                        on.Person.FilterCity = null;
                                                                                        on.Person.FilterAge = null;
                                                                                        _onlinesRepository.UpdateOnline(on);
                                                                                        _onlinesRepository.Save();

                                                                                        sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                        await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                        m.User2 = on.Person.chatid;
                                                                                        m.Person.CommandName = "StopKeyboard";
                                                                                        _onlinesRepository.UpdateOnline(m);
                                                                                        _onlinesRepository.Save();
                                                                                        per.FilterGender = null;
                                                                                        per.FilterCity = null;
                                                                                        per.FilterAge = null;
                                                                                        _personRepository.UpdatePerson(per);
                                                                                        _personRepository.Save();

                                                                                        await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && on.User2 == 0 && m.User2 == 0)
                                                                                {
                                                                                    on.User2 = chatId;
                                                                                    on.Person.CommandName = "StopKeyboard";
                                                                                    on.Person.FilterGender = null;
                                                                                    on.Person.FilterCity = null;
                                                                                    on.Person.FilterAge = null;
                                                                                    _onlinesRepository.UpdateOnline(on);
                                                                                    _onlinesRepository.Save();

                                                                                    sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                    await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                    m.User2 = on.Person.chatid;
                                                                                    m.Person.CommandName = "StopKeyboard";
                                                                                    _onlinesRepository.UpdateOnline(m);
                                                                                    _onlinesRepository.Save();
                                                                                    per.FilterGender = null;
                                                                                    per.FilterCity = null;
                                                                                    per.FilterAge = null;
                                                                                    _personRepository.UpdatePerson(per);
                                                                                    _personRepository.Save();

                                                                                    await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        var block2 = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == on.Person.chatid);
                                                                        if (block2.Any(b => b.P1Chatid == on.Person.chatid))
                                                                        {
                                                                            foreach (BlockList bl2 in block2)
                                                                            {
                                                                                if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && !block2.Any(b => b.P2Chatid == chatId) && on.User2 == 0 && m.User2 == 0)
                                                                                {
                                                                                    on.User2 = chatId;
                                                                                    on.Person.CommandName = "StopKeyboard";
                                                                                    on.Person.FilterGender = null;
                                                                                    on.Person.FilterCity = null;
                                                                                    on.Person.FilterAge = null;
                                                                                    _onlinesRepository.UpdateOnline(on);
                                                                                    _onlinesRepository.Save();

                                                                                    sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                    await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                    m.User2 = on.Person.chatid;
                                                                                    m.Person.CommandName = "StopKeyboard";
                                                                                    _onlinesRepository.UpdateOnline(m);
                                                                                    _onlinesRepository.Save();
                                                                                    per.FilterGender = null;
                                                                                    per.FilterCity = null;
                                                                                    per.FilterAge = null;
                                                                                    _personRepository.UpdatePerson(per);
                                                                                    _personRepository.Save();

                                                                                    await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && on.User2 == 0 && m.User2 == 0)
                                                                            {
                                                                                on.User2 = chatId;
                                                                                on.Person.CommandName = "StopKeyboard";
                                                                                on.Person.FilterGender = null;
                                                                                on.Person.FilterCity = null;
                                                                                on.Person.FilterAge = null;
                                                                                _onlinesRepository.UpdateOnline(on);
                                                                                _onlinesRepository.Save();

                                                                                sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                m.User2 = on.Person.chatid;
                                                                                m.Person.CommandName = "StopKeyboard";
                                                                                _onlinesRepository.UpdateOnline(m);
                                                                                _onlinesRepository.Save();
                                                                                per.FilterGender = null;
                                                                                per.FilterCity = null;
                                                                                per.FilterAge = null;
                                                                                _personRepository.UpdatePerson(per);
                                                                                _personRepository.Save();

                                                                                await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }


                                                    }

                                                }
                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);
                                            }
                                        }
                                    }
                                }
                                else if (command == "25 تا 30 سال")
                                {

                                    foreach (Person per in person)
                                    {
                                        if (per.chatid == from.Id)
                                        {
                                            if (per.CommandName == "AgeKeyboard")
                                            {

                                                if (per.chatid == from.Id)
                                                {
                                                    per.CommandName = "SearchKeyboard";
                                                    per.PersonAge = "25 - 30";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();


                                                }

                                                StringBuilder sb = new StringBuilder();

                                                sb.AppendLine("<b>ثبت نام با موفقیت انجام شد\U0000263A</b>");
                                                sb.AppendLine("<b>حالا برات چه کاری انجام بدم؟</b>");
                                                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                            }
                                            else if (per.CommandName == "AgeFilterKeyboard")
                                            {
                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
                                                sbwait.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, CancellKeyboard);
                                                per.CommandName = "CancellKeyboard";
                                                per.FilterAge = "25 - 30";
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();



                                                if (person.Any(p => p.chatid == from.Id))
                                                {
                                                    if (per.chatid == from.Id)
                                                    {
                                                        if (!online.Any(o => o.PersonId == per.PersonId))
                                                        {
                                                            Onlines o = new Onlines()
                                                            {
                                                                Username = per.PersonName,
                                                                PersonId = per.PersonId,
                                                                chatid = per.chatid
                                                            };
                                                            _onlinesRepository.InsertOnline(o);
                                                            _onlinesRepository.Save();
                                                        }
                                                    }


                                                }


                                                if (person.Any(p => p.chatid == from.Id))
                                                {

                                                    if (per.chatid == from.Id)
                                                    {


                                                        var onlines = _onlinesRepository.GetAllOnlines().Where(oo => oo.User2 == 0).ToList();
                                                        var onlines2 = onlines.Where(oo => (per.FilterGender != null) ? oo.Person.PersonGender == per.FilterGender : oo.User2 == 0).ToList();
                                                        var onlines3 = onlines2.Where(oo => (oo.Person.FilterGender != null) ? oo.Person.FilterGender == per.PersonGender : oo.User2 == 0).ToList();
                                                        var onlines4 = onlines3.Where(oo => (per.FilterCity != null) ? oo.Person.PersonCity == per.FilterCity : oo.User2 == 0).ToList();
                                                        var onlines5 = onlines4.Where(oo => (oo.Person.FilterCity != null) ? oo.Person.FilterCity == per.PersonCity : oo.User2 == 0).ToList();
                                                        var onlines6 = onlines5.Where(oo => (per.FilterAge != null) ? oo.Person.PersonAge == per.FilterAge : oo.User2 == 0).ToList();
                                                        var onlines7 = onlines6.Where(oo => (oo.Person.FilterAge != null) ? oo.Person.FilterAge == per.PersonAge : oo.User2 == 0).ToList();
                                                        var me = _onlinesRepository.GetAllOnlines().Where(oo => oo.PersonId == per.PersonId);
                                                        StringBuilder sb2 = new StringBuilder();
                                                        Random rand = new Random();
                                                        if (onlines7.Count() > 0)
                                                        {

                                                            foreach (Onlines on in onlines7)
                                                            {
                                                                foreach (Onlines m in me)
                                                                {
                                                                    var block = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == chatId);
                                                                    if (block.Any(b => b.P1Chatid == chatId))
                                                                    {
                                                                        foreach (BlockList bl in block)
                                                                        {
                                                                            var block2 = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == on.Person.chatid);
                                                                            if (block2.Any(b => b.P1Chatid == on.Person.chatid))
                                                                            {
                                                                                foreach (BlockList bl2 in block2)
                                                                                {
                                                                                    if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && !block2.Any(b => b.P2Chatid == chatId) && on.User2 == 0 && m.User2 == 0)
                                                                                    {
                                                                                        on.User2 = chatId;
                                                                                        on.Person.CommandName = "StopKeyboard";
                                                                                        on.Person.FilterGender = null;
                                                                                        on.Person.FilterCity = null;
                                                                                        on.Person.FilterAge = null;
                                                                                        _onlinesRepository.UpdateOnline(on);
                                                                                        _onlinesRepository.Save();

                                                                                        sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                        await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                        m.User2 = on.Person.chatid;
                                                                                        m.Person.CommandName = "StopKeyboard";
                                                                                        _onlinesRepository.UpdateOnline(m);
                                                                                        _onlinesRepository.Save();
                                                                                        per.FilterGender = null;
                                                                                        per.FilterCity = null;
                                                                                        per.FilterAge = null;
                                                                                        _personRepository.UpdatePerson(per);
                                                                                        _personRepository.Save();

                                                                                        await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && on.User2 == 0 && m.User2 == 0)
                                                                                {
                                                                                    on.User2 = chatId;
                                                                                    on.Person.CommandName = "StopKeyboard";
                                                                                    on.Person.FilterGender = null;
                                                                                    on.Person.FilterCity = null;
                                                                                    on.Person.FilterAge = null;
                                                                                    _onlinesRepository.UpdateOnline(on);
                                                                                    _onlinesRepository.Save();

                                                                                    sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                    await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                    m.User2 = on.Person.chatid;
                                                                                    m.Person.CommandName = "StopKeyboard";
                                                                                    _onlinesRepository.UpdateOnline(m);
                                                                                    _onlinesRepository.Save();
                                                                                    per.FilterGender = null;
                                                                                    per.FilterCity = null;
                                                                                    per.FilterAge = null;
                                                                                    _personRepository.UpdatePerson(per);
                                                                                    _personRepository.Save();

                                                                                    await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        var block2 = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == on.Person.chatid);
                                                                        if (block2.Any(b => b.P1Chatid == on.Person.chatid))
                                                                        {
                                                                            foreach (BlockList bl2 in block2)
                                                                            {
                                                                                if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && !block2.Any(b => b.P2Chatid == chatId) && on.User2 == 0 && m.User2 == 0)
                                                                                {
                                                                                    on.User2 = chatId;
                                                                                    on.Person.CommandName = "StopKeyboard";
                                                                                    on.Person.FilterGender = null;
                                                                                    on.Person.FilterCity = null;
                                                                                    on.Person.FilterAge = null;
                                                                                    _onlinesRepository.UpdateOnline(on);
                                                                                    _onlinesRepository.Save();

                                                                                    sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                    await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                    m.User2 = on.Person.chatid;
                                                                                    m.Person.CommandName = "StopKeyboard";
                                                                                    _onlinesRepository.UpdateOnline(m);
                                                                                    _onlinesRepository.Save();
                                                                                    per.FilterGender = null;
                                                                                    per.FilterCity = null;
                                                                                    per.FilterAge = null;
                                                                                    _personRepository.UpdatePerson(per);
                                                                                    _personRepository.Save();

                                                                                    await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && on.User2 == 0 && m.User2 == 0)
                                                                            {
                                                                                on.User2 = chatId;
                                                                                on.Person.CommandName = "StopKeyboard";
                                                                                on.Person.FilterGender = null;
                                                                                on.Person.FilterCity = null;
                                                                                on.Person.FilterAge = null;
                                                                                _onlinesRepository.UpdateOnline(on);
                                                                                _onlinesRepository.Save();

                                                                                sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                m.User2 = on.Person.chatid;
                                                                                m.Person.CommandName = "StopKeyboard";
                                                                                _onlinesRepository.UpdateOnline(m);
                                                                                _onlinesRepository.Save();
                                                                                per.FilterGender = null;
                                                                                per.FilterCity = null;
                                                                                per.FilterAge = null;
                                                                                _personRepository.UpdatePerson(per);
                                                                                _personRepository.Save();

                                                                                await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }


                                                    }

                                                }
                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);

                                            }
                                        }
                                    }
                                }
                                else if (command == "بالای 30 سال")
                                {

                                    foreach (Person per in person)
                                    {
                                        if (per.chatid == from.Id)
                                        {
                                            if (per.CommandName == "AgeKeyboard")
                                            {

                                                if (per.chatid == from.Id)
                                                {
                                                    per.CommandName = "SearchKeyboard";
                                                    per.PersonAge = "بالای 30";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();

                                                }

                                                StringBuilder sb = new StringBuilder();

                                                sb.AppendLine("<b>ثبت نام با موفقیت انجام شد\U0000263A</b>");
                                                sb.AppendLine("<b>حالا برات چه کاری انجام بدم؟</b>");
                                                await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                            }
                                            else if (per.CommandName == "AgeFilterKeyboard")
                                            {
                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
                                                sbwait.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, CancellKeyboard);
                                                per.CommandName = "CancellKeyboard";
                                                per.FilterAge = "بالای 30";
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();



                                                if (person.Any(p => p.chatid == from.Id))
                                                {
                                                    if (per.chatid == from.Id)
                                                    {
                                                        if (!online.Any(o => o.PersonId == per.PersonId))
                                                        {
                                                            Onlines o = new Onlines()
                                                            {
                                                                Username = per.PersonName,
                                                                PersonId = per.PersonId,
                                                                chatid = per.chatid
                                                            };
                                                            _onlinesRepository.InsertOnline(o);
                                                            _onlinesRepository.Save();
                                                        }
                                                    }


                                                }


                                                if (person.Any(p => p.chatid == from.Id))
                                                {

                                                    if (per.chatid == from.Id)
                                                    {


                                                        var onlines = _onlinesRepository.GetAllOnlines().Where(oo => oo.User2 == 0).ToList();
                                                        var onlines2 = onlines.Where(oo => (per.FilterGender != null) ? oo.Person.PersonGender == per.FilterGender : oo.User2 == 0).ToList();
                                                        var onlines3 = onlines2.Where(oo => (oo.Person.FilterGender != null) ? oo.Person.FilterGender == per.PersonGender : oo.User2 == 0).ToList();
                                                        var onlines4 = onlines3.Where(oo => (per.FilterCity != null) ? oo.Person.PersonCity == per.FilterCity : oo.User2 == 0).ToList();
                                                        var onlines5 = onlines4.Where(oo => (oo.Person.FilterCity != null) ? oo.Person.FilterCity == per.PersonCity : oo.User2 == 0).ToList();
                                                        var onlines6 = onlines5.Where(oo => (per.FilterAge != null) ? oo.Person.PersonAge == per.FilterAge : oo.User2 == 0).ToList();
                                                        var onlines7 = onlines6.Where(oo => (oo.Person.FilterAge != null) ? oo.Person.FilterAge == per.PersonAge : oo.User2 == 0).ToList();
                                                        var me = _onlinesRepository.GetAllOnlines().Where(oo => oo.PersonId == per.PersonId);
                                                        StringBuilder sb2 = new StringBuilder();
                                                        Random rand = new Random();
                                                        if (onlines7.Count() > 0)
                                                        {

                                                            foreach (Onlines on in onlines7)
                                                            {
                                                                foreach (Onlines m in me)
                                                                {
                                                                    var block = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == chatId);
                                                                    if (block.Any(b => b.P1Chatid == chatId))
                                                                    {
                                                                        foreach (BlockList bl in block)
                                                                        {
                                                                            var block2 = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == on.Person.chatid);
                                                                            if (block2.Any(b => b.P1Chatid == on.Person.chatid))
                                                                            {
                                                                                foreach (BlockList bl2 in block2)
                                                                                {
                                                                                    if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && !block2.Any(b => b.P2Chatid == chatId) && on.User2 == 0 && m.User2 == 0)
                                                                                    {
                                                                                        on.User2 = chatId;
                                                                                        on.Person.CommandName = "StopKeyboard";
                                                                                        on.Person.FilterGender = null;
                                                                                        on.Person.FilterCity = null;
                                                                                        on.Person.FilterAge = null;
                                                                                        _onlinesRepository.UpdateOnline(on);
                                                                                        _onlinesRepository.Save();

                                                                                        sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                        await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                        m.User2 = on.Person.chatid;
                                                                                        m.Person.CommandName = "StopKeyboard";
                                                                                        _onlinesRepository.UpdateOnline(m);
                                                                                        _onlinesRepository.Save();
                                                                                        per.FilterGender = null;
                                                                                        per.FilterCity = null;
                                                                                        per.FilterAge = null;
                                                                                        _personRepository.UpdatePerson(per);
                                                                                        _personRepository.Save();

                                                                                        await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && on.User2 == 0 && m.User2 == 0)
                                                                                {
                                                                                    on.User2 = chatId;
                                                                                    on.Person.CommandName = "StopKeyboard";
                                                                                    on.Person.FilterGender = null;
                                                                                    on.Person.FilterCity = null;
                                                                                    on.Person.FilterAge = null;
                                                                                    _onlinesRepository.UpdateOnline(on);
                                                                                    _onlinesRepository.Save();

                                                                                    sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                    await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                    m.User2 = on.Person.chatid;
                                                                                    m.Person.CommandName = "StopKeyboard";
                                                                                    _onlinesRepository.UpdateOnline(m);
                                                                                    _onlinesRepository.Save();
                                                                                    per.FilterGender = null;
                                                                                    per.FilterCity = null;
                                                                                    per.FilterAge = null;
                                                                                    _personRepository.UpdatePerson(per);
                                                                                    _personRepository.Save();

                                                                                    await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        var block2 = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == on.Person.chatid);
                                                                        if (block2.Any(b => b.P1Chatid == on.Person.chatid))
                                                                        {
                                                                            foreach (BlockList bl2 in block2)
                                                                            {
                                                                                if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && !block2.Any(b => b.P2Chatid == chatId) && on.User2 == 0 && m.User2 == 0)
                                                                                {
                                                                                    on.User2 = chatId;
                                                                                    on.Person.CommandName = "StopKeyboard";
                                                                                    on.Person.FilterGender = null;
                                                                                    on.Person.FilterCity = null;
                                                                                    on.Person.FilterAge = null;
                                                                                    _onlinesRepository.UpdateOnline(on);
                                                                                    _onlinesRepository.Save();

                                                                                    sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                    await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                    m.User2 = on.Person.chatid;
                                                                                    m.Person.CommandName = "StopKeyboard";
                                                                                    _onlinesRepository.UpdateOnline(m);
                                                                                    _onlinesRepository.Save();
                                                                                    per.FilterGender = null;
                                                                                    per.FilterCity = null;
                                                                                    per.FilterAge = null;
                                                                                    _personRepository.UpdatePerson(per);
                                                                                    _personRepository.Save();

                                                                                    await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && on.User2 == 0 && m.User2 == 0)
                                                                            {
                                                                                on.User2 = chatId;
                                                                                on.Person.CommandName = "StopKeyboard";
                                                                                on.Person.FilterGender = null;
                                                                                on.Person.FilterCity = null;
                                                                                on.Person.FilterAge = null;
                                                                                _onlinesRepository.UpdateOnline(on);
                                                                                _onlinesRepository.Save();

                                                                                sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                m.User2 = on.Person.chatid;
                                                                                m.Person.CommandName = "StopKeyboard";
                                                                                _onlinesRepository.UpdateOnline(m);
                                                                                _onlinesRepository.Save();
                                                                                per.FilterGender = null;
                                                                                per.FilterCity = null;
                                                                                per.FilterAge = null;
                                                                                _personRepository.UpdatePerson(per);
                                                                                _personRepository.Save();

                                                                                await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }


                                                    }

                                                }
                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);
                                            }
                                        }
                                    }
                                }


                                //***************************************************************************************************************************************************


                                else if (command == "دوستان من" + "\U0001F465")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "SearchKeyboard")
                                                {
                                                    per.CommandName = "SearchKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();

                                                    StringBuilder sbtext = new StringBuilder();
                                                    var friends = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId);
                                                    if (friends.Any(f => f.P1Chatid == chatId))
                                                    {
                                                        StringBuilder sb = new StringBuilder();
                                                        sb.AppendLine("دوستان شما");
                                                        await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                                        foreach (FriendsList fr in friends)
                                                        {
                                                            sbtext.AppendLine(fr.name + " " +
                                                                                 $"<a href='https://t.me/Beyab_Bot?start={fr.P2Chatid}'>درخواست</a>");
                                                            urlname = "https://t.me/Beyab_Bot?start={fr.P2Chatid}";
                                                        }
                                                        sbtext.AppendLine(" ");
                                                        sbtext.AppendLine("<b>برای درخواست صحبت یا حذف و ویرایش دوست روی درخواست کلیک کنید و سپس start رو بزنید.</b>");
                                                        sbtext.AppendLine("\U0001F449\U0001F449@Beyab_Bot\U0001F448\U0001F448");
                                                        await bot.SendTextMessageAsync(chatId, sbtext.ToString(),
                                                            ParseMode.Html, null, true, false, 0, false, SearchKeyboard);
                                                    }
                                                    else
                                                    {
                                                        StringBuilder sbtext2 = new StringBuilder();
                                                        sbtext2.AppendLine("<b>کاربر گرامی شما هنوز کسی رو به لیست دوستان خود اضافه نکردید.</b>");
                                                        await bot.SendTextMessageAsync(chatId, sbtext2.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);
                                                    }
                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }
                                }

                                //***************************************************************************************************************************************************
                                else if (command == "مشخصات من" + "\U0001F4CB")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "SearchKeyboard")
                                                {
                                                    per.CommandName = "SearchKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();

                                                    StringBuilder sb = new StringBuilder();
                                                    sb.AppendLine("<b>مشخصات شما</b>"); ;
                                                    sb.AppendLine(" ");
                                                    if (per.PersonGender == "دختر")
                                                    {
                                                        sb.AppendLine($"<b>جنسیت : {per.PersonGender} \U0001F467</b>");
                                                    }
                                                    else
                                                    {
                                                        sb.AppendLine($"<b>جنسیت : {per.PersonGender} \U0001F466</b>");
                                                    }
                                                    sb.AppendLine($"<b>استان : {per.PersonCity}</b>");
                                                    sb.AppendLine($"<b>سن : {per.PersonAge}</b>");
                                                    sb.AppendLine(" ");

                                                    sb.AppendLine("<b>بخش ویرایش مشخصات به زودی اضافه خواهد شد</b>");
                                                    sb.AppendLine("\U0001F449\U0001F449@Beyab_Bot\U0001F448\U0001F448");
                                                    await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);
                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }
                                }

                                //***************************************************************************************************************************************************

                                else if (command == "\U0001F50D" + "برام یه هم صحبت پیدا کن!")
                                {
                                    foreach (Person per in person)
                                    {

                                        if (per.chatid == from.Id)
                                        {


                                            if (per.CommandName == "SearchKeyboard")
                                            {

                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>هم صحبتت پسر باشه یا دختر باشه؟\U00002049</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, GenderFilterKeyboard);
                                                per.CommandName = "GenderFilterKeyboard";
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();

                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);
                                            }

                                        }

                                    }


                                }

                                //***************************************************************************************************************************************************

                                else if (command == "دختر باشه" + " \U0001F467")
                                {
                                    foreach (Person per in person)
                                    {
                                        if (per.chatid == from.Id)
                                        {
                                            if (per.CommandName == "GenderFilterKeyboard")
                                            {
                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>هم صحبتت از چه استانی باشه\U00002049</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, CityFilterKeyboard);
                                                per.CommandName = "CityFilterKeyboard";
                                                per.FilterGender = "دختر";
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();
                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);
                                            }
                                        }
                                    }
                                }

                                //***************************************************************************************************************************************************

                                else if (command == "پسر باشه" + " \U0001F466")
                                {
                                    foreach (Person per in person)
                                    {
                                        if (per.chatid == from.Id)
                                        {
                                            if (per.CommandName == "GenderFilterKeyboard")
                                            {
                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>هم صحبتت از چه استانی باشه\U00002049</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, CityFilterKeyboard);
                                                per.CommandName = "CityFilterKeyboard";
                                                per.FilterGender = "پسر";
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();
                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);
                                            }
                                        }
                                    }
                                }

                                //***************************************************************************************************************************************************


                                else if (command == "اتمام صحبت")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "StopKeyboard")
                                                {

                                                    per.CommandName = "BeCancelledKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();
                                                    StringBuilder sbBC = new StringBuilder();
                                                    sbBC.AppendLine("<b>آیا مطمئنی میخوای صحبت رو ببندی؟\U0001F628</b>");
                                                    await bot.SendTextMessageAsync(chatId, sbBC.ToString(), ParseMode.Html, null, false, false, 0, false, BeCancelledKeyboard);

                                                }
                                                else
                                                {
                                                    if (per.CommandName == "CancellKeyboard")
                                                    {
                                                        StringBuilder sbRepeat = new StringBuilder();
                                                        sbRepeat.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
                                                        sbRepeat.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                                                        CommandCheck(chatId, sbRepeat);
                                                    }
                                                    else if (per.CommandName == "ComeBackKeyboard")
                                                    {
                                                        var friends = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId);



                                                        if (!friends.Any(f => f.P2Chatid == per.LastUser))
                                                        {
                                                            if (!friends.Any(f => f.name == up.Message.Text))
                                                            {
                                                                if (friends.Count() == 1)
                                                                {
                                                                    var friend = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId).SingleOrDefault();
                                                                    friend.P2Chatid = per.LastUser;
                                                                    friend.name = up.Message.Text;
                                                                    _friendRepository.UpdateFriend(friend);
                                                                    _friendRepository.Save();
                                                                    per.CommandName = "SearchKeyboard";
                                                                    _personRepository.UpdatePerson(per);
                                                                    _personRepository.Save();
                                                                    StringBuilder sbtext = new StringBuilder();
                                                                    sbtext.AppendLine(
                                                                        "<b>کاربر با موفقیت به لیست دوستات اضافه شد\U00002705</b>");
                                                                    sbtext.AppendLine("<b>خب حالا چه کاری برات انجام بدم؟</b>");
                                                                    await bot.SendTextMessageAsync(chatId,
                                                                        sbtext.ToString(), ParseMode.Html, null, false,
                                                                        false, 0, false, SearchKeyboard);

                                                                }
                                                                else
                                                                {
                                                                    FriendsList fr = new FriendsList()
                                                                    {
                                                                        P1Chatid = chatId,
                                                                        P2Chatid = per.LastUser,
                                                                        name = up.Message.Text,
                                                                        PersonId = per.PersonId
                                                                    };
                                                                    _friendRepository.InsertFriend(fr);
                                                                    _friendRepository.Save();
                                                                    per.CommandName = "SearchKeyboard";
                                                                    _personRepository.UpdatePerson(per);
                                                                    _personRepository.Save();
                                                                    StringBuilder sbtext = new StringBuilder();
                                                                    sbtext.AppendLine(
                                                                        "<b>کاربر با موفقیت به لیست دوستات اضافه شد\U00002705</b>");
                                                                    sbtext.AppendLine("<b>خب حالا چه کاری برات انجام بدم؟</b>");
                                                                    await bot.SendTextMessageAsync(chatId,
                                                                        sbtext.ToString(), ParseMode.Html, null, false,
                                                                        false, 0, false, SearchKeyboard);

                                                                }
                                                            }
                                                            else
                                                            {
                                                                StringBuilder sbtext = new StringBuilder();
                                                                sbtext.AppendLine("<b>کاربر گرامی متاسفانه این اسم رو قبلا برای دوست دیگه ای انتخاب کردی لطفا یه اسم دیگه مشخض کن\U0001F64F</b>");
                                                                await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, null, false, false, 0, false, ComeBackKeyboard);
                                                            }
                                                        }
                                                        else
                                                        {

                                                            foreach (FriendsList fr in friends)
                                                            {
                                                                if (fr.P2Chatid == per.LastUser)
                                                                {
                                                                    per.CommandName = "SearchKeyboard";
                                                                    _personRepository.UpdatePerson(per);
                                                                    _personRepository.Save();
                                                                    fr.name = up.Message.Text;
                                                                    _friendRepository.UpdateFriend(fr);
                                                                    _friendRepository.Save();
                                                                    StringBuilder sbtext = new StringBuilder();
                                                                    sbtext.AppendLine("<b>نام دوستت با موفقیت تغییر کرد\U00002705</b>");
                                                                    await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                                                }
                                                            }
                                                        }


                                                    }
                                                    else
                                                    {
                                                        StringBuilder sbRepeat = new StringBuilder();
                                                        sbRepeat.AppendLine("<b>متوجه نشدم!</b>");
                                                        sbRepeat.AppendLine("<b>لطفا از گزینه های زیر استفاده کنید</b>");
                                                        CommandCheck(chatId, sbRepeat);
                                                    }


                                                }

                                            }
                                        }
                                    }

                                }


                                //***************************************************************************************************************************************************

                                else if (command == "اتمام")
                                {
                                    string strConnString = "Data Source=.;Initial Catalog=Beyab_DB;Integrated Security=True"; // get it from Web.config file  
                                    SqlTransaction objTrans = null;
                                    var chatid1 = chatId;
                                    var chatid2 = chatId;
                                    using (SqlConnection objConn = new SqlConnection(strConnString))
                                    {
                                        objConn.Open();
                                        objTrans = objConn.BeginTransaction();
                                        if (person.Any(p => p.chatid == from.Id))
                                        {
                                            foreach (Person per in person)
                                            {
                                                if (per.chatid == from.Id)
                                                {

                                                    if (per.CommandName == "BeCancelledKeyboard")
                                                    {

                                                        per.CommandName = "AfterCancelledKeyboard";
                                                        _personRepository.UpdatePerson(per);
                                                        _personRepository.Save();


                                                        if (online.Any(o => o.PersonId == per.PersonId))
                                                        {
                                                            foreach (Onlines o in online)
                                                            {
                                                                if (o.PersonId == per.PersonId)
                                                                {

                                                                    SqlCommand objCmd1 = new SqlCommand($"DELETE FROM Onlines WHERE OnlineID={o.OnlineID};", objConn);
                                                                    

                                                                    chatid1 = o.chatid;
                                                                    try
                                                                    {
                                                                        objCmd1.ExecuteNonQuery();

                                                                        objTrans.Commit();
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        objTrans.Rollback();
                                                                    }
                                                                    finally
                                                                    {
                                                                        objConn.Close();
                                                                    }
                                                                }
                                                                if (o.User2 == from.Id)
                                                                {

                                                                    per.LastUser = o.Person.chatid;
                                                                    _personRepository.UpdatePerson(per);

                                                                    o.Person.CommandName = "AfterCancelledKeyboard";
                                                                    o.Person.LastUser = chatId;
                                                                    _onlinesRepository.UpdateOnline(o);

                                                                    SqlCommand objCmd2 = new SqlCommand($"DELETE FROM Onlines WHERE OnlineID={o.OnlineID};", objConn);

                                                                    chatid2 = o.chatid;
                                                                    try
                                                                    {
                                                                        objCmd2.ExecuteNonQuery();

                                                                        objTrans.Commit();
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        objTrans.Rollback();
                                                                    }
                                                                    finally
                                                                    {
                                                                        objConn.Close();
                                                                    }

                                                                }

                                                                
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        await WhichKeyboard(up, online, chatId, per);
                                                    }




                                                }
                                            }
                                            db.SaveChanges();

                                            StringBuilder sbABC = new StringBuilder();
                                            sbABC.AppendLine("<b>صحبت رو قطع کردی حالا چه کاری برات انجام بدم؟\U0001F606</b>");
                                            await bot.SendTextMessageAsync(chatid1, sbABC.ToString(), ParseMode.Html, null, false, false, 0, false, AfterCancelledKeyboard);

                                            StringBuilder sbABC2 = new StringBuilder();
                                            sbABC2.AppendLine("<b>کاربر از چت خارج شد حالا چه کاری برات انجام بدم؟</b>");
                                            await bot.SendTextMessageAsync(chatid2, sbABC2.ToString(), ParseMode.Html, null, false, false, 0, false, AfterCancelledKeyboard);

                                        }

                                        var newOnline = _onlinesRepository.GetAllOnlines().Where(o => o.chatid == chatId || o.User2 == chatId);
                                        if (newOnline.Count() == 0)
                                        {
                                            break;
                                        }
                                    }

                                }


                                //***************************************************************************************************************************************************

                                else if (command == "ادامه صحبت")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "BeCancelledKeyboard")
                                                {
                                                    StringBuilder sbABC3 = new StringBuilder();
                                                    sbABC3.AppendLine("<b>شما در حال چت با هم صحبت خود هستید\U0001F60A</b>");
                                                    await bot.SendTextMessageAsync(chatId, sbABC3.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);
                                                    per.CommandName = "StopKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();

                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }

                                }


                                //***************************************************************************************************************************************************

                                else if (command == "منوی اصلی")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "AfterCancelledKeyboard")
                                                {

                                                    per.CommandName = "SearchKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();

                                                    StringBuilder sbMenu = new StringBuilder();
                                                    sbMenu.AppendLine("<b>حله حالا چه کاری برات انجام بدم؟</b>");
                                                    await bot.SendTextMessageAsync(chatId, sbMenu.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }
                                }


                                //***************************************************************************************************************************************************

                                else if (command == "بلاک")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "AfterCancelledKeyboard")
                                                {

                                                    per.CommandName = "BlockKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();
                                                    StringBuilder sbtext = new StringBuilder();
                                                    sbtext.AppendLine("<b>آیا میخوای کابر رو بلاک کنی؟</b>");
                                                    await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, null, false, false, 0, false, BlockKeyboard);

                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }
                                }


                                //***************************************************************************************************************************************************


                                else if (command == "افزودن به دوست")
                                {

                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {

                                            if (per.chatid == from.Id)
                                            {

                                                if (per.CommandName == "AfterCancelledKeyboard")
                                                {
                                                    var friends = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId);
                                                    if (!friends.Any(f => f.P2Chatid == per.LastUser))
                                                    {

                                                        per.CommandName = "ComeBackKeyboard";
                                                        _personRepository.UpdatePerson(per);
                                                        _personRepository.Save();
                                                        StringBuilder sbtext = new StringBuilder();
                                                        sbtext.AppendLine(
                                                            "<b>لطفا یه اسم برای دوستت مشخص کن ( فقط حواست باشه این اسم رو قبلا برای دوست دیگه ای انتخاب نکرده باشی )</b>");
                                                        await bot.SendTextMessageAsync(chatId, sbtext.ToString(),
                                                            ParseMode.Html, null, false, false, 0, false,
                                                            ComeBackKeyboard);
                                                    }
                                                    else
                                                    {
                                                        var friend = _friendRepository.GetAllFriendList().Where(f =>
                                                            f.P1Chatid == chatId && f.P2Chatid == per.LastUser).SingleOrDefault();
                                                        StringBuilder sbtext = new StringBuilder();
                                                        sbtext.AppendLine($"<b>کاربر گرامی شما قبلا این شخص رو با نام '{friend.name}' داخل لیست دوستات ذخیره کردی. برای ویرایش دوستت یا حذف ، به بخش دوستان من برو</b>");
                                                        await bot.SendTextMessageAsync(chatId, sbtext.ToString(),
                                                            ParseMode.Html, null, false, false, 0, false);
                                                    }
                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }

                                            }
                                        }
                                    }
                                }


                                //***************************************************************************************************************************************************


                                else if (command == "بازگشت" + "\U0001F519")
                                {
                                    var friends = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId);
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (friends.Any(f => f.P2Chatid == per.LastUser))
                                                {

                                                    per.CommandName = "SearchKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();
                                                    StringBuilder sbChat = new StringBuilder();
                                                    sbChat.AppendLine("<b>حله حالا چه کاری برات انجام بدم؟</b>");
                                                    await bot.SendTextMessageAsync(chatId, sbChat.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                                }
                                                else
                                                {

                                                    per.CommandName = "AfterCancelledKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();
                                                    StringBuilder sbChat = new StringBuilder();
                                                    sbChat.AppendLine("<b>حله حالا چه کاری برات انجام بدم؟</b>");
                                                    await bot.SendTextMessageAsync(chatId, sbChat.ToString(), ParseMode.Html, null, false, false, 0, false, AfterCancelledKeyboard);

                                                }
                                            }
                                        }
                                    }

                                }


                                //***************************************************************************************************************************************************
                                else if (command == "انصراف")
                                {
                                    var persons = _personRepository.GetAllPerson();
                                    if (persons.Any(p => p.chatid == chatId))
                                    {
                                        foreach (Person per in persons)
                                        {
                                            if (per.chatid == chatId)
                                            {
                                                var vld = _validationRepository.GetAllValidations();
                                                if (vld.Any(v => v.P2Chatid == chatId && v.P1Chatid == per.LastUser))
                                                {
                                                    foreach (RequestValidation rv in vld)
                                                    {
                                                        if (rv.P2Chatid == chatId && rv.P1Chatid == per.LastUser)
                                                        {

                                                            var vld2 = _validationRepository.GetAllValidations().Where(v => v.P1Chatid == per.LastUser);

                                                            _validationRepository.DeleteValidation(rv);
                                                            _validationRepository.Save();
                                                            per.CommandName = "SearchKeyboard";
                                                            _personRepository.UpdatePerson(per);
                                                            _personRepository.Save();
                                                            StringBuilder sbChat = new StringBuilder();
                                                            sbChat.AppendLine("<b>متوجه شدم ، درخواست حذف شد. حالا چه کاری برات انجام بدم؟</b>");
                                                            await bot.SendTextMessageAsync(chatId, sbChat.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                    }


                                }


                                //***************************************************************************************************************************************************



                                else if (command == "آره")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "BlockKeyboard")
                                                {
                                                    per.CommandName = "SearchKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();

                                                    var blocks = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == chatId);

                                                    if (!blocks.Any(b => b.P2Chatid == per.LastUser))
                                                    {
                                                        BlockList block = new BlockList()
                                                        {
                                                            P1Chatid = chatId,
                                                            P2Chatid = per.LastUser,
                                                            PersonId = per.PersonId
                                                        };
                                                        _blockRepository.InsertBlock(block);
                                                        _blockRepository.Save();

                                                        var friends = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId);
                                                        if (friends.Any(f => f.P2Chatid == per.LastUser))
                                                        {
                                                            foreach (FriendsList fr in friends)
                                                            {
                                                                if (fr.P1Chatid == chatId && fr.P2Chatid == per.LastUser)
                                                                {

                                                                    _friendRepository.DeleteFriend(fr);
                                                                    _friendRepository.Save();
                                                                }
                                                            }
                                                        }
                                                        var val = _validationRepository.GetAllValidations().Where(f => f.P1Chatid == chatId);
                                                        if (val.Any(v => v.P2Chatid == per.LastUser))
                                                        {
                                                            foreach (RequestValidation vr in val)
                                                            {
                                                                if (vr.P1Chatid == chatId && vr.P2Chatid == per.LastUser)
                                                                {
                                                                    _validationRepository.DeleteValidation(vr);
                                                                    _validationRepository.Save();
                                                                }
                                                            }
                                                        }
                                                        var val2 = _validationRepository.GetAllValidations().Where(f => f.P1Chatid == per.LastUser);
                                                        if (val2.Any(v => v.P1Chatid == per.LastUser))
                                                        {
                                                            foreach (var vr in val2)
                                                            {
                                                                if (vr.P1Chatid == per.LastUser && vr.P2Chatid == chatId)
                                                                {
                                                                    _validationRepository.DeleteValidation(vr);
                                                                    _validationRepository.Save();
                                                                }
                                                            }
                                                        }
                                                    }



                                                    StringBuilder sbChat = new StringBuilder();
                                                    sbChat.AppendLine("<b>کاربر بلاک شد\U0001F6AB</b>");
                                                    await bot.SendTextMessageAsync(chatId, sbChat.ToString(), ParseMode.Html, null, false, false, 0, false);
                                                    StringBuilder sbChat2 = new StringBuilder();
                                                    sbChat2.AppendLine("<b>حالا چه کاری برات انجام بدم؟</b>");
                                                    await bot.SendTextMessageAsync(chatId, sbChat2.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);
                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }
                                }


                                //***************************************************************************************************************************************************

                                else if (command == "نه")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "BlockKeyboard")
                                                {
                                                    per.CommandName = "AfterCancelledKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();
                                                    StringBuilder sbRepeat = new StringBuilder();
                                                    sbRepeat.AppendLine("<b>حله ، چه کاری برات انجام بدم؟</b>");
                                                    if (per.Block_Ckeck == null)
                                                    {
                                                        await bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, AfterCancelledKeyboard);

                                                    }
                                                    else
                                                    {
                                                        BlockCheck(chatId, sbRepeat);
                                                        per.Block_Ckeck = null;
                                                        _personRepository.UpdatePerson(per);
                                                        _personRepository.Save();
                                                    }
                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }
                                }


                                //***************************************************************************************************************************************************


                                else if (command == "حذف")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "DeleteFriendKeyboard")
                                                {
                                                    var DeleteFriend = _friendRepository.GetAllFriendList()
                                                        .Where(f => f.P1Chatid == chatId);
                                                    var val1 = _validationRepository.GetAllValidations()
                                                        .Where(v => v.P1Chatid == chatId);
                                                    var val2 = _validationRepository.GetAllValidations()
                                                        .Where(v => v.P1Chatid == per.LastUser);
                                                    if (val1.Any(v => v.P1Chatid == chatId))
                                                    {
                                                        foreach (var vr in val1)
                                                        {
                                                            if (vr.P1Chatid == chatId && vr.P2Chatid == per.LastUser)
                                                            {
                                                                _validationRepository.DeleteValidation(vr);
                                                                _validationRepository.Save();
                                                            }
                                                        }
                                                    }
                                                    if (val2.Any(v => v.P1Chatid == per.LastUser))
                                                    {
                                                        foreach (var vr in val2)
                                                        {
                                                            if (vr.P1Chatid == per.LastUser && vr.P2Chatid == chatId)
                                                            {

                                                                _validationRepository.DeleteValidation(vr);
                                                                _validationRepository.Save();
                                                            }
                                                        }
                                                    }
                                                    if (DeleteFriend.Any(f => f.P2Chatid == per.LastUser))
                                                    {
                                                        foreach (FriendsList frd in DeleteFriend)
                                                        {
                                                            if (frd.P2Chatid == per.LastUser)
                                                            {
                                                                _friendRepository.DeleteFriend(frd);
                                                                _friendRepository.Save();
                                                                per.CommandName = "SearchKeyboard";
                                                                _personRepository.UpdatePerson(per);
                                                                _personRepository.Save();

                                                                StringBuilder sbchat = new StringBuilder();
                                                                sbchat.AppendLine("<b>کاربر با موفقیت از لیست دوستات حذف شد\U0000274C</b>" +
                                                                                  "<b> حالا چه کاری برات انجام بدم؟</b>");
                                                                await bot.SendTextMessageAsync(chatId, sbchat.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }
                                }


                                //***************************************************************************************************************************************************

                                else if (command == "بیخیال")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "DeleteFriendKeyboard")
                                                {
                                                    per.CommandName = "SearchKeyboard";
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();
                                                    StringBuilder sbchat = new StringBuilder();
                                                    sbchat.AppendLine("<b>حله حالا چه کاری برات انجام بدم؟</b>");
                                                    await bot.SendTextMessageAsync(chatId, sbchat.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);
                                                }
                                                else
                                                {
                                                    await WhichKeyboard(up, online, chatId, per);
                                                }
                                            }
                                        }
                                    }
                                }


                                //***************************************************************************************************************************************************


                                else if (command == "لغو جستجو")
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "CancellKeyboard")
                                                {
                                                    per.CommandName = "SearchKeyboard";
                                                    per.FilterGender = null;
                                                    per.FilterCity = null;
                                                    per.FilterAge = null;
                                                    _personRepository.UpdatePerson(per);
                                                    _personRepository.Save();

                                                    foreach (Onlines o in online)
                                                    {
                                                        if (o.PersonId == per.PersonId)
                                                        {
                                                            _onlinesRepository.DeleteOnline(o.OnlineID);
                                                            _onlinesRepository.Save();
                                                        }
                                                    }
                                                    StringBuilder sbCancell = new StringBuilder();
                                                    sbCancell.AppendLine("<b>حله حالا چه کاری برات انجام بدم؟</b>");
                                                    await bot.SendTextMessageAsync(chatId, sbCancell.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);
                                                }
                                                else
                                                {
                                                    if (per.CommandName == "StopKeyboard")
                                                    {
                                                        if (online.Any(o => o.PersonId == per.PersonId))
                                                        {
                                                            foreach (Onlines on in online)
                                                            {
                                                                if (on.PersonId == per.PersonId)
                                                                {
                                                                    StringBuilder sbchat = new StringBuilder();
                                                                    sbchat.AppendLine(up.Message.Text.ToLower());
                                                                    await bot.SendTextMessageAsync(on.User2, sbchat.ToString(), ParseMode.Default, null, false, false, 0, false);
                                                                }
                                                            }
                                                        }

                                                    }
                                                    else if (per.CommandName == "ComeBackKeyboard")
                                                    {
                                                        var friends = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId);



                                                        if (!friends.Any(f => f.P2Chatid == per.LastUser))
                                                        {
                                                            if (!friends.Any(f => f.name == up.Message.Text))
                                                            {
                                                                if (friends.Count() == 1)
                                                                {
                                                                    var friend = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId).SingleOrDefault();
                                                                    friend.P2Chatid = per.LastUser;
                                                                    friend.name = up.Message.Text;
                                                                    _friendRepository.UpdateFriend(friend);
                                                                    _friendRepository.Save();
                                                                    per.CommandName = "SearchKeyboard";
                                                                    _personRepository.UpdatePerson(per);
                                                                    _personRepository.Save();
                                                                    StringBuilder sbtext = new StringBuilder();
                                                                    sbtext.AppendLine(
                                                                        "<b>کاربر با موفقیت به لیست دوستات اضافه شد\U00002705</b>");
                                                                    sbtext.AppendLine("<b>خب حالا چه کاری برات انجام بدم؟</b>");
                                                                    await bot.SendTextMessageAsync(chatId,
                                                                        sbtext.ToString(), ParseMode.Html, null, false,
                                                                        false, 0, false, SearchKeyboard);

                                                                }
                                                                else
                                                                {
                                                                    FriendsList fr = new FriendsList()
                                                                    {
                                                                        P1Chatid = chatId,
                                                                        P2Chatid = per.LastUser,
                                                                        name = up.Message.Text,
                                                                        PersonId = per.PersonId
                                                                    };
                                                                    _friendRepository.InsertFriend(fr);
                                                                    _friendRepository.Save();
                                                                    per.CommandName = "SearchKeyboard";
                                                                    _personRepository.UpdatePerson(per);
                                                                    _personRepository.Save();
                                                                    StringBuilder sbtext = new StringBuilder();
                                                                    sbtext.AppendLine(
                                                                        "<b>کاربر با موفقیت به لیست دوستات اضافه شد\U00002705</b>");
                                                                    sbtext.AppendLine("<b>خب حالا چه کاری برات انجام بدم؟</b>");
                                                                    await bot.SendTextMessageAsync(chatId,
                                                                        sbtext.ToString(), ParseMode.Html, null, false,
                                                                        false, 0, false, SearchKeyboard);

                                                                }
                                                            }
                                                            else
                                                            {
                                                                StringBuilder sbtext = new StringBuilder();
                                                                sbtext.AppendLine("<b>کاربر گرامی متاسفانه این اسم رو قبلا برای دوست دیگه ای انتخاب کردی لطفا یه اسم دیگه مشخض کن\U0001F64F</b>");
                                                                await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, null, false, false, 0, false, ComeBackKeyboard);
                                                            }
                                                        }
                                                        else
                                                        {

                                                            foreach (FriendsList fr in friends)
                                                            {
                                                                if (fr.P2Chatid == per.LastUser)
                                                                {
                                                                    per.CommandName = "SearchKeyboard";
                                                                    _personRepository.UpdatePerson(per);
                                                                    _personRepository.Save();
                                                                    fr.name = up.Message.Text;
                                                                    _friendRepository.UpdateFriend(fr);
                                                                    _friendRepository.Save();
                                                                    StringBuilder sbtext = new StringBuilder();
                                                                    sbtext.AppendLine("<b>نام دوستت با موفقیت تغییر کرد\U00002705</b>");
                                                                    await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                                                }
                                                            }
                                                        }


                                                    }
                                                    else
                                                    {
                                                        StringBuilder sbRepeat = new StringBuilder();
                                                        sbRepeat.AppendLine("<b>متوجه نشدم!</b>");
                                                        sbRepeat.AppendLine("<b>لطفا از گزینه های زیر استفاده کنید</b>");
                                                        CommandCheck(chatId, sbRepeat);
                                                    }


                                                }
                                            }
                                        }
                                    }
                                }
                                //***************************************************************************************************************************************************
                                else if (command == "مهم نیست" + " \U0001F605")
                                {

                                    foreach (Person per in person)
                                    {
                                        if (per.chatid == from.Id)
                                        {
                                            if (per.CommandName == "GenderFilterKeyboard")
                                            {

                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>هم صحبتت از چه استانی باشه\U00002049</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, CityFilterKeyboard);
                                                per.CommandName = "CityFilterKeyboard";
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();
                                            }
                                            else if (per.CommandName == "CityFilterKeyboard")
                                            {
                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>هم صحبتت چند ساله باشه\U00002049</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, AgeFilterKeyboard);
                                                per.CommandName = "AgeFilterKeyboard";
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();

                                            }
                                            else if (per.CommandName == "AgeFilterKeyboard")
                                            {
                                                StringBuilder sbwait = new StringBuilder();
                                                sbwait.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
                                                sbwait.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                                                await bot.SendTextMessageAsync(chatId, sbwait.ToString(), ParseMode.Html, null, false, false, 0, false, CancellKeyboard);
                                                per.CommandName = "CancellKeyboard";
                                                _personRepository.UpdatePerson(per);
                                                _personRepository.Save();



                                                if (person.Any(p => p.chatid == from.Id))
                                                {
                                                    if (per.chatid == from.Id)
                                                    {
                                                        if (!online.Any(o => o.PersonId == per.PersonId))
                                                        {
                                                            Onlines o = new Onlines()
                                                            {
                                                                Username = per.PersonName,
                                                                PersonId = per.PersonId,
                                                                chatid = per.chatid
                                                            };
                                                            _onlinesRepository.InsertOnline(o);
                                                            _onlinesRepository.Save();
                                                        }
                                                    }


                                                }


                                                if (person.Any(p => p.chatid == from.Id))
                                                {

                                                    if (per.chatid == from.Id)
                                                    {


                                                        var onlines = _onlinesRepository.GetAllOnlines().Where(oo => oo.User2 == 0).ToList();
                                                        var onlines2 = onlines.Where(oo => (per.FilterGender != null) ? oo.Person.PersonGender == per.FilterGender : oo.User2 == 0).ToList();
                                                        var onlines3 = onlines2.Where(oo => (oo.Person.FilterGender != null) ? oo.Person.FilterGender == per.PersonGender : oo.User2 == 0).ToList();
                                                        var onlines4 = onlines3.Where(oo => (per.FilterCity != null) ? oo.Person.PersonCity == per.FilterCity : oo.User2 == 0).ToList();
                                                        var onlines5 = onlines4.Where(oo => (oo.Person.FilterCity != null) ? oo.Person.FilterCity == per.PersonCity : oo.User2 == 0).ToList();
                                                        var onlines6 = onlines5.Where(oo => (per.FilterAge != null) ? oo.Person.PersonAge == per.FilterAge : oo.User2 == 0).ToList();
                                                        var onlines7 = onlines6.Where(oo => (oo.Person.FilterAge != null) ? oo.Person.FilterAge == per.PersonAge : oo.User2 == 0).ToList();
                                                        var me = _onlinesRepository.GetAllOnlines().Where(oo => oo.PersonId == per.PersonId);
                                                        StringBuilder sb2 = new StringBuilder();
                                                        Random rand = new Random();
                                                        if (onlines7.Count() > 0)
                                                        {

                                                            foreach (Onlines on in onlines7)
                                                            {
                                                                foreach (Onlines m in me)
                                                                {
                                                                    var block = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == chatId);
                                                                    if (block.Any(b => b.P1Chatid == chatId))
                                                                    {
                                                                        foreach (BlockList bl in block)
                                                                        {
                                                                            var block2 = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == on.Person.chatid);
                                                                            if (block2.Any(b => b.P1Chatid == on.Person.chatid))
                                                                            {
                                                                                foreach (BlockList bl2 in block2)
                                                                                {
                                                                                    if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && !block2.Any(b => b.P2Chatid == chatId) && on.User2 == 0 && m.User2 == 0)
                                                                                    {
                                                                                        on.User2 = chatId;
                                                                                        on.Person.CommandName = "StopKeyboard";
                                                                                        on.Person.FilterGender = null;
                                                                                        on.Person.FilterCity = null;
                                                                                        on.Person.FilterAge = null;
                                                                                        _onlinesRepository.UpdateOnline(on);
                                                                                        _onlinesRepository.Save();

                                                                                        sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                        await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                        m.User2 = on.Person.chatid;
                                                                                        m.Person.CommandName = "StopKeyboard";
                                                                                        _onlinesRepository.UpdateOnline(m);
                                                                                        _onlinesRepository.Save();
                                                                                        per.FilterGender = null;
                                                                                        per.FilterCity = null;
                                                                                        per.FilterAge = null;
                                                                                        _personRepository.UpdatePerson(per);
                                                                                        _personRepository.Save();

                                                                                        await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && on.User2 == 0 && m.User2 == 0)
                                                                                {
                                                                                    on.User2 = chatId;
                                                                                    on.Person.CommandName = "StopKeyboard";
                                                                                    on.Person.FilterGender = null;
                                                                                    on.Person.FilterCity = null;
                                                                                    on.Person.FilterAge = null;
                                                                                    _onlinesRepository.UpdateOnline(on);
                                                                                    _onlinesRepository.Save();

                                                                                    sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                    await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                    m.User2 = on.Person.chatid;
                                                                                    m.Person.CommandName = "StopKeyboard";
                                                                                    _onlinesRepository.UpdateOnline(m);
                                                                                    _onlinesRepository.Save();
                                                                                    per.FilterGender = null;
                                                                                    per.FilterCity = null;
                                                                                    per.FilterAge = null;
                                                                                    _personRepository.UpdatePerson(per);
                                                                                    _personRepository.Save();

                                                                                    await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        var block2 = _blockRepository.GetAllBlockList().Where(b => b.P1Chatid == on.Person.chatid);
                                                                        if (block2.Any(b => b.P1Chatid == on.Person.chatid))
                                                                        {
                                                                            foreach (BlockList bl2 in block2)
                                                                            {
                                                                                if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && !block2.Any(b => b.P2Chatid == chatId) && on.User2 == 0 && m.User2 == 0)
                                                                                {
                                                                                    on.User2 = chatId;
                                                                                    on.Person.CommandName = "StopKeyboard";
                                                                                    on.Person.FilterGender = null;
                                                                                    on.Person.FilterCity = null;
                                                                                    on.Person.FilterAge = null;
                                                                                    _onlinesRepository.UpdateOnline(on);
                                                                                    _onlinesRepository.Save();

                                                                                    sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                    await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                    m.User2 = on.Person.chatid;
                                                                                    m.Person.CommandName = "StopKeyboard";
                                                                                    _onlinesRepository.UpdateOnline(m);
                                                                                    _onlinesRepository.Save();
                                                                                    per.FilterGender = null;
                                                                                    per.FilterCity = null;
                                                                                    per.FilterAge = null;
                                                                                    _personRepository.UpdatePerson(per);
                                                                                    _personRepository.Save();

                                                                                    await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (on.PersonId != per.PersonId && !block.Any(b => b.P2Chatid == on.Person.chatid) && on.User2 == 0 && m.User2 == 0)
                                                                            {
                                                                                on.User2 = chatId;
                                                                                on.Person.CommandName = "StopKeyboard";
                                                                                on.Person.FilterGender = null;
                                                                                on.Person.FilterCity = null;
                                                                                on.Person.FilterAge = null;
                                                                                _onlinesRepository.UpdateOnline(on);
                                                                                _onlinesRepository.Save();

                                                                                sb2.AppendLine("<b>هم صحبت پیدا شد بهش سلام کن\U0001F603</b>");
                                                                                await bot.SendTextMessageAsync(on.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);


                                                                                m.User2 = on.Person.chatid;
                                                                                m.Person.CommandName = "StopKeyboard";
                                                                                _onlinesRepository.UpdateOnline(m);
                                                                                _onlinesRepository.Save();
                                                                                per.FilterGender = null;
                                                                                per.FilterCity = null;
                                                                                per.FilterAge = null;
                                                                                _personRepository.UpdatePerson(per);
                                                                                _personRepository.Save();

                                                                                await bot.SendTextMessageAsync(m.Person.chatid, sb2.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }


                                                    }

                                                }
                                            }
                                            else
                                            {
                                                await WhichKeyboard(up, online, chatId, per);
                                            }
                                        }
                                    }

                                }
                                //***************************************************************************************************************************************************

                                else
                                {
                                    if (person.Any(p => p.chatid == from.Id))
                                    {
                                        foreach (Person per in person)
                                        {
                                            if (per.chatid == from.Id)
                                            {
                                                if (per.CommandName == "StopKeyboard")
                                                {
                                                    if (online.Any(o => o.PersonId == per.PersonId))
                                                    {
                                                        foreach (Onlines on in online)
                                                        {
                                                            if (on.PersonId == per.PersonId)
                                                            {
                                                                try
                                                                {
                                                                    if (up.Message.Type == MessageType.Text)
                                                                    {

                                                                        if (!up.Message.Text.Split(' ').Any(t => t == "کص") && !up.Message.Text.Split(' ').Any(t => t == "کس") && !up.Message.Text.Split(' ').Any(t => t == "کصکش") && !up.Message.Text.Split(' ').Any(t => t == "کسکش") && !up.Message.Text.Split(' ').Any(t => t == "کیر") && !up.Message.Text.Split(' ').Any(t => t == "کیری") && !up.Message.Text.Split(' ').Any(t => t == "کیرم") && !up.Message.Text.Split(' ').Any(t => t == "کیرت") && !up.Message.Text.Split(' ').Any(t => t == "جنده") && !up.Message.Text.Split(' ').Any(t => t == "مادرجنده") && !up.Message.Text.Split(' ').Any(t => t == "مادرقهوه") && !up.Message.Text.Split(' ').Any(t => t == "کون") && !up.Message.Text.Split(' ').Any(t => t == "کونی") && !up.Message.Text.Split(' ').Any(t => t == "کونده") && !up.Message.Text.Split(' ').Any(t => t == "کونکش") && !up.Message.Text.Split(' ').Any(t => t == "گاییدم") && !up.Message.Text.Split(' ').Any(t => t == "میگام") && !up.Message.Text.Split(' ').Any(t => t == "بگام") && !up.Message.Text.Split(' ').Any(t => t == "بگا") && !up.Message.Text.Split(' ').Any(t => t == "گایید") && !up.Message.Text.Split(' ').Any(t => t == "چوچول") && !up.Message.Text.Split(' ').Any(t => t == "چوچولت") && !up.Message.Text.Split(' ').Any(t => t == "کونت") && !up.Message.Text.Split(' ').Any(t => t == "کصت") && !up.Message.Text.Split(' ').Any(t => t == "کست") && !up.Message.Text.Split(' ').Any(t => t == "کصش") && !up.Message.Text.Split(' ').Any(t => t == "کسش") && !up.Message.Text.Split(' ').Any(t => t == "کونش") && !up.Message.Text.Split(' ').Any(t => t == "کیرش") && !up.Message.Text.Split(' ').Any(t => t == "ممه") && !up.Message.Text.Split(' ').Any(t => t == "خارکصه") && !up.Message.Text.Split(' ').Any(t => t == "دودول") && !up.Message.Text.Split(' ').Any(t => t == "شومبول") && !up.Message.Text.Split(' ').Any(t => t == "کصشر") && !up.Message.Text.Split(' ').Any(t => t == "کسشر") && !up.Message.Text.Split(' ').Any(t => t == "داشاق") && !up.Message.Text.Split(' ').Any(t => t == "میکنمت") && !up.Message.Text.Split(' ').Any(t => t == "بکنمت") && !up.Message.Text.Split(' ').Any(t => t == "ننتو") && !up.Message.Text.Split(' ').Any(t => t == "خایه") && !up.Message.Text.Split(' ').Any(t => t == "تخمم") && !up.Message.Text.Split(' ').Any(t => t == "ساک بزن"))
                                                                        {
                                                                            StringBuilder sbchat = new StringBuilder();
                                                                            sbchat.AppendLine(up.Message.Text.ToLower());
                                                                            await bot.SendTextMessageAsync(on.User2, sbchat.ToString(), ParseMode.Default, null, false, false, 0, false);
                                                                        }
                                                                        else
                                                                        {
                                                                            StringBuilder sbchat = new StringBuilder();
                                                                            sbchat.AppendLine("کاربر گرامی متن پیام شما حاوی الفاظ رکیک و نامناسب بود به همین دلیل ارسال نشد. لطفا از کلمات مناسب استفاده کنید و به دیگران بی احترامی نکنید.");
                                                                            await bot.SendTextMessageAsync(chatId, sbchat.ToString(), ParseMode.Default, null, false, false, 0, false);
                                                                        }
                                                                    }
                                                                    else if (up.Message.Type == MessageType.Photo)
                                                                    {
                                                                        await bot.SendPhotoAsync(on.User2, up.Message.Photo[up.Message.Photo.Count() - 1].FileId);

                                                                    }
                                                                    else if (up.Message.Type == MessageType.Video)
                                                                    {
                                                                        if (up.Message.Video.Duration < 61)
                                                                        {
                                                                            await bot.SendVideoAsync(on.User2, up.Message.Video.FileId);
                                                                        }
                                                                        else
                                                                        {
                                                                            StringBuilder sb = new StringBuilder();
                                                                            sb.AppendLine("<b>کاربر گرامی ، حجم فایلی که ارسال کرده اید بیشتر از حد مجاز است لطفا از ویدئو های کمتر از 1 دقیقه استفاده کنید.</b>");
                                                                            await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                                                        }

                                                                    }
                                                                    else if (up.Message.Type == MessageType.Sticker)
                                                                    {
                                                                        await bot.SendStickerAsync(on.User2, up.Message.Sticker.FileId);
                                                                    }
                                                                    else if (up.Message.Type == MessageType.Document)
                                                                    {
                                                                        await bot.SendDocumentAsync(on.User2, up.Message.Document.FileId);
                                                                    }
                                                                    else if (up.Message.Type == MessageType.Voice)
                                                                    {
                                                                        await bot.SendVoiceAsync(on.User2, up.Message.Voice.FileId);
                                                                    }
                                                                    else if (up.Message.Type == MessageType.Audio)
                                                                    {
                                                                        await bot.SendAudioAsync(on.User2, up.Message.Audio.FileId);
                                                                    }
                                                                    else if (up.Message.Type == MessageType.VideoNote)
                                                                    {
                                                                        await bot.SendVideoNoteAsync(on.User2, up.Message.VideoNote.FileId);
                                                                    }
                                                                }
                                                                catch
                                                                {
                                                                    StringBuilder sbchat = new StringBuilder();
                                                                    sbchat.AppendLine("<b>کاربر گرامی اشکالی در ارتباط بوجود آمده و ارسال پیام امکان پذیر نیست. ممکن است دلیل اختلال بوجود آمده بلاک کردن ربات از سمت کاربر مقابل باشد </b>");
                                                                    await bot.SendTextMessageAsync(up.CallbackQuery.From.Id, sbchat.ToString(), ParseMode.Html, null, false, false, 0, false);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (per.CommandName == "CancellKeyboard")
                                                    {
                                                        StringBuilder sbRepeat = new StringBuilder();
                                                        sbRepeat.AppendLine("<b>یه هم صحبت برات پیدا میکنیم\U0001F50D<b>");
                                                        sbRepeat.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                                                        CommandCheck(chatId, sbRepeat);
                                                    }
                                                    else if (per.CommandName == "ComeBackKeyboard")
                                                    {
                                                        var friends = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId);



                                                        if (!friends.Any(f => f.P2Chatid == per.LastUser))
                                                        {
                                                            if (!friends.Any(f => f.name == up.Message.Text))
                                                            {
                                                                FriendsList fr = new FriendsList()
                                                                {
                                                                    P1Chatid = chatId,
                                                                    PersonId = per.PersonId,
                                                                    P2Chatid = per.LastUser,
                                                                    name = up.Message.Text
                                                                };
                                                                _friendRepository.InsertFriend(fr);
                                                                _friendRepository.Save();
                                                                per.CommandName = "SearchKeyboard";
                                                                _personRepository.UpdatePerson(per);
                                                                _personRepository.Save();
                                                                StringBuilder sbtext = new StringBuilder();
                                                                sbtext.AppendLine(
                                                                    "<b>کاربر با موفقیت به لیست دوستات اضافه شد\U00002705</b>");
                                                                sbtext.AppendLine("<b>خب حالا چه کاری برات انجام بدم؟</b>");
                                                                await bot.SendTextMessageAsync(chatId,
                                                                    sbtext.ToString(), ParseMode.Html, null, false,
                                                                    false, 0, false, SearchKeyboard);

                                                            }
                                                            else
                                                            {
                                                                StringBuilder sbtext = new StringBuilder();
                                                                sbtext.AppendLine("<b>کاربر گرامی متاسفانه این اسم رو قبلا برای دوست دیگه ای انتخاب کردی لطفا یه اسم دیگه مشخض کن\U0001F64F</b>");
                                                                await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, null, false, false, 0, false, ComeBackKeyboard);
                                                            }
                                                        }
                                                        else
                                                        {

                                                            foreach (FriendsList fr in friends)
                                                            {
                                                                if (fr.P2Chatid == per.LastUser)
                                                                {
                                                                    per.CommandName = "SearchKeyboard";
                                                                    _personRepository.UpdatePerson(per);
                                                                    _personRepository.Save();
                                                                    fr.name = up.Message.Text;
                                                                    _friendRepository.UpdateFriend(fr);
                                                                    _friendRepository.Save();
                                                                    StringBuilder sbtext = new StringBuilder();
                                                                    sbtext.AppendLine("<b>نام دوستت با موفقیت تغییر کرد\U00002705</b>");
                                                                    await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                                                                }
                                                            }
                                                        }


                                                    }
                                                    else
                                                    {
                                                        if (command != null)
                                                        {
                                                            StringBuilder sbRepeat = new StringBuilder();
                                                            sbRepeat.AppendLine("<b>متوجه نشدم!</b>");
                                                            sbRepeat.AppendLine("<b>لطفا از گزینه های زیر استفاده کنید</b>");
                                                            CommandCheck(chatId, sbRepeat);
                                                        }

                                                    }


                                                }
                                            }
                                        }
                                    }
                                }
                                //}
                                //else
                                //{
                                //    var person2 = _personRepository.GetAllPerson();
                                //    if (person2.Any(p => p.chatid == chatId))
                                //    {
                                //        foreach (var per in person2)
                                //        {
                                //            if (per.chatid == chatId)
                                //            {
                                //                if (per.CommandName == "StopKeyboard")
                                //                {
                                //                    if (command == "اتمام صحبت")
                                //                    {
                                //                        per.CommandName = "BeCancelledKeyboard";
                                //                        _personRepository.UpdatePerson(per);
                                //                        _personRepository.Save();
                                //                        StringBuilder sbBC = new StringBuilder();
                                //                        sbBC.AppendLine("<b>آیا مطمئنی میخوای صحبت رو ببندی؟\U0001F628</b>");
                                //                        await bot.SendTextMessageAsync(chatId, sbBC.ToString(), ParseMode.Html, null, false, false, 0, false, BeCancelledKeyboard);
                                //                    }
                                //                    else
                                //                    {
                                //                        var online2 = _onlinesRepository.GetAllOnlines();
                                //                        if (online2.Any(o => o.PersonId == per.PersonId))
                                //                        {
                                //                            foreach (Onlines on in online2)
                                //                            {
                                //                                if (on.PersonId == per.PersonId)
                                //                                {
                                //                                    if (up.Message.Type == MessageType.Text)
                                //                                    {
                                //                                        StringBuilder sbchat = new StringBuilder();
                                //                                        sbchat.AppendLine(up.Message.Text.ToLower());
                                //                                        await bot.SendTextMessageAsync(on.User2, sbchat.ToString(), ParseMode.Default, null, false, false, 0, false);
                                //                                    }
                                //                                    else if (up.Message.Type == MessageType.Photo)
                                //                                    {
                                //                                        await bot.SendPhotoAsync(on.User2, up.Message.Photo[up.Message.Photo.Count() - 1].FileId);

                                //                                    }
                                //                                    else if (up.Message.Type == MessageType.Video)
                                //                                    {
                                //                                        if (up.Message.Video.Duration < 61)
                                //                                        {
                                //                                            await bot.SendVideoAsync(on.User2, up.Message.Video.FileId);
                                //                                        }
                                //                                        else
                                //                                        {
                                //                                            StringBuilder sb = new StringBuilder();
                                //                                            sb.AppendLine("<b>کاربر گرامی ، حجم فایلی که ارسال کرده اید بیشتر از حد مجاز است لطفا از ویدئو های کمتر از 1 دقیقه استفاده کنید.</b>");
                                //                                            await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);

                                //                                        }

                                //                                    }
                                //                                    else if (up.Message.Type == MessageType.Sticker)
                                //                                    {
                                //                                        await bot.SendStickerAsync(on.User2, up.Message.Sticker.FileId);
                                //                                    }
                                //                                    else if (up.Message.Type == MessageType.Document)
                                //                                    {
                                //                                        await bot.SendDocumentAsync(on.User2, up.Message.Document.FileId);
                                //                                    }
                                //                                    else if (up.Message.Type == MessageType.Voice)
                                //                                    {
                                //                                        await bot.SendVoiceAsync(on.User2, up.Message.Voice.FileId);
                                //                                    }
                                //                                    else if (up.Message.Type == MessageType.Audio)
                                //                                    {
                                //                                        await bot.SendAudioAsync(on.User2, up.Message.Audio.FileId);
                                //                                    }
                                //                                    else if (up.Message.Type == MessageType.VideoNote)
                                //                                    {
                                //                                        await bot.SendVideoNoteAsync(on.User2, up.Message.VideoNote.FileId);
                                //                                    }
                                //                                }
                                //                            }
                                //                        }
                                //                    }
                                //                }
                                //                else if (per.CommandName == "BeCancelledKeyboard")
                                //                {
                                //                    if (command == "اتمام")
                                //                    {
                                //                        per.CommandName = "AfterCancelledKeyboard";
                                //                        _personRepository.UpdatePerson(per);
                                //                        _personRepository.Save();
                                //                        var online2 = _onlinesRepository.GetAllOnlines();
                                //                        if (online2.Any(o => o.PersonId == per.PersonId))
                                //                        {
                                //                            foreach (Onlines o in online2)
                                //                            {
                                //                                if (o.PersonId == per.PersonId)
                                //                                {
                                //                                    _onlinesRepository.DeleteOnline(o.OnlineID);
                                //                                    _onlinesRepository.Save();
                                //                                    StringBuilder sbABC = new StringBuilder();
                                //                                    sbABC.AppendLine("<b>صحبت رو قطع کردی حالا چه کاری برات انجام بدم؟\U0001F606</b>");
                                //                                    await bot.SendTextMessageAsync(chatId, sbABC.ToString(), ParseMode.Html, null, false, false, 0, false, AfterCancelledKeyboard);
                                //                                }
                                //                                if (o.User2 == from.Id)
                                //                                {
                                //                                    per.LastUser = o.Person.chatid;
                                //                                    _personRepository.UpdatePerson(per);
                                //                                    _personRepository.Save();
                                //                                    o.Person.CommandName = "AfterCancelledKeyboard";
                                //                                    o.Person.LastUser = chatId;
                                //                                    _onlinesRepository.UpdateOnline(o);
                                //                                    _onlinesRepository.Save();
                                //                                    _onlinesRepository.DeleteOnline(o.OnlineID);
                                //                                    _onlinesRepository.Save();
                                //                                    StringBuilder sbABC2 = new StringBuilder();
                                //                                    sbABC2.AppendLine("<b>کاربر از چت خارج شد حالا چه کاری برات انجام بدم؟</b>");
                                //                                    await bot.SendTextMessageAsync(o.chatid, sbABC2.ToString(), ParseMode.Html, null, false, false, 0, false, AfterCancelledKeyboard);
                                //                                }
                                //                            }
                                //                        }
                                //                    }
                                //                    else if (command == "ادامه صحبت")
                                //                    {
                                //                        StringBuilder sbABC3 = new StringBuilder();
                                //                        sbABC3.AppendLine("<b>شما در حال چت با هم صحبت خود هستید\U0001F60A</b>");
                                //                        await bot.SendTextMessageAsync(chatId, sbABC3.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);
                                //                        per.CommandName = "StopKeyboard";
                                //                        _personRepository.UpdatePerson(per);
                                //                        _personRepository.Save();
                                //                    }
                                //                    else
                                //                    {
                                //                        StringBuilder sb = new StringBuilder();
                                //                        sb.AppendLine("<b>متوجه نشدم!</b>");
                                //                        sb.AppendLine("<b>لطفا از گزینه های زیر استفاده کنید</b>");
                                //                        await bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, null, false, false, 0, false);

                                //                    }
                                //                }
                                //                else if (per.CommandName != "StopKeyboard" && per.CommandName != "BeCancelledKeyboard")
                                //                {
                                //                    var channelID = bot.GetChatAsync(-1001524352586).Result.Username.ToString();
                                //                    StringBuilder sbRepeat = new StringBuilder();
                                //                    sbRepeat.AppendLine("<b>شما هنوز عضو کانال " + "@" + bot.GetChatAsync(-1001524352586).Result.Username + " نشده اید.</b>");
                                //                    sbRepeat.AppendLine("<b>لطفا ابتدا در کانال عضو شده و سپس روی /start بزنید.</b>");
                                //                    bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html);
                                //                }
                                //            }
                                //        }
                                //    }

                                //}
                            }


                        }


                    }
                    catch
                    {

                    }
                }

            }

        }

        private async Task WhichKeyboard(Update up, IEnumerable<Onlines> online, long chatId, Person per)
        {
            if (per.CommandName == "CancellKeyboard")
            {
                StringBuilder sbRepeat = new StringBuilder();
                sbRepeat.AppendLine("<b>داریم یه هم صحبت برات پیدا میکنیم\U0001F50D</b>");
                sbRepeat.AppendLine("<b>پیدا کردن هم صحبت ممکنه چند دقیقه ای زمان ببره ، لطفا صبور باشید.</b>");
                CommandCheck(chatId, sbRepeat);
            }
            else if (per.CommandName == "StopKeyboard")
            {
                if (online.Any(o => o.PersonId == per.PersonId))
                {
                    foreach (Onlines on in online)
                    {
                        if (on.PersonId == per.PersonId)
                        {
                            StringBuilder sbchat = new StringBuilder();
                            sbchat.AppendLine(up.Message.Text.ToLower());
                            await bot.SendTextMessageAsync(on.User2, sbchat.ToString(), ParseMode.Default, null, false, false, 0, false);
                        }
                    }
                }

            }
            else if (per.CommandName == "ComeBackKeyboard")
            {
                var friends = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId);



                if (!friends.Any(f => f.P2Chatid == per.LastUser))
                {
                    if (!friends.Any(f => f.name == up.Message.Text))
                    {
                        if (friends.Count() == 1)
                        {
                            var friend = _friendRepository.GetAllFriendList().Where(f => f.P1Chatid == chatId).SingleOrDefault();
                            friend.P2Chatid = per.LastUser;
                            friend.name = up.Message.Text;
                            _friendRepository.UpdateFriend(friend);
                            _friendRepository.Save();
                            per.CommandName = "SearchKeyboard";
                            _personRepository.UpdatePerson(per);
                            _personRepository.Save();
                            StringBuilder sbtext = new StringBuilder();
                            sbtext.AppendLine(
                                "<b>کاربر با موفقیت به لیست دوستات اضافه شد\U00002705</b>");
                            sbtext.AppendLine("<b>خب حالا چه کاری برات انجام بدم؟</b>");
                            await bot.SendTextMessageAsync(chatId,
                                sbtext.ToString(), ParseMode.Html, null, false,
                                false, 0, false, SearchKeyboard);

                        }
                        else
                        {
                            FriendsList fr = new FriendsList()
                            {
                                P1Chatid = chatId,
                                P2Chatid = per.LastUser,
                                name = up.Message.Text,
                                PersonId = per.PersonId
                            };
                            _friendRepository.InsertFriend(fr);
                            _friendRepository.Save();
                            per.CommandName = "SearchKeyboard";
                            _personRepository.UpdatePerson(per);
                            _personRepository.Save();
                            StringBuilder sbtext = new StringBuilder();
                            sbtext.AppendLine(
                                "<b>کاربر با موفقیت به لیست دوستات اضافه شد\U00002705</b>");
                            sbtext.AppendLine("<b>خب حالا چه کاری برات انجام بدم؟</b>");
                            await bot.SendTextMessageAsync(chatId,
                                sbtext.ToString(), ParseMode.Html, null, false,
                                false, 0, false, SearchKeyboard);

                        }
                    }
                    else
                    {
                        StringBuilder sbtext = new StringBuilder();
                        sbtext.AppendLine("<b>کاربر گرامی متاسفانه این اسم رو قبلا برای دوست دیگه ای انتخاب کردی لطفا یه اسم دیگه مشخض کن\U0001F64F</b>");
                        await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, null, false, false, 0, false, ComeBackKeyboard);

                    }
                }
                else
                {

                    foreach (FriendsList fr in friends)
                    {
                        if (fr.P2Chatid == per.LastUser)
                        {
                            per.CommandName = "SearchKeyboard";
                            _personRepository.UpdatePerson(per);
                            _personRepository.Save();
                            fr.name = up.Message.Text;
                            _friendRepository.UpdateFriend(fr);
                            _friendRepository.Save();
                            StringBuilder sbtext = new StringBuilder();
                            sbtext.AppendLine("<b>نام دوستت با موفقیت تغییر کرد\U00002705</b>");
                            await bot.SendTextMessageAsync(chatId, sbtext.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);

                        }
                    }
                }


            }
            else
            {
                StringBuilder sbRepeat = new StringBuilder();
                sbRepeat.AppendLine("<b>متوجه نشدم!</b>");
                sbRepeat.AppendLine("<b>لطفا از گزینه های زیر استفاده کنید</b>");
                CommandCheck(chatId, sbRepeat);
            }
        }

        void RefreshDG()
        {

            var p = _personRepository.GetAllPerson();

            var o = _onlinesRepository.GetAllOnlines();
            foreach (var per in p)
            {
                if (!DG.Rows.Cast<DataGridViewRow>().Any(d => d.Cells[1].Value.ToString() == per.chatid.ToString()))
                {
                    if (o.Any(oo => oo.chatid == per.chatid))
                    {
                        foreach (var on in o)
                        {
                            if (on.chatid == per.chatid)
                            {
                                DG.Rows.Add(per.PersonId, per.chatid, per.Username, on.User2);
                            }
                        }
                    }
                    else
                    {
                        DG.Rows.Add(per.PersonId, per.chatid, per.Username, "-");
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in DG.Rows)
                    {
                        if (item.Cells[1].Value.ToString() == per.chatid.ToString())
                        {
                            if (o.Any(oo => oo.chatid == per.chatid))
                            {
                                foreach (var on in o)
                                {
                                    if (on.chatid == per.chatid)
                                    {
                                        item.Cells[3].Value = on.User2;
                                    }
                                }
                            }
                            else
                            {
                                item.Cells[3].Value = "-";
                            }

                        }
                    }
                }
            }
            label2.Text += " " + p.Count();
        }

        private void CommandCheck(long chatId, StringBuilder sbRepeat)
        {
            var person = _personRepository.GetAllPerson();
            foreach (Person per in person)
            {
                if (per.chatid == chatId)
                {
                    if (per.CommandName == "mainKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, mainKeyboard);
                    }
                    if (per.CommandName == "GenderKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, GenderKeyboard);
                    }
                    if (per.CommandName == "AgeKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, AgeKeyboard);
                    }
                    if (per.CommandName == "CityKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, CityKeyboard);
                    }
                    if (per.CommandName == "SearchKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);
                    }
                    if (per.CommandName == "StopKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);
                    }
                    if (per.CommandName == "CancellKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, CancellKeyboard);
                    }
                    if (per.CommandName == "BeCancelledKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, BeCancelledKeyboard);
                    }
                    if (per.CommandName == "AfterCancelledKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, AfterCancelledKeyboard);
                    }
                    if (per.CommandName == "GenderFilterKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, GenderFilterKeyboard);
                    }
                    if (per.CommandName == "CityFilterKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, CityFilterKeyboard);
                    }
                    if (per.CommandName == "AgeFilterKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, AgeFilterKeyboard);
                    }
                    if (per.CommandName == "BlockKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, BlockKeyboard);
                    }
                    if (per.CommandName == "ComeBackKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, ComeBackKeyboard);
                    }
                    if (per.CommandName == "DeleteFriendKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, DeleteFriendKeyboard);
                    }
                    if (per.CommandName == "QuitKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, QuitKeyboard);
                    }

                }
            }

        }

        private void BlockCheck(long chatId, StringBuilder sbRepeat)
        {
            var person = _personRepository.GetAllPerson();
            foreach (Person per in person)
            {
                if (per.chatid == chatId)
                {
                    if (per.Block_Ckeck == "mainKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, mainKeyboard);
                        per.CommandName = "mainKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "GenderKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, GenderKeyboard);
                        per.CommandName = "GenderKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "AgeKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, AgeKeyboard);
                        per.CommandName = "AgeKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "CityKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, CityKeyboard);
                        per.CommandName = "CityKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "SearchKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, SearchKeyboard);
                        per.CommandName = "SearchKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "StopKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, StopKeyboard);
                        per.CommandName = "StopKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "CancellKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, CancellKeyboard);
                        per.CommandName = "CancellKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "BeCancelledKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, BeCancelledKeyboard);
                        per.CommandName = "BeCancelledKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "AfterCancelledKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, AfterCancelledKeyboard);
                        per.CommandName = "AfterCancelledKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "GenderFilterKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, GenderFilterKeyboard);
                        per.CommandName = "GenderFilterKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "CityFilterKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, CityFilterKeyboard);
                        per.CommandName = "CityFilterKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "AgeFilterKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, AgeFilterKeyboard);
                        per.CommandName = "AgeFilterKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "BlockKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, BlockKeyboard);
                        per.CommandName = "BlockKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "ComeBackKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, ComeBackKeyboard);
                        per.CommandName = "ComeBackKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "DeleteFriendKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, DeleteFriendKeyboard);
                        per.CommandName = "DeleteFriendKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }
                    if (per.Block_Ckeck == "QuitKeyboard")
                    {
                        bot.SendTextMessageAsync(chatId, sbRepeat.ToString(), ParseMode.Html, null, false, false, 0, false, QuitKeyboard);
                        per.CommandName = "QuitKeyboard";
                        _personRepository.UpdatePerson(per);
                        _personRepository.Save();
                    }


                }
            }

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            botThread.Abort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDG();
        }

        private void btnSentmsg_Click(object sender, EventArgs e)
        {
            if (txtText.Text != "")
            {

                var p = _personRepository.GetAllPerson();

                if (txtPhoto.Text == "")
                {
                    foreach (var per in p)
                    {
                        bot.SendTextMessageAsync(per.chatid, txtText.Text, ParseMode.Html, null, false, false, 0, false);
                    }
                }
                else
                {


                    using (var stream = File.Open(txtPhoto.Text, FileMode.Open))
                    {
                        foreach (var per in p)
                        {
                            bot.SendPhotoAsync(per.chatid, stream, txtText.Text);
                        }
                    }


                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtPhoto.Text = open.FileName;
            }
        }
    }
}
