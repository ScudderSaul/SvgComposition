using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventAtributeCodeWriter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        public List<string> GlobalEventNames = new List<string>
        {
            "oncancel", "oncanplay", "oncanplaythrough", "onchange", "onclick", "onclose",
            "oncuechange", "ondblclick", "ondrag", "ondragend", "ondragenter", "ondragexit",
            "ondragleave", "ondragover", "ondragstart", "ondrop", "ondurationchange", "onemptied",
            "onended", "onerror", "onfocus", "oninput", "oninvalid", "onkeydown", "onkeypress",
            "onkeyup", "onload", "onloadeddata", "onloadedmetadata", "onloadstart", "onmousedown",
            "onmouseenter", "onmouseleave", "onmousemove", "onmouseout", "onmouseover", "onmouseup",
            "onmousewheel", "onpause", "onplay", "onplaying", "onprogress", "onratechange", "onreset",
            "onresize", "onscroll", "onseeked", "onseeking", "onselect", "onshow", "onstalled",
            "onsubmit", "onsuspend", "ontimeupdate", "ontoggle", "onvolumechange", "onwaiting"
        };

        public List<string> AnimationEventNames = new List<string>
        {
            "onbegin", "onend", "onrepeat"
        };

        public List<string> DocumentEventAttributes = new List<string>
        {
            "onabort", "onerror", "onresize", "onscroll", "onunload"
        };

        public List<string> GraphicalEventAttributes = new List<string>
        {
            "onactivate", "onfocusin", "onfocusout"
        };

        public List<string> DocumentElementEventAttributes = new List<string>
        {
            "oncopy", "oncut", "onpaste"
        };

        

        void CreateFiles(string name)
        {
            string stcs = $"Svg{name}.xaml.cs";
            string vcs = CodeOut(name);
           
            string pathcs = @"Q:\code\Code_2019\SvgComposition\EventAttributesN\" + stcs;

            using (StreamWriter sw = File.CreateText(pathcs))
            {
                sw.WriteLine(vcs);
            }

            string stxaml = $"Svg{name}.xaml";
            string vxaml = XamlOut(name);
            string pathxaml = @"Q:\code\Code_2019\SvgComposition\EventAttributesN\" + stxaml;

            using (StreamWriter sw = File.CreateText(pathxaml))
            {
                sw.WriteLine(vxaml);
            }

        }

        string CodeOut(string name)
        {
            string st = string.Empty;


            st += $"using System.ComponentModel;\r\n";
            st += $"using System.Linq;\r\n";
            st += $"using System.Runtime.CompilerServices;\r\n";
            st += $"using System.Windows;\r\n";
            st += $"using System.Windows.Controls;\r\n";
            st += $"using SvgComposition.Controls;\r\n";
            st += $"using SvgComposition.Model;\r\n";

            st += $"  \r\n";
            st += $"namespace SvgComposition.AttributeControls.EventAttributes\r\n";
            st += $"{{\r\n";
            st += $"  \r\n";

            st += $"     public partial class Svg{name} : UserControl, ISvgAttributeControl, INotifyPropertyChanged\r\n";
            st += $"     {{\r\n";
            st += $"  \r\n";
            st += $"     private string _labelText;\r\n";
            st += $"     private string _svgAttributeLocalValue = \" \";\r\n";
            st += $"  \r\n";
            st += $"       public Svg{name}()\r\n";
            st += $"       {{\r\n";
            st += $"          InitializeComponent();\r\n";
            st += $"          DataContext = this;\r\n";
            st += $"          LabelText = \"{name}\";\r\n";
            st += $"       }}\r\n";
            st += $"  \r\n";

            st += $"        public static readonly DependencyProperty IsLabelTextProperty =  \r\n";
            st += $"            DependencyProperty.Register(\"IsLabelText\", typeof(string), typeof(Svg{name}),   \r\n";
            st += $"                new FrameworkPropertyMetadata(\"none\", FrameworkPropertyMetadataOptions.AffectsRender,  \r\n";
            st += $"                    null));  \r\n";
            st += $"  \r\n";

            st += $"  \r\n";
            st += $"       public string LabelText\r\n";
            st += $"       {{\r\n";
            st += $"            get {{ return (string)GetValue(IsLabelTextProperty); }}\r\n";
            st += $"            set\r\n";
            st += $"            {{\r\n";
            st += $"               SetValue(IsLabelTextProperty, value);\r\n";
            st += $"               _labelText = value;\r\n";
            st += $"                OnPropertyChanged();\r\n";
            st += $"            }}\r\n";
            st += $"      }}\r\n";
            st += $"  \r\n";

            st += $"        public static readonly DependencyProperty SvgAttributeCurrentValueProperty =  \r\n";
            st += $"            DependencyProperty.Register(\"SvgAttributeCurrentValue\", typeof(string), typeof(Svg{name}),   \r\n";
            st += $"                new FrameworkPropertyMetadata(\"none\", FrameworkPropertyMetadataOptions.AffectsRender,  \r\n";
            st += $"                    null));  \r\n";
            st += $"  \r\n";

            st += $"  \r\n";
            st += $"       public string SvgAttributeCurrentValue\r\n";
            st += $"       {{\r\n";
            st += $"            get {{ return (string)GetValue(SvgAttributeCurrentValueProperty); }}\r\n";
            st += $"            set\r\n";
            st += $"            {{\r\n";
            st += $"               SetValue(SvgAttributeCurrentValueProperty, value);\r\n";
            st += $"               _svgAttributeLocalValue = value;\r\n";
            st += $"                OnPropertyChanged();\r\n";
            st += $"            }}\r\n";
            st += $"      }}\r\n";
            st += $"  \r\n";



            st += $"  \r\n";
            st += $"       public bool CanAnimate {{ get; set; }} = false;\r\n";
            st += $"  \r\n";
            st += $"       public SvgAttribute Create(ref SvgElement ele)\r\n";
            st += $"       {{\r\n";
            st += $"           SvgAttribute at = new SvgAttribute();\r\n";
            st += $"           at.Id = SvgAttributeCreator.FindNextSvgAttributeId();\r\n";
            st += $"           at.AttributeType = SvgAttributeType.Value;\r\n";
            st += $"           at.AttributeValue = \"\";\r\n";
            st += $"           at.AttributeName = _labelText;\r\n";
            st += $"           at.SvgElement = ele;\r\n";
            st += $"           at.SvgElementId = ele.Id;\r\n";
            st += $"           ele.SvgAttributes.Add(at);\r\n";
            st += $"           SvgCompositionControl.Context.SvgAttributes.Add(at);\r\n";
            st += $"           SvgCompositionControl.Context.SaveChanges();\r\n";
            st += $"           Load(at);\r\n";
            st += $"           return (at);\r\n";
            st += $"      }}\r\n";
            st += $"  \r\n";
            st += $"     public void Load(SvgAttribute at)\r\n";
            st += $"     {{\r\n";
            st += $"           SvgAttributeCurrentValue =  SvgAttributeBlock.Text = at.AttributeValue;\r\n";
            st += $"     }}\r\n";
            st += $"  \r\n";
            st += $"     public void Save(SvgAttribute at)\r\n";
            st += $"     {{\r\n";
            st += $"          at.AttributeValue = SvgAttributeCurrentValue =  SvgAttributeBlock.Text;\r\n";
            st += $"          at.AttributeType = SvgAttributeType.Value;\r\n";
            st += $"            SvgCompositionControl.Context.SaveChanges();\r\n";
            st += $"     }}\r\n";
            st += $"  \r\n";
            st += $"         public event PropertyChangedEventHandler PropertyChanged;  \r\n";
            st += $"  \r\n";
            st += $"         protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)\r\n";
            st += $"       {{\r\n";
            st += $"           var handler = PropertyChanged;\r\n";
            st += $"            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));\r\n";
            st += $"        }}\r\n";
            st += $"  \r\n";
            st += $"  \r\n";
            st += $"  \r\n";

            st += $"  }}\r\n";
            st += $"}}\r\n";
            st += $"  \r\n";
            st += $"  \r\n";
            return (st);
        }


        string XamlOut(string name)
        {
            string st = string.Empty;
            st += $"<UserControl x:Class=\"SvgComposition.AttributeControls.EventAttributes.Svg{name}\"\r\n";
            st += $"            xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" \r\n";
            st += $"            xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"  \r\n";
            st += $"            xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"   \r\n";
            st += $"            xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"   \r\n";
            st += $"            xmlns:local=\"clr-namespace:SvgComposition.AttributeControls\"  \r\n";
            st += $"            mc:Ignorable=\"d\"   \r\n";
            st += $"            d:DesignHeight=\"400\" d:DesignWidth=\"400\">  \r\n";
            st += $"     <GroupBox Margin=\"2\" >\r\n";
            st += $"        <GroupBox.Header>\r\n";
            st += $"            <ContentControl>\r\n";
            st += $"                <TextBlock x:Name=\"LabelBlock\" Margin=\"2,3,3,2\" FontSize=\"10\" Width=\"100\" \r\n";
            st += $"                          Text=\"{{Binding Path=LabelText, Mode=OneWay}}\"   \r\n";
            st += $"                          Foreground=\"MidnightBlue\" >\r\n";
            st += $"               </TextBlock>\r\n";
            st += $"           </ContentControl>  \r\n";
            st += $"        </GroupBox.Header>  \r\n";
            st += $"       <Grid HorizontalAlignment=\"Left\" VerticalAlignment=\"Top\" Margin=\"2\">\r\n";
            st += $"           <Grid.RowDefinitions>\r\n";
            st += $"               <RowDefinition Height=\"33\"></RowDefinition>  \r\n";
            st += $"           </Grid.RowDefinitions>  \r\n";
            st += $"          <Grid.ColumnDefinitions>  \r\n";
            st += $"               <ColumnDefinition Width=\"300\"></ColumnDefinition>  \r\n";
            st += $"          </Grid.ColumnDefinitions>  \r\n";
            st += $"          <TextBox x:Name=\"SvgAttributeBlock\" Margin=\"2,3,3,2\" FontSize=\"9\" Width=\"300\"\r\n";
            st += $"             Text=\"{{Binding Path=SvgAttributeCurrentValue, Mode=TwoWay}}\" Height=\"26\" VerticalAlignment=\"Top\"\r\n";
            st += $"             Foreground=\"Black\"  Grid.Column=\"0\" Grid.Row=\"0\">\r\n";
            st += $"            <TextBox.ToolTip>  \r\n";
            st += $"               <TextBlock Text=\"Enter call to a [{name}Action(evt)] Event function for {name} \" Foreground=\"DarkGreen\" FontSize=\"9\" Margin=\"1\"></TextBlock>\r\n";
            st += $"            </TextBox.ToolTip>\r\n";
            st += $"           </TextBox>\r\n";
            st += $"       </Grid>\r\n";
            st += $"    </GroupBox>\r\n";
            st += $"</UserControl>\r\n";
            st += $"  \r\n";
            
            return (st);
        }

        public void ElemList()
        {
            string gt = string.Empty;

            gt = $"// AnimationEventNames -----------------------------------\r\n";
            foreach (string ss in AnimationEventNames)
            {
                gt += $"           AttributeTypesDictionary.Add(\"{ss}\", typeof(Svg{ss}));\r\n";
            }
            gt += $"// DocumentEventAttributes -----------------------------------\r\n";
            foreach (string ss in DocumentEventAttributes)
            {
                gt += $"           AttributeTypesDictionary.Add(\"{ss}\", typeof(Svg{ss}));\r\n";
            }

            gt += $"// GraphicalEventAttributes -----------------------------------\r\n";
            foreach (string ss in GraphicalEventAttributes)
            {
                gt += $"           AttributeTypesDictionary.Add(\"{ss}\", typeof(Svg{ss}));\r\n";
            }

            gt += $"// DocumentElementEventAttributes -----------------------------------\r\n";
            foreach (string ss in DocumentElementEventAttributes)
            {
                gt += $"           AttributeTypesDictionary.Add(\"{ss}\", typeof(Svg{ss}));\r\n";
            }

            gt += $"// GlobalEventNames -----------------------------------\r\n";
            foreach (string ss in GlobalEventNames)
            {
                gt += $"           AttributeTypesDictionary.Add(\"{ss}\", typeof(Svg{ss}));\r\n";
            }

            string pathat = @"Q:\code\Code_2019\SvgComposition\EventAttributesN\AttrAdd.cs";

            using (StreamWriter sw = File.CreateText(pathat))
            {
                sw.WriteLine(gt);
            }

        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Anim_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (string go in AnimationEventNames)
            {
                CreateFiles(go);
            }

            ElemList();
        }


        private void Graph_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (string go in GraphicalEventAttributes)
            {
                CreateFiles(go);
            }

            ElemList();
        }

        private void DocElEv_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (string go in DocumentElementEventAttributes)
            {
                CreateFiles(go);
            }

            ElemList();
        }

        private void Docu_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (string go in DocumentEventAttributes)
            {
                CreateFiles(go);
            }

            ElemList();
        }

        private void General_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (string go in GlobalEventNames)
            {
                CreateFiles(go);
            }

            ElemList();
        }
    }
}
