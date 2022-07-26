using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.LanguageServer.Protocol;
using Microsoft.VisualStudio.Shell.Interop;
using SvgComposition.Model;
using SvgCompositionTool.Dialogs;

namespace SvgComposition.Controls
{
    /// <summary>
    /// Interaction logic for SimpleLine.xaml
    /// </summary>
    public partial class SvgLine : UserControl, INotifyPropertyChanged
    {
        private int _lineX1 = 0;
        private int _lineX2 = 0;
        private int _lineY1 = 0;
        private int _lineY2 = 0;
        private double _strokeWidth = 0;
        private string _stroke = string.Empty;
        private SvgElement _lineElement = null;
        private string _strokeDashArray = "none";
        private string _lineCap = string.Empty;
        private string _svgElementName;
        private string _fillOpacity;
        private string _strokeOpacity;


        public SvgLine()
        {

            this.Visibility = Visibility.Visible;
            this.BorderBrush = new SolidColorBrush(Colors.Black);
            this.BorderThickness = new Thickness(1.0);
            InitializeComponent();
            if (StrokeButton == null)
            {
                StrokeButton = new ColorButton();
            }

            StrokeButton.ButtonText = "Stroke";
            StrokeButton.ChosenColor = Colors.DarkOrchid;
            StrokeButton.Width = 200;



            DataContext = this;
        }

        public SvgLine(int key)
        {
            InitializeComponent();
            DataContext = this;

            IEnumerable<SvgElement> elin = from qq in SvgCompositionControl.Context.SvgElements
                                           where qq.Id == key
                                           select qq;

            if (elin.Count() == 1)
            {
                LineElement = elin.First();
            }


        }

        public int LineX1
        {
            get => _lineX1;
            set
            {
                _lineX1 = value;
                OnPropertyChanged();

            }
        }

        public int LineX2
        {
            get => _lineX2;
            set
            {
                _lineX2 = value;
                OnPropertyChanged();
            }
        }

        public int LineY1
        {
            get => _lineY1;
            set
            {
                _lineY1 = value;
                OnPropertyChanged();
            }
        }

        public int LineY2
        {
            get => _lineY2;
            set
            {
                _lineY2 = value;
                OnPropertyChanged();
            }
        }

        public double StrokeWidth
        {
            get => _strokeWidth;
            set
            {
                _strokeWidth = value;
                OnPropertyChanged();
            }
        }

        public string Stroke
        {
            get => _stroke;
            set
            {
                _stroke = value;
                OnPropertyChanged();
            }
        }

        public string StrokeDashArray
        {
            get => _strokeDashArray;
            set
            {
                _strokeDashArray = value;
                OnPropertyChanged();
            }
        }

        public string LineCap
        {
            get => _lineCap;
            set
            {
                _lineCap = value;
                OnPropertyChanged();
            }
        }

        public string FillOpacity
        {
            get { return _fillOpacity; }
            set
            {
                _fillOpacity = value;
                OnPropertyChanged();
            }
        }

        public string StrokeOpacity
        {
            get { return _strokeOpacity; }
            set
            {
                _strokeOpacity = value;
                OnPropertyChanged();
            }
        }

        private void LineCapBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LineCap = (LineCapBox.SelectedItem as TextBlock).Text;
        }

        public string SvgElementName
        {
            get => _svgElementName;
            set
            {
                _svgElementName = value;
                OnPropertyChanged();
            }
        }

        public SvgElement LineElement
        {
            get => _lineElement;
            set
            {

                _lineElement = value;

                if (_lineElement == null)
                {
                    LineX1 = 0;
                    LineX2 = 0;
                    LineY1 = 0;
                    LineY2 = 0;
                    LineCap = string.Empty;
                    StrokeDashArray = "none";
                    Stroke = Colors.DarkOrchid.ToString();
                    StrokeWidth = 0.0;
                    LineCap = "square";
                    SvgElementName = "Unknown";

                }
                else
                {
                    StrokeButton.IsEnabled = true;
                    LoadLineElement();
                }
                OnPropertyChanged();
            }

        }

