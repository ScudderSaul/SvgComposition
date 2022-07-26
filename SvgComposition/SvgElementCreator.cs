using System;
using System.Collections.Generic;
using System.Linq;
using SvgComposition.Controls;
using SvgComposition.ElementControls;
using SvgComposition.ElementControls.AnimationElements;
using SvgComposition.ElementControls.ContainerElements;
using SvgComposition.ElementControls.DescriptiveElements;
using SvgComposition.ElementControls.BasicShapes;
using SvgComposition.ElementControls.FilterPrimitiveElements;
using SvgComposition.ElementControls.GradientElements;
using SvgComposition.ElementControls.GraphicElements;
using SvgComposition.ElementControls.OtherElements;
using SvgComposition.ElementControls.TextContentElements;
using SvgAnimate = SvgComposition.ElementControls.AnimationElements.SvgAnimate;
using SvgAnimateTransform = SvgComposition.ElementControls.AnimationElements.SvgAnimateTransform;
using SvgCircle = SvgComposition.ElementControls.BasicShapes.SvgCircle;
using SvgDefs = SvgComposition.ElementControls.ContainerElements.SvgDefs;
using SvgEllipse = SvgComposition.ElementControls.BasicShapes.SvgEllipse;
using SvgfeBlend = SvgComposition.ElementControls.FilterPrimitiveElements.SvgfeBlend;
using SvgfeColorMatrix = SvgComposition.ElementControls.FilterPrimitiveElements.SvgfeColorMatrix;
using SvgLine = SvgComposition.ElementControls.BasicShapes.SvgLine;


namespace SvgComposition
{

    public class SvgElementCreator
    {
        public  Dictionary<string, Type> ElementTypesDictionary = new Dictionary<string, Type>();

        public Dictionary<string, string> ElementNamesDictionary = new Dictionary<string, string>();

        private static int _nextSvgElementId = 1;

        static SvgElementCreator()
        {

        }

