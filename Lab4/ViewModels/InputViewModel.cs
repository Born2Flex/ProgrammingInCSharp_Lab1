using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KMA.ProgrammingInCSharp.Managers;
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
        private Action _gotoTable;
    
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
            get
            {
                return _birthDate;
            }
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
    
        public RelayCommand<object> BackCommand
        {
            get { return new RelayCommand<object>(_ => _gotoTable.Invoke()); }
        }
    
        public RelayCommand<object> ProceedCommand
        {
            get { return _proceedCommand ??= new RelayCommand<object>(_ => ProceedInput(), _ => CanExecute()); }
        }
    
        #endregion
    
        public InputViewModel(Action gotoTable)
        {
            _gotoTable = gotoTable;
            PersonManager.CurrentPersonChanged += UpdatePersonData;
            UpdatePersonData();
        }
    
        private async void ProceedInput()
        {
            try
            {
                IsEnabled = false;
                await Task.Run(() =>
                {
                    Person person = new Person(FirstName, LastName, Email, BirthDate);
                    PersonService personService = new PersonService();
                    if (PersonManager.CurrentPerson == null)
                    {
                        personService.AddPerson(person);
                    }
                    else
                    {
                        personService.EditPerson(PersonManager.CurrentPerson, person);
                    }
                    if (DateUtils.TodayIsBirthday(BirthDate))
                    {
                        ShowBirthdayMessage();
                    }
                    _gotoTable.Invoke();
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

        private void UpdatePersonData()
        {
            if (PersonManager.CurrentPerson != null)
            {
                FirstName = PersonManager.CurrentPerson.FirstName;
                LastName = PersonManager.CurrentPerson.LastName;
                Email = PersonManager.CurrentPerson.Email;
                BirthDate = PersonManager.CurrentPerson.BirthDate;
            }
            else
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            BirthDate = DateTime.Today;
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