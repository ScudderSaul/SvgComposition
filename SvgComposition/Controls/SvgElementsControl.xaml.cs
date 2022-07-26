using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;
using SvgComposition.AttributeControls;
using SvgComposition.Controls;
using SvgComposition.Dialogs;
using SvgComposition.ElementControls;
using SvgComposition.Model;
using SvgCompositionTool.Dialogs;


namespace SvgComposition.Controls
{
    /// <summary>
    ///     Interaction logic for SvgElementComposition.xaml
    /// </summary>
    public partial class SvgElementComposition : UserControl, INotifyPropertyChanged
    {
        #region fields

        private ElementInfos _elementInf = new ElementInfos();

        private int? _svgElementId;
        private SvgElement _currentElement;
        private UserControl _currentElementControl;

        #endregion fields

        public SvgElementComposition()
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += OnLoaded;
        //    AttributeControls.LenPerAttributes.SvgCx xx = new AttributeControls.LenPerAttributes.SvgCx();
        }

       

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
          //  _elementInf.BuildSvgInfo();
          InvalidateArrange();
            InvalidateVisual();
        }

        #region properties

        public SvgElement CurrentElement
        {
            get => _currentElement;
            set
            {
                _currentElement = value;
                CheckIfContentPermitted();

                Ie = SvgBuildControl.SvgElementCreator.CreateSvgElement(_currentElement.ElementType);
                UserControl uc = (UserControl) Ie;
                Ie.Load(_currentElement);
                CurrentElementControl = uc;
            }
        }

        public void InitializeScript(SvgElement el)
        {
            ScriptView.Top = el;
        }
        private ISvgElementControl Ie { get; set; }  = null;
        
        public UserControl CurrentElementControl
        {
            get
            {
               return( _currentElementControl);
            }
            set
            {
                _currentElementControl = value;
                ElementControlsWrapPanel.Children.Clear();
                ElementControlsWrapPanel.Children.Add(_currentElementControl);
                ScriptView.Regenerate();
                this.InvalidateVisual();
            } 
        }

    

        #endregion

        #region methods

        void CheckIfContentPermitted()
        {
            if(SvgElementCreator.DescriptiveElements.Contains(CurrentElement.ElementType) || 
               SvgElementCreator.TextContentElements.Contains(CurrentElement.ElementType) ||
               CurrentElement.ElementType == "script" || CurrentElement.ElementType == "style" ||
               CurrentElement.ElementType == "title"  || CurrentElement.ElementType == "foreignObject")
            {
                AddContentButton.IsEnabled = true;
            }
            else
            {
                AddContentButton.IsEnabled = false;
            }
            this.InvalidateVisual();
        }

        #endregion

        #region events

        private void OpenParentButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentElement.SvgParentElement != null)
            {
                CurrentElement = (_currentElement.SvgParentElement);
            }
        }

        private void SaveElementButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Ie != null)
            {
                Ie.Save(ref _currentElement);

                IEnumerable<SvgElement> qop = from uu in SvgCompositionControl.Context.SvgElements
                    where uu.Id == CurrentElement.Id
                    select uu;
                _currentElement = qop.First();

                ScriptView.Regenerate();
            }
            
        }

        private void AddContentButton_OnClick(object sender, RoutedEventArgs e)
        {
            SvgContentControl contentcontrol = new SvgContentControl();
            contentcontrol.Celement = CurrentElement;
            bool? result = contentcontrol.ShowDialog();
            if (result == true)
            {
                if (contentcontrol.Celement.Content != CurrentElement.Content)
                {
                    CurrentElement.Content = contentcontrol.Celement.Content;
                }

                SvgCompositionControl.Context.SaveChanges();
                ScriptView.Regenerate();
            }
        }

        private void EditChildButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChooseSvgElement chch = new ChooseSvgElement();
            ObservableCollection<SvgElement> exi = new ObservableCollection<SvgElement>();

           

            foreach (SvgElement vv in CurrentElement.SvgElements)
            {
                exi.Add(vv);
            }

            if (exi.Any())
            {
                chch.SvgElementObservableCollection = exi;
                bool? result = chch.ShowDialog();
                if (result == true && chch.ChosenSvg != null)
                {
                    foreach (SvgElement vv in CurrentElement.SvgElements)
                    {
                        
                        if (vv.ElementType == chch.ChosenSvg.ElementType && vv.Id == chch.ChosenSvg.Id)
                        {
                            CurrentElement = vv;
                            break;
                        }
                    }
                }
            }
        }



        private void AddChildButton_OnClick(object sender, RoutedEventArgs e)
        {
             ChooseChildElement chch = new ChooseChildElement();
             Ie.BuildPermittedContent();
             chch.SvgChildElementObservableCollection = Ie.PermitedChildElements;
             bool? result = chch.ShowDialog();
             if (result == true && chch.ChosenChild != null)
             {
                 ISvgElementControl Ie = SvgBuildControl.SvgElementCreator.CreateSvgElement(chch.ChosenChild);
                 Ie.Create(ref _currentElement);
                 //var elei = from cce in SvgCompositionControl.Context.SvgElements
                 //    where cce.Id == Ie.SvgElementData.Id
                 //    select cce;
                 //if (elei.Any())
                 //{
                 //    CurrentElement = elei.First();
                 //}
                 //else
                 //{
                 //   _currentElement = Ie.SvgElementData; // should never get here.
                 //}

                 //UserControl uc = (UserControl)Ie;
                 //Ie.Load(_currentElement);
                 //CurrentElementControl = uc;
             }
        }

        private void RemoveChildButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChooseSvgElement chch = new ChooseSvgElement();
            ObservableCollection<SvgElement> exi = new ObservableCollection<SvgElement>();
            foreach (SvgElement vv in CurrentElement.SvgElements)
            {
                exi.Add(vv);
            }

            if (exi.Any())
            {
                chch.SvgElementObservableCollection = exi;
                bool? result = chch.ShowDialog();
                if (result == true && chch.ChosenSvg != null)
                {
                    foreach (SvgElement vv in CurrentElement.SvgElements)
                    {
                        if (vv.ElementType == chch.ChosenSvg.ElementType && vv.Id == chch.ChosenSvg.Id)
                        {

                          MessageBoxResult res =  MessageBox.Show(
                                "Are you sure? This will delete the tree of elements and attributes contained as well.", "Confirm delete");
                          if (res == MessageBoxResult.OK)
                          {
                              RemoveElement(vv);
                              SvgCompositionControl.Context.SaveChanges();
                          }
                          ScriptView.Regenerate();
                          this.InvalidateVisual();
                            break;
                        }
                    }
                }
            }
        }

        private void SwapChildUpButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChooseSvgElement chch = new ChooseSvgElement();
            ObservableCollection<SvgElement> exi = new ObservableCollection<SvgElement>();
            foreach (SvgElement vv in CurrentElement.SvgElements)
            {
                exi.Add(vv);
            }

            if (exi.Any())
            {
                chch.SvgElementObservableCollection = exi;
                bool? result = chch.ShowDialog();
                if (result == true && chch.ChosenSvg != null)
                {
                    if (CurrentElement.SvgElements.Count >= 2)
                    {
                        List<SvgElement> ellist = CurrentElement.SvgElements.ToList();
                        for (int ii = 1; ii < CurrentElement.SvgElements.Count; ii++)
                        {
                            int jj = ii - 1;
                            SvgElement vv = ellist[ii];

                            if (vv.ElementType == chch.ChosenSvg.ElementType && vv.Id == chch.ChosenSvg.Id)
                            {
                                SvgElement uu = ellist[jj];
                                ellist[jj] = vv;
                                ellist[ii] = uu;
                                CurrentElement.SvgElements.Clear();
                                foreach (SvgElement ww in ellist)
                                {
                                    CurrentElement.SvgElements.Add(ww);
                                }
                                ScriptView.Regenerate();
                                this.InvalidateVisual();
                                break;

                            }
                        }
                    }
                }
            }
        }


        private void SwapChildDownButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChooseSvgElement chch = new ChooseSvgElement();
            ObservableCollection<SvgElement> exi = new ObservableCollection<SvgElement>();
            foreach (SvgElement vv in CurrentElement.SvgElements)
            {
                exi.Add(vv);
            }

            if (exi.Any())
            {
                chch.SvgElementObservableCollection = exi;
                bool? result = chch.ShowDialog();
                if (result == true && chch.ChosenSvg != null)
                {
                    if (CurrentElement.SvgElements.Count >= 2)
                    {
                        List<SvgElement> ellist = CurrentElement.SvgElements.ToList();
                        for (int ii = 0; ii < (CurrentElement.SvgElements.Count - 1); ii++)
                        {
                            int jj = ii + 1;
                            SvgElement vv = ellist[ii];

                            if (vv.ElementType == chch.ChosenSvg.ElementType && vv.Id == chch.ChosenSvg.Id)
                            {
                                SvgElement uu = ellist[jj];
                                ellist[jj] = vv;
                                ellist[ii] = uu;
                                CurrentElement.SvgElements.Clear();
                                foreach (SvgElement ww in ellist)
                                {
                                    CurrentElement.SvgElements.Add(ww);
                                }
                                ScriptView.Regenerate();
                                this.InvalidateVisual();
                                break;

                            }
                        }
                    }
                }
            }
        }



        private void DuplicateChildButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChooseSvgElement chch = new ChooseSvgElement();
            ObservableCollection<SvgElement> exi = new ObservableCollection<SvgElement>();



            foreach (SvgElement vv in CurrentElement.SvgElements)
            {
                exi.Add(vv);
            }

            if (exi.Any())
            {
                chch.SvgElementObservableCollection = exi;
                bool? result = chch.ShowDialog();
                if (result == true && chch.ChosenSvg != null)
                {
                    foreach (SvgElement vv in CurrentElement.SvgElements)
                    {

                        if (vv.ElementType == chch.ChosenSvg.ElementType && vv.Id == chch.ChosenSvg.Id)
                        {
                            SvgElement nchild = DuplicateElement(vv);
                            SvgCompositionControl.Context.SaveChanges();
                            CurrentElement.SvgElements.Add(nchild);
                            SvgCompositionControl.Context.SaveChanges();
                            break;
                        }
                    }
                }
            }
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
                foreach (SvgColor eecol in eea.SvgColors )
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


        public void CopyChildButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChooseSvgElement chch = new ChooseSvgElement();
            ObservableCollection<SvgElement> exi = new ObservableCollection<SvgElement>();

            foreach (SvgElement vv in CurrentElement.SvgElements)
            {
                exi.Add(vv);
            }

            if (exi.Any())
            {
                chch.SvgElementObservableCollection = exi;
                bool? result = chch.ShowDialog();
                if (result == true && chch.ChosenSvg != null)
                {
                    foreach (SvgElement vv in CurrentElement.SvgElements)
                    {
                        if (vv.ElementType == chch.ChosenSvg.ElementType && vv.Id == chch.ChosenSvg.Id)
                        {
                            var cpys = from aa in SvgCompositionControl.Context.SvgToolStorages
                                where aa.Name == "DbCopy"
                                select aa;

                            if (cpys.Any() && cpys.Count() == 1)
                            {
                                SvgCompositionControl.Context.SvgToolStorages.Remove(cpys.First());
                                SvgCompositionControl.Context.SaveChanges();
                            }

                            SvgToolStorage  ncpy = new SvgToolStorage();
                            ncpy.Name = "DbCopy";
                            ncpy.Description = "element for copy paste";
                            ncpy.SavedElement = DuplicateElement(vv);
                            SvgCompositionControl.Context.SvgToolStorages.Add(ncpy);
                            SvgCompositionControl.Context.SaveChanges();

                        }
                    }
                }
            }
        }

        public void PasteChildButton_OnClick(object sender, RoutedEventArgs e)
        {
            var cpys = from aa in SvgCompositionControl.Context.SvgToolStorages
                where aa.Name == "DbCopy"
                select aa;

            if (cpys.Any() && cpys.Count() == 1)
            {
                string st = cpys.First().SavedElement.ElementType;
                if (Ie.PermitedChildElements.Contains(st))
                {
                    SvgElement pst = DuplicateElement(cpys.First().SavedElement);
                    CurrentElement.SvgElements.Add(pst);
                    SvgCompositionControl.Context.SaveChanges();
                    MessageBox.Show($"Paste performed {pst.ElementType} {pst.SvgElementName}", "Paste");
                }
                
            }
        }

        public void RemoveElement(SvgElement ee)
        {
            foreach(SvgElement it in ee.SvgElements)
            {
                RemoveElement(it);
            }
            while(ee.SvgAttributes.Any())
            {
                SvgAttribute aa = ee.SvgAttributes.First();
                while(aa.SvgColors.Any())
                {
                    SvgCompositionControl.Context.SvgColors.Remove(aa.SvgColors.First());
                }

                SvgCompositionControl.Context.SvgAttributes.Remove(aa);
            }

            SvgCompositionControl.Context.SvgElements.Remove(ee);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            // Show Svg text here
        }
        #endregion

        
    }
}