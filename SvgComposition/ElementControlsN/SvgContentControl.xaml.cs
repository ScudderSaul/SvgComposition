using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SvgComposition.Model;


namespace SvgComposition.Controls
{
    /// <summary>
    /// Interaction logic for SvgScriptControl.xaml
    /// </summary>
    public partial class SvgScriptControl : UserControl, INotifyPropertyChanged
    {
        private string _svgNodeText;
        public static CodeGeneration ScriptCodeGeneration = new CodeGeneration();

        private SvgElement _top;

        public SvgScriptControl()
        {
            InitializeComponent();
        }

        public SvgElement Top
        {
            get { return (_top); }
            set
            {
                _top = value;
              
                ScriptCodeGeneration.Generate(_top.Id);
                ShowSvgText.Text = ScriptCodeGeneration.Svgtext;
              
               
            }
        }

        public  void Regenerate()
        {
            ScriptCodeGeneration.Regenerate();
            ShowSvgText.Text = ScriptCodeGeneration.Svgtext;
        }


        public string SvgNodeText
        {
            get => _svgNodeText;
            set
            {
                _svgNodeText = value;
                OnPropertyChanged();
            }

        }

        private void ClipboardCopyButton_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(SvgNodeText);
        }

        private void ShowSvgText_OnMouseEnter(object sender, MouseEventArgs e)
        {
            ScriptCodeGeneration.Generate(_top.Id);
            SvgNodeText = ScriptCodeGeneration.Svgtext;
        }

        private void Expander_OnCollapsed(object sender, RoutedEventArgs e)
        {
            ScriptCodeGeneration.Generate(_top.Id);
            SvgNodeText = ScriptCodeGeneration.Svgtext;
            this.InvalidateVisual();
        }

        private void Expander_OnExpanded(object sender, RoutedEventArgs e)
        {
            ScriptCodeGeneration.Generate(_top.Id);
            SvgNodeText = ScriptCodeGeneration.Svgtext;
            this.InvalidateVisual();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
