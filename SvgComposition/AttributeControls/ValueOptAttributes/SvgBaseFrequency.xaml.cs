using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ValueOptAttributes
{
    /// <summary>
    /// Interaction logic for SvgAmplitude.xaml
    /// </summary>
    public partial class SvgBaseFrequency : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue = "0";
        private string _svgAttributeLocalValue = "0";
        private string _svgAttributeLocalOptionalValue;
        private string _labelText;

       
        public SvgBaseFrequency()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "baseFrequency";
        }

        #endregion

        public static readonly DependencyProperty IsLabelTextProperty = 
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgBaseFrequency), new FrameworkPropertyMetadata("none",
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
                typeof(SvgBaseFrequency), new FrameworkPropertyMetadata("none",
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

           public static readonly DependencyProperty SvgAttributeOptionalValueProperty =
            DependencyProperty.Register(
                "SvgAttributeOptionalValue",
                typeof(string),
                typeof(SvgBaseFrequency), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );



        public string SvgAttributeOptionalValue
        {
            get { return (string)GetValue(SvgAttributeOptionalValueProperty); }
            set
            {
                SetValue(SvgAttributeOptionalValueProperty, value);
                _svgAttributeLocalOptionalValue = value;
                OnPropertyChanged("SvgAttributeOptionalValue");
            }
        }

        #region methods
        public bool CanAnimate { get; set; } = true;

        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeType = SvgAttributeType.ValueAndOptional;
            at.AttributeValue = "0";
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
            SvgAttributeBlock.IsReadOnly = false;
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;
            SvgAttributeOptionalValue = SvgAttributeOptionalBlock.Text = at.SecondaryValue;
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            at.SecondaryValue = SvgAttributeOptionalValue = SvgAttributeOptionalBlock.Text;
            at.AttributeType = SvgAttributeType.ValueAndOptional;
        }

        #endregion


        #region events



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
