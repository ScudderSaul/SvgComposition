using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ComboAttributes
{
    /// <summary>
    /// Interaction logic for SvgRepeatCount.xaml
    /// </summary>
    public partial class SvgRepeatCount : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        #endregion

        public SvgRepeatCount()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "repeatCount";
        }

      

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgRepeatCount), new FrameworkPropertyMetadata("none",
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
                typeof(SvgRepeatCount), new FrameworkPropertyMetadata("none",
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
            at.AttributeType = SvgAttributeType.Text;
            at.AttributeValue = "indefinite";
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
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;;
            if (at.AttributeType == SvgAttributeType.Text)
            {
                IndefiniteCheckBox.IsChecked = true;
            }

            if (at.AttributeType == SvgAttributeType.Value)
            {
                IndefiniteCheckBox.IsChecked = false;
            }
           

        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            if (IndefiniteCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Text;
            }
            if (IndefiniteCheckBox.IsChecked == false)
            {
                at.AttributeType = SvgAttributeType.Value;
            }

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

        private void IndefiniteCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            if (SvgAttributeBlock.Text != "indefinite")
            {
                _svgAttributeOriginalValue = SvgAttributeBlock.Text;
            }

            SvgAttributeBlock.Text = "indefinite";
          
        }

        private void IndefiniteCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (SvgAttributeBlock.Text == "indefinite")
            {
                _svgAttributeLocalValue = _svgAttributeOriginalValue;
            }

        }

       
    }
}
