using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.VisualStudio.PlatformUI;

namespace SvgComposition.Dialogs
{
    /// <summary>
    ///     Interaction logic for ColorSelector.xaml
    /// </summary>
    public partial class ColorSelector : DialogWindow, INotifyPropertyChanged
    {
        public ColorSelector()
        {
            ColorReload = false;
            InitializeComponent();
            DataContext = this;

            Loaded += ColorSelector_Loaded;
        }

        private void ColorSelector_Loaded(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            _dispatcherTimer.Tick += DispatcherTimerOnTick;
            _dispatcherTimer.Start();
        }

        #region feilds

        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        //      private Brush _wpfFill;
        private double _linearAngle;
        private string _colorName;
        private CssColorType _cssColorType;
        private int _nextCssColorTypeId;
        private int _nextCssColorId;
        private CssColor _cssColor;
        private bool _flatMode;
        private bool _linearMode;
        private bool _radialMode;
        //private  SortedDictionary<double, CssColor> _colorsSortedDictionary 
        //    = new SortedDictionary<double, CssColor>();

        private string _radialShape;
        private string _radialSize;
        private string _radialCenter;
        private GradientColorItem _cssColorItem;
        private string _textAngle;

        #endregion

        #region properties

        public string ColorName
        {
            get { return _colorName; }
            set
            {
                if (value == _colorName) return;
                _colorName = value;
                OnPropertyChanged();
            }
        }

        public void SetFlatOnly()
        {
            this.LinearGradient.IsEnabled = false;
            this.RadialGradient.IsEnabled = false;

        }

        public bool FlatMode
        {
            get { return _flatMode; }
            set
            {
                if (value.Equals(_flatMode)) return;
                _flatMode = value;
                OnPropertyChanged();
            }
        }

        public bool LinearMode
        {
            get { return _linearMode; }
            set
            {
                if (value.Equals(_linearMode)) return;
                _linearMode = value;
                OnPropertyChanged();
            }
        }

        public bool RadialMode

        {
            get { return _radialMode; }
            set
            {
                if (value.Equals(_radialMode)) return;
                _radialMode = value;
                OnPropertyChanged();
            }
        }

        public CssStyleItem StyleItem { get; set; }

        public static string CssTestString { get; set; }

        public double LinearAngle
        {
            get { return _linearAngle; }
            set
            {
                if (value.Equals(_linearAngle)) return;
                _linearAngle = value;
                TextAngle = _linearAngle.ToString("F");
                OnPropertyChanged();
            }
        }

        public string TextAngle
        {
            get { return _textAngle; }
            set
            {
                if (value == _textAngle) return;
                _textAngle = value;
                _cssColorType.Angle = _textAngle;
                CssClassesToolControl.Context.SaveChanges();
                Load();
                OnPropertyChanged();
            }
        }

        public string RadialRepeats { get; set; } = "false";

        public static bool ColorReload { get; set; }

        public ObservableCollection<GradientColorItem> ColorItems { get; set; }

        #endregion

        #region methods

        public void SetRadialShape(string val)
        {
            foreach (var item in RadialShapeBox.Items)
            {
                var block = item as TextBlock;
                if (block != null && block.Text == val)
                {
                    RadialShapeBox.SelectedItem = item;
                    break;
                }
            }
        }

        public void SetRadialSize(string siz)
        {
            foreach (var item in RadialDistanceBox.Items)
            {
                var block = item as TextBlock;
                if (block != null && block.Text == siz)
                {
                    RadialDistanceBox.SelectedItem = item;
                    break;
                }
            }
        }

        public void SetRadialCenter(string val)
        {
            foreach (var item in RadialCenterBox.Items)
            {
                var block = item as TextBlock;
                if (block != null && block.Text == val)
                {
                    RadialCenterBox.SelectedItem = item;
                    break;
                }
            }
        }

