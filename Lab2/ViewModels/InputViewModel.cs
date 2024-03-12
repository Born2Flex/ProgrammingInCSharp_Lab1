using System;
using System.ComponentModel;
using System.Windows.Input;
using KMA.ProgrammingInCSharp.Navigation;
using KMA.ProgrammingInCSharp.Utils.Tools;

namespace KMA.ProgrammingInCSharp.ViewModels;

public class InputViewModel : INavigatable<BaseNavigationTypes>, INotifyPropertyChanged
{
    #region Fields

    private const int MaxAge = 135;
    private RelayCommand<object> _exitCommand;
    private RelayCommand<object> _proceedCommand;
    private Action _gotoResults;
    
    #endregion

    #region Properties

    public RelayCommand<object> ExitCommand
    {
        get { return _exitCommand ??= new RelayCommand<object>(_ => Environment.Exit(0)); }
    }
    
    public RelayCommand<object> ProceedCommand
    {
        get { return _proceedCommand ??= new RelayCommand<object>(_ => GotoResults()); }
    }
    
    private void GotoResults()
    {
        _gotoResults.Invoke();
    }

    #endregion
    
    public InputViewModel(Action gotoResults)
    {
        _gotoResults = gotoResults;
    }
    
    // private void ProcessData(object obj)
    // {
    //     
    // }
    //
    // private bool CanExecute()
    // {
    //     return true;
    // }
    
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