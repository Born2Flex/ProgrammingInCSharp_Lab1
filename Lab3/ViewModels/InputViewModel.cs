using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KMA.ProgrammingInCSharp.Models;
using KMA.ProgrammingInCSharp.Navigation;
using KMA.ProgrammingInCSharp.services;
using KMA.ProgrammingInCSharp.Utils;
using KMA.ProgrammingInCSharp.Utils.Exceptions;
using KMA.ProgrammingInCSharp.Utils.Tools;

namespace KMA.ProgrammingInCSharp.ViewModels
{
    class InputViewModel : INavigatable<BaseNavigationTypes>, INotifyPropertyChanged
    {
        #region Fields
    
        private RelayCommand<object> _exitCommand;
        private RelayCommand<object> _proceedCommand;
        private Action _gotoResults;
    
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate = DateTime.Today;
    
        private bool _isEnabled = true;

        #endregion

        #region Properties

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
    
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }
    
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }
    
        public RelayCommand<object> ExitCommand
        {
            get { return _exitCommand ??= new RelayCommand<object>(_ => Environment.Exit(0)); }
        }
    
        public RelayCommand<object> ProceedCommand
        {
            get { return _proceedCommand ??= new RelayCommand<object>(_ => ProceedInput(), _ => CanExecute()); }
        }
    
        #endregion
    
        public InputViewModel(Action gotoResults)
        {
            _gotoResults = gotoResults;
        }
    
        private async void ProceedInput()
        {
            try
            {
                IsEnabled = false;
                await Task.Run(() =>
                {
                    PersonService.Person = new Person(FirstName, LastName, Email, BirthDate);
                    if (DateUtils.TodayIsBirthday(BirthDate))
                    {
                        ShowBirthdayMessage();
                    }
                    _gotoResults.Invoke();
                });
            }
            catch (InvalidEmailException e)
            {
                Console.WriteLine($"Email validation exception occured: {e.Message}");
                ShowExceptionMessage(e.Message);
            }
            catch (Exception e) when (e is BirthDateInPastException or BirthDateInFutureException)
            {
                Console.WriteLine($"Date validation exception occured: {e.Message}.");
                ShowExceptionMessage(e.Message);
            }
            finally
            {
                IsEnabled = true;
            }
        }
    
        private void ShowBirthdayMessage()
        {
            MessageBox.Show("Happy Birthday! 🎉🎉🎉",
                "Congratulations",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void ShowExceptionMessage(string message)
        {
            MessageBox.Show(message,"Incorrect input",MessageBoxButton.OK,MessageBoxImage.Warning);
        }

        private bool CanExecute()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Email);
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
}