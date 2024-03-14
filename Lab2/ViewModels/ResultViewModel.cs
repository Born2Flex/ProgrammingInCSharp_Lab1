using System;
using System.ComponentModel;
using KMA.ProgrammingInCSharp.Navigation;
using KMA.ProgrammingInCSharp.services;
using KMA.ProgrammingInCSharp.Utils.Tools;

namespace KMA.ProgrammingInCSharp.ViewModels
{
    class ResultViewModel: INavigatable<BaseNavigationTypes>, INotifyPropertyChanged
    {
        #region Fields

        private RelayCommand<object> _backCommand;
        private Action _gotoInput;

        #endregion

        #region Properties
    
        public string FirstName
        {
            get { return PersonService.Person.FirstName; }
        }
    
        public string LastName
        {
            get { return PersonService.Person.LastName; }
        }
    
        public string Email
        {
            get { return PersonService.Person.Email; }
        }
    
        public DateTime BirthDate
        {
            get { return PersonService.Person.BirthDate; }
        }
    
        public bool IsAdult
        {
            get { return PersonService.Person.IsAdult; }
        }
    
        public string SunSign
        {
            get { return PersonService.Person.SunSign; }
        }
    
        public string ChineseSign
        {
            get { return PersonService.Person.ChineseSign; }
        }
    
        public bool TodayIsBirthday
        {
            get { return PersonService.Person.IsBirthday; }
        }

        public RelayCommand<object> GoBackCommand
        {
            get { return _backCommand ??= new RelayCommand<object>(_ => GotoInput()); }
        }

        #endregion
    
        public ResultViewModel(Action gotoInput)
        {
            _gotoInput = gotoInput;
        }
    
        private void GotoInput()
        {
            _gotoInput.Invoke();
        }
    
        public BaseNavigationTypes ViewType
        {
            get { return BaseNavigationTypes.ShowResult; }
        }
    
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}