        void LoadLineElement()
        {
            string aa;
            IEnumerable<string> gg;
            IEnumerable<SvgAttribute> nn;
            SvgAttribute qq;

            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "x1"
                 select vv.AttributeValue;
            aa = gg.First();
            if (string.IsNullOrWhiteSpace(aa))
            {
                aa = "0";
            }

            LineX1 = int.Parse(aa);

            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "x2"
                 select vv.AttributeValue;
            aa = gg.First();
            if (string.IsNullOrWhiteSpace(aa))
            {
                aa = "0";
            }

            LineX2 = int.Parse(aa);

            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "y1"
                 select vv.AttributeValue;
            aa = gg.First();
            if (string.IsNullOrWhiteSpace(aa))
            {
                aa = "0";
            }

            LineY1 = int.Parse(aa);

            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "y2"
                 select vv.AttributeValue;
            aa = gg.First();
            if (string.IsNullOrWhiteSpace(aa))
            {
                aa = "0";
            }

            LineY2 = int.Parse(aa);

            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "stroke-width"
                 select vv.AttributeValue;
            aa = gg.First();
            if (string.IsNullOrWhiteSpace(aa))
            {
                aa = "0";
            }

            StrokeWidth = double.Parse(aa);

            // stroke-opacity
            gg = from vv in _lineElement.SvgAttributes
                where vv.AttributeName == "stroke-opacity"
                select vv.AttributeValue;
            aa = gg.First();
            if (string.IsNullOrWhiteSpace(aa))
            {
                aa = "1.0";
            }

            StrokeOpacity = aa;

            // fill-opacity
            gg = from vv in _lineElement.SvgAttributes
                where vv.AttributeName == "fill-opacity"
                select vv.AttributeValue;
            aa = gg.First();
            if (string.IsNullOrWhiteSpace(aa))
            {
                aa = "1.0";
            }

            FillOpacity = aa;


            nn = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "stroke"
                 select vv;
            qq = nn.First();

            if (string.IsNullOrWhiteSpace(qq.AttributeValue))
            {
                Color ts = Colors.Black;
                SvgColor col = new SvgColor(ts) {SvgAttributeId = qq.Id};
                qq.SvgColors.Clear();
                qq.SvgColors.Add(col);
                qq.AttributeValue = col.ToString();

            }

            Stroke = qq.AttributeValue;

            StrokeButton.ButtonText = "stroke";
            StrokeButton.ChosenColor = qq.SvgColors.First().Color();
            StrokeButton.InSvgColor = qq.SvgColors.First();

            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "stroke-dasharray"
                 select vv.AttributeValue;
            aa = gg.First();
            if (string.IsNullOrWhiteSpace(aa))
            {
                aa = string.Empty;
            }

            StrokeDashArray = aa;

            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "stroke-linecap"
                 select vv.AttributeValue;
            aa = gg.First();
            if (string.IsNullOrWhiteSpace(aa))
            {
                aa = string.Empty;
            }

            LineCap = aa;


            SvgElementName= LineElement.SvgElementName;
            OnPropertyChanged();

        }

        void Save()
        {

            IEnumerable<SvgAttribute> gg;
            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "x1"
                 select vv;
            gg.First().AttributeValue = $"{LineX1}";
            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "x2"
                 select vv;
            gg.First().AttributeValue = $"{LineX2}";
            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "y1"
                 select vv;
            gg.First().AttributeValue = $"{LineY1}";
            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "y2"
                 select vv;
            gg.First().AttributeValue = $"{LineY2}";

            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "stroke-width"
                 select vv;
            gg.First().AttributeValue = $"{StrokeWidth}";

            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "stroke"
                 select vv;
            gg.First().AttributeValue = StrokeButton.ChosenColor.ToString();
            SvgColor nn = gg.First().SvgColors.First();
            nn.ResetColor(StrokeButton.ChosenColor);

            gg = from vv in _lineElement.SvgAttributes
                where vv.AttributeName == "stroke-opacity"
                select vv;
            gg.First().AttributeValue = StrokeOpacity;

            gg = from vv in _lineElement.SvgAttributes
                where vv.AttributeName == "fill-opacity"
                select vv;
            gg.First().AttributeValue = FillOpacity;


            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "stroke-dasharray"
                 select vv;
            gg.First().AttributeValue = StrokeDashArray;

            gg = from vv in _lineElement.SvgAttributes
                 where vv.AttributeName == "stroke-linecap"
                 select vv;
            gg.First().AttributeValue = LineCap;



            LineElement.SvgElementName = SvgElementName;

            SvgCompositionControl.Context.SaveChanges();
        }


      

        private SvgAttribute CreateSvgAttribute(string name, string val, SvgElement el)
        {
            SvgAttribute it = new SvgAttribute();
            it.Id = SvgBuildControl.FindNextSvgAttributeId();
            it.AttributeName = name;
            it.AttributeValue = val;
            it.SvgElementId = el.Id;
            el.SvgAttributes.Add(it);

            SvgCompositionControl.Context.SvgAttributes.Add(it);
            SvgCompositionControl.Context.SaveChanges();

            var it1 = it;
            var ff = from aa in SvgCompositionControl.Context.SvgAttributes
                     where aa.Id == it1.Id
                     select aa;

            it = ff.First();

            return (it);

        }

