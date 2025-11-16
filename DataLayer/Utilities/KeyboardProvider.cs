using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace DataLayer.Utilities
{
    // Centralized keyboard provider.
    // Provides the reply-keyboards used across handlers. Keeps construction in one place so tests and updates are easy.
    public class KeyboardProvider
    {
        public ReplyKeyboardMarkup MainKeyboard { get; }
        public ReplyKeyboardMarkup GenderKeyboard { get; }
        public ReplyKeyboardMarkup AgeKeyboard { get; }
        public ReplyKeyboardMarkup CityKeyboard { get; }
        public ReplyKeyboardMarkup SearchKeyboard { get; }
        public ReplyKeyboardMarkup StopKeyboard { get; }
        public ReplyKeyboardMarkup CancellKeyboard { get; }
        public ReplyKeyboardMarkup BlockKeyboard { get; }
        public ReplyKeyboardMarkup QuitKeyboard { get; }
        public ReplyKeyboardMarkup DeleteFriendKeyboard { get; }
        public ReplyKeyboardMarkup ComeBackKeyboard { get; }
        public ReplyKeyboardMarkup AfterCancelledKeyboard { get; }
        public ReplyKeyboardMarkup BeCancelledKeyboard { get; }
        public ReplyKeyboardMarkup AgeFilterKeyboard { get; }
        public ReplyKeyboardMarkup GenderFilterKeyboard { get; }
        public ReplyKeyboardMarkup CityFilterKeyboard { get; }
        public KeyboardProvider()
        {
            MainKeyboard = new ReplyKeyboardMarkup();
            KeyboardButton[] row1 =
            {
                new KeyboardButton("ثبت نام" + " \U0001F63B")
            };
            KeyboardButton[] row2 =
            {
                new KeyboardButton("درباره ربات" + " \U00002764")
            };
            MainKeyboard.Keyboard = new KeyboardButton[][]
            {
                row1,row2
            };
            MainKeyboard.ResizeKeyboard = true;
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
        }

        // Utility: split a flat collection of strings into rows of given size
        private static IEnumerable<IEnumerable<string>> ChunkToRows(IEnumerable<string> items, int rowSize)
        {
            var list = items.ToList();
            for (var i = 0; i < list.Count; i += rowSize)
            {
                yield return list.Skip(i).Take(rowSize);
            }
        }
    }
}