        public void CreateFirstColorTypes()
        {
            //  _cssColorType = CssClassesToolControl.Context.CssColorTypes.Create();
            _cssColorType = new CssColorType();
            _cssColorType.ColorType = "Flat";
            _cssColorType.Shape = "ellipse";
            _cssColorType.Size = "farthest-corner";
            _cssColorType.Center = "at left top";
            _cssColorType.Repeats = "false";
            _cssColorType.Angle = "30.0";
            //    _cssColorType.ColorType = "Radial";

            FlatMode = true;

            _cssColorType.CssStyleItemId = StyleItem.Id;
            _cssColorType.Id = FindNextCssColorTypeId();
            CssClassesToolControl.Context.CssColorTypes.Add(_cssColorType);
            CssClassesToolControl.Context.SaveChanges();

            //  _cssColor = CssClassesToolControl.Context.CssColors.Create();
            _cssColor = new CssColor();
            _cssColor.ColorValue = Colors.LightGray.ToString();
            _cssColor.CssColorTypeId = _cssColorType.Id;
            _cssColor.ColorOrder = -1;
            _cssColor.Id = FindNextCssColorId();
            CssClassesToolControl.Context.CssColors.Add(_cssColor);
            CssClassesToolControl.Context.SaveChanges();

            var color = new CssColor();
            //   var color = CssClassesToolControl.Context.CssColors.Create();
            color.ColorValue = Colors.White.ToString();
            color.CssColorTypeId = _cssColorType.Id;
            color.Stop = "0.0";
            color.ColorOrder = 0;
            color.Id = FindNextCssColorId();
            CssClassesToolControl.Context.CssColors.Add(color);
            CssClassesToolControl.Context.SaveChanges();

            //   color = CssClassesToolControl.Context.CssColors.Create();
            color = new CssColor();
            color.ColorValue = Colors.DarkBlue.ToString();
            color.CssColorTypeId = _cssColorType.Id;
            color.Stop = "1.0";
            color.ColorOrder = 1;
            color.Id = FindNextCssColorId();
            CssClassesToolControl.Context.CssColors.Add(color);
            CssClassesToolControl.Context.SaveChanges();
        }

        public List<CssColor> CreateFirstColors()
        {
            var colorlist = new List<CssColor>();

            // _cssColor = CssClassesToolControl.Context.CssColors.Create();
            _cssColor = new CssColor();
            _cssColor.ColorValue = Colors.LightGray.ToString();
            _cssColor.CssColorTypeId = _cssColorType.Id;
            _cssColor.ColorOrder = -1;
            _cssColor.Id = FindNextCssColorId();
            CssClassesToolControl.Context.CssColors.Add(_cssColor);
            CssClassesToolControl.Context.SaveChanges();

            colorlist.Add(_cssColor);

            // var color = CssClassesToolControl.Context.CssColors.Create();
            var color = new CssColor();
            color.ColorValue = Colors.White.ToString();
            color.CssColorTypeId = _cssColorType.Id;
            color.Stop = "0.0";
            color.ColorOrder = 0;
            color.Id = FindNextCssColorId();
            CssClassesToolControl.Context.CssColors.Add(color);
            CssClassesToolControl.Context.SaveChanges();
            colorlist.Add(color);

            //  color = CssClassesToolControl.Context.CssColors.Create();
            color = new CssColor();
            color.ColorValue = Colors.DarkBlue.ToString();
            color.CssColorTypeId = _cssColorType.Id;
            color.Stop = "1.0";
            color.ColorOrder = 1;
            color.Id = FindNextCssColorId();
            CssClassesToolControl.Context.CssColors.Add(color);
            CssClassesToolControl.Context.SaveChanges();
            colorlist.Add(color);

            return (colorlist);
        }

