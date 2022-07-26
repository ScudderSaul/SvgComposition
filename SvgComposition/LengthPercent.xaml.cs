using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.VisualStudio.TextManager.Interop;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition
{
    /// <summary>
    /// Interaction logic for LengthPercent.xaml
    /// </summary>
    public partial class LengthPercent : UserControl, INotifyPropertyChanged
    {
        #region fields

        private string _lenperOriginalValue;
        private string _lenperLocalValue;
        private string _labelText;
        private string _lenperLocalUnit;
        private string _lenperOriginalUnit;
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

        private ObservableCollection<string> _svgAttributeUnits = new ObservableCollection<string>(_svgAttributeUnitsText);

        #endregion

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(LengthPercent), new FrameworkPropertyMetadata("none",
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

        public static readonly DependencyProperty IsLenperCurrentValueProperty =
            DependencyProperty.Register(
                "IsLenperCurrentValue",
                typeof(string),
                typeof(LengthPercent), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );

        public string LenperCurrentValue
        {
            get { return (string)GetValue(IsLenperCurrentValueProperty); }
            set
            {
                SetValue(IsLenperCurrentValueProperty, value);
                _lenperLocalValue = value;
                OnPropertyChanged("LenperCurrentValue");
            }
        }

        public static readonly DependencyProperty IsLenperCurrentUnitProperty =
            DependencyProperty.Register(
                "IsLenperCurrentUnit",
                typeof(string),
                typeof(LengthPercent), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );

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

        public LengthPercent()
        {
            InitializeComponent();
            DataContext = "this";
        }

        #region properties



        #endregion

        public bool HasChanged()
        {
            if (_lenperLocalUnit != _lenperOriginalUnit || _lenperLocalValue != _lenperOriginalValue)
            {
                return (true);
            }

            return (false);
        }

        public void Load(SvgAttribute attrib)
        {
            _lenperOriginalValue = attrib.AttributeValue;
            LenperCurrentValue = _lenperOriginalValue;
            _lenperOriginalUnit = attrib.SvgUnit;
            LenperCurrentUnit = _lenperOriginalUnit;
            LabelText = attrib.AttributeName;
        }

        public void Save(SvgAttribute attrib)
        {
            attrib.AttributeValue = LenperCurrentValue;
            attrib.SvgUnit = LenperCurrentUnit;
            // save db
        }


        #region events

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
