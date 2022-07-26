using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Composition;
using System.Data.Entity.Migrations.Design;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.Utilities;
using OddAndEnds;
using Svg;
using SvgComposition.Dialogs;
using SvgComposition.ElementControls;
using SvgComposition.ElementControls.ContainerElements;
using SvgComposition.Model;
using SvgCompositionTool.Dialogs;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using SvgElement = SvgComposition.Model.SvgElement;

namespace SvgComposition.Controls
{

    /// <summary>
    /// Interaction logic for SvgBuildControl.xaml
    /// same idea as CssBuildControl in CssCompositionTool
    /// </summary>
    public partial class SvgBuildControl : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {

        #region fields



        private readonly Random rand = new Random();
        private static SvgElement _currentTopElement;
        public static SvgElementCreator SvgElementCreator = new SvgElementCreator();
        private ObservableCollection<SvgElement> _svgChildElementObservableCollection = new ObservableCollection<SvgElement>();

        private double _fromHeight = 800.0;
        private double _fromWidth = 800.0;
        private double _thirdwidth;
        private double _twoThirdwidth;

        private SvgElementComposition _elementComposition = null;
        private readonly CodeGeneration _codeGeneration = new CodeGeneration();

        #endregion

        #region Properties

        public double FromHeight
        {
            get
            {
                _fromHeight = SvgCompositionControl.IsYsize;
                return _fromHeight;
            }
        }

        public double FromWidth
        {
            get
            {
                _fromWidth = SvgCompositionControl.IsXsize;
                return _fromWidth;
            }
        }

        public double ThirdWidth
        {
            get { return _thirdwidth; }
            set
            {
                _thirdwidth = value;
                OnPropertyChanged();
            }
        }

        public double TwoThirdWidth
        {
            get { return _twoThirdwidth; }
            set
            {
                _twoThirdwidth = value;
                OnPropertyChanged();
            }
        }

        public static SvgElement CurrentTopElement
        {
            get { return _currentTopElement; }
            set
            {
                _currentTopElement = value;
            }
        }

        #endregion


        public SvgBuildControl()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += OnLoaded;
            SvgElementCreator.BuildElementsDictionary();
            SvgElementCreator.BuildElementsNamesDictionary();
        }

        

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SvgCompositionControl.MainToolControl.SizeChanged += CurrentMainOnSizeChanged;

