using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using KMA.ProgrammingInCSharp.Utils;
using KMA.ProgrammingInCSharp.Utils.Exceptions;

namespace KMA.ProgrammingInCSharp.Models
{
    internal class Person
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

        private const string EmailPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        
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
            set
            {
                if (IsValidEmail(value))
                {
                    _email = value;
                }
                else
                {
                    throw new InvalidEmailException($"Invalid email: {value}");
                }
            }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (DateUtils.IsValidBirthdayDate(value))
                {
                    _birthDate = value;
                }
                else
                {
                    if (value > DateTime.Today)
                    {
                        throw new BirthDateInFutureException("You can't be born in future", value);
                    }
                    throw new BirthDateInPastException("Age should be less than 135 years", value);
                }
            }
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
        
        #region Constructors

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;

            _isAdult = DateUtils.YearsDiff(_birthDate, DateTime.Today) > 18;
            _sunSign = DateUtils.GetSunSign(_birthDate);
            _chineseSign = DateUtils.GetChineseZodiacSign(_birthDate);
            _isBirthday = DateUtils.TodayIsBirthday(_birthDate);
        }
        
        [JsonConstructor]
        public Person(string firstName, string lastName, string email, DateTime birthDate, string sunSign, string chineseSign)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            _sunSign = sunSign;
            _chineseSign = chineseSign;
            
            _isAdult = DateUtils.YearsDiff(_birthDate, DateTime.Today) > 18;
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
        
        #endregion

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, EmailPattern);
        }
    }
}