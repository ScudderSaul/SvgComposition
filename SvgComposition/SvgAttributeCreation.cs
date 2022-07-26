using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SvgComposition.AttributeControls;
using SvgComposition.AttributeControls.AnimationEventAttributes;
using SvgComposition.AttributeControls.ComboAttributes;
using SvgComposition.AttributeControls.LenPerAttributes;
using SvgComposition.AttributeControls.ValueAttributes;
using SvgComposition.AttributeControls.SelectionAttributes;
using SvgComposition.AttributeControls.ColorAttributes;
using SvgComposition.AttributeControls.DocumentElementEventAttributes;
using SvgComposition.AttributeControls.DocumentEventAttributes;
using SvgComposition.AttributeControls.GlobalEventAttributes;
using SvgComposition.AttributeControls.GraphicalEventAttributes;
using SvgComposition.AttributeControls.ValueOptAttributes;
using SvgComposition.Controls;



namespace SvgComposition
{
    public static class SvgAttributeCreator
    {
        public static Dictionary<string, Type> AttributeTypesDictionary = new Dictionary<string, Type>();
        private static int _nextSvgAttributeId = 1;
        private static int _nextSvgColorId = 1;

       static SvgAttributeCreator()
        {

        }

        public static void CreateAttributeDictionary()
        {
            AttributeTypesDictionary.Clear();

            AttributeTypesDictionary.Add("fill", typeof(SvgFill));
            AttributeTypesDictionary.Add("flood-color", typeof(SvgFloodColor));
            AttributeTypesDictionary.Add("lighting-color", typeof(SvgLightingColor));
            AttributeTypesDictionary.Add("stop-color", typeof(SvgStopColor));
            AttributeTypesDictionary.Add("stroke", typeof(SvgStroke));
            AttributeTypesDictionary.Add("text-decoration", typeof(SvgTextDecoration));

            // combo
            AttributeTypesDictionary.Add("clip-path", typeof(SvgClipPath));
            AttributeTypesDictionary.Add("dur", typeof(SvgDur));
            AttributeTypesDictionary.Add("filter", typeof(SvgFilter));
            AttributeTypesDictionary.Add("flood-opacity", typeof(SvgFloodOpacity));
            AttributeTypesDictionary.Add("font-family", typeof(SvgFontFamily));
            AttributeTypesDictionary.Add("font-variant", typeof(SvgFontVariant));
            AttributeTypesDictionary.Add("font-size-adjust", typeof(SvgFontSizeAdjust));
            AttributeTypesDictionary.Add("font-size", typeof(SvgFontSize));
            AttributeTypesDictionary.Add("font-weight", typeof(SvgFontWeight));
            AttributeTypesDictionary.Add("in", typeof(SvgIn));
            AttributeTypesDictionary.Add("in2", typeof(SvgIn2));
            AttributeTypesDictionary.Add("kerning", typeof(SvgKerning));
            AttributeTypesDictionary.Add("letter-spacing", typeof(SvgLetterSpacing));
            AttributeTypesDictionary.Add("marker-end", typeof(SvgMarkerEnd));
            AttributeTypesDictionary.Add("marker-mid", typeof(SvgMarkerMid));
            AttributeTypesDictionary.Add("marker-start", typeof(SvgMarkerStart));
            AttributeTypesDictionary.Add("orient", typeof(SvgOrient));
            AttributeTypesDictionary.Add("patternTransform", typeof(SvgPatternTransform));
            AttributeTypesDictionary.Add("preserveAspectRatio", typeof(SvgPreserveAspectRatio));
            AttributeTypesDictionary.Add("refX", typeof(SvgRefX));
            AttributeTypesDictionary.Add("refY", typeof(SvgRefY));
            AttributeTypesDictionary.Add("repeatCount", typeof(SvgRepeatCount));
            AttributeTypesDictionary.Add("rotate", typeof(SvgRotatea));
            AttributeTypesDictionary.Add("stop-opacity", typeof(SvgStopOpacity));
            AttributeTypesDictionary.Add("stroke-dasharray", typeof(SvgStrokeDasharray));
            AttributeTypesDictionary.Add("transform", typeof(SvgTransform));



            // dim per
            AttributeTypesDictionary.Add("cx", typeof(SvgCx));
            AttributeTypesDictionary.Add("cy", typeof(SvgCy));
            AttributeTypesDictionary.Add("fr", typeof(SvgFr));
            AttributeTypesDictionary.Add("fx", typeof(SvgFx));
            AttributeTypesDictionary.Add("fy", typeof(SvgFy));
            AttributeTypesDictionary.Add("height", typeof(SvgHeight));
            AttributeTypesDictionary.Add("markerHeight", typeof(SvgMarkerHeight));
            AttributeTypesDictionary.Add("markerWidth", typeof(SvgMarkerWidth));
            AttributeTypesDictionary.Add("r", typeof(SvgR));
            AttributeTypesDictionary.Add("rx", typeof(SvgRx));
            AttributeTypesDictionary.Add("ry", typeof(SvgRy));
            AttributeTypesDictionary.Add("stroke-dashoffset", typeof(SvgStrokeDashoffset));
            AttributeTypesDictionary.Add("stroke-width", typeof(SvgStrokeWidth));
            AttributeTypesDictionary.Add("textLength", typeof(SvgTextLength));
            AttributeTypesDictionary.Add("width", typeof(SvgWidth));
            AttributeTypesDictionary.Add("word-spacing", typeof(SvgWordSpacing));
            AttributeTypesDictionary.Add("x", typeof(SvgX));
            AttributeTypesDictionary.Add("x1", typeof(SvgX1));
            AttributeTypesDictionary.Add("x2", typeof(SvgX2));
            AttributeTypesDictionary.Add("y", typeof(SvgY));
            AttributeTypesDictionary.Add("y1", typeof(SvgY1));
            AttributeTypesDictionary.Add("y2", typeof(SvgY2));

            // selection
            AttributeTypesDictionary.Add("accumulate", typeof(SvgAccumulate));
            AttributeTypesDictionary.Add("additive", typeof(SvgAdditive));
            AttributeTypesDictionary.Add("alignment-baseline", typeof(SvgAlignmentBaseline));
            AttributeTypesDictionary.Add("attributeType", typeof(SvgAttributeTypeA));
            AttributeTypesDictionary.Add("calcMode", typeof(SvgCalcMode));
            AttributeTypesDictionary.Add("clip-rule", typeof(SvgClipRule));
            AttributeTypesDictionary.Add("color-interpolation-filters", typeof(SvgColorInterpolationFilters));
            AttributeTypesDictionary.Add("color-rendering", typeof(SvgColorRendering));
            AttributeTypesDictionary.Add("direction", typeof(SvgDirection));
            AttributeTypesDictionary.Add("display", typeof(SvgDisplay));
            AttributeTypesDictionary.Add("dominantBaseline", typeof(SvgDominantBaseline));
            AttributeTypesDictionary.Add("edgeMode", typeof(SvgEdgeMode));
            AttributeTypesDictionary.Add("externalResourcesRequired", typeof(SvgExternalResourcesRequired));
            AttributeTypesDictionary.Add("fill-rule", typeof(SvgFillrule));
            AttributeTypesDictionary.Add("filterUnits", typeof(SvgFilterUnits));
            AttributeTypesDictionary.Add("font-style", typeof(SvgFontStyle));
            AttributeTypesDictionary.Add("gradientUnits", typeof(SvgGradientUnits));
            AttributeTypesDictionary.Add("image-rendering", typeof(SvgImageRendering));
            AttributeTypesDictionary.Add("lengthAdjust", typeof(SvgLengthAdjust));
            AttributeTypesDictionary.Add("markerUnits", typeof(SvgMarkerUnits));
            AttributeTypesDictionary.Add("maskContentUnits", typeof(SvgMaskContentUnits));
            AttributeTypesDictionary.Add("maskUnits", typeof(SvgMaskUnits));
            AttributeTypesDictionary.Add("mode", typeof(SvgMode));
            AttributeTypesDictionary.Add("operator", typeof(SvgOperator));
            AttributeTypesDictionary.Add("overflow", typeof(SvgOverflow));
            AttributeTypesDictionary.Add("paint-order", typeof(SvgPaintOrder));
            AttributeTypesDictionary.Add("patternContentUnits", typeof(SvgPatternContentUnits));
            AttributeTypesDictionary.Add("patternUnits", typeof(SvgPatternUnits));
            AttributeTypesDictionary.Add("pointer-events", typeof(SvgPointerEvents));
            AttributeTypesDictionary.Add("preserveAlpha", typeof(SvgPreserveAlpha));
            AttributeTypesDictionary.Add("primitiveUnits", typeof(SvgPrimitiveUnits));
            AttributeTypesDictionary.Add("restart", typeof(SvgRestart));
            AttributeTypesDictionary.Add("shape-rendering", typeof(SvgShapeRendering));
            AttributeTypesDictionary.Add("spreadMethod", typeof(SvgSpreadMethod));
            AttributeTypesDictionary.Add("stitchTiles", typeof(SvgStitchTiles));
            AttributeTypesDictionary.Add("stroke-linecap", typeof(SvgStrokeLinecap));
            AttributeTypesDictionary.Add("stroke-linejoin", typeof(SvgStrokeLinejoin));
            AttributeTypesDictionary.Add("text-anchor", typeof(SvgTextAnchor));
            AttributeTypesDictionary.Add("text-rendering", typeof(SvgTextRendering));
            AttributeTypesDictionary.Add("type", typeof(SvgType));
            AttributeTypesDictionary.Add("vector-effect", typeof(SvgVectorEffect));
            AttributeTypesDictionary.Add("visibility", typeof(SvgVisibility));
            AttributeTypesDictionary.Add("writing-mode", typeof(SvgWritingMode));
            AttributeTypesDictionary.Add("xChannelSelector", typeof(SvgXChannelSelector));
            AttributeTypesDictionary.Add("yChannelSelector", typeof(SvgYChannelSelector));

            // value 

            AttributeTypesDictionary.Add("mask", typeof(SvgAMask));  // name is to avoid conflict with Element name
            AttributeTypesDictionary.Add("amplitude", typeof(SvgAmplitude));
            AttributeTypesDictionary.Add("style", typeof(SvgAStyle)); // name is to avoid conflict with Element name
            AttributeTypesDictionary.Add("attributeName", typeof(SvgAttributeName));
           
            AttributeTypesDictionary.Add("azimuth", typeof(SvgAzimuth));
            AttributeTypesDictionary.Add("begin", typeof(SvgBegin));
            AttributeTypesDictionary.Add("bias", typeof(SvgBias));
            AttributeTypesDictionary.Add("by", typeof(SvgBy));
            AttributeTypesDictionary.Add("class", typeof(SvgClass));
            AttributeTypesDictionary.Add("dx", typeof(SvgDx));
            AttributeTypesDictionary.Add("dy", typeof(SvgDy));

            AttributeTypesDictionary.Add("elevation", typeof(SvgElevation));
            AttributeTypesDictionary.Add("end", typeof(SvgEnd));
            AttributeTypesDictionary.Add("exponent", typeof(SvgExponent));
            AttributeTypesDictionary.Add("fill-opacity", typeof(SvgFillOpacity));
            AttributeTypesDictionary.Add("from", typeof(SvgFrom));
            AttributeTypesDictionary.Add("gradientTransform", typeof(SvgGradientTransform));
            AttributeTypesDictionary.Add("href", typeof(SvgHref));
            AttributeTypesDictionary.Add("id", typeof(SvgId));
            AttributeTypesDictionary.Add("k1", typeof(SvgK1));
            AttributeTypesDictionary.Add("k2", typeof(SvgK2));
            AttributeTypesDictionary.Add("k3", typeof(SvgK3));
            AttributeTypesDictionary.Add("k4", typeof(SvgK4));
            AttributeTypesDictionary.Add("kernelMatrix", typeof(SvgKernelMatrix));
            AttributeTypesDictionary.Add("keySplines", typeof(SvgKeySplines));
            AttributeTypesDictionary.Add("keyPoints", typeof(SvgKeyPoints));
            AttributeTypesDictionary.Add("keyTimes", typeof(SvgKeyTimes));
            AttributeTypesDictionary.Add("limitingConeAngle", typeof(SvgLimitingConeAngle));
            AttributeTypesDictionary.Add("max", typeof(SvgMax));
            AttributeTypesDictionary.Add("min", typeof(SvgMin));
            AttributeTypesDictionary.Add("numOctaves", typeof(SvgNumOctaves));
            AttributeTypesDictionary.Add("offset", typeof(SvgOffset));
            AttributeTypesDictionary.Add("opacity", typeof(SvgOpacity));
            AttributeTypesDictionary.Add("pathLength", typeof(SvgPathLength));
            AttributeTypesDictionary.Add("points", typeof(SvgPoints));
            AttributeTypesDictionary.Add("pointsAtX", typeof(SvgPointsAtX));
            AttributeTypesDictionary.Add("pointsAtY", typeof(SvgPointsAtY));
            AttributeTypesDictionary.Add("pointsAtZ", typeof(SvgPointsAtZ));
            AttributeTypesDictionary.Add("repeatDur", typeof(SvgRepeatDur));
            AttributeTypesDictionary.Add("result", typeof(SvgResult));
            AttributeTypesDictionary.Add("scale", typeof(SvgScale));
            AttributeTypesDictionary.Add("seed", typeof(SvgSeed));
            AttributeTypesDictionary.Add("specularConstant", typeof(SvgSpecularConstant));
            AttributeTypesDictionary.Add("specularExponent", typeof(SvgSpecularExponent));
            AttributeTypesDictionary.Add("stroke-miterlimit", typeof(SvgStrokeMiterlimit));
            AttributeTypesDictionary.Add("stroke-opacity", typeof(SvgStrokeOpacity));
            AttributeTypesDictionary.Add("tableValues", typeof(SvgTableValues));
            AttributeTypesDictionary.Add("tabindex", typeof(SvgTabindex));
            AttributeTypesDictionary.Add("targetX", typeof(SvgTargetX));
            AttributeTypesDictionary.Add("targetY", typeof(SvgTargetY));
            AttributeTypesDictionary.Add("surfaceScale", typeof(SvgSurfaceScale));
            AttributeTypesDictionary.Add("to", typeof(SvgTo));
            AttributeTypesDictionary.Add("values", typeof(SvgValues));
            AttributeTypesDictionary.Add("viewBox", typeof(SvgViewBox));
            AttributeTypesDictionary.Add("z", typeof(SvgZ));


            // Value opt
            AttributeTypesDictionary.Add("baseFrequency", typeof(SvgBaseFrequency));
            AttributeTypesDictionary.Add("d", typeof(SvgD));
            AttributeTypesDictionary.Add("diffuseConstant", typeof(SvgDiffuseConstant));
            AttributeTypesDictionary.Add("divisor", typeof(SvgDivisor));
            AttributeTypesDictionary.Add("kernelUnitLength", typeof(SvgKernelUnitLength));
            AttributeTypesDictionary.Add("path", typeof(SvgPatha));
            AttributeTypesDictionary.Add("order", typeof(SvgOrder));
            AttributeTypesDictionary.Add("radius", typeof(SvgRadius));
            AttributeTypesDictionary.Add("stdDeviation", typeof(SvgStdDeviation));

            // Animation Event Attributes
            AttributeTypesDictionary.Add("onbegin", typeof(Svgonbegin));
            AttributeTypesDictionary.Add("onend", typeof(Svgonend));
            AttributeTypesDictionary.Add("onrepeat", typeof(Svgonrepeat));


            // DocumentEventAttributes -----------------------------------
            AttributeTypesDictionary.Add("onabort", typeof(Svgonabort));
            //AttributeTypesDictionary.Add("onerror", typeof(Svgonerror));
            //AttributeTypesDictionary.Add("onresize", typeof(Svgonresize));
            //AttributeTypesDictionary.Add("onscroll", typeof(Svgonscroll));
            AttributeTypesDictionary.Add("onunload", typeof(Svgonunload));

            // GraphicalEventAttributes -----------------------------------
            AttributeTypesDictionary.Add("onactivate", typeof(Svgonactivate));
            AttributeTypesDictionary.Add("onfocusin", typeof(Svgonfocusin));
            AttributeTypesDictionary.Add("onfocusout", typeof(Svgonfocusout));

            // DocumentElementEventAttributes -----------------------------------
            AttributeTypesDictionary.Add("oncopy", typeof(Svgoncopy));
            AttributeTypesDictionary.Add("oncut", typeof(Svgoncut));
            AttributeTypesDictionary.Add("onpaste", typeof(Svgonpaste));

            // GlobalEventNames -----------------------------------
            AttributeTypesDictionary.Add("oncancel", typeof(Svgoncancel));
            AttributeTypesDictionary.Add("oncanplay", typeof(Svgoncanplay));
            AttributeTypesDictionary.Add("oncanplaythrough", typeof(Svgoncanplaythrough));
            AttributeTypesDictionary.Add("onchange", typeof(Svgonchange));
            AttributeTypesDictionary.Add("onclick", typeof(Svgonclick));
            AttributeTypesDictionary.Add("onclose", typeof(Svgonclose));
            AttributeTypesDictionary.Add("oncuechange", typeof(Svgoncuechange));
            AttributeTypesDictionary.Add("ondblclick", typeof(Svgondblclick));
            AttributeTypesDictionary.Add("ondrag", typeof(Svgondrag));
            AttributeTypesDictionary.Add("ondragend", typeof(Svgondragend));
            AttributeTypesDictionary.Add("ondragenter", typeof(Svgondragenter));
            AttributeTypesDictionary.Add("ondragexit", typeof(Svgondragexit));
            AttributeTypesDictionary.Add("ondragleave", typeof(Svgondragleave));
            AttributeTypesDictionary.Add("ondragover", typeof(Svgondragover));
            AttributeTypesDictionary.Add("ondragstart", typeof(Svgondragstart));
            AttributeTypesDictionary.Add("ondrop", typeof(Svgondrop));
            AttributeTypesDictionary.Add("ondurationchange", typeof(Svgondurationchange));
            AttributeTypesDictionary.Add("onemptied", typeof(Svgonemptied));
            AttributeTypesDictionary.Add("onended", typeof(Svgonended));

            AttributeTypesDictionary.Add("onfocus", typeof(Svgonfocus));
            AttributeTypesDictionary.Add("oninput", typeof(Svgoninput));
            AttributeTypesDictionary.Add("oninvalid", typeof(Svgoninvalid));
            AttributeTypesDictionary.Add("onkeydown", typeof(Svgonkeydown));
            AttributeTypesDictionary.Add("onkeypress", typeof(Svgonkeypress));
            AttributeTypesDictionary.Add("onkeyup", typeof(Svgonkeyup));
            AttributeTypesDictionary.Add("onload", typeof(Svgonload));
            AttributeTypesDictionary.Add("onloadeddata", typeof(Svgonloadeddata));
            AttributeTypesDictionary.Add("onloadedmetadata", typeof(Svgonloadedmetadata));
            AttributeTypesDictionary.Add("onloadstart", typeof(Svgonloadstart));
            AttributeTypesDictionary.Add("onmousedown", typeof(Svgonmousedown));
            AttributeTypesDictionary.Add("onmouseenter", typeof(Svgonmouseenter));
            AttributeTypesDictionary.Add("onmouseleave", typeof(Svgonmouseleave));
            AttributeTypesDictionary.Add("onmousemove", typeof(Svgonmousemove));
            AttributeTypesDictionary.Add("onmouseout", typeof(Svgonmouseout));
            AttributeTypesDictionary.Add("onmouseover", typeof(Svgonmouseover));
            AttributeTypesDictionary.Add("onmouseup", typeof(Svgonmouseup));
            AttributeTypesDictionary.Add("onmousewheel", typeof(Svgonmousewheel));
            AttributeTypesDictionary.Add("onpause", typeof(Svgonpause));
            AttributeTypesDictionary.Add("onplay", typeof(Svgonplay));
            AttributeTypesDictionary.Add("onplaying", typeof(Svgonplaying));
            AttributeTypesDictionary.Add("onprogress", typeof(Svgonprogress));
            AttributeTypesDictionary.Add("onratechange", typeof(Svgonratechange));
            AttributeTypesDictionary.Add("onreset", typeof(Svgonreset));


            AttributeTypesDictionary.Add("onseeked", typeof(Svgonseeked));
            AttributeTypesDictionary.Add("onseeking", typeof(Svgonseeking));
            AttributeTypesDictionary.Add("onselect", typeof(Svgonselect));
            AttributeTypesDictionary.Add("onshow", typeof(Svgonshow));
            AttributeTypesDictionary.Add("onstalled", typeof(Svgonstalled));
            AttributeTypesDictionary.Add("onsubmit", typeof(Svgonsubmit));
            AttributeTypesDictionary.Add("onsuspend", typeof(Svgonsuspend));
            AttributeTypesDictionary.Add("ontimeupdate", typeof(Svgontimeupdate));
            AttributeTypesDictionary.Add("ontoggle", typeof(Svgontoggle));
            AttributeTypesDictionary.Add("onvolumechange", typeof(Svgonvolumechange));
            AttributeTypesDictionary.Add("onwaiting", typeof(Svgonwaiting));


        }

