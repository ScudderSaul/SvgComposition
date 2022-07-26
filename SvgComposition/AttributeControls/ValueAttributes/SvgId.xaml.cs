using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ValueAttributes
{
    /// <summary>
    /// Interaction logic for SvgId.xaml
    /// </summary>
    public partial class SvgId : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue = " ";
        private string _svgAttributeLocalValue = " ";
        private string _labelText;

        #endregion
        public SvgId()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "id";
        }

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgId), new FrameworkPropertyMetadata("none",
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
                typeof(SvgId), new FrameworkPropertyMetadata("none",
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
        public bool CanAnimate { get; set; } = false;

        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeType = SvgAttributeType.Value;
            at.AttributeValue = "";
            at.AttributeName = _labelText;
            at.SvgElement = ele;
            at.SvgElementId = ele.Id;
          
            SvgCompositionControl.Context.SvgAttributes.Add(at);
            SvgCompositionControl.Context.SaveChanges();
            ele.SvgAttributes.Add(at);
            SvgCompositionControl.Context.SaveChanges();
            Load(at);
            return (at);
        }

        public void Load(SvgAttribute at)
        {
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            if (string.IsNullOrWhiteSpace(SvgAttributeCurrentValue) == false)
            {
                at.SvgElement.SvgElementName = SvgAttributeCurrentValue;
            }

            at.AttributeType = SvgAttributeType.Value;
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

        #endregion
    }
}
