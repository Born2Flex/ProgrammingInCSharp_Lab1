using System.Collections.Generic;
using System.Linq;
using KMA.ProgrammingInCSharp.Navigation;

namespace KMA.ProgrammingInCSharp.ViewModels
{
    class MainWindowViewModel : BaseNavigatableViewModel<BaseNavigationTypes>
    {
        public MainWindowViewModel()
        {
            Navigate(BaseNavigationTypes.DataTable);
        }
        
        protected override INavigatable<BaseNavigationTypes> CreateViewModel(BaseNavigationTypes type)
        {
            switch (type)
            {
                case BaseNavigationTypes.InputData:
                    return new InputViewModel(()=>Navigate(BaseNavigationTypes.DataTable));
                case BaseNavigationTypes.DataTable:
                    return new TableViewModel(() => Navigate(BaseNavigationTypes.InputData));
                default:
                    return null;
            }
        }
    }
}