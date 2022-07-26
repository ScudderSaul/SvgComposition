using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using SvgComposition.Controls;
using SvgComposition.Model;

namespace SvgComposition
{
    public partial class CodeGeneration
    {
        private string svgtext = String.Empty;
        private int spacecnt = 0;
        private string spacelead = string.Empty;
        private int lastid;

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

            IEnumerable<SvgElement> Yop = from qq in SvgCompositionControl.Context.SvgElements
                where qq.Id == idkey && qq.ElementType == "svg" && qq.TopSvg == true
                                          select qq;

            if (!Yop.Any())
            {
                return("No Svg found");
            }
            svgtext = String.Empty;

            spacecnt = 0;
            RunElement(Yop.First());

            return (svgtext);
        }

        public string Regenerate()
        {
            IEnumerable<SvgElement> Yop = from qq in SvgCompositionControl.Context.SvgElements
                where qq.Id == lastid && qq.ElementType == "svg" && qq.TopSvg == true
                select qq;

            if (!Yop.Any())
            {
                return ("No Svg found");
            }
            svgtext = String.Empty;

            spacecnt = 0;
            RunElement(Yop.First());

            return (svgtext);
        }
    

        void RunElement(SvgElement el)
        {
           
            ComposeIndent();
            spacecnt++;
            int lenatstart = svgtext.Length;
            AppendStart(el);

            foreach (SvgAttribute at in el.SvgAttributes)
            {
                IEnumerable<SvgAttribute> Uat = from rr in SvgCompositionControl.Context.SvgAttributes
                    where rr.Id == at.Id
                    select rr;
                if (Uat.Any())
                {
                    RunAttribute(Uat.First());
                }

                if ((svgtext.Length - lenatstart) > 100)
                {
                    svgtext += $"\r\n{spacelead}";
                    lenatstart = svgtext.Length;
                }
            }

            svgtext += ">";
            if (string.IsNullOrWhiteSpace(el.Content) == false)
            { 
                svgtext += $"{el.Content}";
            }
            

            foreach (SvgElement cel in el.SvgElements)
            {
              // svgtext += $"\r\n";
                IEnumerable<SvgElement> qop = from uu in SvgCompositionControl.Context.SvgElements
                    where uu.Id == cel.Id
                    select uu;
                if (qop.Any())
                {
                    RunElement(qop.First());
                }
            }
            svgtext += $"</{el.ElementType}>\r\n";
            spacecnt--;
            ComposeIndent();
        }

        public void RunAttribute(SvgAttribute at)
        {
            if (!string.IsNullOrWhiteSpace(at.AttributeValue))
            {
                if (at.AttributeType == SvgAttributeType.LengthPercent && at.SvgUnit != null)
                {
                    svgtext += $"{at.AttributeName}=\"{at.AttributeValue}{at.SvgUnit}\" ";
                }
                else
                {
                    svgtext += $"{at.AttributeName}=\"{at.AttributeValue.Trim(' ')}\" ";
                }
                
            }
        }

        public void AppendStart(SvgElement el)
        {
            if (el.TopSvg == true)
            {
                svgtext = $"{spacelead}<{el.ElementType} xmlns=\"http://www.w3.org/2000/svg\"\r\n xmlns:xlink=\"http://www.w3.org/1999/xlink\"\r\n{spacelead} ";
            }
            else
            {
                svgtext += $"\r\n{spacelead}<{el.ElementType} ";
            }
        }
    }
}