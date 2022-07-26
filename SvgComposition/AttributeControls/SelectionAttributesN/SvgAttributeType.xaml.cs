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
    /// Interaction logic for SvgClipRule.xaml
    /// </summary>
    public partial class SvgClipRule : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;

        private static List<string> _albaValues = new List<string>
        {
            "nonzero", "evenodd", "inherit",
        };
        public SvgClipRule()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "clip-rule";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
            SelTip.Text = $"The '{LabelText}' attribute choices";
        }

        #endregion

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgClipRule), new FrameworkPropertyMetadata("none",
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
                typeof(SvgClipRule), new FrameworkPropertyMetadata("none",
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

        #region methods
        public bool CanAnimate { get; set; } = true;


        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeType = SvgAttributeType.Text;
            at.AttributeValue = "nonzero";
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
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;;
            SvgAttributeBlock.Text = at.AttributeValue;

            for (int ii = 0; ii < SvgAttributeComboBox.Items.Count; ii++)
            {
                ComboBoxItem itt = SvgAttributeComboBox.Items[ii] as ComboBoxItem;
                if ((itt.Content as string) == at.AttributeValue)
                {
                    SvgAttributeComboBox.SelectedIndex = ii;
                    break;
                }
            }
            SvgAttributeBlock.IsReadOnly = true;
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            at.AttributeType = SvgAttributeType.Text;
            SvgCompositionControl.Context.SaveChanges();
        }

        #endregion

        #region events

        private void SvgAttributeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SvgAttributeBlock.IsReadOnly = false;
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();
            SvgAttributeBlock.IsReadOnly = true;
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
