using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ComboAttributes
{
    /// <summary>
    /// Interaction logic for SvgPreserveAspectRatio.xaml
    /// </summary>
    public partial class SvgPreserveAspectRatio : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        #endregion

        public SvgPreserveAspectRatio()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "preserveAspectRatio";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
           

        }

      

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgPreserveAspectRatio), new FrameworkPropertyMetadata("none",
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
                typeof(SvgPreserveAspectRatio), new FrameworkPropertyMetadata("none",
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
            at.AttributeValue = "xMidYMid meet";
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
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = at.AttributeValue;

            string[] vals = SvgAttributeCurrentValue.Split(' ');

            if (vals.Length > 0)
            {
                for (int ii = 0; ii < AlignComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = AlignComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.AttributeValue)
                    {
                        AlignComboBox.SelectedIndex = ii;
                        break;
                    }
                }
            }
            else
            {
                if (vals.Length > 1)
                {
                    for (int ii = 0; ii < MeetOrSliceComboBox.Items.Count; ii++)
                    {
                        ComboBoxItem itt = MeetOrSliceComboBox.Items[ii] as ComboBoxItem;
                        if ((itt.Content as string) == vals[1])
                        {
                            MeetOrSliceComboBox.SelectedIndex = ii;
                            break;
                        }
                    }
                }
                else
                {
                    MeetOrSliceComboBox.SelectedIndex = 0;
                }
            }

        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
           
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


        private void MeetOrSliceComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            string msl = lbi.Content.ToString();
            ComboBoxItem abi = (AlignComboBox.SelectedItem as ComboBoxItem);
            string asl = abi.Content.ToString();

            if (msl == "empty")
            {
                SvgAttributeBlock.Text = SvgAttributeCurrentValue = asl;
            }
            else
            {
                SvgAttributeBlock.Text = SvgAttributeCurrentValue = $"{asl} {msl}";
            }
           

            this.InvalidateVisual();
        }

        private void AlignComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem abi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            string asl = abi.Content.ToString();
            string msl = "empty";
            if (MeetOrSliceComboBox.SelectedIndex >= 0)
            {
                ComboBoxItem lbi = (MeetOrSliceComboBox.SelectedItem as ComboBoxItem);
                msl = lbi.Content.ToString();
            }

            if (msl == "empty")
            {
                SvgAttributeBlock.Text = SvgAttributeCurrentValue = asl;
            }
            else
            {
                SvgAttributeBlock.Text = SvgAttributeCurrentValue = $"{asl} {msl}";
            }

            this.InvalidateVisual();
        }

        #endregion
    }
}
