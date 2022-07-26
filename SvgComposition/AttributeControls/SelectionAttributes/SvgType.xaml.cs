using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.SelectionAttributes
{
    /// <summary>
    /// Interaction logic for SvgType.xaml
    /// </summary>
    public partial class SvgType : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {
        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
       
        private SvgElement _svgElement = null;
       

        private List<string> _animateTranType = new List<string>
        {
            "translate", "scale", "rotate", "skewX", "skewY"
        };

        private List<string> _feColorMatType = new List<string>
        {
            "matrix", "saturate", "hueRotate", "luminanceToAlpha"
        };

        private List<string> _funcType = new List<string>
        {
            "identity", "table", "discrete", "linear", "gamma"
        };

        private List<string> _feTurbuType = new List<string>
        {
            "fractalNoise", "turbulence"
        };

       

        #endregion

        public SvgType()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "type";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
           
        }

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgType), new FrameworkPropertyMetadata("none",
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
                typeof(SvgType), new FrameworkPropertyMetadata("none",
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

        void SetChoices(string elementname)
        {
            SelectAnimateTransform.Visibility = Visibility.Collapsed;
            SelectFeColorMatrix.Visibility = Visibility.Collapsed;
            SelectFeFunc.Visibility = Visibility.Collapsed;
            SelectFeTurbulence.Visibility = Visibility.Collapsed;

            switch (elementname)
            {
                case "animate​Transform":
                    SelectAnimateTransform.Visibility = Visibility.Visible;

                     CanAnimate = false;
                    break;
                case "feColorMatrix":
                    SelectFeColorMatrix.Visibility = Visibility.Visible;
                    CanAnimate = true;
                    break;
                case "feFuncR":
                case "feFuncG":
                case "feFuncB":
                case "feFuncA":
                    SelectFeFunc.Visibility = Visibility.Visible;
                    CanAnimate = true;
                    break;
                    
                case "feTurbulence":
                    SelectFeTurbulence.Visibility = Visibility.Visible;
                    CanAnimate = true;
                    break;
                default:
                    SvgAttributeCurrentValue = " ";
                    SelectAnimateTransform.Visibility = Visibility.Visible;
                    CanAnimate = false;
                    break;
            }
        }

        public bool CanAnimate { get; set; } = true;

        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeType = SvgAttributeType.Text;
            at.AttributeValue = " ";
            at.AttributeName = _labelText;
            SetChoices(ele.ElementType);
            at.SvgElement = ele;
            at.SvgElementId = ele.Id;
            ele.SvgAttributes.Add(at);

            SvgCompositionControl.Context.SvgAttributes.Add(at);
            SvgCompositionControl.Context.SaveChanges();
            Load(at);
            return (at);
        }

        public void Load(SvgAttribute at)
        {
            _svgElement = at.SvgElement;
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;;
            SetChoices(_svgElement.ElementType);

            SvgAttributeCurrentValue = at.AttributeValue;
            SvgAttributeBlock.Text = at.AttributeValue;

            this.InvalidateVisual();
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            at.AttributeType = SvgAttributeType.Text;
            SvgCompositionControl.Context.SaveChanges();
        }
        #endregion

        #region events

      


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


       

        private void SelectAnimateTransform_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();

            this.InvalidateVisual();
        }

        private void SelectFeColorMatrix_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();

            this.InvalidateVisual();
        }

        private void SelectFeTurbulence_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();

            this.InvalidateVisual();
        }

        private void SelecyFeFunc_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();

            this.InvalidateVisual();
        }

        #endregion
    }
}
