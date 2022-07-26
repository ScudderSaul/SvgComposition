using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.SelectionAttributes
{
    /// <summary>
    /// Interaction logic for SvgBaselineShift.xaml
    /// </summary>
    public partial class SvgDisplay : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {
        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
   //     private string _lenperLocalUnit;

        private static List<string> _displayBoxValues = new List<string>
        {
            "contents", "none",
        };

         private static List<string> _displayOutsideValues = new List<string>
        {
            "block", "inline", "run-in",
        };

         private static List<string> _displayInsideValues = new List<string>
        {
            "flow", "flow-root", "table", "flex", "grid", "ruby",
        };

        private static List<string> _displayInternalValues = new List<string>
        {
            "table-row-group", "table-header-group", "table-footer-group",  "table-row",
            "table-cell",  
            "table-column-group",  "table-column",  "table-caption",  "ruby-base",
            "ruby-text",  
            "ruby-base-container",  "ruby-text-container",
        };

        private static List<string> _displayLegacyValues = new List<string>
        {
            "inline-block",  "inline-list-item",  "inline-table",
            "inline-flex",  "inline-grid",
        };


        #endregion

        public SvgDisplay()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "display";
         
         
            DisplayInsideComboBox.SelectedIndex = 0;
            DisplayBoxComboBox.SelectedIndex = 0;
            DisplayLegacyComboBox.SelectedIndex = 0;
            DisplayInternalComboBox.SelectedIndex = 0;
            DisplayOutsideCheckBox.IsChecked = true;
            DisplayOutsideComboBox.SelectedIndex = 1;
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";

        }

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgDisplay), new FrameworkPropertyMetadata("none",
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
                typeof(SvgDisplay), new FrameworkPropertyMetadata("none",
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

        #endregion

        #region methods

       

        public bool CanAnimate { get; set; } = false;

        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeType = SvgAttributeType.Text;
            at.AttributeValue = "inline";
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

            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;;
            SvgAttributeBlock.Text = at.AttributeValue;

         
                for (int ii = 0; ii < DisplayOutsideComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = DisplayOutsideComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.AttributeValue)
                    {
                        DisplayOutsideComboBox.SelectedIndex = ii;
                        DisplayOutsideCheckBox.IsChecked = true;
                        break;
                    }
                }


                for (int ii = 0; ii < DisplayInsideComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = DisplayInsideComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.AttributeValue)
                    {
                        DisplayInsideComboBox.SelectedIndex = ii;
                        DisplayInsideCheckBox.IsChecked = true;
                        break;
                    }
                }


                for (int ii = 0; ii < DisplayBoxComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = DisplayBoxComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.AttributeValue)
                    {
                        DisplayBoxComboBox.SelectedIndex = ii;
                        DisplayBoxCheckBox.IsChecked = true;
                        break;
                    }
                }

                for (int ii = 0; ii < DisplayLegacyComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = DisplayLegacyComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.AttributeValue)
                    {
                        DisplayLegacyComboBox.SelectedIndex = ii;
                        DisplayLegacyCheckBox.IsChecked = true;
                       break;
                    }
                }

                for (int ii = 0; ii < DisplayInternalComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = DisplayInternalComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.AttributeValue)
                    {
                        DisplayInternalComboBox.SelectedIndex = ii;
                        DisplayInternalCheckBox.IsChecked = true;
                    break;
                    }
                }

                if (at.AttributeValue == "inherit")
                {
                    InheritCheckBox.IsChecked = true;
                }


                
            
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            at.AttributeType = SvgAttributeType.Text;
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

        private void DisplayOutsideCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
          //  DisplayOutsideCheckBox.IsChecked = false;
            DisplayInsideCheckBox.IsChecked = false;
            DisplayInternalCheckBox.IsChecked = false;
            DisplayBoxCheckBox.IsChecked = false;
            DisplayLegacyCheckBox.IsChecked = false;
            InheritCheckBox.IsChecked = false;


            DisplayOutsideComboBox.Visibility = Visibility.Visible;
            DisplayInsideComboBox.Visibility = Visibility.Collapsed;
            DisplayBoxComboBox.Visibility = Visibility.Collapsed;
            DisplayLegacyComboBox.Visibility = Visibility.Collapsed;
            DisplayInternalComboBox.Visibility = Visibility.Collapsed;

            SvgAttributeBlock.Text = DisplayOutsideComboBox.SelectedItem as string;

            this.InvalidateVisual();

        }

        private void DisplayInsideCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            DisplayOutsideCheckBox.IsChecked = false;
           // DisplayInsideCheckBox.IsChecked = false;
            DisplayInternalCheckBox.IsChecked = false;
            DisplayBoxCheckBox.IsChecked = false;
            DisplayLegacyCheckBox.IsChecked = false;
            InheritCheckBox.IsChecked = false;

            DisplayOutsideComboBox.Visibility = Visibility.Collapsed;
            DisplayInsideComboBox.Visibility = Visibility.Visible;
            DisplayBoxComboBox.Visibility = Visibility.Collapsed;
            DisplayLegacyComboBox.Visibility = Visibility.Collapsed;
            DisplayInternalComboBox.Visibility = Visibility.Collapsed;


            SvgAttributeBlock.Text = DisplayInsideComboBox.SelectedItem as string;

            this.InvalidateVisual();
           

        }

        private void DisplayBoxCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            DisplayOutsideCheckBox.IsChecked = false;
            DisplayInsideCheckBox.IsChecked = false;
            DisplayInternalCheckBox.IsChecked = false;
         //   DisplayBoxCheckBox.IsChecked = false;
            DisplayLegacyCheckBox.IsChecked = false;
            InheritCheckBox.IsChecked = false;

            DisplayOutsideComboBox.Visibility = Visibility.Collapsed;
            DisplayInsideComboBox.Visibility = Visibility.Collapsed;
            DisplayBoxComboBox.Visibility = Visibility.Visible;
            DisplayLegacyComboBox.Visibility = Visibility.Collapsed;
            DisplayInternalComboBox.Visibility = Visibility.Collapsed;


            SvgAttributeBlock.Text = DisplayBoxComboBox.SelectedItem as string;

            this.InvalidateVisual();
       
        }

        private void DisplayLegacyCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            DisplayOutsideCheckBox.IsChecked = false;
            DisplayInsideCheckBox.IsChecked = false;
            DisplayInternalCheckBox.IsChecked = false;
            DisplayBoxCheckBox.IsChecked = false;
          //  DisplayLegacyCheckBox.IsChecked = false;
            InheritCheckBox.IsChecked = false;

            DisplayOutsideComboBox.Visibility = Visibility.Collapsed;
            DisplayInsideComboBox.Visibility = Visibility.Collapsed;
            DisplayBoxComboBox.Visibility = Visibility.Collapsed;
            DisplayLegacyComboBox.Visibility = Visibility.Visible;
            DisplayInternalComboBox.Visibility = Visibility.Collapsed;

         
            SvgAttributeBlock.Text = DisplayLegacyComboBox.SelectedItem as string;
           
            this.InvalidateVisual();
         
        }

        private void InheritCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            DisplayOutsideCheckBox.IsChecked = false;
            DisplayInsideCheckBox.IsChecked = false;
            DisplayInternalCheckBox.IsChecked = false;
            DisplayBoxCheckBox.IsChecked = false;
            DisplayLegacyCheckBox.IsChecked = false;
            //  InheritCheckBox.IsChecked = false;

            DisplayOutsideComboBox.Visibility = Visibility.Collapsed;
            DisplayInsideComboBox.Visibility = Visibility.Collapsed;
            DisplayBoxComboBox.Visibility = Visibility.Collapsed;
            DisplayLegacyComboBox.Visibility = Visibility.Collapsed;
            DisplayInternalComboBox.Visibility = Visibility.Collapsed;

         
            SvgAttributeBlock.Text = "inherit";
        
            this.InvalidateVisual();
          
        }

        private void DisplayInternalCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            DisplayOutsideCheckBox.IsChecked = false;
            DisplayInsideCheckBox.IsChecked = false;
          //  DisplayInternalCheckBox.IsChecked = false;
            DisplayBoxCheckBox.IsChecked = false;
            DisplayLegacyCheckBox.IsChecked = false;
            InheritCheckBox.IsChecked = false;

            DisplayOutsideComboBox.Visibility = Visibility.Collapsed;
            DisplayInsideComboBox.Visibility = Visibility.Collapsed;
            DisplayBoxComboBox.Visibility = Visibility.Collapsed;
            DisplayLegacyComboBox.Visibility = Visibility.Collapsed;
            DisplayInternalComboBox.Visibility = Visibility.Visible;

         
            SvgAttributeBlock.Text = DisplayInternalComboBox.SelectedItem as string;
         
            this.InvalidateVisual();
           
        }

        private void DisplayOutsideComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();
            this.InvalidateVisual();
            
        }

        private void DisplayInsideComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();
            this.InvalidateVisual();
        }

        private void DisplayInternalComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();
            this.InvalidateVisual();
        }

        private void DisplayBoxComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();
            this.InvalidateVisual();
        }

        private void DisplayLegacyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();
            this.InvalidateVisual();
        }
    }
}
