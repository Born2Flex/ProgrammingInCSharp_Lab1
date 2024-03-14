using System;

namespace KMA.ProgrammingInCSharp.Utils
{
    public static class DateUtils
    {
        private static readonly int MaxAge = 135;
        private static readonly string[] ChineseSigns =
            { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };

        public static int YearsDiff(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) + (((end.Month > start.Month) ||
                                                   ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }

        public static bool TodayIsBirthday(DateTime date)
        {
            return date.Month == DateTime.Today.Month && date.Day == DateTime.Today.Day;
        }
    
        public static bool IsValidBirthdayDate(DateTime date)
        {
            return DateUtils.YearsDiff(date, DateTime.Today) <= MaxAge && date.CompareTo(DateTime.Now) < 0;
        }

        public static string GetChineseZodiacSign(DateTime birthDate)
        {
            return ChineseSigns[(birthDate.Year - 4) % 12];
        }

        public static string GetSunSign(DateTime birthDate)
        {
            int month = birthDate.Month;
            int day = birthDate.Day;
            switch (month)
            {
                case 12: return (day < 22) ? "Sagittarius" : "Capricorn";
                case 1: return (day < 20) ? "Capricorn" : "Aquarius";
                case 2: return (day < 19) ? "Aquarius" : "Pisces";
                case 3: return (day < 21) ? "Pisces" : "Aries";
                case 4: return (day < 20) ? "Aries" : "Taurus";
                case 5: return (day < 21) ? "Taurus" : "Gemini";
                case 6: return (day < 21) ? "Gemini" : "Cancer";
                case 7: return (day < 23) ? "Cancer" : "Leo";
                case 8: return (day < 23) ? "Leo" : "Virgo";
                case 9: return (day < 23) ? "Virgo" : "Libra";
                case 10: return (day < 23) ? "Libra" : "Scorpio";
                case 11: return (day < 22) ? "Scorpio" : "Sagittarius";
                default: return "Unknown";
            }
        }
    }
}