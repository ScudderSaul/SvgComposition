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
    /// Interaction logic for SvgRect.xaml
    /// </summary>
    public partial class SvgRect : UserControl, INotifyPropertyChanged
    {
        private string _xx = "0";
        private string _rectWidth = "0";
        private string _yy = "0";
        private string _rectHeight = "0";
        private string _rx = "0";
        private string _ry = "0";
        private string _strokeWidth = "0";
        private string _stroke = string.Empty;
        private string _fill;
        private SvgElement _rectElement;
        private string _svgElementName;
        private string _fillOpacity;
        private string _strokeOpacity;

        public SvgRect()
        {
           
            this.Visibility = Visibility.Visible;
            this.BorderBrush = new SolidColorBrush(Colors.Black);
            this.BorderThickness = new Thickness(1.0);
            InitializeComponent();
            if (StrokeButton == null)
            {
                StrokeButton = new ColorButton();
            }

            StrokeButton.Width = 200;
            StrokeButton.ButtonText = "Stroke";
            StrokeButton.ChosenColor = Colors.Black;

            if (FillButton == null)
            {
                FillButton = new ColorButton();
            }
            FillButton.Width = 200;
            FillButton.ButtonText = "Fill";
            FillButton.ChosenColor = Colors.Transparent;
            DataContext = this;
        }

        public string Xx
        {
            get { return _xx; }
            set
            {
                _xx = value;
                OnPropertyChanged();
            }
        }

        public string Yy
        {
            get { return _yy; }
            set
            {
                _yy = value;
                OnPropertyChanged();
            }
        }

        public string RectWidth
        {
            get { return _rectWidth; }
            set
            {
                _rectWidth = value;
                OnPropertyChanged();
            }
        }

        public string RectHeight
        {
            get { return _rectHeight; }
            set
            {
                _rectHeight = value;
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

        public string Fill
        {
            get => _fill;
            set
            {
                _fill = value;
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

        public SvgElement RectElement
        {
            get => _rectElement;
            set
            {
                _rectElement = value;

                if (_rectElement == null)
                {
                    Xx = "0";
                    RectWidth = "0";
                    Yy = "0";
                    RectHeight = "0";
                    Stroke = Colors.Black.ToString();
                    Fill = Colors.Transparent.ToString();
                    FillButton.IsEnabled = false;
                    StrokeButton.IsEnabled = false;
                    FillOpacity = "1.0";
                    StrokeOpacity = "1.0";
                }
                else
                {
                    FillButton.IsEnabled = true;
                    StrokeButton.IsEnabled = true;
                    LoadRectElement();
                }
                
            }
        }



        public void LoadRectElement()
        {

            IEnumerable<SvgAttribute> nn;
            SvgAttribute sa;

            nn = from vv in _rectElement.SvgAttributes
                 where vv.AttributeName == "x"
                 select vv;
            sa = nn.First();
            if (string.IsNullOrWhiteSpace(sa.AttributeValue))
            {
                sa.AttributeValue = "0";
            }

            Xx = sa.AttributeValue;
       //     XxUnit.IsUnitText = SvgAttribute.SvgAttributeUnitToText(sa.SvgUnit);

            nn = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "width"
                select vv;
            sa = nn.First();
            if (string.IsNullOrWhiteSpace(sa.AttributeValue))
            {
                sa.AttributeValue = "0";
            }

            RectWidth = sa.AttributeValue;
         //   WidthUnit.IsUnitText = SvgAttribute.SvgAttributeUnitToText(sa.SvgUnit);

            nn = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "y"
                select vv;
            sa = nn.First();
            if (string.IsNullOrWhiteSpace(sa.AttributeValue))
            {
                sa.AttributeValue = "0";
            }

            Yy = sa.AttributeValue;
         //   YyUnit.IsUnitText = SvgAttribute.SvgAttributeUnitToText(sa.SvgUnit);

            nn = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "height"
                select vv;
            sa = nn.First();
            if (string.IsNullOrWhiteSpace(sa.AttributeValue))
            {
                sa.AttributeValue = "0";
            }

            RectHeight = sa.AttributeValue;
         //   HeightUnit.IsUnitText = SvgAttribute.SvgAttributeUnitToText(sa.SvgUnit);

            nn = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "stroke-width"
                select vv;
            sa = nn.First();
            if (string.IsNullOrWhiteSpace(sa.AttributeValue) ||
               sa.AttributeType == SvgAttributeType.Default)
            {
                sa.AttributeValue = "1";
                sa.SvgUnit = "px";
                sa.AttributeType = SvgAttributeType.Value;
            }

            StrokeWidthType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((sa.AttributeType));
         //   StrokeWidthUnit.IsUnitText = SvgAttribute.SvgAttributeUnitToText(sa.SvgUnit);

            if (sa.AttributeType == SvgAttributeType.Value)
            {
                StrokeWidth = sa.AttributeValue;
            }




            // stroke-opacity
            nn = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "stroke-opacity"
                select vv;
            sa = nn.First();
            if (string.IsNullOrWhiteSpace(sa.AttributeValue) ||
                sa.AttributeType == SvgAttributeType.Default)
            {
                sa.AttributeValue = "1.0";
                sa.SvgUnit ="px";
                sa.AttributeType = SvgAttributeType.Value;
            }

            StrokeOpacityType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((sa.AttributeType));
            StrokeOpacity = sa.AttributeValue;

            // fill-opacity
            nn = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "fill-opacity"
                select vv;
            sa = nn.First();
            if (string.IsNullOrWhiteSpace(sa.AttributeValue) ||
                sa.AttributeType == SvgAttributeType.Default)
            {
                sa.AttributeValue = "1.0";
                sa.SvgUnit = "px";
                sa.AttributeType = SvgAttributeType.Value;
            }

            FillOpacityType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((sa.AttributeType));
            FillOpacity = sa.AttributeValue;

            //stroke
            nn = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "stroke"
                select vv;
            sa = nn.First();

            if (string.IsNullOrWhiteSpace(sa.AttributeValue) ||
                sa.AttributeType == SvgAttributeType.Default)
            {
                Color ts = Colors.Black;
                sa.AttributeValue = ts.ToString();
                sa.AttributeType = SvgAttributeType.Value;
            }

            StrokeType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((sa.AttributeType));
            Stroke = sa.AttributeValue;

            StrokeButton.ButtonText = "stroke";
            StrokeButton.ChosenColor = sa.SvgColors.First().Color();
            StrokeButton.InSvgColor = sa.SvgColors.First();

            // fill
            nn = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "fill"
                select vv;
            sa = nn.First();

            if (string.IsNullOrWhiteSpace(sa.AttributeValue))
            {
                Color ts = Colors.Transparent;
                sa.AttributeValue = ts.ToString();
                sa.AttributeType = SvgAttributeType.Value;
            }

            Fill = sa.AttributeValue;
            FillType.IsTypeText = SvgAttribute.SvgAttributeTypeToText((sa.AttributeType));

            FillButton.ButtonText = "fill";
            FillButton.ChosenColor = sa.SvgColors.First().Color();
            FillButton.InSvgColor = sa.SvgColors.First();


            nn = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "rx"
                select vv;
            sa = nn.First(); 
            sa.AttributeValue = Rx;
          //  sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(RxUnit.IsUnitText);

           nn = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "ry"
                select vv;
            sa = nn.First();
            sa.AttributeValue = Ry;
         //   sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(RyUnit.IsUnitText);


            SvgElementName = RectElement.SvgElementName;

            OnPropertyChanged();
        }

        void Save()
        {
            IEnumerable<SvgAttribute> gg;
            SvgAttribute sa = null;

            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "x"
                select vv;
            sa = gg.First();
            sa.AttributeValue = Xx;
         //   sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(XxUnit.IsUnitText);


            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "width"
                select vv;
            sa = gg.First();
            sa.AttributeValue = RectWidth;
         //   sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(WidthUnit.IsUnitText);




            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "y"
                select vv;
            sa = gg.First();
            sa.AttributeValue = Yy;
            sa.AttributeType = SvgAttributeType.Value;
          //  sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(YyUnit.IsUnitText);

            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "height"
                select vv;
            sa = gg.First();
            sa.AttributeValue = RectHeight;
            sa.AttributeType = SvgAttributeType.Value;
        //    sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(HeightUnit.IsUnitText);


            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "stroke-width"
                select vv;
            sa = gg.First();
            sa.AttributeValue = StrokeWidth;
         //   sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(StrokeWidthUnit.IsUnitText);
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(StrokeWidthType.IsTypeText);

            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "stroke-opacity"
                select vv;
            sa = gg.First();
            sa.AttributeValue = StrokeOpacity;
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(StrokeOpacityType.IsTypeText);

            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "fill-opacity"
                select vv;
            sa = gg.First();
            sa.AttributeValue = FillOpacity;
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(FillOpacityType.IsTypeText);

            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "rx"
                select vv;
            sa = gg.First();
            sa.AttributeValue = Rx;
       //     sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(RxUnit.IsUnitText);

            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "ry"
                select vv;
            sa = gg.First();
            sa.AttributeValue = Ry;
         //   sa.SvgUnit = SvgAttribute.TextToSvgAttributeUnit(RxUnit.IsUnitText);


            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "stroke"
                select vv;
            sa = gg.First();
            sa.AttributeValue = StrokeButton.ChosenColor.ToString();
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(StrokeType.IsTypeText);

            SvgColor nn = sa.SvgColors.First();
            nn.ResetColor(StrokeButton.ChosenColor);

            gg = from vv in _rectElement.SvgAttributes
                where vv.AttributeName == "fill"
                select vv;
            sa = gg.First();
            sa.AttributeValue = FillButton.ChosenColor.ToString();
            sa.AttributeType = SvgAttribute.TextToSvgAttributeType(FillType.IsTypeText);

            SvgColor nnn = sa.SvgColors.First();
            nnn.ResetColor(FillButton.ChosenColor);

            RectElement.SvgElementName = SvgElementName;


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

     

        private void SaveRectButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (RectElement != null)
            {
                Save();
            }
        }

        public void DeleteAttribute(SvgAttribute att)
        {
            foreach (SvgColor cc in att.SvgColors)
            {
              //  att.SvgColors.Remove(cc);
                SvgCompositionControl.Context.SvgColors.Remove(cc);
            }

          //  RectElement.SvgAttributes.Remove(att);
            SvgCompositionControl.Context.SvgAttributes.Remove(att);
          
        }

        private void RemoveRectButton_OnClick(object sender, RoutedEventArgs e)
        {
            // are you sure??
            if (RectElement == null)
            {
                return;
            }

            var atrt = from ss in SvgCompositionControl.Context.SvgAttributes
                where ss.SvgElementId == RectElement.Id
                select ss;

            if (atrt.Any())
            {
                foreach (SvgAttribute att in atrt)
                {
                    DeleteAttribute(att);
                }
            }

            var pp = from qq in SvgCompositionControl.Context.SvgElements
                where qq.Id == RectElement.SvgParentElementId
                     select qq;

            SvgElement owner = pp.First();
            
            SvgCompositionControl.Context.SvgElements.Remove(RectElement);
           
            SvgCompositionControl.Context.SaveChanges();
            RectElement = null;

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


       
    }
}
