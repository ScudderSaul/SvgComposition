using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ComboAttributes
{
    /// <summary>
    /// Interaction logic for SvgFontFamily.xaml
    /// </summary>
    public partial class SvgFontFamily : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;

        private List<string> _fontFamilyNames = new List<string>
        {
            "Arial, \"Helvetica Neue\", Helvetica, sans-serif",
            "\"Arial Black\", \"Arial Bold\", Gadget, sans-serif",
            "\"Arial Narrow\", Arial, sans-serif",
            "\"Arial Rounded MT Bold\", \"Helvetica Rounded\", Arial, sans-serif",
            "\"Avant Garde\", Avantgarde, \"Century Gothic\", CenturyGothic, \"AppleGothic\", sans-serif",
            "Calibri, Candara, Segoe, \"Segoe UI\", Optima, Arial, sans-serif",
            "Candara, Calibri, Segoe, \"Segoe UI\", Optima, Arial, sans-serif",
            "\"Century Gothic\", CenturyGothic, AppleGothic, sans-serif",
            "\"Franklin Gothic Medium\", \"Franklin Gothic\", \"ITC Franklin Gothic\", Arial, sans-serif",
            "Futura, \"Trebuchet MS\", Arial, sans-serif",
            "Geneva, Tahoma, Verdana, sans-serif",
            "\"Gill Sans\", \"Gill Sans MT\", Calibri, sans-serif",
            "\"Helvetica Neue\", Helvetica, Arial, sans-serif",
            "Impact, Haettenschweiler, \"Franklin Gothic Bold\", Charcoal, \"Helvetica Inserat\", \"Bitstream Vera Sans Bold\", \"Arial Black\", sans serif",
            "\"Lucida Grande\", \"Lucida Sans Unicode\", \"Lucida Sans\", Geneva, Verdana, sans-serif",
            "Optima, Segoe, \"Segoe UI\", Candara, Calibri, Arial, sans-serif",
            "\"Segoe UI\", Frutiger, \"Frutiger Linotype\", \"Dejavu Sans\", \"Helvetica Neue\", Arial, sans-serif",
            "Tahoma, Verdana, Segoe, sans-serif",
            "\"Trebuchet MS\", \"Lucida Grande\", \"Lucida Sans Unicode\", \"Lucida Sans\", Tahoma, sans-serif",
            "Verdana, Geneva, sans-serif",
            "Baskerville, \"Baskerville Old Face\", \"Hoefler Text\", Garamond, \"Times New Roman\", serif",
            "\"Big Caslon\", \"Book Antiqua\", \"Palatino Linotype\", Georgia, serif",
            "\"Bodoni MT\", Didot, \"Didot LT STD\", \"Hoefler Text\", Garamond, \"Times New Roman\", serif",
            "\"Book Antiqua\", Palatino, \"Palatino Linotype\", \"Palatino LT STD\", Georgia, serif",
            "\"Calisto MT\", \"Bookman Old Style\", Bookman, \"Goudy Old Style\", Garamond, \"Hoefler Text\", \"Bitstream Charter\", Georgia, serif",
            "Cambria, Georgia, serif",
            "Didot, \"Didot LT STD\", \"Hoefler Text\", Garamond, \"Times New Roman\", serif",
            "Garamond, Baskerville, \"Baskerville Old Face\", \"Hoefler Text\", \"Times New Roman\", serif",
            "Georgia, Times, \"Times New Roman\", serif",
            "\"Goudy Old Style\", Garamond, \"Big Caslon\", \"Times New Roman\", serif",
            "\"Hoefler Text\", \"Baskerville old face\", Garamond, \"Times New Roman\", serif",
            "\"Lucida Bright\", Georgia, serif",
            "Palatino, \"Palatino Linotype\", \"Palatino LT STD\", \"Book Antiqua\", Georgia, serif",
            "Perpetua, Baskerville, \"Big Caslon\", \"Palatino Linotype\", Palatino, \"URW Palladio L\", \"Nimbus Roman No9 L\", serif",
            "Rockwell, \"Courier Bold\", Courier, Georgia, Times, \"Times New Roman\", serif",
            "\"Rockwell Extra Bold\", \"Rockwell Bold\", monospace",
            "TimesNewRoman, \"Times New Roman\", Times, Baskerville, Georgia, serif",
            "\"Andale Mono\", AndaleMono, monospace",
            "Consolas, monaco, monospace",
            "\"Courier New\", Courier, \"Lucida Sans Typewriter\", \"Lucida Typewriter\", monospace",
            "\"Lucida Console\", \"Lucida Sans Typewriter\", Monaco, \"Bitstream Vera Sans Mono\", monospace",
            "\"Avant Garde\", Avantgarde, \"Century Gothic\", CenturyGothic, \"AppleGothic\", sans-serif",
            "Monaco, Consolas, \"Lucida Console\", monospace",
            "Papyrus, fantasy",
            "\"Brush Script MT\", cursive",
            "Copperplate, \"Copperplate Gothic Light\", fantasy"
        };

        public List<string> _genericFontNames = new List<string>
        {
            "serif", "sans-serif", "cursive", "fantasy", "monospace",
        };


        #endregion

        public SvgFontFamily()
        {
            InitializeComponent();
            DataContext = this;
            BuildFontSelections();
            LabelText = "font-family";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
            SelTip.Text = $"The '{LabelText}' attribute choices";
        }

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgFontFamily), new FrameworkPropertyMetadata("none",
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
                typeof(SvgFontFamily), new FrameworkPropertyMetadata("none",
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

        void BuildFontSelections()
        {
            foreach (string vv in _fontFamilyNames)
            {
                ComboBoxItem it = new ComboBoxItem();
                it.Content = vv;
                SelectionComboBox.Items.Add(it);
            }

            foreach (string vv in _genericFontNames)
            {
                ComboBoxItem it = new ComboBoxItem();
                it.Content = vv;
                SelectionComboBox.Items.Add(it);
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
            SelectionComboBox.SelectedIndex = 0;
            ComboBoxItem lbi = (SelectionComboBox.SelectedItem as ComboBoxItem);
            
            at.AttributeValue = lbi.Content.ToString(); ;
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

            if (at.AttributeType == SvgAttributeType.Text)
            {
                SelectCheckBox.IsChecked = true;

                SelectionComboBox.Visibility = Visibility.Visible;

                for (int ii = 0; ii < SelectionComboBox.Items.Count; ii++)
                {
                    ComboBoxItem itt = SelectionComboBox.Items[ii] as ComboBoxItem;
                    if ((itt.Content as string) == at.AttributeValue)
                    {
                        SelectionComboBox.SelectedIndex = ii;
                        break;
                    }
                }
            }

            if (at.AttributeType == SvgAttributeType.Value)
            {
                SelectCheckBox.IsChecked = false;
                SelectionComboBox.Visibility = Visibility.Collapsed;
            }

            this.InvalidateVisual();

        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            if (SelectCheckBox.IsChecked == true)
            {
                at.AttributeType = SvgAttributeType.Text;
            }
            if (SelectCheckBox.IsChecked == false)
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


        private void SelectCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            SelectionComboBox.Visibility = Visibility.Visible;
            if (SelectionComboBox.SelectedIndex < 0)
            {
                SelectionComboBox.SelectedIndex = 0;
            }

            this.InvalidateVisual();

        }

        private void SelectCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {

            SelectionComboBox.Visibility = Visibility.Collapsed;

            this.InvalidateVisual();

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