        public void Load()
        {
            var qq = from nn in CssClassesToolControl.Context.CssColorTypes
                     where nn.CssStyleItemId == StyleItem.Id
                     select nn;

            var tl = qq.ToList();
            if (tl.Count == 0)
            {
                CreateFirstColorTypes();
            }
            else
            {
                _cssColorType = tl.First();
            }

            var colors = from zz in CssClassesToolControl.Context.CssColors
                         where zz.CssColorTypeId == _cssColorType.Id
                         select zz;

            var clist = colors.ToList();

            if (clist.Count == 0)
            {
                clist = CreateFirstColors();
            }

            ColorItems = new ObservableCollection<GradientColorItem>();
            FlatPanel.Children.Clear();
            StopsView.Children.Clear();

            foreach (var color in clist)
            {
                var item = new GradientColorItem();
                item.LinearColor = (Color)ColorConverter.ConvertFromString(color.ColorValue);
                item.InCssColor = color;
                item.OutPercent = color.Stop;

                if (color.ColorOrder < 0)
                {
                    _cssColor = color;
                    _cssColorItem = item;
                    item.HidePercentBox();
                    FlatPanel.Children.Add(item);
                }
                else
                {
                    ColorItems.Add(item);
                    StopsView.Children.Add(item);
                }
            }

            if (_cssColorType.ColorType == "Flat")
            {
                FlatMode = true;
                FlatColor.IsChecked = true;
                StopsView.Visibility = Visibility.Collapsed;
                RadialPanel.Visibility = Visibility.Collapsed;
                LinearPanel.Visibility = Visibility.Collapsed;
                FlatPanel.Visibility = Visibility.Visible;
                ColorBlock.Text = "Flat Color";
            }
            if (_cssColorType.ColorType == "Radial")
            {
                SetRadialCenter(_cssColorType.Center);
                SetRadialShape(_cssColorType.Shape);
                SetRadialSize(_cssColorType.Size);

                RadialMode = true;

                RadialGradient.IsChecked = true;

                StopsView.Visibility = Visibility.Visible;
                RadialPanel.Visibility = Visibility.Visible;
                LinearPanel.Visibility = Visibility.Collapsed;
                FlatPanel.Visibility = Visibility.Collapsed;
                ColorBlock.Text = "Radial Gradient";
            }
            if (_cssColorType.ColorType == "Linear")
            {
                SetLinearAngle(_cssColorType.Angle);
                LinearMode = true;
                LinearGradient.IsChecked = true;
                StopsView.Visibility = Visibility.Visible;
                RadialPanel.Visibility = Visibility.Collapsed;
                LinearPanel.Visibility = Visibility.Visible;
                FlatPanel.Visibility = Visibility.Collapsed;
                ColorBlock.Text = "Linear Gradient";
            }
            //  ShowColors();
        }

        private void DispatcherTimerOnTick(object sender, EventArgs eventArgs)
        {
            if (ColorReload)
            {
                _dispatcherTimer.Stop();
                CssClassesToolControl.Context.SaveChanges();
                ColorReload = false;
                Load();
                _dispatcherTimer.Start();
            }
            if (CssClassesToolControl.MainToolControl.IsColorPage == false)
            {
                _dispatcherTimer.Stop();
            }
        }

        private string ColorFunction(string colortext)
        {
            var ret = "inherit";
            if (string.IsNullOrWhiteSpace(colortext) == false)
            {
                var color = (Color)ColorConverter.ConvertFromString(colortext);
                ret = string.Format("rgba({0},{1},{2},{3,2:F})", color.R, color.G, color.B, ((double)color.A));
            }

            return (ret);
        }

        //public void ShowColors()
        //{
        //    if (_cssColor == null)
        //    {
        //        return;
        //    }

        //    CssTestString = string.Format(".{0}{{\r\n", "ColorBox");
        //    CssTestString += "height:200px;\r\n";
        //    if (FlatMode)
        //    {
        //        var cv = ColorFunction(_cssColor.ColorValue);
        //        CssTestString += string.Format("BackgroundColor: {0};\r\n", cv);
        //    }
        //    if (LinearMode)
        //    {
        //        CssTestString += "background:";
        //        CssTestString += string.Format("linear-gradient({0}deg, ", _cssColorType.Angle);

        //        foreach (var vv in ColorItems)
        //        {
        //            var color = vv.InCssColor;
        //            CssTestString += string.Format(" {0} {1}%", ColorFunction(color.ColorValue), color.Stop);
        //            if (vv != ColorItems.Last())
        //            {
        //                CssTestString += ",";
        //            }
        //        }
        //        CssTestString += ")\r\n";
        //    }
        //    if (RadialMode)
        //    {
        //        CssTestString += "background:";
        //        CssTestString += string.Format("radial-gradient({0} {1} {2},",
        //            _cssColorType.Shape, _cssColorType.Size, _cssColorType.Center);

        //        foreach (var vv in ColorItems)
        //        {
        //            var color = vv.InCssColor;
        //            CssTestString += string.Format(" {0} {1}%", ColorFunction(color.ColorValue), color.Stop);
        //            if (vv != ColorItems.Last())
        //            {
        //                CssTestString += ",";
        //            }
        //        }
        //        CssTestString += ")\r\n";
        //    }

        //    CssTestString += "}\r\n";
        //    CssTestString += "\r\n";

        //    Css_Classes2019.ShowFont showfont = new CssClassesExtension.ShowFont();
        //    string html = showfont.TransformText();
        //    CssClassesToolControl.OpenBrowserPage(html);

        //    //  if (ShowBrowser != null)
        //    {
        //        //  ShowBrowser.NavigateToString(html);

        //    }
        //}