            _elementComposition = new SvgElementComposition();
            if (_currentTopElement == null)
            {
                IEnumerable<SvgElement> topElements = from ww in SvgCompositionControl.Context.SvgElements
                                                      where ww.ElementType == "svg" && ww.TopSvg == true
                                                      select ww;
               
                SvgCompositionControl.Context.SaveChanges();

                if (topElements.Count() != 0)
                {
                    _currentTopElement = topElements.First();
                    ElementComposition.CurrentElement = _currentTopElement;
                    ElementComposition.InitializeScript(_currentTopElement);
                }

                if (_currentTopElement == null)
                {
                    CreateTopElement();
                }


                ActionBox.Content = _elementComposition;
                //   ElementComposition.SvgScriptControl.CurrentGroup = _currentTopElement;

              
            }
        }

        public SvgElementComposition ElementComposition
        {
            get { return _elementComposition; }
            set { _elementComposition = value; }
        }

        private void CurrentMainOnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            OnPropertyChanged(nameof(FromHeight));
            OnPropertyChanged(nameof(FromWidth));
        }

        public void CreateTopElement()
        {
            SvgSvg svg = new SvgSvg();

            svg.Create();
            _currentTopElement = svg.SvgElementData;
            _currentTopElement.TopSvg = true;
            _currentTopElement.SvgElementName = OddsAndEnds.RandomName();

            ElementComposition.CurrentElement = _currentTopElement;
            ElementComposition.InitializeScript(_currentTopElement);
            
        }

        private void CreateSvgButton_OnClick(object sender, RoutedEventArgs e)
        {
            CreateTopElement();
        }

        public void DeleteChildAttributes(SvgElement e)
        {
            if (e != null)
            {
                foreach (SvgAttribute aa in e.SvgAttributes)
                {
                    foreach (SvgColor cc in aa.SvgColors)
                    {
                        aa.SvgColors.Remove(cc);
                        SvgCompositionControl.Context.SvgColors.Remove(cc);
                    }

                    e.SvgAttributes.Remove(aa);
                    SvgCompositionControl.Context.SvgAttributes.Remove(aa);
                }
            }
        }


       


        private void DeleteSvgMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = System.Windows.MessageBox.Show(
                $"Are you sure? This will delete the current SVG and all its child elements and attributes they contain as well.",
                "Confirm delete");
            if (res == MessageBoxResult.OK)
            {
                if (CurrentTopElement != null)
                {
                    RemoveElement(CurrentTopElement);
                }

                IEnumerable<SvgElement> topElements = from ww in SvgCompositionControl.Context.SvgElements
                    where ww.ElementType == "svg" && ww.TopSvg == true
                    select ww;

                if (topElements.Any())
                {
                    _currentTopElement = topElements.First();
                    ElementComposition.CurrentElement = _currentTopElement;
                    ElementComposition.InitializeScript(_currentTopElement);
                }

                if (_currentTopElement == null)
                {
                    CreateTopElement();
                }
               
                InvalidateVisual();
            }
        }

        public void RemoveElement(SvgElement ee)
        {
            while (ee.SvgElements.Any())
            {
                RemoveElement(ee.SvgElements.First());
            }
            while (ee.SvgAttributes.Any())
            {
                SvgAttribute aa = ee.SvgAttributes.First();
                while (aa.SvgColors.Any())
                {
                    SvgCompositionControl.Context.SvgColors.Remove(aa.SvgColors.First());
                }

                SvgCompositionControl.Context.SvgAttributes.Remove(aa);
            }

            SvgCompositionControl.Context.SvgElements.Remove(ee);
            SvgCompositionControl.Context.SaveChanges();
        }

        private void ChooseSvgMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            IEnumerable<SvgElement> topGroups = from ww in SvgCompositionControl.Context.SvgElements
                                                where ww.ElementType == "svg" && ww.TopSvg == true
                                                select ww;
            ObservableCollection<SvgElement> obe = new ObservableCollection<SvgElement>(topGroups);
            ChooseSvgElement chooser = new ChooseSvgElement
            {
                ChosenSvg = _currentTopElement,
                SvgElementObservableCollection = obe
            };

            if (chooser.ShowDialog() == true)
            {
                if (chooser.ChosenSvg != null)
                {
                    CurrentTopElement = chooser.ChosenSvg;
                    ElementComposition.CurrentElement = _currentTopElement;
                    ElementComposition.InitializeScript(_currentTopElement);
                    InvalidateVisual();
                }
            }
        }

        private void BuildSvgButton_OnClick(object sender, RoutedEventArgs e)
        {
            ElementComposition.ScriptView.Top = CurrentTopElement;
        }

      



        private void ShowSvgButton_OnClick(object sender, RoutedEventArgs e)
        {
            // var fileGenerator = new FileGenerator();
            string html = string.Empty;
            string title = _currentTopElement.SvgElementName;

            //   html = "<!DOCTYPE html>\r\n";
            html = "<!DOCTYPE HTML PUBLIC \" -//W3C//DTD HTML 4.0 Transitional//EN\">";
            html += "<html lang = \"en\" xmlns = \"http://www.w3.org/1999/xhtml\">\r\n";
            html += "<head>\r\n";
            html += "<meta charset = \"utf-8\"/>\r\n";
            html += "<title>";
            html += title;
            html += "\r\n</title>\r\n";

            html += "</head>\r\n";
            html += "<body>\r\n";
            html += "<div class=\"";
            html += $"For{title}";
            html += "\">\r\n";
            html += "<p>This shows the svg</p> ";
            html += _codeGeneration.Generate(_currentTopElement.Id);

            html += "</div>\r\n";
            html += "</body>\r\n";
            html += "</html>";

            SvgCompositionControl.OpenBrowserPage(html);
       //     BrowserWindow browserwindow = new BrowserWindow();
       //     browserwindow.SetBrowserText(html, title);
       //     browserwindow.ShowDialog();
       //     browserwindow.Close();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        [Import]
        internal IContentTypeRegistryService ContentTypeRegistryService { get; set; }


        internal IContentType GetSvgContentType()
        {
            IContentType tmp = null;
            try
            {
                tmp = ContentTypeRegistryService.GetContentType("Svg");
            }
            catch (Exception ee)
            {
                string cc = ee.Message;

            }
            return (tmp);
        }

        private void ExportButton_OnClick(object sender, RoutedEventArgs e)
        {

            var dialog = new SaveFileDialog();
            dialog.Filter = "html documents (.html)|*.html|All Files (*.*)|*.*";
            dialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();


            var ww = from nn in _currentTopElement.SvgAttributes
                where nn.AttributeName == "id"
                select nn;

            if (ww.Any())
            {
                string name = ww.First().AttributeValue;
                dialog.FileName = $"Svg{_currentTopElement.ElementType}_{_currentTopElement.Id}_{name.Replace(' ', '_')}";
            }
            else
            {
                dialog.FileName = $"Svg_{_currentTopElement.ElementType}_{_currentTopElement.Id}_{rand.Next(10000).ToString()}";
            }

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string html = string.Empty;
                    string title = _currentTopElement.SvgElementName;

                    //   html = "<!DOCTYPE html>\r\n";
                    html = "<!DOCTYPE HTML PUBLIC \" -//W3C//DTD HTML 4.0 Transitional//EN\">";
                    html += "<html lang = \"en\" xmlns = \"http://www.w3.org/1999/xhtml\">\r\n";
                    html += "<head>\r\n";
                    html += "<meta charset = \"utf-8\"/>\r\n";
                    html += "<title>";
                    html += title;
                    html += "\r\n</title>\r\n";

                    html += "</head>\r\n";
                    html += "<body>\r\n";
                    html += "<div class=\"";
                    html += $"For{title}";
                    html += "\">\r\n";
                    html += "<p>This shows the svg</p> ";
                    html += _codeGeneration.Generate(_currentTopElement.Id);

                    html += "</div>\r\n";
                    html += "</body>\r\n";
                    html += "</html>";

                    StreamWriter streamwriter = File.CreateText(dialog.FileName);

                    
                    html += "\r\n";
                    streamwriter.Write(html);


                    streamwriter.Flush();
                    streamwriter.Close();

                }
                catch (Exception ee)
                {
                    string tt = ee.Message;
                }
            }
        }


        public string ExportData(out string fname)
        {
            fname = "onetow";
            return ("aaa");
        }

        private void WriteFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            string txt = _codeGeneration.Generate(_currentTopElement.Id);


            var dialog = new SaveFileDialog();
            dialog.Filter = "svg documents (.svg)|*.svg|All Files (*.*)|*.*";
            dialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            var ww = from nn in _currentTopElement.SvgAttributes
                where nn.AttributeName == "id"
                select nn;

            if (ww.Any())
            {
                string name = ww.First().AttributeValue;
                dialog.FileName = $"{_currentTopElement.ElementType}_{_currentTopElement.Id}_{name.Replace(' ', '_')}";
            }
            else
            {
                dialog.FileName = $"{_currentTopElement.ElementType}_{_currentTopElement.Id}";
            }
           
            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, txt);
            }
        }

    

        private CodeToDuplicateSvgData CodeBuilder { get; set; } = new CodeToDuplicateSvgData();

        private void CodeForDuplicate_OnClick(object sender, RoutedEventArgs e)
        {
            string st = CodeBuilder.Generate(_currentTopElement.Id);

            var dialog = new SaveFileDialog();
            dialog.Filter = "cs documents (.cs)|*.cs|All Files (*.*)|*.*";
            dialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            var ww = from nn in _currentTopElement.SvgAttributes
                where nn.AttributeName == "id"
                select nn;

            if (ww.Any())
            {
                string name = ww.First().AttributeValue;
                dialog.FileName = $"{_currentTopElement.ElementType}_{_currentTopElement.Id}_{name.Replace(' ', '_')}";
            }
            else
            {
                dialog.FileName = $"{_currentTopElement.ElementType}_{_currentTopElement.Id}";
            }

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, st);
            }
        }


        private void AboutButton_OnClick(object sender, RoutedEventArgs e)
        {
           Popup pv = new Popup();
           pv.PlacementTarget = this;
           pv.HorizontalOffset = 200;
           pv.VerticalOffset = 200;
           pv.Placement = PlacementMode.Top;
           pv.Child = new HelpControl();
           pv.IsOpen = true;
        }


        private void DuplicateSvgMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            SvgElement nchild = DuplicateElement(_currentTopElement);
            SvgCompositionControl.Context.SaveChanges();
        }

        SvgElement DuplicateElement(SvgElement ee)
        {
            SvgElement en = new SvgElement();
            en.ElementType = ee.ElementType;
            en.Content = ee.Content;
            en.ElementOrder = ee.ElementOrder;
            en.Id = SvgElementCreator.FindNextSvgElementId();
            en.SvgElementName = ee.SvgElementName;
            en.TopSvg = ee.TopSvg;
            SvgCompositionControl.Context.SvgElements.Add(en);
            SvgCompositionControl.Context.SaveChanges();

            foreach (SvgAttribute eea in ee.SvgAttributes)
            {
                SvgAttribute ena = new SvgAttribute();
                ena.AttributeName = eea.AttributeName;
                ena.AttributeType = eea.AttributeType;
                ena.AttributeValue = eea.AttributeValue;
                ena.SecondaryValue = eea.SecondaryValue;
                ena.SvgUnit = eea.SvgUnit;
                ena.Id = SvgAttributeCreator.FindNextSvgAttributeId();
                ena.SvgElement = en;
                ena.SvgElementId = en.Id;
                en.SvgAttributes.Add(ena);
                SvgCompositionControl.Context.SvgAttributes.Add(ena);
                SvgCompositionControl.Context.SaveChanges();
                foreach (SvgColor eecol in eea.SvgColors)
                {
                    SvgColor encol = new SvgColor();
                    encol.Id = SvgAttributeCreator.FindNextSvgColorId();
                    encol.SvgAttribute = ena;
                    encol.SvgAttributeId = ena.Id;
                    encol.ResetColor(eecol.Color());
                    SvgCompositionControl.Context.SvgColors.Add(encol);
                    SvgCompositionControl.Context.SaveChanges();
                }
            }

            foreach (SvgElement eee in ee.SvgElements)
            {
                en.SvgElements.Add(DuplicateElement(eee));
            }

            return (en);
        }


        private void ViewImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            string txt = _codeGeneration.Generate(_currentTopElement.Id);

            Popup pp = new Popup();
            pp.PlacementTarget = this;
            pp.HorizontalOffset = 200;
            pp.VerticalOffset = 200;
            pp.Placement = PlacementMode.Top;

            BitmapSource bms = null;
            try
            {
                SvgDocument svgDoc = SvgDocument.FromSvg<SvgDocument>(txt);
                Bitmap vv = svgDoc.Draw();
                bms = BitmapConversion.ToWpfBitmap(vv);
            }
            catch (Exception exception)
            {
                string unused = exception.Message;

            }
            
            SvgViewerWpf vwpf = new SvgViewerWpf();
            pp.Child = vwpf;
            if (bms != null)
            {
                vwpf.InsertImage(bms);
            }

            pp.IsOpen = true;
        }
    }
}
