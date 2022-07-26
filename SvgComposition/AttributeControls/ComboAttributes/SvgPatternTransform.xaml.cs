using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls.ComboAttributes
{
    /// <summary>
    /// Interaction logic for SvgPatternTransform.xaml
    /// </summary>
    public partial class SvgPatternTransform : UserControl, ISvgAttributeControl, INotifyPropertyChanged
    {

        #region fields
        private string _svgAttributeOriginalValue;
        private string _svgAttributeLocalValue;
        private string _labelText;
        #endregion

        public SvgPatternTransform()
        {
            InitializeComponent();
            DataContext = this;
            LabelText = "patternTransform";
            AtthTip.Text = $"The '{LabelText}' attribute";
            AttvTip.Text = $"The '{LabelText}' attribute value";
           

        }

      

        #region properties

        public static readonly DependencyProperty IsLabelTextProperty =
            DependencyProperty.Register(
                "IsLabelText",
                typeof(string),
                typeof(SvgPatternTransform), new FrameworkPropertyMetadata("none",
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
                typeof(SvgPatternTransform), new FrameworkPropertyMetadata("none",
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

        private void ComposeTransformList()
        {
            // matrix
            string matrix = " ";
            bool gomatrix = true;
            List<string> coifs = new List<string>();
            coifs.Clear();
           
            foreach (var vv in MatrixCoifPanel.Children)
            {
                string c = (vv as TextBox).Text;
                if (string.IsNullOrWhiteSpace(c))
                {
                    gomatrix = false;
                    break;
                }
                coifs.Add(c); 
            }

            if (gomatrix)
            {
                matrix = $"matrix({coifs[0]} {coifs[1]} {coifs[2]} {coifs[3]} {coifs[4]} {coifs[5]}) ";
            }

            // translate
          
            string translate = " ";
          
            coifs.Clear();
            foreach (var vv in TranslateCoifPanel.Children)
            {
                string c = (vv as TextBox).Text;
                coifs.Add(c);
            }

            if (string.IsNullOrWhiteSpace(coifs[0]) == false && string.IsNullOrWhiteSpace(coifs[1]) == false)
            {
                translate = $"translate({coifs[0]} {coifs[1]} {coifs[2]})";
            }

            // scale

            string scale = " ";
            coifs.Clear();
            foreach (var vv in ScaleCoifPanel.Children)
            {
                string c = (vv as TextBox).Text;
                coifs.Add(c);
            }

            if (string.IsNullOrWhiteSpace(coifs[0]) == false)
            {
                if (string.IsNullOrWhiteSpace(coifs[1]))
                {
                    coifs[1] = coifs[0];
                }

                scale = $"scale({coifs[0]} {coifs[1]})";
            }

            // rotate

            string rotate = " ";
            coifs.Clear();
            foreach (var vv in RotateCoifPanel.Children)
            {
                string c = (vv as TextBox).Text;
                coifs.Add(c);
            }

            if (string.IsNullOrWhiteSpace(coifs[0]) == false)
            {
                rotate = $"rotate({coifs[0]} {coifs[1]} {coifs[2]})";
            }
            
            // skewX

            string skewx = " ";
            coifs.Clear();
            foreach (var vv in SkewXCoifPanel.Children)
            {
                string c = (vv as TextBox).Text;
                coifs.Add(c);
            }

            if (string.IsNullOrWhiteSpace(coifs[0]) == false)
            {
                skewx = $"skewX({coifs[0]})";
            }

            // skewY

            string skewy = " ";
            coifs.Clear();
            foreach (var vv in SkewYCoifPanel.Children)
            {
                string c = (vv as TextBox).Text;
                coifs.Add(c);
            }

            if (string.IsNullOrWhiteSpace(coifs[0]) == false)
            {
                skewy = $"skewY({coifs[0]})";
            }

            SvgAttributeBlock.Text = $"{matrix} {translate} {scale} {rotate} {skewx} {skewy}";
        }


        private void ParseTransforms(string st)
        {
            char[] splits = new char[]
            {
                ' ', ',',
            }; 
            string[] all = st.Split(')');

            foreach (string vv in all)
            {
                if (vv.Contains("matrix"))
                {
                    try
                    {
                        int cc = vv.IndexOf('(') + 2;
                        string sub = vv.Substring(cc);
                        string[] coif = sub.Split(splits);
                        for (int ii = 0; ii < coif.Length;ii++)
                        {
                            (MatrixCoifPanel.Children[ii] as TextBox).Text = coif[ii];
                        }
                    }
                    catch (Exception e)
                    {
                       
                    }
                }
                if (vv.Contains("translate"))
                {
                    try
                    {
                        int cc = vv.IndexOf('(') + 2;
                        string sub = vv.Substring(cc);
                        string[] coif = sub.Split(splits);
                        for (int ii = 0; ii < coif.Length;ii++)
                        {
                            (TranslateCoifPanel.Children[ii] as TextBox).Text = coif[ii];
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (vv.Contains("scale"))
                {
                    try
                    {
                        int cc = vv.IndexOf('(') + 2;
                        string sub = vv.Substring(cc);
                        string[] coif = sub.Split(splits);
                        for (int ii = 0; ii < coif.Length; ii++)
                        {
                            (ScaleCoifPanel.Children[ii] as TextBox).Text = coif[ii];
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (vv.Contains("rotate"))
                {
                    try
                    {
                        int cc = vv.IndexOf('(') + 2;
                        string sub = vv.Substring(cc);
                        string[] coif = sub.Split(splits);
                        for (int ii = 0; ii < coif.Length; ii++)
                        {
                            (RotateCoifPanel.Children[ii] as TextBox).Text = coif[ii];
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (vv.Contains("skewX"))
                {
                    try
                    {
                        int cc = vv.IndexOf('(') + 2;
                        string sub = vv.Substring(cc);
                        string[] coif = sub.Split(splits);
                        for (int ii = 0; ii < coif.Length; ii++)
                        {
                            (SkewXCoifPanel.Children[ii] as TextBox).Text = coif[ii];
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
                if (vv.Contains("skewY"))
                {
                    try
                    {
                        int cc = vv.IndexOf('(') + 2;
                        string sub = vv.Substring(cc);
                        string[] coif = sub.Split(splits);
                        for (int ii = 0; ii < coif.Length; ii++)
                        {
                            (SkewYCoifPanel.Children[ii] as TextBox).Text = coif[ii];
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        public bool CanAnimate { get; set; } = false;
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
            SvgAttributeCurrentValue = SvgAttributeBlock.Text =  at.AttributeValue;
            if (string.IsNullOrWhiteSpace(SvgAttributeBlock.Text) == false)
            {
                ParseTransforms(SvgAttributeCurrentValue);
            }
        }

        public void Save(SvgAttribute at)
        {
            at.AttributeValue = SvgAttributeCurrentValue = SvgAttributeBlock.Text;
            SvgCompositionControl.Context.SaveChanges();

        }

        #endregion

        #region events

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            ComposeTransformList();
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
