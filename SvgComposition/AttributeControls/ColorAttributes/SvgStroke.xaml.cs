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
    /// Interaction logic for SvgBaselineShift.xaml
    /// </summary>
    public partial class SvgStroke: UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {
        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        private SvgColor _svgCurrentColor;

        private static List<string> _albaValues = new List<string>
        {
            "none", "context-fill", "context-stroke"
        };

       


        #endregion

        public SvgStroke()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "stroke";
        }

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgStroke), new FrameworkPropertyMetadata("none",
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
                typeof(SvgStroke), new FrameworkPropertyMetadata("none",
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
            at.AttributeType = SvgAttributeType.Color;
            at.AttributeValue = "#000000";
            SvgColor col = new SvgColor();
            col.Id = SvgAttributeCreator.FindNextSvgColorId();
            col.SvgAttributeId = at.Id;
            col.SvgAttribute = at;
            at.AttributeValue = col.ColorValue;
            at.AttributeName = _labelText;
            at.SvgElement = ele;
            at.SvgElementId = ele.Id;
            ele.SvgAttributes.Add(at);
            SvgCompositionControl.Context.SvgColors.Add(col);
            SvgCompositionControl.Context.SvgAttributes.Add(at);
            SvgCompositionControl.Context.SaveChanges();
            Load(at);
            return (at);
        }

        public void Load(SvgAttribute at)
        {

            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;
            _svgCurrentColor = at.SvgColors.First();
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());


            if (at.AttributeType == SvgAttributeType.Text)
            {
                if (SvgAttributeComboBox.Items.Contains(at.AttributeValue))
                {
                    SvgAttributeComboBox.SelectedIndex = SvgAttributeComboBox.Items.IndexOf(at.AttributeValue);
                    TextCheckBox.IsChecked = true;
                }
            }

            if (at.AttributeType == SvgAttributeType.Color)
            {
                ColorCheckBox.IsChecked = true;
                SvgAttributeCurrentValue = SvgAttributeBlock.Text = SvgCurrentColor.ColorValue;
                SvgColorTextBox.Text = SvgCurrentColor.ColorValue;
            }

            if (at.AttributeType == SvgAttributeType.Url)
            {
                UrlCheckBox.IsChecked = true;
                SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;
                SvgExtraAttributeBlock.Text = at.SecondaryValue;
            }
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            if (ColorCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Color;
                at.SvgColors.First().ResetColor(_svgCurrentColor.Color());
            }

            if (UrlCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Url;
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
            SvgAttributeBlock.IsReadOnly = false;
            string tmp = (SvgAttributeComboBox.SelectedItem as string);
            SvgAttributeCurrentValue = tmp;
            //      SvgAttributeBlock.Text = SvgCurrentColor.ColorValue;
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());
            SvgAttributeBlock.IsReadOnly = true;
            this.InvalidateVisual();
            
        }

         



        private void TextCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            SvgAttributeBlock.IsReadOnly = false;
            SvgAttributeBlock.Text = SvgAttributeComboBox.Items[0] as string;

            ColorCheckBox.IsChecked = false;
            UrlCheckBox.IsChecked = false;
            //  TextCheckBox.IsChecked = false;
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());
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

            SvgAttributeBlock.Text = SvgAttributeCurrentValue = SvgCurrentColor.ColorValue;
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());
            ColorAttrButton.Visibility = Visibility.Visible;

            SvgAttributeComboBox.Visibility = Visibility.Collapsed;
            SvgExtraAttributeBlock.Visibility = Visibility.Collapsed;
            SvgAttributeBlock.IsReadOnly = true;

            this.InvalidateVisual();
            
           
        }

        private void UrlCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            SvgAttributeBlock.IsReadOnly = false;

            ColorCheckBox.IsChecked = false;
        //   UrlCheckBox.IsChecked = false;
            TextCheckBox.IsChecked = false;

            //  SvgAttributeCurrentValue = " ";


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
            SvgAttributeBlock.Text =  SvgCurrentColor.ColorValue;
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