        public static bool AttributeImplemented(string name)
       {
           return (AttributeTypesDictionary.Keys.Contains(name));
        }


        public static void SafeAdd(string st, System.Type tt)
        {
            if (AttributeTypesDictionary.Keys.Contains(st) == false)
            {
                AttributeTypesDictionary.Add("st", tt);
            }
            else
            {
                throw (new Exception($"duplicate {st}"));
            }

        }

        public static ISvgAttributeControl CreateSvgAttribute(string st)
        {
            try
            {
                Type tt = AttributeTypesDictionary[st];
                ISvgAttributeControl oo = (ISvgAttributeControl) Activator.CreateInstance(tt, null);
                return (oo);

            }
            catch (Exception e)
            {
                MessageBox.Show($" {st} {AttributeTypesDictionary[st]}\r\n {e.Message}");
                return (null);
            }
        }

        public static string ConvertToUiName(string uin)
        {
            string res = string.Empty;
            string[] kk = uin.Split('-');
            foreach (string st in kk)
            {
                res += st;
            }

            return (res + "Attr");
        }


        public static int FindNextSvgAttributeId()
        {
            var items = from tut in SvgCompositionControl.Context.SvgAttributes
                select tut;

            foreach (var item in items)
            {
                if (item.Id >= _nextSvgAttributeId)
                {
                    _nextSvgAttributeId = item.Id + 1;
                }
            }

            return (_nextSvgAttributeId);
        }

