using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ColorAttributes
{
    /// <summary>
    /// Interaction logic for SvgFill.xaml
    /// </summary>
    public partial class SvgFill: UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {
        #region fields
  //      private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        private SvgColor _svgCurrentColor;


        private static List<string> _albaValues = new List<string>
        {
            "none", "context-fill", "context-stroke"
        };

       


        #endregion

        public SvgFill()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "fill";
        }

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgFill), new FrameworkPropertyMetadata("none",
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
                typeof(SvgFill), new FrameworkPropertyMetadata("none",
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
        public bool CanAnimate { get; set; } = true;

        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            SvgColor col = new SvgColor();
            col.Id = SvgAttributeCreator.FindNextSvgColorId();
            col.SvgAttributeId = at.Id;
            col.SvgAttribute = at;
            if (SvgElementCreator.AnimationElements.Contains(ele.ElementType))
            {
                at.AttributeType = SvgAttributeType.Text;
                at.AttributeValue = "freeze";
            }
            else
            {
                at.AttributeType = SvgAttributeType.Color;
                at.AttributeValue = "#000000";
                at.AttributeValue = col.ColorValue;
            }

            at.AttributeName = _labelText;
            at.SvgElement = ele;
            at.SvgElementId = ele.Id;
            ele.SvgAttributes.Add(at);
            at.SvgColors.Add(col);

            SvgCompositionControl.Context.SvgColors.Add(col);
            SvgCompositionControl.Context.SvgAttributes.Add(at);
            SvgCompositionControl.Context.SaveChanges();

            Load(at);
            return (at);
        }



        public void Load(SvgAttribute at)
        {
            if (SvgElementCreator.AnimationElements.Contains(at.SvgElement.ElementType))
            {
                SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;
                at.AttributeType = SvgAttributeType.Text;
                SvgAnimationComboBox.Visibility = Visibility.Visible;
                SvgAttributeBlock.Visibility = Visibility.Visible;
                ColorAttrButton.Visibility = Visibility.Collapsed;
                TextCheckBox.Visibility = Visibility.Collapsed;
                UrlCheckBox.Visibility = Visibility.Collapsed;
                ColorCheckBox.Visibility = Visibility.Collapsed;
                SvgExtraAttributeBlock.Visibility = Visibility.Collapsed;
                SvgAttributeComboBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                SvgAnimationComboBox.Visibility = Visibility.Collapsed;
                SvgAttributeComboBox.Visibility = Visibility.Collapsed;
                SvgAttributeBlock.Visibility = Visibility.Visible;
                ColorAttrButton.Visibility = Visibility.Visible;
                TextCheckBox.Visibility = Visibility.Visible;
                UrlCheckBox.Visibility = Visibility.Visible;
                ColorCheckBox.Visibility = Visibility.Visible;
                SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;
                _svgCurrentColor = at.SvgColors.First();
                ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());

                if (at.AttributeType == SvgAttributeType.Text)
                {
                    TextCheckBox.IsChecked = true;
                    SvgExtraAttributeBlock.Visibility = Visibility.Collapsed;
                }

                if (at.AttributeType == SvgAttributeType.Color)
                {
                    ColorCheckBox.IsChecked = true;
                    SvgExtraAttributeBlock.Visibility = Visibility.Collapsed;
                    SvgAttributeBlock.Text = SvgAttributeCurrentValue = SvgCurrentColor.ColorValue;
                    SvgColorTextBox.Text = SvgCurrentColor.ColorValue;
                }

                if (at.AttributeType == SvgAttributeType.Url)
                {
                    UrlCheckBox.IsChecked = true;
                    SvgExtraAttributeBlock.Visibility = Visibility.Visible;
                    SvgAttributeBlock.Text = SvgAttributeCurrentValue = at.AttributeValue;
                    SvgExtraAttributeBlock.Text = at.SecondaryValue;
                }
            }
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            if (ColorCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Color;
                at.SvgColors.First().ResetColor( _svgCurrentColor.Color());
            }

            if (UrlCheckBox.IsChecked == true)
            {
                at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
                at.AttributeType = SvgAttributeType.Url;
                at.SecondaryValue = SvgExtraAttributeBlock.Text;
            }

            if (TextCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Text;
            }
            SvgCompositionControl.Context.SaveChanges();
        }
        #endregion


        #region events

        private void SvgAttributeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();

            this.InvalidateVisual();

        }

        private void SvgAnimationComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();

            this.InvalidateVisual();
        }



        private void TextCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
          
          //  SvgAttributeBlock.Text = SvgAttributeComboBox.Items[0] as string;
            
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());

            ColorCheckBox.IsChecked = false;
            UrlCheckBox.IsChecked = false;
            //  TextCheckBox.IsChecked = false;
            SvgAttributeBlock.Visibility = Visibility.Visible;
            ColorAttrButton.Visibility = Visibility.Collapsed;
            SvgAttributeComboBox.Visibility = Visibility.Visible;
            SvgExtraAttributeBlock.Visibility = Visibility.Collapsed;
            this.InvalidateVisual();
        }

        private void ColorCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
           

          //  ColorCheckBox.IsChecked = false;
            UrlCheckBox.IsChecked = false;
            TextCheckBox.IsChecked = false;
            SvgAttributeBlock.IsReadOnly = false;
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());

            SvgAttributeBlock.Visibility = Visibility.Visible;
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = SvgCurrentColor.ColorValue; 

            ColorAttrButton.Visibility = Visibility.Visible;

            SvgAttributeComboBox.Visibility = Visibility.Collapsed;
            SvgExtraAttributeBlock.Visibility = Visibility.Collapsed;
          

            this.InvalidateVisual();
           
           
        }

        private void UrlCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
           

            ColorCheckBox.IsChecked = false;
        //   UrlCheckBox.IsChecked = false;
            TextCheckBox.IsChecked = false;

            

            SvgAttributeBlock.Visibility = Visibility.Visible;
            SvgAttributeComboBox.IsReadOnly = false;

            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());
            ColorAttrButton.Visibility = Visibility.Collapsed;
            SvgAttributeComboBox.Visibility = Visibility.Collapsed;
            SvgExtraAttributeBlock.Visibility = Visibility.Visible;

            this.InvalidateVisual();
            
        }


        public SvgColor SvgCurrentColor
        {
            get { return _svgCurrentColor; }
            set { _svgCurrentColor = value; }
        }

        public void ColorSelected(object sender, EventArgs e)
        {
            var colorPicker = sender as ColorPicker;
            SvgCurrentColor.ResetColor(colorPicker.SelectedColor);
            SvgAttributeBlock.Text = SvgCurrentColor.ColorValue;
            SvgAttributeCurrentValue = SvgColorTextBox.Text = SvgCurrentColor.ColorValue;
            SolidColorBrush vv = new SolidColorBrush(SvgCurrentColor.Color());
            ColorRectangle.Fill = vv;
            InvalidateVisual();
        }

        private void ColorAttrButton_OnClick(object sender, RoutedEventArgs e)
        {
            var colorpopup = new Popup();
            var colorPicker = new ColorPicker();
            colorPicker.SelectedColor = SvgCurrentColor.Color();
            colorPicker.RaiseSelection += ColorSelected;
            colorpopup.Child = colorPicker;

            colorpopup.VerticalOffset = 300;

            colorpopup.IsOpen = true;
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
