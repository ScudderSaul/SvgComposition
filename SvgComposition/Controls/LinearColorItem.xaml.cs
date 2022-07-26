using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using CssCompositionTool.Dialogs;
using CssCompositionTool.Model;

namespace CssCompositionTool.Controls
{
    /// <summary>
    ///     Interaction logic for GradientColorItem.xaml
    /// </summary>
    public partial class GradientColorItem : UserControl, INotifyPropertyChanged
    {
        private string _outPercent;

        public GradientColorItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string OutPercent
        {
            get
            {
                var dn = StopPercentBox.Value;
                try
                {
                    _outPercent = dn.ToString("F");
                }
                catch (Exception ee)
                {

                    string tt = ee.Message;
                }

                return _outPercent;
            }
            set
            {
                _outPercent = value;
                SetBoxPercent(_outPercent);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void HidePercentBox()
        {
            StopPercentBox.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
        }

        public void SetBoxPercent(string vv)
        {
            var dd = 0.0;
            if (double.TryParse(vv, out dd))
            {
                StopPercentBox.Value = dd;
            }
        }

        private void StopPercentBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var dn = StopPercentBox.Value;
            if (InCssColor != null)
            {
                InCssColor.Stop = dn.ToString("F");

                ColorSelector.ColorReload = true;
            }
        }

        public void ColorSelected(object sender, EventArgs e)
        {
            var colorPicker = sender as ColorPicker;
            LinearColor = colorPicker.SelectedColor;

            InCssColor.ColorValue = LinearColor.ToString();
            ColorSelector.ColorReload = true;
            InvalidateVisual();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var colorpopup = new Popup();
            var colorPicker = new ColorPicker();
            colorPicker.SelectedColor = LinearColor;
            colorPicker.RaiseSelection += ColorSelected;
            colorpopup.Child = colorPicker;

            colorpopup.VerticalOffset = 300;
            colorpopup.HorizontalOffset = 30;

            colorpopup.IsOpen = true;
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (InCssColor != null)
            {
                var cntc = from vv in CssClassesToolControl.Context.CssColors
                    where vv.CssColorTypeId == InCssColor.CssColorTypeId
                    select vv;

                if (cntc.ToList().Count > 2)
                {
                    CssClassesToolControl.Context.CssColors.Remove(InCssColor);

                    Visibility = Visibility.Collapsed;
                    ColorSelector.ColorReload = true;
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Dependency Properties

        public static readonly DependencyProperty IsLinearColorProperty =
            DependencyProperty.Register(
                "IsLinearColor",
                typeof (Color),
                typeof (GradientColorItem), new FrameworkPropertyMetadata(Colors.Black,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
                );

        public Color LinearColor
        {
            get { return (Color) GetValue(IsLinearColorProperty); }
            set
            {
                SetValue(IsLinearColorProperty, value);
                var abrush = new SolidColorBrush(value);
                ColorRectangle.Fill = abrush;
                OnPropertyChanged();
            }
        }

        public CssColor InCssColor { get; set; }

        #endregion
    }
}