        public void BuildElementsNamesDictionary()
        {
            ElementNamesDictionary.Clear();
            // animation elements
            ElementNamesDictionary.Add("animate", "SvgAnimate");
            ElementNamesDictionary.Add("animateMotion", "SvgAnimateMotion");
            ElementNamesDictionary.Add("animateTransform", "SvgAnimateTransform");
            ElementNamesDictionary.Add("set", "SvgSet");
            ElementNamesDictionary.Add("mpath", "SvgMpath");

            // basic shapes
            ElementNamesDictionary.Add("circle", "SvgCircle");
            ElementNamesDictionary.Add("ellipse", "SvgEllipse");
            ElementNamesDictionary.Add("rect", "SvgRect");
            ElementNamesDictionary.Add("line", "SvgLine");
            ElementNamesDictionary.Add("path", "SvgPath");
            ElementNamesDictionary.Add("polygon", "SvgPolygon");
            ElementNamesDictionary.Add("polyline", "SvgPolyline");

            // container

            ElementNamesDictionary.Add("svg", "SvgSvg");
            ElementNamesDictionary.Add("a", "SvgA");
            ElementNamesDictionary.Add("g", "SvgG");
            ElementNamesDictionary.Add("defs", "SvgDefs");
            ElementNamesDictionary.Add("marker", "SvgMarker");
            ElementNamesDictionary.Add("mask", "SvgMask");
            ElementNamesDictionary.Add("pattern", "SvgPattern");
            ElementNamesDictionary.Add("switch", "SvgSwitch");
            ElementNamesDictionary.Add("symbol", "SvgSymbol");

            //  Descriptive
            ElementNamesDictionary.Add("desc", "SvgDesc");
            ElementNamesDictionary.Add("metadata", "SvgMetadata");
            ElementNamesDictionary.Add("title", "SvgTitle");

            //filter primitave
            ElementNamesDictionary.Add("feBlend", "SvgfeBlend");
            ElementNamesDictionary.Add("feColorMatrix", "SvgfeColorMatrix");
            ElementNamesDictionary.Add("feComponentTransfer", "SvgfeComponentTransfer");
            ElementNamesDictionary.Add("feComposite", "SvgfeComposite");
            ElementNamesDictionary.Add("feConvolveMatrix", "SvgfeConvolveMatrix");
            ElementNamesDictionary.Add("feDiffuseLighting", "SvgfeDiffuseLighting");
            ElementNamesDictionary.Add("feDisplacementMap", "SvgfeDisplacementMap");
            ElementNamesDictionary.Add("feDistantLight", "SvgfeDistantLight");
            ElementNamesDictionary.Add("feDropShadow", "SvgfeDropShadow");
            ElementNamesDictionary.Add("feFlood", "SvgfeFlood");
            ElementNamesDictionary.Add("feFuncA", "SvgfeFuncA");
            ElementNamesDictionary.Add("feFuncB", "SvgfeFuncB");
            ElementNamesDictionary.Add("feFuncG", "SvgfeFuncG");
            ElementNamesDictionary.Add("feFuncR", "SvgfeFuncR");
            ElementNamesDictionary.Add("feGaussianBlur", "SvgfeGaussianBlur");
            ElementNamesDictionary.Add("feImage", "SvgfeImage");
            ElementNamesDictionary.Add("feMerge", "SvgfeMerge");
            ElementNamesDictionary.Add("feMergeNode", "SvgfeMergeNode");
            ElementNamesDictionary.Add("feMorphology", "SvgfeMorphology");
            ElementNamesDictionary.Add("feOffset", "SvgfeOffset");
            ElementNamesDictionary.Add("fePointLight", "SvgfePointLight");
            ElementNamesDictionary.Add("feSpecularLighting", "SvgfeSpecularLighting");
            ElementNamesDictionary.Add("feSpotLight", "SvgfeSpotLight");
            ElementNamesDictionary.Add("feTile", "SvgfeTile");
            ElementNamesDictionary.Add("feTurbulence", "SvgfeTurbulence");

            // gradient

            ElementNamesDictionary.Add("linearGradient", "SvgLinearGradient");
            ElementNamesDictionary.Add("radialGradient", "SvgRadialGradient");
            ElementNamesDictionary.Add("stop", "SvgStop");

            // graphics

            ElementNamesDictionary.Add("image", "SvgImage");
            ElementNamesDictionary.Add("use", "SvgUse");

            // other
            ElementNamesDictionary.Add("filter", "SvgFilter");
            ElementNamesDictionary.Add("foreignObject", "SvgForeignObject");
            ElementNamesDictionary.Add("hatch", "SvgHatch");
            ElementNamesDictionary.Add("hatchpath", "SvgHatchpath");
            ElementNamesDictionary.Add("script", "SvgScript");
            ElementNamesDictionary.Add("style", "SvgStyle");
            ElementNamesDictionary.Add("view", "SvgView");

            // text content


            ElementNamesDictionary.Add("text", "SvgComposition.ElementControls.TextContentElements.SvgText");
            ElementNamesDictionary.Add("textPath", "SvgTextPath");
            ElementNamesDictionary.Add("tspan", "SvgTspan");

           
        }