        public int FindNextCssColorTypeId()
        {
            var items = from tut in CssClassesToolControl.Context.CssColorTypes
                        select tut;

            foreach (var item in items)
            {
                if (item.Id >= _nextCssColorTypeId)
                {
                    _nextCssColorTypeId = item.Id + 1;
                }
            }

            if (_nextCssColorTypeId <= 0)
            {
                _nextCssColorTypeId = 1;
            }

            return (_nextCssColorTypeId);
        }

        public int FindNextCssColorId()
        {
            var items = from tut in CssClassesToolControl.Context.CssColors
                        select tut;

            foreach (var item in items)
            {
                if (item.Id >= _nextCssColorId)
                {
                    _nextCssColorId = item.Id + 1;
                }
            }

            if (_nextCssColorId <= 0)
            {
                _nextCssColorId = 1;
            }
            return (_nextCssColorId);
        }

        public void SetLinearAngle(string st)
        {
            double zz;
            if (double.TryParse(st, out zz))
            {
                if (_cssColorType != null)
                {
                    LinearAngle = zz;
                }
            }
        }

        //private void RetAnimate_OnClick(object sender, RoutedEventArgs e)
        //{

        //    var ff = (Parent as Popup);

        //    if (ff != null)
        //    {
        //        _dispatcherTimer.Stop();
        //        CssClassesToolControl.Context.SaveChanges();
        //        ff.IsOpen = false;
        //    }

        //    // CssClassesToolControl.MainToolControl.ToCssAnimationPage();
        //}

        //private void RetStyle_OnClick(object sender, RoutedEventArgs e)
        //{
        //    var ff = (Parent as Popup);

        //    if (ff != null)
        //    {
        //        _dispatcherTimer.Stop();
        //        CssClassesToolControl.Context.SaveChanges();
        //        ff.IsOpen = false;
        //    }

        //    //   CssClassesToolControl.MainToolControl.ToCssPage();
        //}

        private void RetDone_OnClick(object sender, RoutedEventArgs e)
        {
                _dispatcherTimer.Stop();
                CssClassesToolControl.Context.SaveChanges();
            DialogResult = true;
            this.Close();


            // CssClassesToolControl.MainToolControl.ToCssAnimationPage();
        }

        #endregion

        #region events

        private void LinearAngleBox_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            SetLinearAngle(LinearAngleBox.Text);
        }


        private void FlatColor_OnClick(object sender, RoutedEventArgs e)
        {
            _cssColorType.ColorType = "Flat";
            CssClassesToolControl.Context.SaveChanges();
            Load();
        }

        private void RadialGradient_OnClick(object sender, RoutedEventArgs e)
        {
            _cssColorType.ColorType = "Radial";
            CssClassesToolControl.Context.SaveChanges();
            Load();
        }

        private void LinearGradient_OnClick(object sender, RoutedEventArgs e)
        {
            _cssColorType.ColorType = "Linear";
            CssClassesToolControl.Context.SaveChanges();
            Load();
        }

        private void AddLinearStopButton_OnClick(object sender, RoutedEventArgs e)
        {
            var arnd = new Random();
            double bb = bb = arnd.Next(1000) / 10.0;


            //    var color = CssClassesToolControl.Context.CssColors.Create();
            var color = new CssColor();
            color.ColorValue = Colors.LightGray.ToString();
            color.CssColorTypeId = _cssColorType.Id;
            color.Stop = bb.ToString("F");
            color.ColorOrder = ColorItems.Count + 10;
            color.Id = FindNextCssColorId();
            CssClassesToolControl.Context.CssColors.Add(color);
            CssClassesToolControl.Context.SaveChanges();
            Load();
        }

        private void RadialShapeBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var val = (RadialShapeBox.SelectedItem as TextBlock).Text;
                if (_cssColorType != null && val != _radialShape)
                {
                    _cssColorType.Shape = val;
                    _radialShape = val;
                    CssClassesToolControl.Context.SaveChanges();
                    Load();
                }
            }
        }

        private void RadialCenterBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var val = (RadialCenterBox.SelectedItem as TextBlock).Text;

            if (_cssColorType != null && val != _radialCenter)
            {
                _cssColorType.Center = val;
                _radialCenter = val;

                CssClassesToolControl.Context.SaveChanges();
                Load();
            }
        }

        private void RadialDistanceBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var val = (RadialDistanceBox.SelectedItem as TextBlock).Text;
            if (_cssColorType != null && val != _radialSize)
            {
                _cssColorType.Size = val;
                _radialSize = val;
                CssClassesToolControl.Context.SaveChanges();
                Load();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }




        #endregion

    }
}