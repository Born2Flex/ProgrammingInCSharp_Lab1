using System;
using KMA.ProgrammingInCSharp.Utils;

namespace KMA.ProgrammingInCSharp.Models
{
    class Person
    {
        #region Fields

        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate;

        private readonly bool _isAdult;
        private readonly string _sunSign;
        private readonly string _chineseSign;
        private readonly bool _isBirthday;

        #endregion

        #region Properties

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public bool IsAdult
        {
            get { return _isAdult; }
        }

        public string SunSign
        {
            get { return _sunSign; }
        }

        public string ChineseSign
        {
            get { return _chineseSign; }
        }

        public bool IsBirthday
        {
            get { return _isBirthday; }
        }

        #endregion

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthDate = birthDate;

            _isAdult = DateUtils.YearsDiff(_birthDate, DateTime.Today) > 18;
            _sunSign = DateUtils.GetSunSign(_birthDate);
            _chineseSign = DateUtils.GetChineseZodiacSign(_birthDate);
            _isBirthday = DateUtils.TodayIsBirthday(_birthDate);
        }

        public Person(string firstName, string lastName, string email) :
            this(firstName, lastName, email, DateTime.Today)
        {
        }

        public Person(string firstName, string lastName, DateTime birthDate) :
            this(firstName, lastName, string.Empty, birthDate)
        {
        }
        
        // Second variant of constructors
        // Which one is better?
        // public Person(string firstName, string lastName, string email, DateTime birthDate):
        //     this(firstName, lastName, birthDate)
        // {
        //     _email = email;
        // }
        //
        // public Person(string firstName, string lastName, string email) :
        //     this(firstName, lastName)
        // {
        //     _email = email;
        // }
        //
        // public Person(string firstName, string lastName, DateTime birthDate) :
        //     this(firstName, lastName)
        // {
        //     _birthDate = birthDate;
        //     _isAdult = DateUtils.YearsDiff(_birthDate, DateTime.Today) > 18;
        //     _sunSign = DateUtils.GetSunSign(_birthDate);
        //     _chineseSign = DateUtils.GetChineseZodiacSign(_birthDate);
        //     _isBirthday = DateUtils.TodayIsBirthday(_birthDate);
        // }
        //
        // public Person(string firstName, string lastName)
        // {
        //     _firstName = firstName;
        //     _lastName = lastName;
        // }
    }
}