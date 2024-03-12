using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.Utils.Tools.Controls;

public partial class TextBlockWithCaption : UserControl
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
    
    public string Content
    {
        get
        {
            return TbContent.Text;
        }
        set
        {
            TbContent.Text = value;
        }
    }
    
    public TextBlockWithCaption()
    {
        InitializeComponent();
    }
}