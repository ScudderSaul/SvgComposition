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
    /// Interaction logic for SvgTextDecoration.xaml
    /// </summary>
    public partial class SvgTextDecoration : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {
        #region fields
  //      private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        private SvgColor _svgCurrentColor;


        private List<string> _decorationLine = new List<string>
        {
            "none", "underline", "overline", "line-through", "blink",
        };

        private List<string> _decorationStyle = new List<string>
        {
            "solid", "double", "dotted", "dashed", "wavy",
        };

       

        private static List<string> _albaValues = new List<string>
        {
            "inherit", "currentcolor", 
        };

       


        #endregion

        public SvgTextDecoration()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "text-decoration";
        }

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgTextDecoration), new FrameworkPropertyMetadata("none",
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
                typeof(SvgTextDecoration), new FrameworkPropertyMetadata("none",
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
            at.AttributeValue = "none solid #000000";
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
            SvgAttributeBlock.Text = at.AttributeValue;
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;;
            _svgCurrentColor = at.SvgColors.First();
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());

            string[] st = at.AttributeValue.Split(' ');

            for (int ii = 0; ii < DecorationLineComboBox.Items.Count; ii++)
            {
                ComboBoxItem itt = DecorationLineComboBox.Items[ii] as ComboBoxItem;
                if ((itt.Content as string) == st[0])
                {
                    DecorationLineComboBox.SelectedIndex = ii;
                    break;
                }
            }

            for (int ii = 0; ii < DecorationStyleComboBox.Items.Count; ii++)
            {
                ComboBoxItem itt = DecorationStyleComboBox.Items[ii] as ComboBoxItem;
                if ((itt.Content as string) == st[1])
                {
                    DecorationStyleComboBox.SelectedIndex = ii;
                    break;
                }
            }

            if (at.AttributeType == SvgAttributeType.Text)
            {
                for (int ii = 0; ii < SvgAttributeComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = SvgAttributeComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == st[2])
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

            string line = "none";
            if (DecorationLineComboBox.SelectedIndex >= 0)
            {
                line = DecorationLineComboBox.SelectedValue as string;
            }

            string sty = "solid";
            if (DecorationStyleComboBox.SelectedIndex >= 0)
            {
                sty = DecorationStyleComboBox.SelectedValue as string;
            }

            SvgAttributeBlock.Text = SvgAttributeCurrentValue = $"{line} {sty} {lbi.Content.ToString()}";  

            this.InvalidateVisual();

        }

        private void DecorationStyleComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);

            string line = "none";
            if (DecorationLineComboBox.SelectedIndex >= 0)
            {
                line = DecorationLineComboBox.SelectedValue as string;
            }

            string col = "#000000";
            if (ColorCheckBox.IsChecked == true)
            {
                col = SvgCurrentColor.ColorValue;
            }
            else
            {
                col = SvgAttributeComboBox.SelectedValue as string;
            }

            SvgAttributeBlock.Text = SvgAttributeCurrentValue = $"{line} {lbi.Content.ToString()} {col}";

        }

        private void DecorationLineComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);

            string sty = "solid";
            if (DecorationStyleComboBox.SelectedIndex >= 0)
            {
                sty = DecorationStyleComboBox.SelectedValue as string;
            }


            string col = "#000000";
            if (ColorCheckBox.IsChecked == true)
            {
                col = SvgCurrentColor.ColorValue;
            }
            else
            {
                col = SvgAttributeComboBox.SelectedValue as string;
            }

            SvgAttributeBlock.Text = SvgAttributeCurrentValue = $"{lbi.Content.ToString()} {sty} {col}";
        }


        private void TextCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            string col = "#000000";
            if (SvgAttributeComboBox.SelectedIndex >= 0)
            {
                col = SvgAttributeComboBox.SelectedValue as string;
            }
            else
            {
                SvgAttributeComboBox.SelectedIndex = 0;
                col = SvgAttributeComboBox.SelectedValue as string;
            }

            string sty = "solid";
            if (DecorationStyleComboBox.SelectedIndex >= 0)
            {
                sty = DecorationStyleComboBox.SelectedValue as string;
            }
            else
            {
                DecorationStyleComboBox.SelectedIndex = 0;
                sty = DecorationStyleComboBox.SelectedValue as string;
            }

            string line = "none";
            if (DecorationLineComboBox.SelectedIndex >= 0)
            {
                line = DecorationLineComboBox.SelectedValue as string;
            }
            else
            {
                DecorationLineComboBox.SelectedIndex = 0;
                line = DecorationLineComboBox.SelectedValue as string;
            }


            SvgAttributeBlock.Text = SvgAttributeCurrentValue = $"{line} {sty} {col}";


          
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());

            ColorCheckBox.IsChecked = false;

            ColorAttrButton.Visibility = Visibility.Collapsed;
            SvgAttributeComboBox.Visibility = Visibility.Visible;
      
            this.InvalidateVisual();
        }

        private void ColorCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {

            TextCheckBox.IsChecked = false;
            ColorRectangle.Fill = new SolidColorBrush(SvgCurrentColor.Color());


            string col = SvgCurrentColor.ColorValue; ;
            

            string sty = "solid";
            if (DecorationStyleComboBox.SelectedIndex >= 0)
            {
                sty = DecorationStyleComboBox.SelectedValue as string;
            }
            else
            {
                DecorationStyleComboBox.SelectedIndex = 0;
                sty = DecorationStyleComboBox.SelectedValue as string;
            }

            string line = "none";
            if (DecorationLineComboBox.SelectedIndex >= 0)
            {
                line = DecorationLineComboBox.SelectedValue as string;
            }
            else
            {
                DecorationLineComboBox.SelectedIndex = 0;
                line = DecorationLineComboBox.SelectedValue as string;
            }


            SvgAttributeBlock.Text = SvgAttributeCurrentValue = $"{line} {sty} {col}";

            ColorAttrButton.Visibility = Visibility.Visible;

            SvgAttributeComboBox.Visibility = Visibility.Collapsed;

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
