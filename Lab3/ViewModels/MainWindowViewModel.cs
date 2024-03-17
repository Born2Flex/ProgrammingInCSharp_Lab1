using System.Collections.Generic;
using System.Linq;
using KMA.ProgrammingInCSharp.Navigation;

namespace KMA.ProgrammingInCSharp.ViewModels
{
    class MainWindowViewModel : BaseNavigatableViewModel<BaseNavigationTypes>
    {
        public MainWindowViewModel()
        {
            Navigate(BaseNavigationTypes.InputData);
        }
        
        protected override INavigatable<BaseNavigationTypes> CreateViewModel(BaseNavigationTypes type)
        {
            switch (type)
            {
                case BaseNavigationTypes.InputData:
                    return new InputViewModel(()=>Navigate(BaseNavigationTypes.ShowResult));
                case BaseNavigationTypes.ShowResult:
                    return new ResultViewModel(() => Navigate(BaseNavigationTypes.InputData));
                default:
                    return null;
            }
        }
    }
}