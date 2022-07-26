using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ComboAttributes
{
    /// <summary>
    /// Interaction logic for SvgLetterSpacing.xaml
    /// </summary>
    public partial class SvgLetterSpacing : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        #endregion

        public SvgLetterSpacing()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "letter-spacing";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
           

        }

      

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgLetterSpacing), new FrameworkPropertyMetadata("none",
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
                typeof(SvgLetterSpacing), new FrameworkPropertyMetadata("none",
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
                typeof(SvgLetterSpacing), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );

        public string LenperCurrentUnit
        {
            get { return (string)GetValue(IsLenperCurrentUnitProperty); }
            set
            {
                SetValue(IsLenperCurrentUnitProperty, value);


            }
        }


        #endregion

        #region methods
        public bool CanAnimate { get; set; } = false;
        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeType = SvgAttributeType.Text;
            at.AttributeValue = "normal";
            at.AttributeName = _labelText;
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
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;

            if (at.AttributeType == SvgAttributeType.Text)
            {
                NormalCheckBox.IsChecked = true;
                LenPerUnitComboBox.Visibility = Visibility.Collapsed;
            }

            if (at.AttributeType == SvgAttributeType.LengthPercent)
            {
                NormalCheckBox.IsChecked = false;
                LenPerUnitComboBox.Visibility = Visibility.Visible;
                LenPerUnitComboBox.SelectedIndex = 2;

                for (int ii = 0; ii < LenPerUnitComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = LenPerUnitComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.SvgUnit)
                    {
                        LenPerUnitComboBox.SelectedIndex = ii;
                        break;
                    }
                }
            }
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            if (NormalCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Text;
            }
            if (NormalCheckBox.IsChecked == false)
            {
               
                at.AttributeType = SvgAttributeType.LengthPercent;
                ComboBoxItem lbi = (LenPerUnitComboBox.SelectedItem as ComboBoxItem);
                at.SvgUnit = lbi.Content.ToString();
            }
            SvgCompositionControl.Context.SaveChanges();

        }

        #endregion

        #region events



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void NormalCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            if (SvgAttributeBlock.Text != "Normal")
            {
                _svgAttributeOriginalValue = SvgAttributeBlock.Text;
            }
            LenPerUnitComboBox.Visibility = Visibility.Collapsed;
            SvgAttributeBlock.Text = "Normal";
          
        }

        private void NormalCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (SvgAttributeBlock.Text == "Normal")
            {
                _svgAttributeLocalValue = _svgAttributeOriginalValue;
            }
            LenPerUnitComboBox.Visibility = Visibility.Visible;
            SvgAttributeBlock.Text = " ";
        }


        private void LenPerUnitComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            LenperCurrentUnit = lbi.Content.ToString();
            this.InvalidateVisual();
        }

    }
}