        public  void BuildElementsDictionary()
        {
            ElementTypesDictionary.Clear();
        // animation elements
            ElementTypesDictionary.Add("animate", typeof(SvgAnimate));
            ElementTypesDictionary.Add("animateMotion", typeof(SvgAnimateMotion));
            ElementTypesDictionary.Add("animateTransform", typeof(SvgAnimateTransform));
            ElementTypesDictionary.Add("set", typeof(SvgSet));
            ElementTypesDictionary.Add("mpath", typeof(SvgMpath));

            // basic shapes
            ElementTypesDictionary.Add("circle", typeof(SvgCircle));
            ElementTypesDictionary.Add("ellipse", typeof(SvgEllipse));
            ElementTypesDictionary.Add("rect", typeof(SvgRect));
            ElementTypesDictionary.Add("line", typeof(SvgLine));
            ElementTypesDictionary.Add("path", typeof(SvgPath));
            ElementTypesDictionary.Add("polygon", typeof(SvgPolygon));
            ElementTypesDictionary.Add("polyline", typeof(SvgPolyline));

            // container

            ElementTypesDictionary.Add("svg", typeof(SvgSvg));
            ElementTypesDictionary.Add("a", typeof(SvgA));
            ElementTypesDictionary.Add("g", typeof(SvgG));
            ElementTypesDictionary.Add("defs", typeof(SvgDefs));
            ElementTypesDictionary.Add("marker", typeof(SvgMarker));
            ElementTypesDictionary.Add("mask", typeof(SvgMask));
            ElementTypesDictionary.Add("pattern", typeof(SvgPattern));
            ElementTypesDictionary.Add("switch", typeof(SvgSwitch));
            ElementTypesDictionary.Add("symbol", typeof(SvgSymbol));

            //  Descriptive
            ElementTypesDictionary.Add("desc", typeof(SvgDesc));
            ElementTypesDictionary.Add("metadata", typeof(SvgMetadata));
            ElementTypesDictionary.Add("title", typeof(SvgTitle));

            //filter primitave
            ElementTypesDictionary.Add("feBlend", typeof(SvgfeBlend));
            ElementTypesDictionary.Add("feColorMatrix", typeof(SvgfeColorMatrix));
            ElementTypesDictionary.Add("feComponentTransfer", typeof(SvgfeComponentTransfer));
            ElementTypesDictionary.Add("feComposite", typeof(SvgfeComposite));
            ElementTypesDictionary.Add("feConvolveMatrix", typeof(SvgfeConvolveMatrix));
            ElementTypesDictionary.Add("feDiffuseLighting", typeof(SvgfeDiffuseLighting));
            ElementTypesDictionary.Add("feDisplacementMap", typeof(SvgfeDisplacementMap));
            ElementTypesDictionary.Add("feDistantLight", typeof(SvgfeDistantLight));
            ElementTypesDictionary.Add("feDropShadow", typeof(SvgfeDropShadow));
            ElementTypesDictionary.Add("feFlood", typeof(SvgfeFlood));
            ElementTypesDictionary.Add("feFuncA", typeof(SvgfeFuncA));
            ElementTypesDictionary.Add("feFuncB", typeof(SvgfeFuncB));
            ElementTypesDictionary.Add("feFuncG", typeof(SvgfeFuncG));
            ElementTypesDictionary.Add("feFuncR", typeof(SvgfeFuncR));
            ElementTypesDictionary.Add("feGaussianBlur", typeof(SvgfeGaussianBlur));
            ElementTypesDictionary.Add("feImage", typeof(SvgfeImage));
            ElementTypesDictionary.Add("feMerge", typeof(SvgfeMerge));
            ElementTypesDictionary.Add("feMergeNode", typeof(SvgfeMergeNode));
            ElementTypesDictionary.Add("feMorphology", typeof(SvgfeMorphology));
            ElementTypesDictionary.Add("feOffset", typeof(SvgfeOffset));
            ElementTypesDictionary.Add("fePointLight", typeof(SvgfePointLight));
            ElementTypesDictionary.Add("feSpecularLighting", typeof(SvgfeSpecularLighting));
            ElementTypesDictionary.Add("feSpotLight", typeof(SvgfeSpotLight));
            ElementTypesDictionary.Add("feTile", typeof(SvgfeTile));
            ElementTypesDictionary.Add("feTurbulence", typeof(SvgfeTurbulence));

            // gradient

            ElementTypesDictionary.Add("linearGradient", typeof(SvgLinearGradient));
            ElementTypesDictionary.Add("radialGradient", typeof(SvgRadialGradient));
            ElementTypesDictionary.Add("stop", typeof(SvgStop));

            // graphics

            ElementTypesDictionary.Add("image", typeof(SvgImage));
            ElementTypesDictionary.Add("use", typeof(SvgUse));

            // other
            ElementTypesDictionary.Add("filter", typeof(SvgFilter));
            ElementTypesDictionary.Add("foreignObject", typeof(SvgForeignObject));
            ElementTypesDictionary.Add("hatch", typeof(SvgHatch));
            ElementTypesDictionary.Add("hatchpath", typeof(SvgHatchpath));
            ElementTypesDictionary.Add("script", typeof(SvgScript));
            ElementTypesDictionary.Add("style", typeof(SvgStyle));
            ElementTypesDictionary.Add("view", typeof(SvgView));

            // text content
           
         
            ElementTypesDictionary.Add("text", typeof(SvgComposition.ElementControls.TextContentElements.SvgText));
            ElementTypesDictionary.Add("textPath", typeof(SvgTextPath));
            ElementTypesDictionary.Add("tspan", typeof(SvgTspan));

         
        }




