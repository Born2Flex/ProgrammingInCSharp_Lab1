using System.Windows;
using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.Utils.Tools.Controls
{
    public partial class HorizontalTextBlockWithCaption : UserControl
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
            nameof(Text),
            typeof(string),
            typeof(HorizontalTextBlockWithCaption),
            new PropertyMetadata(null)
        );
    
        public HorizontalTextBlockWithCaption()
        {
            InitializeComponent();
        }
    }
}