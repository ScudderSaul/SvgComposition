using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition
{
    public partial class CodeToDuplicateSvgData
    {
        private string svgtext = String.Empty;
        private int spacecnt = 0;
        private string spacelead = string.Empty;
        private int lastid;
        private Random _rand = new Random();

        private int elcnt = 0;
        private int atcnt = 0;
        private int colcnt = 0;
        private int pcnt = 0;

        public string Svgtext
        {
            get { return svgtext; }
            set { svgtext = value; }
        }

        void ComposeIndent()
        {
            int tt = spacecnt * 5 + 3;
            spacelead = string.Empty;
            for (int ii = 0; ii < tt; ii++)
            {
                spacelead += " ";
            }
        }



        public string Generate(int idkey)
        {
            lastid = idkey;
            elcnt = 0;
            atcnt = 0;
            colcnt = 0;
            pcnt = 0;
            IEnumerable<SvgElement> Yop = from qq in SvgCompositionControl.Context.SvgElements
                where qq.Id == idkey && qq.ElementType == "svg" && qq.TopSvg == true
                                          select qq;


            if (!Yop.Any())
            {
                return("No Svg found");
            }
            svgtext = String.Empty;

            svgtext = "using System.Collections.Generic;\r\n ";
            svgtext += $"using System.Collections.ObjectModel;\r\n ";
            svgtext += $"using System.ComponentModel;\r\n";
            svgtext += $"using System.Linq;\r\n";
            svgtext += $"using System.Runtime.CompilerServices;\r\n";
            svgtext += $"using System.Windows;\r\n";
            svgtext += $"using System.Windows.Controls;\r\n";
            svgtext += $"using System.Windows.Media;\r\n";
            svgtext += $"using SvgComposition.AttributeControls;\r\n";
            svgtext += $"using SvgComposition.Controls;\r\n";
            svgtext += $"using SvgComposition.Model;\r\n";
            svgtext += $"using SvgCompositionTool.Dialogs;\r\n ";
            svgtext += $"\r\n";
            svgtext += $"namespace SvgComposition.ElementControls\r\n";
            svgtext += $"{{\r\n";

            spacecnt = 0;
            svgtext += $"public void CreateElement{_rand.Next(10000)}()\r\n";
            svgtext += $"{{\r\n";

            RunElement(Yop.First());
            svgtext += $"     }}\r\n";
            svgtext += $"}}\r\n";
            
            return (svgtext);
        }

        

        void RunElement(SvgElement el, int parcnt = 0)
        {
            
            ComposeIndent();

            elcnt++;
            string lv = "false";
            if (el.TopSvg == true)
            {
               lv  = "true";
            }

           
            svgtext += $"SvgElement el{elcnt} = CreateAnElement({lv}, el{parcnt}, \"{el.SvgElementName}\", \"{el.Content}\", \"{el.ElementType}\",{el.ElementOrder});\r\n";
            
            foreach (SvgAttribute at in el.SvgAttributes)
            {
                svgtext += $"SvgAttribute at{atcnt +1} = CreateAnAttribute(el{elcnt}, \"{at.AttributeName}\", \"{at.AttributeValue}\", \"{at.SecondaryValue}\", SvgAttributeType.{at.AttributeType}, \"{at.SvgUnit}\");\r\n";

                foreach (SvgColor col in at.SvgColors)
                {
                    svgtext += $"CreateAColor(at{atcnt + 1}, {col.ColorIndex}, {col.Color().A}, {col.Color().R}, {col.Color().G}, {col.Color().B});\r\n";
                }

                atcnt++;

            }

            parcnt = elcnt;
            foreach (SvgElement ee in el.SvgElements)
            {
               
                RunElement(ee, parcnt);
            }


            spacecnt--;
            ComposeIndent();
        }

      


    }
}