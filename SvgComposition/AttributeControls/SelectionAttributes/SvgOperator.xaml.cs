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
    /// Interaction logic for SvgOperator.xaml
    /// </summary>
    public partial class SvgOperator : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        private SvgElement _svgElement = null;

        private List<string> _feCompositeValues = new List<string>
        {
            "over", "in", "out", "atop", "xor", "arithmetic",
        };

        private List<string> _feMorphologyValues = new List<string>
        {
            "erode", "dilate",
        };

        #endregion
        #region ctor
        public SvgOperator()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "operator";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
          
        }

        #endregion

        #region properties
        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgOperator), new FrameworkPropertyMetadata("none",
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
                typeof(SvgOperator), new FrameworkPropertyMetadata("none",
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
            at.AttributeType = SvgAttributeType.Text;
            if (ele.ElementType == "feComposite")
            {
                at.AttributeValue = "over";
            }
            else
            {
                at.AttributeValue = "erode";
            }
            
            at.AttributeName = _labelText;  
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
            SvgCompositeComboBox.Visibility = Visibility.Collapsed;
            SvgMorphologyComboBox.Visibility = Visibility.Collapsed;

            if (_svgElement.ElementType == "feComposite")
            {
                SvgCompositeComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                SvgMorphologyComboBox.Visibility = Visibility.Visible;
            }

            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;
         
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            at.AttributeType = SvgAttributeType.Text;
            SvgCompositionControl.Context.SaveChanges();
        }
        #endregion

        #region events

        private void SvgCompositeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();
            this.InvalidateVisual();
        }

        private void SvgMorphologyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();
            this.InvalidateVisual();
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