        public static int FindNextSvgColorId()
        {
            var items = from tut in SvgCompositionControl.Context.SvgColors
                select tut;

            foreach (var item in items)
            {
                if (item.Id >= _nextSvgColorId)
                {
                    _nextSvgColorId = item.Id + 1;
                }
            }

            return (_nextSvgColorId);
        }

        public static List<string> PresentationAttributesNames = new List<string>
        {
            "clip-path", "clip-rule", "color", "color-interpolation", "color-rendering",
            "cursor", "display", "fill", "fill-opacity", "fill-rule", "filter", "mask",
            "opacity", "pointer-events", "shape-rendering", "stroke", "stroke-dasharray",
            "stroke-dashoffset", "stroke-linecap", "stroke-linejoin", "stroke-miterlimit",
            "stroke-opacity", "stroke-width", "transform", "vector-effect", "visibility"
        };

        public static List<string> MpAttributesNames = new List<string>
        {
            "style",
            "class",
            "alignment-baseline",
            "baseline-shift",
            "clip",
            "clip-path",
            "clip-rule",
            "color",
            "color-interpolation",
            "color-interpolation-filters",
            "color-profile",
            "color-rendering",
            "cursor",
            "direction",
            "display",
            "dominant-baseline",
            "enable-background",
            "fill",
            "fill-opacity",
            "fill-rule",
            "filter",
            "flood-color",
            "flood-opacity",
            "font-family",
            "font-size",
            "font-size-adjust",
            "font-stretch",
            "font-style",
            "font-variant",
            "font-weight",
            "glyph-orientation-horizontal",
            "glyph-orientation-vertical",
            "image-rendering",
            "kerning",
            "letter-spacing",
            "lighting-color",
            "marker-end",
            "marker-mid",
            "marker-start",
            "mask",
            "opacity",
            "overflow",
            "pointer-events",
            "shape-rendering",
            "solid-color",
            "solid-opacity",
            "stop-color",
            "stop-opacity",
            "stroke",
            "stroke-dasharray",
            "stroke-dashoffset",
            "stroke-linecap",
            "stroke-linejoin",
            "stroke-miterlimit",
            "stroke-opacity",
            "stroke-width",
            "text-anchor",
            "text-decoration",
            "text-rendering",
            "transform",
            "unicode-bidi",
            "vector-effect",
            "visibility",
            "word-spacing",
            "writing-mode",
        };

