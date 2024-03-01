using System;
using System.ComponentModel;
using System.Windows;
using KMA.ProgrammingInCSharp.Models;
using KMA.ProgrammingInCSharp.Utils;
using KMA.ProgrammingInCSharp.Utils.Tools;

namespace KMA.ProgrammingInCSharp.ViewModels
{
    public class BirthInfoViewModel : INotifyPropertyChanged
    {
        #region Fields

        private const int MaxAge = 135;
        private BirthInfoModel _birthInfoModel = new BirthInfoModel();
        private RelayCommand<object> _calculateCommand;
        private RelayCommand<object> _exitCommand;

        #endregion

        #region Properties

        public DateTime BirthdayDate
        {
            get { return _birthInfoModel.BirthDate; }
            set
            {
                _birthInfoModel.BirthDate = value;
                OnPropertyChanged(nameof(BirthdayDate));
            }
        }

        public int? Age
        {
            get { return _birthInfoModel.Age; }
            set
            {
                _birthInfoModel.Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public string WesternZodiacSign
        {
            get { return _birthInfoModel.WesternZodiacSign; }
            set
            {
                _birthInfoModel.WesternZodiacSign = value;
                OnPropertyChanged(nameof(WesternZodiacSign));
            }
        }

        public string ChineseZodiacSign
        {
            get { return _birthInfoModel.ChineseZodiacSign; }
            set
            {
                _birthInfoModel.ChineseZodiacSign = value;
                OnPropertyChanged(nameof(ChineseZodiacSign));
            }
        }

        public RelayCommand<object> CalculateCommand
        {
            get { return _calculateCommand ??= new RelayCommand<object>(_ => CalculateBirthInfo()); }
        }

        public RelayCommand<object> ExitCommand
        {
            get { return _exitCommand ??= new RelayCommand<object>(_ => Environment.Exit(0)); }
        }

        #endregion

        private void CalculateBirthInfo()
        {
            if (IsValidBirthdayDate(BirthdayDate))
            {
                SetBirthInfo(DateUtils.YearsDiff(BirthdayDate, DateTime.Today),
                    DateUtils.GetWesternZodiacSign(BirthdayDate),
                    DateUtils.GetChineseZodiacSign(BirthdayDate));
                if (DateUtils.TodayIsBirthday(BirthdayDate))
                {
                    ShowBirthdayMessage();
                }
            }
            else
            {
                SetBirthInfo(null, String.Empty, String.Empty);
                ShowInvalidDateMessage();
            }
        }

        private void SetBirthInfo(int? age, string westernSign, string chineseSign)
        {
            Age = age;
            WesternZodiacSign = westernSign;
            ChineseZodiacSign = chineseSign;
        }

        private void ShowBirthdayMessage()
        {
            MessageBox.Show("Happy Birthday! 🎉🎉🎉",
                "Congratulations",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void ShowInvalidDateMessage()
        {
            MessageBox.Show(BirthdayDate > DateTime.Today
                    ? "You can't be born in future! Please enter a valid birth date."
                    : "Age could not be more than 135 years!", "Incorrect Date",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        private bool IsValidBirthdayDate(DateTime date)
        {
            return DateUtils.YearsDiff(BirthdayDate, DateTime.Today) <= MaxAge && date.CompareTo(DateTime.Now) < 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}