        public  ISvgElementControl CreateSvgElement(string st)
        {

                Type tt = ElementTypesDictionary[st];
                string ss = ElementNamesDictionary[st];

                ISvgElementControl oo = (ISvgElementControl)Activator.CreateInstance(tt, true);
                if (oo == null)
                {
                    throw (new Exception($"CreateInstance returned null for {tt} from st"));
                }
                return (oo);
        }
            
        

        public  string ConvertToUiName(string uin)
        {
            string res = string.Empty;
            string[] kk = uin.Split('-');
            foreach (string st in kk)
            {
                res += st;
            }

            return (res + "Ele");
        }

        public static int FindNextSvgElementId()
        {
            var items = from tut in SvgCompositionControl.Context.SvgElements
                select tut;

            foreach (var item in items)
            {
                if (item.Id >= _nextSvgElementId)
                {
                    _nextSvgElementId = item.Id + 1;
                }
            }
            return (_nextSvgElementId);
        }

        #region ElementTypeLists

        public static List<string> ContainerElements = new List<string>
        {
            "a",
            "defs",
            "g",
            "marker",
            "mask",
            "pattern",
            "svg",
            "switch",
            "symbol"
        };

        public static List<string> AnimationElements = new List<string>
        {
            "animate", "animateColor", "animateMotion", "animateTransform", "discard", "mpath", "set"
        };

        public static List<string> BasicShapes = new List<string>
        {
            "circle", "ellipse", "line", "polygon", "polyline", "rect"
       };

        public static List<string> ShapeElements = new List<string>
        {
            "circle", "ellipse", "line",  "path", "polygon", "polyline", "rect"
        };

        public static List<string> StructuralElements = new List<string>
        {
            "defs", "g", "svg", "symbol", "use"
        };

        public static List<string> DescriptiveElements = new List<string>
        {
            "desc", "metadata", "title"
        };

        public static List<string> FilterPrimitiveElements = new List<string>
        {
            "feBlend", "feColorMatrix", "feComponentTransfer", "feComposite",
            "feConvolveMatrix", "feDiffuseLighting", "feDisplacementMap", "feDropShadow",
            "feFlood","feFuncA", "feFuncB", "feFuncG", "feFuncR","feGaussianBlur", "feImage",
            "feMerge", "feMergeNode", "feMorphology", "feOffset", "feSpecularLighting", "feTile",
            "feTurbulence"
        };

        public static List<string> PaintServerElements = new List<string>
        {
            "hatch", "linearGradient",  "pattern", "radialGradient", "solidcolor"
        };

        public static List<string> TextContentElements = new List<string>
        {
            "textPath", "text", "tspan"
        };

        public static List<string> TextContentChildElements = new List<string>
        {
           "textPath",  "tspan"
        };

        public static List<string> GradientElements = new List<string>
        {
            "linearGradient",  "radialGradient", "stop"
        };

        public static List<string> GraphicsElements = new List<string>
        {
            "circle", "ellipse", "line",  "path", "polygon", "polyline", "rect", "text", "use"
        };

        public static List<string> LightSourceElements = new List<string>
        {
            "feDistantLight",  "fePointLight",  "feSpotLight"
        };

        public static List<string> UncategorizedElements = new List<string>
        {
            "clipPath", "color-profile", "cursor", "filter", "foreignObject",
            "hatchpath", "script", "style", "view"
        };

        public static List<string> DepreciatedElements = new List<string>
        {

            "altGlyph",  "altGlyphDef",  "altGlyphItem",  "animateColor",
            "cursor",
            "font", "font-face", "font-face-format", "font-face-name", "font-face-src", "font-face-uri",
            "glyph", "glyphRef",
            "hkern",
            "missing-glyph",
            "tref",
            "vkern"
        };



        #endregion

    }
}