        public static List<string> DeprAttributes = new List<string>
        {
            "clip", "color-profile", "enable-background",
            "glyph-orientation-horizontal", "glyph-orientation-vertical",
            "kerning",
        };

        public static List<string> GlobalEventAttributes = new List<string>
        {
            "oncancel", "oncanplay", "oncanplaythrough", "onchange", "onclick", "onclose",
            "oncuechange", "ondblclick", "ondrag", "ondragend", "ondragenter",
            "ondragexit", "ondragleave", "ondragover", "ondragstart",
            "ondrop", "ondurationchange", "onemptied", "onended", "onerror",
            "onfocus", "oninput", "oninvalid", "onkeydown", "onkeypress", "onkeyup",
            "onload", "onloadeddata", "onloadedmetadata", "onloadstart", "onmousedown",
            "onmouseenter", "onmouseleave", "onmousemove", "onmouseout", "onmouseover",
            "onmouseup", "onmousewheel", "onpause", "onplay", "onplaying", "onprogress",
            "onratechange", "onreset", "onresize", "onscroll", "onseeked", "onseeking",
            "onselect", "onshow", "onstalled", "onsubmit", "onsuspend", "ontimeupdate",
            "ontoggle", "onvolumechange", "onwaiting",
        };

        public static List<string> AnimationEventAttributes = new List<string>
        {
            "onbegin", "onend", "onrepeat",
        };

