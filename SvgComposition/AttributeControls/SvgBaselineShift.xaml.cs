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

namespace SvgComposition.AttributeControls
{
    /// <summary>
    /// Interaction logic for SvgBaselineShift.xaml
    /// </summary>
    public partial class SvgBaselineShift : UserControl, INotifyPropertyChanged
    {
        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        private string _lenperLocalUnit;

        private static List<string> _albaValues = new List<string>
        {
            "super", "sub",
        };

        private static List<string> _svgAttributeUnitsText = new List<string>
        {
            "em", // The default font size - usually the height of a character.
            "ex", // The height of the character x
            "px", // Pixels
            "pt", // Points (1 / 72 of an inch)
            "pc", // Picas (1 / 6 of an inch)
            "cm", // Centimeters
            "mm", // Millimeters
            "in", // Inches
            "%"
        };

        #endregion

        public SvgBaselineShift()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "baseline-shift";
        }

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgBaselineShift), new FrameworkPropertyMetadata("none",
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
                typeof(SvgBaselineShift), new FrameworkPropertyMetadata("none",
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


        public static readonly DependencyProperty IsLenperCurrentUnitProperty =
            DependencyProperty.Register(
                "IsLenperCurrentUnit",
                typeof(string),
                typeof(SvgBaselineShift), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );

        #endregion

        #region methods


        public string LenperCurrentUnit
        {
            get { return (string)GetValue(IsLenperCurrentUnitProperty); }
            set
            {
                SetValue(IsLenperCurrentUnitProperty, value);
                _lenperLocalUnit = value;
                LenPerUnitComboBox.SelectedItem = _lenperLocalUnit;
                OnPropertyChanged("LenperCurrentUnit");
            }
        }

        public bool CanAnimate { get; set; } = true;

        public void Create(SvgAttribute at)
        {
            at.AttributeType = SvgAttributeType.LengthPercent;
            at.AttributeValue = "0";
            Load(at);
        }

        public void Load(SvgAttribute at)
        {
            if(at.AttributeType == SvgAttributeType.LengthPercent)
            { 
                
            SvgAttributeBlock.IsReadOnly = false;
            SvgAttributeCurrentValue = at.AttributeValue;

        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue;
            at.AttributeType = SvgAttributeType.Value;
        }

        #endregion

        #region events

        private void SvgAttributeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SvgAttributeBlock.IsReadOnly = false;
            SvgAttributeBlock.Text = SvgAttributeComboBox.SelectedItem as string;
            SvgAttributeBlock.IsReadOnly = true;
            this.InvalidateVisual();
            OnPropertyChanged("SvgAttributeCurrentValue");
        }


        private void AutoCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {

            SvgAttributeBlock.Text = "auto";
            SvgAttributeBlock.IsReadOnly = true;
            // AlbaUnitComboBox.Visibility = Visibility.Collapsed;
            InheritCheckBox.IsChecked = false;
            ValueCheckBox.IsChecked = false;
            TextCheckBox.IsChecked = false;
            LenPerUnitComboBox.Visibility = Visibility.Collapsed;
            SvgAttributeComboBox.Visibility = Visibility.Collapsed;
            this.InvalidateVisual();
        }

        private void ValueCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            SvgAttributeComboBox.Visibility = Visibility.Collapsed;
            LenPerUnitComboBox.Visibility = Visibility.Visible;
            InheritCheckBox.IsChecked = false;
            TextCheckBox.IsChecked = false;
            AutoCheckBox.IsChecked = false;
            SvgAttributeBlock.IsReadOnly = false;
          
            this.InvalidateVisual();
            OnPropertyChanged("SvgAttributeCurrentValue");
            // ValueCheckBox.IsChecked = false;
        }

        private void InheritCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            SvgAttributeBlock.IsReadOnly = false;
            SvgAttributeCurrentValue = "inherit";
            SvgAttributeBlock.IsReadOnly = true;
            // AlbaUnitComboBox.Visibility = Visibility.Collapsed;
            // InheritCheckBox.IsChecked = false;
            ValueCheckBox.IsChecked = false;
            TextCheckBox.IsChecked = false;
            AutoCheckBox.IsChecked = false;
            LenPerUnitComboBox.Visibility = Visibility.Collapsed;
            SvgAttributeComboBox.Visibility = Visibility.Collapsed;

            this.InvalidateVisual();
            OnPropertyChanged("SvgAttributeCurrentValue");
        }

        private void TextCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            InheritCheckBox.IsChecked = false;
            ValueCheckBox.IsChecked = false;
          //  TextCheckBox.IsChecked = false;
            AutoCheckBox.IsChecked = false;
            LenPerUnitComboBox.Visibility = Visibility.Collapsed;
            SvgAttributeComboBox.Visibility = Visibility.Visible;
            SvgAttributeBlock.IsReadOnly = false;
          
            SvgAttributeCurrentValue = SvgAttributeComboBox.SelectedItem as string;
            SvgAttributeBlock.IsReadOnly = true;
            this.InvalidateVisual();
            OnPropertyChanged("SvgAttributeCurrentValue");
        }

        private void LenPerUnitComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _lenperLocalUnit = LenPerUnitComboBox.SelectionBoxItem as string;
            SetValue(IsLenperCurrentUnitProperty, _lenperLocalUnit);
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
