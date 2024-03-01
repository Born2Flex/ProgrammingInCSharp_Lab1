using System;

namespace KMA.ProgrammingInCSharp.Models
{
    // This class is created for demonstration purposes to represent birth information and usage of MVVM pattern.
    // Similar functionality could be achieved without it.
    public class BirthInfoModel
    {
        #region Fields

        private DateTime _birthDate = DateTime.Today;
        private int? _age;
        private string _chineseZodiacSign = String.Empty;
        private string _westernZodiacSign = String.Empty;

        #endregion

        #region Properties

        public int? Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public string ChineseZodiacSign
        {
            get { return _chineseZodiacSign; }
            set { _chineseZodiacSign = value; }
        }

        public string WesternZodiacSign
        {
            get { return _westernZodiacSign; }
            set { _westernZodiacSign = value; }
        }

        #endregion
    }
}