using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SvgComposition.Model;
using SvgCompositionTool.Dialogs;

namespace SvgComposition.Controls
{
    /// <summary>
    /// Interaction logic for Svgellipse.xaml
    /// </summary>
    public partial class SvgEllipse : UserControl, INotifyPropertyChanged
    {
        private string _cx = "0";
      
        private string _cy = "0";
        private string _rx = "0";
        private string _ry = "0";
        private string _strokeWidth = "0";
        private string _strokeDashArray = "none";
        private string _stroke = Colors.Black.ToString();
        private string _fill = Colors.Transparent.ToString();
        private SvgElement _ellipseElement;
        private string _svgElementName;
        private string _fillOpacity;
        private string _strokeOpacity;
        

        public SvgEllipse()
        {
           
            this.Visibility = Visibility.Visible;
            this.BorderBrush = new SolidColorBrush(Colors.Black);
            this.BorderThickness = new Thickness(1.0);
            InitializeComponent();
            if (StrokeButton == null)
            {
                StrokeButton = new ColorButton();
            }

            StrokeButton.Width = 120;
            StrokeButton.ButtonText = "Stroke";
            StrokeButton.ChosenColor = Colors.Black;

            if (FillButton == null)
            {
                FillButton = new ColorButton();
            }
            FillButton.Width = 120;
            FillButton.ButtonText = "Fill";
            FillButton.ChosenColor = Colors.Transparent;
            DataContext = this;

            
        }

        public string Cx
        {
            get { return _cx; }
            set
            {
                _cx = value;
                OnPropertyChanged();
            }
        }

        public string Cy
        {
            get { return _cy; }
            set
            {
                _cy = value;
                OnPropertyChanged();
            }
        }

       

        public string Rx
        {
            get { return _rx; }
            set
            {
                _rx = value;
                OnPropertyChanged();
            }
        }

        public string Ry
        {
            get { return _ry; }
            set
            {
                _ry = value;
                OnPropertyChanged();
            }
        }

        public string StrokeWidth
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

