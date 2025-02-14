﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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
        
        private RelayCommand<object> _sortByFirstNameCommand;
        private RelayCommand<object> _sortByLastNameCommand;
        private RelayCommand<object> _sortByEmailCommand;
        private RelayCommand<object> _sortByBirthDateCommand;
        private RelayCommand<object> _sortByIsAdultCommand;
        private RelayCommand<object> _sortBySunSignCommand;
        private RelayCommand<object> _sortByChineseSignCommand;
        private RelayCommand<object> _sortByIsBirthdayCommand;

        private RelayCommand<object> _searchCommand;
        private RelayCommand<object> _clearCommand;
        
        private readonly Action _gotoInput;

        private Person? _selectedPerson;
        private string? _selectedOption;
        private string? _searchValue;
        
        private ObservableCollection<Person> _persons;
        private static readonly List<string> Options = new() { "First Name", "Last Name", "Email", "Birth Date", "Is Adult", "Sun Sign", "Chinese Sign", "Is Birthday"};
        private bool _isEnabled = true;
        
        #endregion

        #region CommandsProperties

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
        
        public RelayCommand<object> SortByFirstNameCommand
        {
            get { return _sortByFirstNameCommand ??= new RelayCommand<object>(_ => SortByFirstName()); }
        }
        
        public RelayCommand<object> SortByLastNameCommand
        {
            get { return _sortByLastNameCommand ??= new RelayCommand<object>(_ => SortByLastName()); }
        }
        
        public RelayCommand<object> SortByEmailCommand
        {
            get { return _sortByEmailCommand ??= new RelayCommand<object>(_ => SortByEmail()); }
        }
        
        public RelayCommand<object> SortByBirthDateCommand
        {
            get { return _sortByBirthDateCommand ??= new RelayCommand<object>(_ => SortByBirthDate()); }
        }
        
        public RelayCommand<object> SortByIsAdultCommand
        {
            get { return _sortByIsAdultCommand ??= new RelayCommand<object>(_ => SortByIsAdult()); }
        }
        
        public RelayCommand<object> SortBySunSignCommand
        {
            get { return _sortBySunSignCommand ??= new RelayCommand<object>(_ => SortBySunSign()); }
        }
        
        public RelayCommand<object> SortByChineseSignCommand
        {
            get { return _sortByChineseSignCommand ??= new RelayCommand<object>(_ => SortByChineseSign()); }
        }
        
        public RelayCommand<object> SortByIsBirthdayCommand
        {
            get { return _sortByIsBirthdayCommand ??= new RelayCommand<object>(_ => SortByIsBirthday()); }
        }
        
        public RelayCommand<object> SearchCommand
        {
            get { return _searchCommand ??= new RelayCommand<object>(_ => SearchPersons()); }
        }
        
        public RelayCommand<object> ClearCommand
        {
            get { return _clearCommand ??= new RelayCommand<object>(_ => ClearView()); }
        }

        #endregion

        #region Properties
        
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
        
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
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
        
        private async void DeletePerson()
        {
            IsEnabled = false;
            await Task.Run(() =>
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want\nto delete this person?", 
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    new PersonService().DeletePerson(SelectedPerson);
                }
                SelectedPerson = null;
            });
            IsEnabled = true;
        }

        #region Sorting
        private void SortByFirstName()
        {
            Persons = new ObservableCollection<Person>(Persons.OrderBy(p => p.FirstName));
        }
        
        private void SortByLastName()
        {
            Persons = new ObservableCollection<Person>(Persons.OrderBy(p => p.LastName));
        }

        private void SortByEmail()
        {
            Persons = new ObservableCollection<Person>(Persons.OrderBy(p => p.Email));
        }

        private void SortByBirthDate()
        {
            Persons = new ObservableCollection<Person>(Persons.OrderBy(p => p.BirthDate));
        }

        private void SortByIsAdult()
        {
            Persons = new ObservableCollection<Person>(Persons.OrderBy(p => p.IsAdult));
        }

        private void SortBySunSign()
        {
            Persons = new ObservableCollection<Person>(Persons.OrderBy(p => p.SunSign));
        }

        private void SortByChineseSign()
        {
            Persons = new ObservableCollection<Person>(Persons.OrderBy(p => p.ChineseSign));
        }

        private void SortByIsBirthday()
        {
            Persons = new ObservableCollection<Person>(Persons.OrderBy(p => p.IsBirthday));
        }

        #endregion
        
        private async void SearchPersons()
        {
            if (SelectedOption == null || string.IsNullOrWhiteSpace(SearchValue))
            {
                MessageBox.Show("Please select search option and enter search value", "Search", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            IsEnabled = false;
            await Task.Run(() =>
            {
                try
                {
                    Persons = new ObservableCollection<Person>(new PersonService().SearchPersons(SelectedOption, SearchValue));
                }
                catch (InvalidSearchValueException e)
                {
                    Console.WriteLine($"An error occured while searching. {e.Message}");
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
            IsEnabled = true;
        }
        
        private void ClearView()
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