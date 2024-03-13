using System;
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
    
    public DateTime PickedDate
    {
        get
        {
            return (DateTime)GetValue(DateProperty);
        }
        set
        {
            SetValue(DateProperty, value);
        }
    }
    
    public static readonly DependencyProperty DateProperty = DependencyProperty.Register
    (
        "PickedDate",
        typeof(DateTime),
        typeof(DatePickerWithCaption),
        new PropertyMetadata(null)
    );
    
    public DatePickerWithCaption()
    {
        InitializeComponent();
    }
}