        public string Fill
        {
            get => _fill;
            set
            {
                _fill = value;
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

        public string SvgElementName
        {
            get => _svgElementName;
            set
            {
                _svgElementName = value;
                OnPropertyChanged();
            }
        }

        public SvgElement EllipseElement
        {
            get => _ellipseElement;
            set
            {
                _ellipseElement = value;

                if (_ellipseElement == null)
                {
                    Cx = "0";
                  
                    Cy = "0";
                    Rx = "0";
                    Ry = "0";
                    StrokeDashArray = "none";
                    Stroke = Colors.Black.ToString();
                    Fill = Colors.Transparent.ToString();
                    FillButton.IsEnabled = false;
                    FillOpacity = "1.0";
                    StrokeOpacity = "1.0";
                    StrokeButton.IsEnabled = false;
                }
                else
                {

                    FillButton.IsEnabled = true;
                    StrokeButton.IsEnabled = true;
                    LoadEllipseElement();
                }
                
            }
        }



        public void LoadEllipseElement()
        {
          //  string aa;
          //  IEnumerable<string> gg;
            IEnumerable<SvgAttribute> nn;
            SvgAttribute qq;
// Cx
            nn = from vv in _ellipseElement.SvgAttributes
                 where vv.AttributeName == "cx"
                 select vv;
            qq = nn.First();
            if (string.IsNullOrWhiteSpace(qq.AttributeValue))
            {
                qq.AttributeValue = "0";
                qq.SvgUnit = "px";
            }

            Cx = qq.AttributeValue;
        //    CxUnit.IsUnitText = SvgAttribute.SvgAttributeUnitToText(qq.SvgUnit);

// Cy
            nn = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "cy"
                select vv;
            qq = nn.First();
            if (string.IsNullOrWhiteSpace(qq.AttributeValue))
            {
                qq.AttributeValue = "0";
                qq.SvgUnit = "px";
            }

            Cy = qq.AttributeValue;
         //   CyUnit.IsUnitText = SvgAttribute.SvgAttributeUnitToText(qq.SvgUnit);

// Rx
            nn = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "rx"
                select vv;
            qq = nn.First();
            if (string.IsNullOrWhiteSpace(qq.AttributeValue))
            {
                qq.AttributeValue = "0";
                qq.SvgUnit = "px";
            }

            Rx = qq.AttributeValue;
         //   RxUnit.IsUnitText = SvgAttribute.SvgAttributeUnitToText(qq.SvgUnit);

            // Ry
            nn = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "ry"
                select vv;
            qq = nn.First();
            if (string.IsNullOrWhiteSpace(qq.AttributeValue))
            {
                qq.AttributeValue = "0";
                qq.SvgUnit = "px";
            }

            Ry = qq.AttributeValue;
        //    RyUnit.IsUnitText = SvgAttribute.SvgAttributeUnitToText(qq.SvgUnit);

            nn = from vv in _ellipseElement.SvgAttributes
                 where vv.AttributeName == "stroke-width"
                 select vv;
            qq = nn.First();
            if (string.IsNullOrWhiteSpace(qq.AttributeValue) || 
                qq.AttributeType == SvgAttributeType.Default)
            {
                qq.AttributeValue = "1";
                qq.SvgUnit = "px";
                qq.AttributeType = SvgAttributeType.Value;
            }

            StrokeWidthType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((qq.AttributeType));
     //       StrokeWidthUnit.IsUnitText = SvgAttribute.SvgAttributeUnitToText(qq.SvgUnit);

            if (qq.AttributeType == SvgAttributeType.Value)
            {
                StrokeWidth = qq.AttributeValue;
            }

           
            //stroke
            nn = from vv in _ellipseElement.SvgAttributes
                 where vv.AttributeName == "stroke"
                 select vv;
            qq = nn.First();

            if (string.IsNullOrWhiteSpace(qq.AttributeValue) ||
                qq.AttributeType == SvgAttributeType.Default)
            {
                Color ts = Colors.Black;
                qq.AttributeValue = ts.ToString();
                qq.AttributeType = SvgAttributeType.Value;
            }

            StrokeType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((qq.AttributeType));
            Stroke = qq.AttributeValue;

            StrokeButton.ButtonText = "stroke";
            StrokeButton.ChosenColor = qq.SvgColors.First().Color();
            StrokeButton.InSvgColor = qq.SvgColors.First();


            // stroke-dasharray
            nn = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "stroke-dasharray"
                 select vv;
            qq = nn.First();
            if (string.IsNullOrWhiteSpace(qq.AttributeValue) ||
                qq.AttributeType == SvgAttributeType.Default)
            {
                qq.AttributeValue = string.Empty;
                qq.AttributeType = SvgAttributeType.Value;
            }

            StrokeDashArrayType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((qq.AttributeType));
            StrokeDashArray = qq.AttributeValue;

            // stroke-opacity
            nn = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "stroke-opacity"
                 select vv;
            qq = nn.First();
            if (string.IsNullOrWhiteSpace(qq.AttributeValue) ||
                qq.AttributeType == SvgAttributeType.Default)
            {
                qq.AttributeValue = "1.0";
                qq.SvgUnit = "px";
                qq.AttributeType = SvgAttributeType.Value;
            }

            StrokeOpacityType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((qq.AttributeType));
            StrokeOpacity = qq.AttributeValue;

            // fill-opacity
            nn = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "fill-opacity"
                 select vv;
            qq = nn.First();
            if (string.IsNullOrWhiteSpace(qq.AttributeValue) ||
                qq.AttributeType == SvgAttributeType.Default)
            {
                qq.AttributeValue = "1.0";
             //   qq.SvgUnit = SvgAttributeUnit.Px;
                qq.AttributeType = SvgAttributeType.Value;
            }

            FillOpacityType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((qq.AttributeType));
            FillOpacity = qq.AttributeValue;

            // fill
            nn = from vv in _ellipseElement.SvgAttributes
                 where vv.AttributeName == "fill"
                 select vv;
            qq = nn.First();

            if (string.IsNullOrWhiteSpace(qq.AttributeValue))
            {
                Color ts = Colors.Transparent;  
                qq.AttributeValue = ts.ToString();
                qq.AttributeType = SvgAttributeType.Value;
            }

            Fill = qq.AttributeValue;
            FillType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((qq.AttributeType));            

            FillButton.ButtonText = "fill";

            FillButton.ChosenColor = qq.SvgColors.First().Color();
            FillButton.InSvgColor = qq.SvgColors.First();

            SvgElementName  = EllipseElement.SvgElementName;

            OnPropertyChanged();
        }

        void Save()
        {

            IEnumerable<SvgAttribute> gg;
            SvgAttribute sa = null;

            gg = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "cx"
                select vv;
            sa = gg.First();
            sa.AttributeValue = Cx;
        //    sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(CxUnit.IsUnitText);
           

            gg = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "cy"
                select vv;
            sa = gg.First();
            sa.AttributeValue = Cy;
         //   sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(CyUnit.IsUnitText);

            gg = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "rx"
                select vv;
            sa = gg.First();
            sa.AttributeValue = Rx;
        //    sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(RxUnit.IsUnitText);
           
            gg = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "ry"
                select vv;
            sa = gg.First();
            sa.AttributeValue = Ry;
       //     sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(RyUnit.IsUnitText);
          

            gg = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "stroke-width"
                select vv;
            sa = gg.First();
            sa.AttributeValue = StrokeWidth;
      //     sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(StrokeWidthUnit.IsUnitText);
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(StrokeWidthType.IsTypeText);

            gg = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "stroke"
                select vv;
            sa = gg.First();
          //  sa.AttributeValue = StrokeButton.ChosenColor.ToString();
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(StrokeType.IsTypeText);
           
            SvgColor nn = sa.SvgColors.First();
            nn.ResetColor(StrokeButton.ChosenColor);
            sa.AttributeValue = nn.ColorValue;

            gg = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "stroke-dasharray"
                select vv;
            sa = gg.First();
            sa.AttributeValue = StrokeDashArray;
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(StrokeDashArrayType.IsTypeText);

            gg = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "stroke-opacity"
                select vv;
            sa = gg.First();
            sa.AttributeValue = StrokeOpacity;
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(StrokeOpacityType.IsTypeText);

           

            gg = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "fill-opacity"
                select vv;
            sa = gg.First();
            sa.AttributeValue = FillOpacity;
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(FillOpacityType.IsTypeText);


            gg = from vv in _ellipseElement.SvgAttributes
                where vv.AttributeName == "fill"
                select vv;
            sa = gg.First();
          //  sa.AttributeValue = FillButton.ChosenColor.ToString();
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(FillType.IsTypeText);
            SvgColor nnn = sa.SvgColors.First();
            nnn.ResetColor(FillButton.ChosenColor);
            sa.AttributeValue = nn.ColorValue;

            EllipseElement.SvgElementName = SvgElementName;


            SvgCompositionControl.Context.SaveChanges();
        }

        

        private SvgAttribute CreateSvgAttribute(string name, string val, bool col, bool tty, bool un, SvgElement el)
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

        public string Xrgb(System.Windows.Media.Color col)
        {
            return ($"#{col.R:x2}{col.G:x2}{col.B:x2}");
        }

       

        private void CreateEllipseButton_OnClick(object sender, RoutedEventArgs e)
        {
            SvgElement element = new SvgElement();
            element.Id = SvgBuildControl.FindNextSvgElementId();

            element.SvgElementName = SvgBuildControl.CurrentTopElement.SvgElementName + $"_ellipse{element.Id}";
            element.ElementType = SvgElementType.Ellipse;
            element.SvgParentElementId = SvgBuildControl.CurrentTopElement.Id;
            element.SvgParentElement = SvgBuildControl.CurrentTopElement;
            element.ElementOrder = SvgBuildControl.CurrentTopElement.SvgElements.Count;
            SvgBuildControl.CurrentTopElement.SvgElements.Add(element);

            SvgCompositionControl.Context.SvgElements.Add(element);
            SvgCompositionControl.Context.SaveChanges();

         

            CreateSvgAttribute("cx", $"0", false, false, true, element);     
            CreateSvgAttribute("cy", $"0", false, false, true, element);
            CreateSvgAttribute("rx", $"0", false, false, true, element);
            CreateSvgAttribute("ry", $"0", false, false, true, element);
            CreateSvgAttribute("stroke-dasharray", "none", false, true, true, element);
            CreateSvgAttribute("stroke-width", $"1", false, true, true, element);
            CreateSvgAttribute("stroke-opacity", "1.0", false, true, false, element);
            CreateSvgAttribute("fill-opacity", "1.0", false, true, false, element);

            string xcol = Xrgb(System.Windows.Media.Colors.Gainsboro);
            SvgAttribute fc = CreateSvgAttribute("fill", xcol, true, true, false, element);
            CreateSvgColor(System.Windows.Media.Colors.Gainsboro, fc);
            fc.AttributeValue = fc.SvgColors.First().ColorValue;

            xcol = Xrgb(System.Windows.Media.Colors.Gainsboro);
            fc = CreateSvgAttribute("stroke", xcol, true, true, false, element);
            CreateSvgColor(System.Windows.Media.Colors.Black, fc);
            fc.AttributeValue = fc.SvgColors.First().ColorValue;

            SvgCompositionControl.Context.SaveChanges();

            EllipseElement = element;
        }

        private void SaveEllipseButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (EllipseElement != null)
            {
                Save();
            }
        }

        public void DeleteAttribute(SvgAttribute att)
        {
            foreach (SvgColor cc in att.SvgColors)
            {
                att.SvgColors.Remove(cc);
                SvgCompositionControl.Context.SvgColors.Remove(cc);
            }

            EllipseElement.SvgAttributes.Remove(att);
            SvgCompositionControl.Context.SvgAttributes.Remove(att);
        }

        private void RemoveEllipseButton_OnClick(object sender, RoutedEventArgs e)
        {
            // are you sure??
            if (EllipseElement == null)
            {
                return;
            }

            foreach (SvgAttribute att in EllipseElement.SvgAttributes)
            {
                DeleteAttribute(att);
            }

            var pp = from qq in SvgCompositionControl.Context.SvgElements
                where qq.Id == EllipseElement.SvgParentElementId
                     select qq;

            SvgElement owner = pp.First();
            
            SvgCompositionControl.Context.SvgElements.Remove(EllipseElement);
            EllipseElement = null;
            SvgCompositionControl.Context.SaveChanges();
        }

       

     
      
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


        private void DeleteFillOpacity_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
