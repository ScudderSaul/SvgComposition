using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using SvgComposition.Model;

namespace SvgComposition.Controls
{
    /// <summary>
    ///     Interaction logic for ColorButton.xaml
    /// </summary>
    public partial class ColorButton : UserControl, INotifyPropertyChanged
    {
        private SvgColor _inSvgColor = null;
       

        #region ctor

        public ColorButton()
        {
            InitializeComponent();
            DataContext = this;
            
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty IsChosenColorProperty =
            DependencyProperty.Register(
                "IsChosenColor",
                typeof (Color),
                typeof (ColorButton), new FrameworkPropertyMetadata(Colors.Black,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
                );


         
        /// <summary>
        /// get and set Sytem.Windows.Media Color
        /// </summary>
        public Color ChosenColor
        {
            get { return (Color) GetValue(IsChosenColorProperty); }

            
            set
            {
                SetValue(IsChosenColorProperty, value);
                var abrush = new SolidColorBrush(value);
                ColorRectangle.Fill = abrush;
                OnPropertyChanged("ChosenColor");
            }
        }

        public static readonly DependencyProperty IsButtonTextProperty =
            DependencyProperty.Register(
                "IsButtonText",
                typeof (string),
                typeof (ColorButton), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
                );

        public string ButtonText
        {
            get { return (string) GetValue(IsButtonTextProperty); }
            set
            {
                SetValue(IsButtonTextProperty, value);
              //  TextShown.Text = value;
                OnPropertyChanged("ButtonText");
            }
        }


        public static readonly DependencyProperty SvgColorTextProperty =
            DependencyProperty.Register(
                "SvgColorText",
                typeof(string),
                typeof(ColorButton), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );

       

        public string SvgColorText
        {
            get { return (string)GetValue(SvgColorTextProperty); }
            set
            {
                SetValue(SvgColorTextProperty, value);
                OnPropertyChanged("SvgColorText");
            }
        }

        public static readonly DependencyProperty SvgOpacityProperty =
            DependencyProperty.Register(
                "SvgOpacity",
                typeof(string),
                typeof(ColorButton), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );

        public string SvgOpacity
        {
            get { return (string)GetValue(SvgOpacityProperty); }
            set
            {
                SetValue(SvgOpacityProperty, value);
                _inSvgColor.Opacity = value;
                ChosenColor = _inSvgColor.Color();
                OnPropertyChanged("SvgOpacity");
            }
        }

        public SvgColor InSvgColor
        {
            get => _inSvgColor;
            set
            { 
                _inSvgColor = value;
                SvgColorText = _inSvgColor.ColorValue;
                SvgOpacity = _inSvgColor.Opacity;
                ChosenColor = _inSvgColor.Color();
                OnPropertyChanged("InSvgColor");
            }
        }

      

        #endregion

        #region events

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ColorSelected(object sender, EventArgs e)
        {
            var colorPicker = sender as ColorPicker;
            ChosenColor = colorPicker.SelectedColor;

         //   InSvgColor.ColorValue = ChosenColor.ToString();
            InSvgColor.ResetColor(ChosenColor);
            SvgColorText = _inSvgColor.ColorValue;
            SvgOpacity = _inSvgColor.Opacity;


            // SvgCompositionControl.Context.SaveChanges();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var colorpopup = new Popup();
            var colorPicker = new ColorPicker();
            colorPicker.SelectedColor = ChosenColor;
            colorPicker.RaiseSelection += ColorSelected;
            colorpopup.Child = colorPicker;

            colorpopup.VerticalOffset = 300;

            colorpopup.IsOpen = true;
        }

        #endregion

      
    }
}