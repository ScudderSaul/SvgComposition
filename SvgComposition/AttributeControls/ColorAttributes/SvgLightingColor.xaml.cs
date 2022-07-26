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
    /// Interaction logic for SvgLightingColor.xaml
    /// </summary>
    public partial class SvgLightingColor : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {
        #region fields
  //      private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        private SvgColor _svgCurrentColor;


        private static List<string> _albaValues = new List<string>
        {
            "currentColor", "inherit",
        };

       


        #endregion

        public SvgLightingColor()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "lighting-color";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
            SelTip.Text = $"The '{LabelText}' attribute choices";
        }

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgLightingColor), new FrameworkPropertyMetadata("none",
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
                typeof(SvgLightingColor), new FrameworkPropertyMetadata("none",
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
            at.AttributeName = _labelText;
            at.AttributeType = SvgAttributeType.Color;
            at.AttributeValue = "#FFFFFF";
            at.SvgElement = ele;
            at.SvgElementId = ele.Id;

            ele.SvgAttributes.Add(at);
            SvgCompositionControl.Context.SvgAttributes.Add(at);
            SvgCompositionControl.Context.SaveChanges();

            SvgColor col = new SvgColor();
            col.Id = SvgAttributeCreator.FindNextSvgColorId();
            col.SvgAttributeId = at.Id;
            col.SvgAttribute = at;
            at.AttributeValue = col.ColorValue;
            at.SvgColors.Add(col);
            _svgCurrentColor = col;
            SvgCompositionControl.Context.SvgColors.Add(col);
            SvgCompositionControl.Context.SaveChanges();

            Load(at);
            return (at);
        }



        public void Load(SvgAttribute at)
        {

            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;;
            _svgCurrentColor = at.SvgColors.First();
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());

            if (at.AttributeType == SvgAttributeType.Text)
            {
                for (int ii = 0; ii < SvgAttributeComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = SvgAttributeComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.AttributeValue)
                    {
                        SvgAttributeComboBox.SelectedIndex = ii;
                        TextCheckBox.IsChecked = true;
                        break;
                    }
                }
            }

            _svgCurrentColor = at.SvgColors.First();

            if (at.AttributeType == SvgAttributeType.Color)
            {
                ColorCheckBox.IsChecked = true;
                SvgAttributeBlock.Text = SvgCurrentColor.ColorValue;
                SvgColorTextBox.Text = SvgCurrentColor.ColorValue;
            }

          
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeBlock.Text;
            if (ColorCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Color;

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

         



        private void TextCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
          
            SvgAttributeBlock.Text = SvgAttributeComboBox.Items[0] as string;
           
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());

            ColorCheckBox.IsChecked = false;
          //  UrlCheckBox.IsChecked = false;
            //  TextCheckBox.IsChecked = false;

            ColorAttrButton.Visibility = Visibility.Collapsed;
            SvgAttributeComboBox.Visibility = Visibility.Visible;
            SvgExtraAttributeBlock.Visibility = Visibility.Collapsed;
            this.InvalidateVisual();
        }

        private void ColorCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
           

          //  ColorCheckBox.IsChecked = false;
          //  UrlCheckBox.IsChecked = false;
            TextCheckBox.IsChecked = false;
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());

           
            SvgAttributeBlock.Text = SvgCurrentColor.ColorValue; 

            ColorAttrButton.Visibility = Visibility.Visible;

            SvgAttributeComboBox.Visibility = Visibility.Collapsed;
            SvgExtraAttributeBlock.Visibility = Visibility.Collapsed;
          

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
            SvgColorTextBox.Text = SvgCurrentColor.ColorValue;
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
