﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SvgComposition.AttributeControls;
using SvgComposition.Controls;
using SvgComposition.Model;
using SvgCompositionTool.Dialogs;

namespace SvgComposition.ElementControls.BasicShapes
{
    /// <summary>
    /// Interaction logic for SvgEllipse.xaml
    /// </summary>
    public partial class SvgEllipse : UserControl, ISvgElementControl, INotifyPropertyChanged
    {
      #region fields
        private SvgElement _svgElement;

        private ObservableCollection<string> _permitedChildElements = new ObservableCollection<string>();

        private ObservableCollection<string> _attributeChoices = new ObservableCollection<string>();

        private List<string> LocalAttributes = new List<string>
        {
            "id", "style", "class"
        };


        private List<string> _startWith = new List<string>
        {
            "cx", "cy", "rx", "ry", "stroke", "stroke-opacity", "stroke-width", "fill", "fill-opacity",
        };
        #endregion
        public SvgEllipse()
        {
            this.Visibility = Visibility.Visible;
            this.BorderBrush = new SolidColorBrush(Colors.Black);
            this.BorderThickness = new Thickness(1.0);
            BuildPermittedContent();
            SvgElementTypeName = "ellipse";
            InitializeComponent();
            DataContext = this;
        }

        // the svg element's name, ie. circle, ellipse etc
        public string SvgElementTypeName { get; set; }

        public ObservableCollection<string> PermitedChildElements
        {
            get { return _permitedChildElements; }

        }

        public ObservableCollection<string> PossibleAttributes
        {
            get { return _attributeChoices; }
        }

        public SvgElement SvgElementData
        {
            get { return _svgElement; }
            set { _svgElement = value; }
        }

        public void Create(ref SvgElement parent)
        {
            _svgElement = new SvgElement();
            _svgElement.Id = SvgElementCreator.FindNextSvgElementId();
            _svgElement.ElementType = SvgElementTypeName;
            _svgElement.SvgParentElement = parent;
            _svgElement.SvgParentElementId = parent.Id;
            parent.SvgElements.Add(_svgElement);
            SvgCompositionControl.Context.SvgElements.Add(_svgElement);
            SvgCompositionControl.Context.SaveChanges();

            foreach (string st in _startWith)
            {
                if (SvgAttributeCreator.AttributeImplemented(st) == false)
                {
                    MessageBox.Show($"No ISvgAttributeControl exists for {st}");
                }
                else
                {
                    ISvgAttributeControl ic = SvgAttributeCreator.CreateSvgAttribute(st);
                    if (ic == null)
                    {
                        MessageBox.Show($"ISvgAttributeControl ic == null for {st}");
                    }
                    else
                    {
                        UserControl uc = (UserControl)ic;
                        AttributeHolder.Children.Add(uc);
                        uc.Name = SvgAttributeCreator.ConvertToUiName(st);
                        ic.Create(ref _svgElement);
                    }
                }
            }
            SvgCompositionControl.Context.SaveChanges();
        }


        public void Load(SvgElement ee)
        {
            _svgElement = ee;
            AttributeHolder.Children.Clear();
            foreach (SvgAttribute at in ee.SvgAttributes)
            {
                ISvgAttributeControl ic = SvgAttributeCreator.CreateSvgAttribute(at.AttributeName);
                UserControl uc = (UserControl)ic;
                AttributeHolder.Children.Add(uc);
                uc.Name = SvgAttributeCreator.ConvertToUiName(at.AttributeName);
                ic.Load(at);
            }
        }

        public void Save(ref SvgElement ee)
        {
            foreach (SvgAttribute at in ee.SvgAttributes)
            {
                string aname = SvgAttributeCreator.ConvertToUiName(at.AttributeName);
                foreach (ISvgAttributeControl ic in AttributeHolder.Children)
                {
                    UserControl uc = (UserControl)ic;
                    if (uc.Name == aname)
                    {
                        ic.Save(at);
                    }
                }
                
                SvgCompositionControl.Context.SaveChanges();
            }
        }