        private void CreateSvgColor(System.Windows.Media.Color col, SvgAttribute att)
        {
            SvgColor cc = new SvgColor(col);

            cc.Id = SvgBuildControl.FindNextSvgColorId();
            int n = att.SvgColors.Count;
            cc.ColorIndex = n;
            cc.SvgAttributeId = att.Id;
            cc.SvgAttribute = att;
            att.SvgColors.Add(cc);

            SvgCompositionControl.Context.SvgColors.Add(cc);

            SvgCompositionControl.Context.SaveChanges();


        }

        private void CreateLineButton_OnClick(object sender, RoutedEventArgs e)
        {
            SvgElement element = new SvgElement();
            element.Id = SvgBuildControl.FindNextSvgElementId();

            element.SvgElementName = SvgBuildControl.CurrentTopElement.SvgElementName + $"_Line{element.Id}";
            element.ElementType = SvgElementType.Line;
            element.SvgParentElementId = SvgBuildControl.CurrentTopElement.Id;
            element.SvgParentElement = SvgBuildControl.CurrentTopElement;
            element.ElementOrder = SvgBuildControl.CurrentTopElement.SvgElements.Count;
            SvgBuildControl.CurrentTopElement.SvgElements.Add(element);

            SvgCompositionControl.Context.SvgElements.Add(element);
            SvgCompositionControl.Context.SaveChanges();

            CreateSvgAttribute("x1", $"0", element);
            CreateSvgAttribute("y1", $"0", element);
            CreateSvgAttribute("x2", $"0", element);
            CreateSvgAttribute("y2", $"0", element);
            CreateSvgAttribute("stroke-width", $"1", element);
            CreateSvgAttribute("stroke-linecap", "square", element);
            CreateSvgAttribute("stroke-dasharray", "none", element);
            CreateSvgAttribute("stroke-opacity", $"1.0", element);
            CreateSvgAttribute("fill-opacity", $"1.0", element);

            SvgAttribute fc = CreateSvgAttribute("stroke", System.Windows.Media.Colors.Black.ToString(), element);

            CreateSvgColor(System.Windows.Media.Colors.Black, fc);

           
            


            SvgCompositionControl.Context.SaveChanges();

            LineElement = element;
        }

        public void DeleteAttribute(SvgAttribute att)
        {
            foreach( SvgColor cc in att.SvgColors)
            {
                att.SvgColors.Remove(cc);
                SvgCompositionControl.Context.SvgColors.Remove(cc);
            }

            LineElement.SvgAttributes.Remove(att);
            SvgCompositionControl.Context.SvgAttributes.Remove(att);
        }


        private void RemoveLineButton_OnClick(object sender, RoutedEventArgs e)
        {

            // are you sure??
            if (LineElement == null)
            {
                return;
            }

            foreach (SvgAttribute att in LineElement.SvgAttributes)
            {
                DeleteAttribute(att);
            }

            var pp = from qq in SvgCompositionControl.Context.SvgElements
                where qq.Id == LineElement.SvgParentElementId
                     select qq;

            SvgElement owner = pp.First();
            
            SvgCompositionControl.Context.SvgElements.Remove(LineElement);
            LineElement = null;
            SvgCompositionControl.Context.SaveChanges();
        }

        private void SaveLineButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (LineElement == null)
            {
                return;
            }
            Save();
        }

        #region erase


        private void DeleteLineX2_OnClick(object sender, RoutedEventArgs e)
        {
            LineX2 = 0;
            OnPropertyChanged();
        }

        private void DeleteLineY1_OnClick(object sender, RoutedEventArgs e)
        {
            LineY1 = 0;
            OnPropertyChanged();
        }

        private void DeleteLineY2_OnClick(object sender, RoutedEventArgs e)
        {
            LineY1 = 0;
            OnPropertyChanged();
        }

        private void DeleteLineX1_OnClick(object sender, RoutedEventArgs e)
        {
            LineX1 = 0;
            OnPropertyChanged();
        }

        private void DeleteLine_Cap_OnClick(object sender, RoutedEventArgs e)
        {
            LineCap = "butt";
        }

        private void DeleteStrokeWidth_OnClick(object sender, RoutedEventArgs e)
        {
            _strokeWidth = 0.0;

        }

        private void DeleteStrokeDashArray_OnClick(object sender, RoutedEventArgs e)
        {
            _strokeDashArray = "none";
        }

        #endregion

        private void Expander_OnCollapsed(object sender, RoutedEventArgs e)
        {
            // Show Svg text here
            this.InvalidateVisual();
        }

        private void Expander_OnExpanded(object sender, RoutedEventArgs e)
        {
            // Show Svg text here
            this.InvalidateVisual();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


      
    }
}