        public static List<string> DocumentEventAttributes = new List<string>
        {
            "onabort", "onerror", "onresize", "onscroll", "onunload"
        };

        public static List<string> GraphicalEventAttributes = new List<string>
        {
            "onactivate", "onfocusin", "onfocusout"
        };

        public static List<string> DocumentElementEventAttributes = new List<string>
        {
            "oncopy", "oncut", "onpaste"
        };



        public static List<string> AnimationTimingAttributes = new List<string>
        {
            "begin", "dur", "end", "min", "max", "restart", "repeatCount", "repeatDur", "fill"
        };

        public static List<string> AnimationValueAttributes = new List<string>
        {
            "calcMode", "values", "keyTimes", "keySplines", "from", "to", "by", "autoReverse",
            "accelerate", "decelerate", "result"
        };

       

        public static List<string> AnimationAdditionAttributes = new List<string>
        {
            "additive", "accumulate"
        };

        public static List<string> AnimationAttributeTargetAttributes = new List<string>
        {
            "attributeType", "attributeName"
        };

       

        public static List<string> FilterPrimitiveAttributes = new List<string>
        {
            "height", "result", "width", "x", "y", "result"
        };

        public static List<string> TransferFunctionAttributes = new List<string>
        {
            "type", "tableValues", "slope", "intercept", "amplitude", "exponent", "offset"
        };

    }
}
