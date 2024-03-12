using System.Windows;
using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.Utils.Tools.Controls
{
    public partial class TextBoxWithCaption : UserControl
    {
        public string Caption
        {
            get
            {
                return TbCaption.Text;
            }
            set
            {
                TbCaption.Text = value;
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register
        (
            "Text",
            typeof(string),
            typeof(TextBoxWithCaption),
            new PropertyMetadata(null)
        );

        public TextBoxWithCaption()
        {
            InitializeComponent();
        }
    }
}