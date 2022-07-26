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
    /// Interaction logic for SvgBaselineShift.xaml
    /// </summary>
    public partial class SvgClipPath : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {
        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        private string _lenperLocalUnit;

        private static List<string> _albaValues = new List<string>
        {
            "inset()", "circle()", "ellipse()", "polygon()", "path()"
        };

        private static List<string> _extraValues = new List<string>
        {
           string.Empty, "fill-box", "stroke-box", "view-box",
        };


        #endregion

        public SvgClipPath()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "clip-path";
        }

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgClipPath), new FrameworkPropertyMetadata("none",
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
                typeof(SvgClipPath), new FrameworkPropertyMetadata("none",
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

        #region methods
        public bool CanAnimate { get; set; } = false;

        public void Create(SvgAttribute at)
        {
            at.AttributeType = SvgAttributeType.None;
            at.AttributeName = "clip-path";
            at.AttributeValue = "none";
            Load(at);
        }

        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeType = SvgAttributeType.None;
            at.AttributeValue = "none";
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

            if (at.AttributeType == SvgAttributeType.None)
            {
                NoneCheckBox.IsChecked = true;
            }

            if (at.AttributeType == SvgAttributeType.Url)
            {
                UrlCheckBox.IsChecked = true;
            }

            if (at.AttributeType == SvgAttributeType.Text)
            {
               string[] su = at.AttributeValue.Split(' ');

                if (SvgAttributeComboBox.Items.Contains(su[0]))
                {
                    SvgAttributeComboBox.SelectedIndex = SvgAttributeComboBox.Items.IndexOf(su[0]);
                    if (SvgExtraComboBox.Items.Contains(su[1]))
                    {
                        SvgExtraComboBox.SelectedIndex = SvgAttributeComboBox.Items.IndexOf(su[1]);
                    }

                }
                else
                {
                    SvgAttributeComboBox.SelectedIndex = 0;
                    SvgExtraComboBox.SelectedIndex = 0;
                }
               
            }
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            if (UrlCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Url;
            }
            if (NoneCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.None;
            }

            if (ShapeCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Text;
            }
        }
        #endregion

        #region events

        private void SvgAttributeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            string tmp = (SvgAttributeComboBox.SelectedItem as string) + " " +
                         (SvgExtraComboBox.SelectedItem as string);
            SvgAttributeCurrentValue = tmp;
           
            this.InvalidateVisual();
            OnPropertyChanged("SvgAttributeCurrentValue");
        }

          private void SvgExtraComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            string tmp = (SvgAttributeComboBox.SelectedItem as string) + " " +
                         (SvgExtraComboBox.SelectedItem as string);
            SvgAttributeCurrentValue = tmp;
           
            this.InvalidateVisual();
            OnPropertyChanged("SvgAttributeCurrentValue");
        }



        private void NoneCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            
            SvgAttributeBlock.Text = "none";
           
            // AlbaUnitComboBox.Visibility = Visibility.Collapsed;
            ShapeCheckBox.IsChecked = false;
            UrlCheckBox.IsChecked = false;
           
            SvgExtraComboBox.Visibility = Visibility.Collapsed;
            SvgAttributeComboBox.Visibility = Visibility.Collapsed;
            this.InvalidateVisual();
        }

        private void ShapeCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            SvgAttributeComboBox.Visibility = Visibility.Visible;
            SvgExtraComboBox.Visibility = Visibility.Visible;
            NoneCheckBox.IsChecked = false;
            UrlCheckBox.IsChecked = false;
         

            string tmp = (SvgAttributeComboBox.SelectedItem as string) + " " +
                         (SvgExtraComboBox.SelectedItem as string);
            SvgAttributeCurrentValue = tmp;

            this.InvalidateVisual();
            OnPropertyChanged("SvgAttributeCurrentValue");
            // ValueCheckBox.IsChecked = false;
        }

        private void UrlCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
           


            SvgAttributeCurrentValue = " ";
           
            // AlbaUnitComboBox.Visibility = Visibility.Collapsed;
            // InheritCheckBox.IsChecked = false;
           
            ShapeCheckBox.IsChecked = false;
            NoneCheckBox.IsChecked = false;
           
            SvgAttributeComboBox.Visibility = Visibility.Collapsed;

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
