using System.Windows.Controls;
using KMA.ProgrammingInCSharp.ViewModels;

namespace KMA.ProgrammingInCSharp.Views
{
    public partial class BirthInfoView : UserControl
    {
        private BirthInfoViewModel _birthInfoViewModel;
        public BirthInfoView()
        {
            InitializeComponent();
            DataContext = _birthInfoViewModel = new BirthInfoViewModel();
        }
    }
}