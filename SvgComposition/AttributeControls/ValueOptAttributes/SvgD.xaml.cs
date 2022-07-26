using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ValueOptAttributes
{
    /// <summary>
    /// Interaction logic for SvgD.xaml
    /// </summary>
    public partial class SvgD : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue = "";
        private string _svgAttributeLocalValue = "";
        private string _labelText;

       
        public SvgD()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "d";
        }

        #endregion

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgD), new FrameworkPropertyMetadata("none",
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
                typeof(SvgD), new FrameworkPropertyMetadata("none",
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
            at.AttributeType = SvgAttributeType.Value;
            at.AttributeValue = " ";
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
            SvgAttributeCurrentValue = at.AttributeValue;

        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            at.AttributeType = SvgAttributeType.Value;
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

        

        private void Movem_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} m {Boxmx.Text} {Boxmy.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            Boxmx.Text = string.Empty;
            Boxmy.Text = string.Empty;
        }

        private void MoveM_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} M {BoxMx.Text} {BoxMy.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            BoxMx.Text = string.Empty;
            BoxMy.Text = string.Empty;
        }

        private void Movel_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} l {Boxlx.Text} {Boxly.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            Boxlx.Text = string.Empty;
            Boxly.Text = string.Empty;
        }

        private void MoveL_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} L {BoxLx.Text} {BoxLy.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            BoxLx.Text = string.Empty;
            BoxLy.Text = string.Empty;
        }

        private void Movev_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} v {Boxv.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            Boxv.Text = string.Empty;
           
        }

        private void MoveV_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} V {BoxV.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            BoxV.Text = string.Empty;
         
        }

        private void Moveh_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} h {Boxh.Text}";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            Boxh.Text = string.Empty;
           
        }

        private void MoveH_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} H {BoxH.Text}";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            BoxH.Text = string.Empty;
           
        }

        private void MoveC_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} C {BoxCx1.Text} {BoxCy1.Text}  {BoxCx2.Text} {BoxCy2.Text} {BoxCx.Text} {BoxCy.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            BoxCx1.Text = string.Empty;
            BoxCy1.Text = string.Empty;
            BoxCx2.Text = string.Empty;
            BoxCy2.Text = string.Empty;
            BoxCx.Text = string.Empty;
            BoxCy.Text = string.Empty;
        }

        private void Movec_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} c {Boxcx1.Text} {Boxcy1.Text}  {Boxcx2.Text} {Boxcy2.Text} {Boxcx.Text} {Boxcy.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            Boxcx1.Text = string.Empty;
            Boxcy1.Text = string.Empty;
            Boxcx2.Text = string.Empty;
            Boxcy2.Text = string.Empty;
            Boxcx.Text = string.Empty;
            Boxcy.Text = string.Empty;
        }
        private void Moves_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} s {Boxsx1.Text} {Boxsy1.Text} {Boxsx.Text} {Boxsy.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            Boxsx1.Text = string.Empty;
            Boxsy1.Text = string.Empty;
            Boxsx.Text = string.Empty;
            Boxsy.Text = string.Empty;
        }

        private void MoveS_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} S {BoxSx1.Text} {BoxSy1.Text} {BoxSx.Text} {BoxSy.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            BoxSx1.Text = string.Empty;
            BoxSy1.Text = string.Empty;
            BoxSx.Text = string.Empty;
            BoxSy.Text = string.Empty;
        }

        private void Moveq_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} q {Boxqx1.Text} {Boxqy1.Text} {Boxqx.Text} {Boxqy.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            Boxqx1.Text = string.Empty;
            Boxqy1.Text = string.Empty;
            Boxqx.Text = string.Empty;
            Boxqy.Text = string.Empty;
        }

        private void MoveQ_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} Q {BoxQx1.Text} {BoxQy1.Text} {BoxQx.Text} {BoxQy.Text} ";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            BoxQx1.Text = string.Empty;
            BoxQy1.Text = string.Empty;
            BoxQx.Text = string.Empty;
            BoxQy.Text = string.Empty;
        }

        private void Movet_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} t {Boxtx.Text} {Boxty.Text}";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            Boxtx.Text = string.Empty;
            Boxty.Text = string.Empty;
        }

        private void MoveT_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} T {BoxTx.Text} {BoxTy.Text}";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            BoxTx.Text = string.Empty;
            BoxTy.Text = string.Empty;
        }

        private void MoveA_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} A {BoxArx.Text} {BoxAry.Text} {BoxAA.Text} {BoxAf1.Text} {BoxAf2.Text} {BoxAx.Text} {BoxAy.Text}";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
            BoxArx.Text = string.Empty;
            BoxAry.Text = string.Empty;
            BoxAA.Text = string.Empty;
            BoxAf1.Text = string.Empty;
            BoxAf2.Text = string.Empty;
            BoxAx.Text = string.Empty;
            BoxAy.Text = string.Empty;
        }

        private void Movea_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} a {Boxarx.Text} {Boxary.Text} {BoxaA.Text} {Boxaf1.Text} {Boxaf2.Text} {Boxax.Text} {Boxay.Text}";
           SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
           Boxarx.Text = string.Empty;
           Boxary.Text = string.Empty;
           BoxaA.Text = string.Empty;
           Boxaf1.Text = string.Empty;
           Boxaf2.Text = string.Empty;
           Boxax.Text = string.Empty;
           Boxay.Text = string.Empty;

        }


        private void MoveZ_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} Z";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
        }
        private void Movez_OnClick(object sender, RoutedEventArgs e)
        {
            string ns = $"{SvgAttributeBlock.Text} z";
            SvgAttributeCurrentValue = SvgAttributeBlock.Text = ns;
        }


    }
}
