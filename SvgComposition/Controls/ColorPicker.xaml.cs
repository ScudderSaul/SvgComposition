using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SvgComposition.Controls
{
    /// <summary>
    ///     Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl, INotifyPropertyChanged
    {
        private Color _selectedColor;

        #region ctor

        public ColorPicker()
        {
            InitializeComponent();
            ValueRect.Fill = new SolidColorBrush(SelectedColor);
            CreateColorsList();
        }

        #endregion

        #region Properties

        public Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                if (value.Equals(_selectedColor)) return;
                _selectedColor = value;
                block = true;
                Aslider.Value = _selectedColor.A;
                Rslider.Value = _selectedColor.R;
                Gslider.Value = _selectedColor.G;
                Bslider.Value = _selectedColor.B;
                ValueRect.Fill = new SolidColorBrush(SelectedColor);
                PoundRgb.Text = $"#{_selectedColor.R:x2}{_selectedColor.G:x2}{_selectedColor.B:x2}";
                InvalidateArrange();
                OnPropertyChanged("SelectedColor");
                block = false;
            }
        }

        #endregion

        #region events

        private bool block;

        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (block)
            {
                return;
            }

            var tran = (byte) Aslider.Value;
            var red = (byte) Rslider.Value;
            var green = (byte) Gslider.Value;
            var blue = (byte) Bslider.Value;
            SelectedColor = Color.FromArgb(tran, red, green, blue);
           

            OnPropertyChanged("SelectedColor");
            InvalidateArrange();
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler RaiseSelection;

        protected virtual void OnRaiseSelection(EventArgs e)
        {
            var handler = RaiseSelection;
            if (handler != null) handler(this, new EventArgs());
        }

        private void SelectButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Parent != null)
            {
                OnRaiseSelection(new EventArgs());
                (Parent as Popup).IsOpen = false;
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Parent != null)
                (Parent as Popup).IsOpen = false;
        }

        public void CreateColorsList()
        {
            var tt = typeof (Colors);
            var pi = tt.GetProperties(BindingFlags.Public | BindingFlags.Static);


            for (var ii = 0; ii < pi.Length; ii++)
            {
                var st = new StackPanel();
                st.Background = new SolidColorBrush(Colors.White);
                st.Orientation = Orientation.Horizontal;
                var rr = new Rectangle();
                rr.Width = 20;
                rr.Height = 17;
                rr.Margin = new Thickness(1.0);
                //  PropertyInfo propertyInfo = typeof(Colors).GetProperty(pi[ii].Name)


                var acolor = (Color) pi[ii].GetValue(null, null);
                // pi[ii].Name, BindingFlags.Public | BindingFlags.Static
                //  BindingFlags.Static & BindingFlags.InvokeMethod
                // , null, null, null);
                // Color acolor = (Color) pi[ii].GetValue();
                rr.Fill = new SolidColorBrush(acolor);
                st.Children.Add(rr);
                var text = new TextBlock();
                text.FontSize = 6.0;
                text.Width = 40;
                text.Margin = new Thickness(1.0);
                text.Text = string.Format("{0} ", pi[ii].Name);
                st.Tag = pi[ii].Name;
                st.Children.Add(text);
                st.MouseLeftButtonDown += StOnMouseLeftButtonDown;
                GridColors.Children.Add(st);
            }
        }

        private void StOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var sp = sender as StackPanel;
            var name = sp.Tag as string;
            var color = (Color) typeof (Colors).GetProperty(name, BindingFlags.Public | BindingFlags.Static)
                .GetValue(null, null);
            SelectedColor = color;
            
        }

        #endregion
    }
}