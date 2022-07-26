using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.Shell;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.LenPerAttributes
{
    /// <summary>
    /// Interaction logic for SvgCy.xaml
    /// </summary>
    public partial class SvgCx : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {
        #region fields
    

        private string _labelText;



        #endregion

        public SvgCx()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "cx";
        }

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgCx), new FrameworkPropertyMetadata("none",
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
                typeof(SvgCx), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null) 
            );

       

        public string SvgAttributeCurrentValue
        {
            get { return (string)GetValue(SvgAttributeCurrentValueProperty); }
            set
            {
                SetValue(SvgAttributeCurrentValueProperty, value);

                OnPropertyChanged("SvgAttributeCurrentValue");
            }
        }


        public static readonly DependencyProperty IsLenperCurrentUnitProperty =
            DependencyProperty.Register(
                "IsLenperCurrentUnit",
                typeof(string),
                typeof(SvgCx), new FrameworkPropertyMetadata("none",
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null)
            );

        public string LenperCurrentUnit
        {
            get { return (string)GetValue(IsLenperCurrentUnitProperty); }
            set
            {
                SetValue(IsLenperCurrentUnitProperty, value);
                OnPropertyChanged("LenperCurrentUnit");
            }
        }

        #region methods
        public bool CanAnimate { get; set; } = true;

        public SvgAttribute Create(ref SvgElement ele)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeType = SvgAttributeType.LengthPercent;
            at.AttributeValue = "0";
            at.AttributeName = _labelText;
            at.SvgUnit = " ";
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
            LenperCurrentUnit = SvgUnitBlock.Text = at.SvgUnit;
            InvalidateVisual();
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text; 
            at.AttributeType = SvgAttributeType.LengthPercent;
            at.SvgUnit = LenperCurrentUnit = SvgUnitBlock.Text; 
            SvgCompositionControl.Context.SaveChanges();
        }

        #endregion


        #region events



        private void LenPerUnitComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            LenperCurrentUnit = SvgUnitBlock.Text =  lbi.Content.ToString();
            InvalidateVisual();
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
