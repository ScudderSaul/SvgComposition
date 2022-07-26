using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ComboAttributes
{
    /// <summary>
    /// Interaction logic for SvgRefX.xaml
    /// </summary>
    public partial class SvgRefX : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;

        private List<string> _selectList = new List<string>
        {
            "top", "center", "bottom"
        };
        #endregion

        public SvgRefX()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "refX";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
            SelTip.Text = $"The '{LabelText}' attribute choices";

        }

      

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgRefX), new FrameworkPropertyMetadata("none",
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
                typeof(SvgRefX), new FrameworkPropertyMetadata("none",
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
                typeof(SvgRefX), new FrameworkPropertyMetadata("none",
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
        public bool CanAnimate { get; set; } = true;
        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeType = SvgAttributeType.Value;
            at.AttributeValue = "0";
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
                LenPerCheckBox.IsChecked = false;
                ValCheckBox.IsChecked = false;
                SelectionComboBox.Visibility = Visibility.Visible;
                LenPerUnitComboBox.Visibility = Visibility.Collapsed;

                for (int ii = 0; ii < SelectionComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = SelectionComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.AttributeValue)
                    {
                        SelectionComboBox.SelectedIndex = ii;
                        break;
                    }
                }

                SelectCheckBox.IsChecked = true;
            }

            if (at.AttributeType == SvgAttributeType.LengthPercent)
            {
                SelectCheckBox.IsChecked = false;
                ValCheckBox.IsChecked = false;
                SelectionComboBox.Visibility = Visibility.Visible;
                LenPerUnitComboBox.Visibility = Visibility.Collapsed;

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
                LenPerCheckBox.IsChecked = true;
            }

            if (at.AttributeType == SvgAttributeType.Value)
            {
                SelectCheckBox.IsChecked = false;
                LenPerCheckBox.IsChecked = false;
                ValCheckBox.IsChecked = true;
                SelectionComboBox.Visibility = Visibility.Collapsed;
                LenPerUnitComboBox.Visibility = Visibility.Collapsed;
            }
           

        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            if (SelectCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Text;
            }
            if (LenPerCheckBox.IsChecked == false)
            {
                at.AttributeType = SvgAttributeType.LengthPercent;
            }

            if (ValCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Value;
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

      


        private void SelectCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
         //   SelectCheckBox.IsChecked = false;
            LenPerCheckBox.IsChecked = false;
            ValCheckBox.IsChecked = false;
            SelectionComboBox.Visibility = Visibility.Visible;
            LenPerUnitComboBox.Visibility = Visibility.Collapsed;

        }

        private void LenPerCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            SelectCheckBox.IsChecked = false;
          //  LenPerCheckBox.IsChecked = false;
            ValCheckBox.IsChecked = false;
            SelectionComboBox.Visibility = Visibility.Collapsed;
            LenPerUnitComboBox.Visibility = Visibility.Visible;

        }

        private void ValCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            SelectCheckBox.IsChecked = false;
            LenPerCheckBox.IsChecked = false;
            //   ValCheckBox.IsChecked = false;
            SelectionComboBox.Visibility = Visibility.Collapsed;
            LenPerUnitComboBox.Visibility = Visibility.Collapsed;

        }

        private void LenPerUnitComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            LenperCurrentUnit = lbi.Content.ToString();
            this.InvalidateVisual();
        }

        private void SelectionComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();

            this.InvalidateVisual();
        }

        #endregion
    }
}
