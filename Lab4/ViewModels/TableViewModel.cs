using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using KMA.ProgrammingInCSharp.Managers;
using KMA.ProgrammingInCSharp.Models;
using KMA.ProgrammingInCSharp.Navigation;
using KMA.ProgrammingInCSharp.Repositories;
using KMA.ProgrammingInCSharp.services;
using KMA.ProgrammingInCSharp.Utils.Exceptions;
using KMA.ProgrammingInCSharp.Utils.Tools;

namespace KMA.ProgrammingInCSharp.ViewModels
{
    class TableViewModel: INavigatable<BaseNavigationTypes>, INotifyPropertyChanged
    {
        #region Fields
        private RelayCommand<object> _exitCommand;
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _editCommand;
        private RelayCommand<object> _deleteCommand;

        private RelayCommand<object> _searchCommand;
        private RelayCommand<object> _clearCommand;
        
        private readonly Action _gotoInput;

        private Person? _selectedPerson;
        private string? _selectedOption;
        private string? _searchValue;
        
        private ObservableCollection<Person> _persons;
        private static readonly List<string> Options = new() { "First Name", "Last Name", "Email", "Birth Date", "Is Adult", "Sun Sign", "Chinese Sign", "Is Birthday"};
        
        #endregion

        #region Properties
        
        public RelayCommand<object> ExitCommand
        {
            get { return _exitCommand ??= new RelayCommand<object>(_ => Environment.Exit(0)); }
        }
        
        public RelayCommand<object> AddCommand
        {
            get { return _addCommand ??= new RelayCommand<object>(_ => AddPerson()); }
        }
        
        public RelayCommand<object> EditCommand
        {
            get { return _editCommand ??= new RelayCommand<object>(_ => EditPerson(), _ => SelectedPerson != null); }
        }

        public RelayCommand<object> DeleteCommand
        {
            get { return _deleteCommand ??= new RelayCommand<object>(_ => DeletePerson(), _ => SelectedPerson != null); }
        }
        
        public RelayCommand<object> SearchCommand
        {
            get { return _searchCommand ??= new RelayCommand<object>(_ => Search()); }
        }
        
        public RelayCommand<object> ClearCommand
        {
            get { return _clearCommand ??= new RelayCommand<object>(_ => Clear()); }
        }
        
        public Person? SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }
        
        public string? SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
            }
        }
        
        public string? SearchValue
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                OnPropertyChanged(nameof(SearchValue));
            }
        }
        
        
        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set
            {
                _persons = value;
                OnPropertyChanged(nameof(Persons));
            }
        }
        
        public List<string> SearchOptions
        {
            get { return Options; }
        }
        #endregion
        
        public TableViewModel(Action gotoInput)
        {
            _gotoInput = gotoInput;
            _persons = new ObservableCollection<Person>(new PersonService().GetAllPersons());
            FileRepository.Instance.DataChanged += UpdatePersons;
        }
        
        private void UpdatePersons()
        {
            Persons = new ObservableCollection<Person>(new PersonService().GetAllPersons());
        }

        private void AddPerson()
        {
            PersonManager.CurrentPerson = null;
            SelectedPerson = null;
            _gotoInput.Invoke();
        }
        
        private void EditPerson()
        {
            PersonManager.CurrentPerson = SelectedPerson;
            SelectedPerson = null;
            _gotoInput.Invoke();
        }
        
        private void DeletePerson()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want\nto delete this person?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                new PersonService().DeletePerson(SelectedPerson);
            }
            SelectedPerson = null;
        }
        
        private void Search()
        {
            if (SelectedOption == null || string.IsNullOrWhiteSpace(SearchValue))
            {
                MessageBox.Show("Please select search option and enter search value", "Search", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                Persons = new ObservableCollection<Person>(
                    new PersonService().SearchPersons(SelectedOption, SearchValue));
            }
            catch (InvalidSearchValueException e)
            {
                Console.WriteLine($"An error occured while searching. {e.Message}");
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void Clear()
        {
            UpdatePersons();
            SelectedOption = null;
            SearchValue = null;
        }
    
        public BaseNavigationTypes ViewType
        {
            get { return BaseNavigationTypes.DataTable; }
        }
    
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}