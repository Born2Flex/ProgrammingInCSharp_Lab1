using System.Windows;
using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.Utils.Tools.Controls;

public partial class DatePickerWithCaption : UserControl
{
    public string Caption
    {
        get
        {
            return PickerCaption.Text;
        }
        set
        {
            PickerCaption.Text = value;
        }
    }
    
    public DatePickerWithCaption()
    {
        InitializeComponent();
    }
}