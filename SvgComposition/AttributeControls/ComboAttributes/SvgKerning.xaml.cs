using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ComboAttributes
{
    /// <summary>
    /// Interaction logic for SvgKerning.xaml
    /// </summary>
    public partial class SvgKerning : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;



        #endregion

        public SvgKerning()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "kerning";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
            SelTip.Text = $"The '{LabelText}' attribute choices";

        }

      

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgKerning), new FrameworkPropertyMetadata("none",
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
                typeof(SvgKerning), new FrameworkPropertyMetadata("none",
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
                typeof(SvgKerning), new FrameworkPropertyMetadata("none",
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
            at.AttributeValue = "auto";
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
                SelectCheckBox.IsChecked = true;
                LenPerUnitComboBox.Visibility = Visibility.Collapsed;
                SelectionComboBox.Visibility = Visibility.Visible;

                for (int ii = 0; ii < SelectionComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = SelectionComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.AttributeValue)
                    {
                        SelectionComboBox.SelectedIndex = ii;
                        break;
                    }
                }
            }

            if (at.AttributeType == SvgAttributeType.LengthPercent)
            {
                SelectCheckBox.IsChecked = false;
                LenPerUnitComboBox.Visibility = Visibility.Visible;
                SelectionComboBox.Visibility = Visibility.Collapsed;

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
            at.AttributeValue = SvgAttributeBlock.Text;
            if (SelectCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Text;
            }
            if (SelectCheckBox.IsChecked == false)
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



        private void SelectCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            LenPerUnitComboBox.Visibility = Visibility.Collapsed;
            SelectionComboBox.Visibility = Visibility.Visible;
            if (SelectionComboBox.SelectedIndex < 0)
            {
                SelectionComboBox.SelectedIndex = 3;
            }
            this.InvalidateVisual();
        }

        private void SelectCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            LenPerUnitComboBox.Visibility = Visibility.Visible;
            SelectionComboBox.Visibility = Visibility.Collapsed;
            if (LenPerUnitComboBox.SelectedIndex < 0)
            {
                LenPerUnitComboBox.SelectedIndex = 2;

            }
            this.InvalidateVisual();
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
