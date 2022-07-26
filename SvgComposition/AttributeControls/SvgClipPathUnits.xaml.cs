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

namespace SvgComposition.AttributeControls
{
    /// <summary>
    /// Interaction logic for SvgAdditive.xaml
    /// </summary>
    public partial class SvgClipPathUnits : UserControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue = "userSpaceOnUse";
        private string _labelText;

        private static List<string> _albaValues = new List<string>
        {
            "userSpaceOnUse", "objectBoundingBox",
        };
        public SvgClipPathUnits()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "clipPathUnits";
        }

        #endregion

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgClipPathUnits), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );

        public string LabelText
        {
            get { return (string)GetValue(IsLabelTextProperty); }
            set
            {
                SetValue(IsLabelTextProperty, value);
                _labelText = value;
                OnPropertyChanged("LabelText");
            }
        }

        public static readonly DependencyProperty SvgAttributeCurrentValueProperty =
            DependencyProperty.Register(
                "SvgAttributeCurrentValue",
                typeof(string),
                typeof(SvgClipPathUnits), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );



        public string SvgAttributeCurrentValue
        {
            get { return (string)GetValue(SvgAttributeCurrentValueProperty); }
            set
            {
                SetValue(SvgAttributeCurrentValueProperty, value);
                _svgAttributeLocalValue = value;
                OnPropertyChanged("SvgAttributeCurrentValue");
            }
        }

        #region events

        private void SvgAttributeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SvgAttributeBlock.IsReadOnly = false;
            SvgAttributeBlock.Text = SvgAttributeComboBox.SelectedItem as string;
            SvgAttributeBlock.IsReadOnly = true;
            this.InvalidateVisual();
            OnPropertyChanged("SvgAttributeCurrentValue");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
