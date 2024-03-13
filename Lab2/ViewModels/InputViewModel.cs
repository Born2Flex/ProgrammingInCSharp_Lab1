using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using KMA.ProgrammingInCSharp.Navigation;
using KMA.ProgrammingInCSharp.Utils;
using KMA.ProgrammingInCSharp.Utils.Tools;

namespace KMA.ProgrammingInCSharp.ViewModels;

public class InputViewModel : INavigatable<BaseNavigationTypes>, INotifyPropertyChanged
{
    #region Fields
    
    private RelayCommand<object> _exitCommand;
    private RelayCommand<object> _proceedCommand;
    private Action _gotoResults;
    
    private string _firstName;
    private string _lastName;
    private string _email;
    private DateTime _birthdayDate = DateTime.MinValue;
    
    #endregion

    #region Properties

    public RelayCommand<object> ExitCommand
    {
        get { return _exitCommand ??= new RelayCommand<object>(_ => Environment.Exit(0)); }
    }
    
    public RelayCommand<object> ProceedCommand
    {
        get { return _proceedCommand ??= new RelayCommand<object>(_ => ProceedInput(), _ => CanExecute()); }
    }
    
    private void ProceedInput()
    {
        if (DateUtils.IsValidBirthdayDate(BirthdayDate))
        {
            // SetBirthInfo(DateUtils.YearsDiff(BirthdayDate, DateTime.Today),
            //     DateUtils.GetSunSign(BirthdayDate),
            //     DateUtils.GetChineseZodiacSign(BirthdayDate));
            if (DateUtils.TodayIsBirthday(BirthdayDate))
            {
                ShowBirthdayMessage();
            }
        }
        else
        {
           // SetBirthInfo(null, String.Empty, String.Empty);
            ShowInvalidDateMessage();
        }
        _gotoResults.Invoke();
    }

    public string FirstName
    {
        get { return _firstName; }
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }
    
    public string LastName
    {
        get { return _lastName; }
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }
    
    public string Email
    {
        get { return _email; }
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
    
    public DateTime BirthdayDate
    {
        get { return _birthdayDate; }
        set
        {
            _birthdayDate = value;
            OnPropertyChanged(nameof(BirthdayDate));
        }
    }
    
    #endregion
    
    public InputViewModel(Action gotoResults)
    {
        _gotoResults = gotoResults;
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

    
    
    // private void ProcessData(object obj)
    // {
    //     
    // }

    private bool CanExecute()
    {
        return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Email);
        // return true;
    }
    
    public BaseNavigationTypes ViewType
    {
        get { return BaseNavigationTypes.InputData; }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}