using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.LenPerAttributes
{
    /// <summary>
    /// Interaction logic for SvgCx.xaml
    /// </summary>
    public partial class SvgHeight : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {
        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        private string _lenperLocalUnit;
        private ObservableCollection<string> _svgAttributeUnits = new ObservableCollection<string>
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

        public SvgHeight()
        {
            InitializeComponent();
            LabelText = "height";
            DataContext = this;
         
        }

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgHeight), new FrameworkPropertyMetadata("none",
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
                typeof(SvgHeight), new FrameworkPropertyMetadata("none",
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
                typeof(SvgHeight), new FrameworkPropertyMetadata("none",
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

        public bool CanAnimate { get; set; } = true;
       

        #region methods

        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeType = SvgAttributeType.LengthPercent;
            at.AttributeValue = "0";
            at.AttributeName = _labelText;
            at.SvgUnit = "";
            at.SvgElement = ele;
            at.SvgElementId = ele.Id;
            ele.SvgAttributes.Add(at);
            SvgCompositionControl.Context.SvgAttributes.Add(at);
            SvgCompositionControl.Context.SaveChanges();

            Load(at);  
            return (at);
        }

        public void Load(SvgAttribute at)
        {
            SvgAttributeBlock.IsReadOnly = false;
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue; ;
            LenperCurrentUnit = SvgUnitBlock.Text = at.SvgUnit;
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text; ;
            at.AttributeType = SvgAttributeType.LengthPercent;
            at.SvgUnit = LenperCurrentUnit = SvgUnitBlock.Text;
            SvgCompositionControl.Context.SaveChanges();
        }

        #endregion

        #region events


        private void LenPerUnitComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            LenperCurrentUnit = SvgUnitBlock.Text = lbi.Content.ToString();
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