        public void AddAttribute(string name)
        {
            var qq = from cc in _svgElement.SvgAttributes
                where cc.AttributeName == name
                select cc;

            if (qq.Any())
            {
                MessageBox.Show($"Attribute {name} already exists");
                return;
            }

            if (SvgAttributeCreator.AttributeImplemented(name) == false)
            {

                MessageBox.Show($"Debug: No ISvgAttributeControl exists for {name}");
                return;
            }
            ISvgAttributeControl ic = SvgAttributeCreator.CreateSvgAttribute(name);
            if (ic == null)
            {
                MessageBox.Show($"No ISvgAttributeControl was returned for {name}");
            }
            else
            {
                UserControl uc = (UserControl)ic;
                AttributeHolder.Children.Add(uc);
                uc.Name = SvgAttributeCreator.ConvertToUiName(name);
                ic.Create(ref _svgElement);
            }

           
        }



        public void BuildPermittedContent()
        {
            foreach (string gg in SvgElementCreator.DescriptiveElements)
            {
                if (_permitedChildElements.Contains(gg) == false)
                {
                    _permitedChildElements.Add(gg);
                }
            }
            foreach (string gg in SvgElementCreator.AnimationElements)
            {
                if (_permitedChildElements.Contains(gg) == false)
                {
                    _permitedChildElements.Add(gg);
                }
            }

            foreach (string ss in SvgElementCreator.DepreciatedElements)
            {
                _permitedChildElements.Remove(ss);
                _permitedChildElements.Remove(ss);
            }
        }

        public void BuildUnusedAttributeList()
        {
            PossibleAttributes.Clear();
            foreach (string st in LocalAttributes)
            {
                _attributeChoices.Add(st);
            }

            foreach (string st in _startWith)
            {
                if (_attributeChoices.Contains(st) == false)
                {
                    _attributeChoices.Add(st);
                }
            }

            foreach (string st in SvgAttributeCreator.MpAttributesNames)
            {
                if (_attributeChoices.Contains(st) == false)
                {
                    _attributeChoices.Add(st);
                }
            }

            foreach (string st in SvgAttributeCreator.GlobalEventAttributes)
            {
                if (_attributeChoices.Contains(st) == false)
                {
                    _attributeChoices.Add(st);
                }
            }

            foreach (string st in SvgAttributeCreator.GraphicalEventAttributes)
            {
                if (_attributeChoices.Contains(st) == false)
                {
                    _attributeChoices.Add(st);
                }
            }


            var qq = from cc in _svgElement.SvgAttributes
                select cc.AttributeName;

            foreach (string st in qq)
            {
                _attributeChoices.Remove(st);
            }
        }
       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        private void RemoveAttributeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var qq = from cc in _svgElement.SvgAttributes
                select cc.AttributeName;

            ObservableCollection<string> ob = new ObservableCollection<string>();

            foreach (string st in qq)
            {
                ob.Add(st);
            }
            ChooseAttribute chch = new ChooseAttribute();
            chch.SvgAttributeNameObservableCollection = ob;
            bool? result = chch.ShowDialog();
            if (result == true)
            {
                var ww = from cc in _svgElement.SvgAttributes
                    where chch.ChosenAttribute == cc.AttributeName
                    select cc;

                if (ww.Any())
                {
                    SvgAttribute at = ww.First();
                    List<SvgColor> css = new List<SvgColor>();
                    foreach (SvgColor col in at.SvgColors)
                    {
                        css.Add(col);
                    }

                    foreach (SvgColor col in css)
                    {
                        SvgCompositionControl.Context.SvgColors.Remove(col);
                    }

                    SvgCompositionControl.Context.SvgAttributes.Remove(at);
                    SvgCompositionControl.Context.SaveChanges();
                    Load(_svgElement);
                }
            }
        }
        private void AddAttributeButton_OnClick(object sender, RoutedEventArgs e)
        {
            BuildUnusedAttributeList();
            if (PossibleAttributes.Any())
            {
                ChooseAttribute chch = new ChooseAttribute();
                chch.SvgAttributeNameObservableCollection = PossibleAttributes;
                bool? result = chch.ShowDialog();
                if (result == true)
                {
                    AddAttribute(chch.ChosenAttribute);
                }
            }
        }

    }
}