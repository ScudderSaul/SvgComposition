using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TextManager.Interop;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ComboAttributes
{
    /// <summary>
    /// Interaction logic for SvgIn2.xaml
    /// </summary>
    public partial class SvgIn2 : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        #endregion

        private List<string> selections = new List<string>
        {
            "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint"
        };

        public SvgIn2()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "in2";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
            SelTip.Text = $"'{LabelText}' selections";

        }

      

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgIn2), new FrameworkPropertyMetadata("none",
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
                typeof(SvgIn2), new FrameworkPropertyMetadata("none",
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
            at.AttributeValue = "SourceAlpha";
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
            SvgAttributeCurrentValue = SvgAttributeBlock.Text =  at.AttributeValue;

            if (at.AttributeType == SvgAttributeType.Text)
            {
                FilterCheckBox.IsChecked = false;
                SelectionComboBox.Visibility = Visibility.Visible;
            }

            if (at.AttributeType == SvgAttributeType.Value)
            {
                FilterCheckBox.IsChecked = true;
                SelectionComboBox.Visibility = Visibility.Collapsed;

            }
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            if (FilterCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Value;
            }
            if (FilterCheckBox.IsChecked == false)
            {
                at.AttributeType = SvgAttributeType.Text;


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


        private void FilterCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            SelectionComboBox.Visibility = Visibility.Collapsed;
        }

        private void FilterCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            SelectionComboBox.Visibility = Visibility.Visible;
        }


        private void SelectionComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem lbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            SvgAttributeBlock.Text = SvgAttributeCurrentValue = lbi.Content.ToString();

            this.InvalidateVisual();
        }

        #endregion
    }
}
