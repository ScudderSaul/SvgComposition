using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualStudio.Shell;

namespace SvgComposition
{
    public class SvgAttributeInfo
    {
        public SvgAttributeInfo(string anam)
        {
            Name = anam;
        }
        public SvgAttributeInfo()
        {

        }

        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultUnit { get; set; }
        public List<string> DataType { get; set; }
        public bool Animatable { get; set; }

        public static List<string> SpecialDataDescripters = new List<string>
        {
            "<length-percentage>", "<length>", "<url>", "<transform-list>", "<number+>", "<value>",  "<bool>", "<inherit>",
            "<auto>", "<none>", "<number>", "<filter-function+>", "<length-percentage+>", "<align>", "<angle>", "<coordinate>",
            "<string>", "<filter-primitive-reference>", "<number-optional-number>", "<paint>", "<color>", "<begin-value-list>",
        };

        public static List<string> PresentationAttributesNames = new List<string>
        {
            "clip-path", "clip-rule", "color", "color-interpolation", "color-rendering",
            "cursor", "display", "fill", "fill-opacity", "fill-rule", "filter", "mask",
            "opacity", "pointer-events", "shape-rendering", "stroke", "stroke-dasharray",
            "stroke-dashoffset", "stroke-linecap", "stroke-linejoin", "stroke-miterlimit",
            "stroke-opacity", "stroke-width", "transform", "vector-effect", "visibility",
        };

        public static List<string> FilterPrimitiveElementNames = new List<string>
        {
            "feBlend", "feColorMatrix", "feComponentTransfer", "feComposite", "feConvolveMatrix", "feDiffuseLighting",
            "feDisplacementMap", "feDropShadow", "feFlood","feGaussianBlur",
            "feImage", "feMerge", "feMergeNode", "feMorphology", "feOffset", "feSpecularLighting", "feTile", "feTurbulence"
        };

        public static List<string> SvgDataTypes = new List<string>
        {
            "angle",
            "angle-percentage",
            "basic-shape",
            "blend-mode",
            "color",
            "custom-ident",
            "dimension",
            "filter-function",
            "filter-function+",
            "flex",
            "frequency",
            "frequency-percentage",
            "gradient",
            "image",
            "integer",
            "length",
            "length-percentage",
            "length-percentage+",
            "number",
            "number+",
            "percentage",
            "position",
            "ratio",
            "resolution",
            "shape-box",
            "string",
            "time",
            "time-percentage",
            "timing-function",
            "transform-function",
            "transform-function+",
            "url",
        };

        public static List<string> AngleUnits = new List<string>
        {
            "deg", "grad", "rad",
        };

        public static List<string> FrequencyUnits = new List<string>
        {
            "Hz",
            "kHz",
        };

        public static List<string> LengthUnits = new List<string>
        {
            "em", "ex", "px", "in", "cm", "mm", "pt",
        };

        public static List<string> SpreadValues = new List<string>
        {
            "pad", "reflect", "repeat",
        };

        public static List<string> TransformListValues = new List<string>
        {
            "matrix", "translate", "scale", "rotate", "skewX", "skewY",
        };

        public static List<string> FillRuleValues = new List<string>
        {
            "nonzero",  "evenodd",
        };

        public static List<string> ColorInterpolationValues = new List<string>
        {
            "sRGB",  "linearRGB",
        };

        public static List<string> ColorRenderingValues = new List<string>
        {
            "optimizeSpeed", "optimizeQuality",
        };

        public static List<string> StrokeLinecapValues = new List<string>
        {
            "butt", "round", "square",
        };

        public static List<string> StrokeLinejoinValues = new List<string>
        {
            "arcs", "bevel", "miter", "miter-clip", "round",
        };

        public static List<string> AnimationTimingAttributeNames = new List<string>
        {
            "begin", "dur", "end", "min", "max", "restart", "repeatCount", "repeatDur", "fill",
        };

        public static List<string> AnimationValueAttributeNames = new List<string>
        {
            "calcMode", "values", "keyTimes", "keySplines", "from", "to", "by", "autoReverse", "accelerate", "decelerate",
        };

        public static List<string> AnimationAdditionAttributeNames = new List<string>
        {
            "additive", "accumulate",
        };

        public static List<string> AnimationEventAttributeNames = new List<string>
        {
            "onbegin", "onend", "onrepeat",
        };

        public static List<string> AnimationTargetAttributeNames = new List<string>
        {
            "attributeType", "attributeName",
        };


        public static List<string> VectorEffectValues = new List<string>
        {
            "default",  "non-scaling-stroke",
        };



        public static List<string> VisabilityValues = new List<string>
        {
            "visible", "hidden", "collapse",
        };

        public static List<string> DisplayValues = new List<string>
        {
/* <display-outside> values */
            "block",
            "inline",
            "run-in",

/* <display-inside> values */
            "flow",
            "flow-root",
            "table",
            "flex",
            "grid",
            "ruby",

/* <display-outside> plus <display-inside> values */
            "block flow",
            "inline table",
            "flex run-in",

            /* <display-listitem> values */
            "list-item",
            "list-item block",
            "list-item inline",
            "list-item flow",
            "list-item flow-root",
            "list-item block flow",
            "list-item block flow-root",
            "flow list-item block",

            /* <display-internal> values */
            "table-row-group",
            "table-header-group",
            "table-footer-group",
            "table-row",
            "table-cell",
            "table-column-group",
            "table-column",
            "table-caption",
            "ruby-base",
            "ruby-text",
            "ruby-base-container",
            "ruby-text-container",

/* <display-box> values */
            "contents",
            "none",

/* <display-legacy> values */
            "inline-block",
            "inline-table",
            "inline-flex",
            "inline-grid",

/* Global values */
            "inherit",
            "initial",
            "unset",
        };

    }

    public class ElementInfo
    {


        public ElementInfo(string nam)
        {
            ElementName = nam;
        }

        #region properties
        public string ElementName { get; set; }

        public Dictionary<string, SvgAttributeInfo> Info = new Dictionary<string, SvgAttributeInfo>();

        public List<string> PermitedContent = new List<string>();

        #endregion

        #region static lists

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
            "linearGradient", "radialGradient", "stop"
        };

        public static List<string> LightSourceElements = new List<string>
        {
            "feDistantLight",  "fePointLight",  "feSpotLight"
        };


        public static List<string> Misclo1 = new List<string>
        {
            "a", "altGlyphDef", "clipPath", "color-profile", "cursor", "filter", "font",
            "font-face", "foreignObject", "image", "marker", "mask", "pattern", "script", "style", "switch", "text",
            "view"
        };
        #endregion



    }

      
    public class ElementInfos
    {
        #region Properties
        public Dictionary<string, ElementInfo> Efos = new Dictionary<string, ElementInfo>();

        public Dictionary<string, SvgAttributeInfo> PresentationAttributeInfo = new Dictionary<string, SvgAttributeInfo>();
        public Dictionary<string, SvgAttributeInfo> AnimationTimingAttributeInfo = new Dictionary<string, SvgAttributeInfo>();
        public Dictionary<string, SvgAttributeInfo> AnimationValueAttributeInfo = new Dictionary<string, SvgAttributeInfo>();
        public Dictionary<string, SvgAttributeInfo> AnimationEventAttributeInfo = new Dictionary<string, SvgAttributeInfo>();
        public Dictionary<string, SvgAttributeInfo> AnimationTargetAttributeInfo = new Dictionary<string, SvgAttributeInfo>();
        public Dictionary<string, SvgAttributeInfo> AnimationAdditionAttributeInfo = new Dictionary<string, SvgAttributeInfo>();
        public Dictionary<string, SvgAttributeInfo> FilterPrimitiveAttributeInfo = new Dictionary<string, SvgAttributeInfo>();
        #endregion

        #region Methods
        public void AddEfo(string nam)
        {
            if (Efos.Keys.Contains(nam) == false)
            {
                Efos.Add(nam, new ElementInfo(nam));
                Efos[nam].Info.Add("id", new SvgAttributeInfo("id"));
            }
        }

        public void AddPermited(string name, List<string> ls)
        {
            if (Efos.Keys.Contains(name) == false)
            {
                Efos.Add(name, new ElementInfo(name));
            }

            foreach (string sy in ls)
            {
                if (Efos.Keys.Contains(sy) == false)
                {
                    Efos.Add(sy, new ElementInfo(sy));
                }

                if (Efos[name].PermitedContent.Contains(sy) == false)
                {
                    Efos[name].PermitedContent.Add(sy);
                }
            }
        }
        public void AddPermited(string name, string sy)
        {
            if (Efos.Keys.Contains(name) == false)
            {
                Efos.Add(name, new ElementInfo(name));
            }

            if (Efos.Keys.Contains(sy) == false)
            {
                Efos.Add(sy, new ElementInfo(sy));
            }

            if (Efos[name].PermitedContent.Contains(sy) == false)
            {

                Efos[name].PermitedContent.Add(sy);
            }
        }

        public void AddAttribute(string enam, string anam)
        {
            if (Efos.Keys.Contains(enam) == false)
            {
                Efos.Add(enam, new ElementInfo(enam));
            }

            if (Efos[enam].Info.Keys.Contains(anam) == false)
            {
                Efos[enam].Info.Add(anam, new SvgAttributeInfo(anam));
            }
        }

        public void AddAttribute(string enam, string anam, string isdefault)
        {
            if (Efos.Keys.Contains(enam) == false)
            {
                Efos.Add(enam, new ElementInfo(enam));
            }

            if (Efos[enam].Info.Keys.Contains(anam) == false)
            {
                Efos[enam].Info.Add(anam, new SvgAttributeInfo(anam));
                Efos[enam].Info[anam].DefaultValue = isdefault;
            }
            else
            {
                Efos[enam].Info[anam].DefaultValue = isdefault;
            }

        }

        public void AddAttribute(string enam, string anam,
            string isdefault, string defaultunit)
        {
            if (Efos.Keys.Contains(enam) == false)
            {
                Efos.Add(enam, new ElementInfo(enam));
            }

            if (Efos[enam].Info.Keys.Contains(anam) == false)
            {
                Efos[enam].Info.Add(anam, new SvgAttributeInfo(anam));
                Efos[enam].Info[anam].DefaultValue = isdefault;
                Efos[enam].Info[anam].DefaultUnit = defaultunit;
            }
            else
            {
                Efos[enam].Info[anam].DefaultValue = isdefault;
                Efos[enam].Info[anam].DefaultUnit = defaultunit;
            }

        }

        public void AddAttribute(string enam,
            string anam,
            string isdefault,
            string defaultunit,
            bool cananamate)
        {
            if (Efos.Keys.Contains(enam) == false)
            {
                Efos.Add(enam, new ElementInfo(enam));
            }

            if (Efos[enam].Info.Keys.Contains(anam) == false)
            {
                Efos[enam].Info.Add(anam, new SvgAttributeInfo(anam));
                Efos[enam].Info[anam].DefaultValue = isdefault;
                Efos[enam].Info[anam].DefaultUnit = defaultunit;
                Efos[enam].Info[anam].Animatable = cananamate;
            }
            else
            {
                Efos[enam].Info[anam].DefaultValue = isdefault;
                Efos[enam].Info[anam].DefaultUnit = defaultunit;
                Efos[enam].Info[anam].Animatable = cananamate;
            }


        }

        public void AddAttribute(string enam, SvgAttributeInfo ainf)
        {
            if (Efos.Keys.Contains(enam) == false)
            {
                Efos.Add(enam, new ElementInfo(enam));
            }

            if (Efos[enam].Info.Keys.Contains(ainf.Name) == false)
            {
                Efos[enam].Info.Add(ainf.Name, ainf);
            }
            else
            {
                Efos[enam].Info[ainf.Name] = ainf;
            }
        }

        public void BuildSvgInfo()
        {
            BuildElementInfo();
            BuildPresentationAttributes();
            AddAttributeInfo();
        }

        List<string> BracketStringsExtract(List<string> inlist)
        {
            List<string> outlist = new List<string>();
            foreach (string sg in inlist)
            {
                if (sg.Contains(">") == true && sg.Contains("<") == true)
                {
                    outlist.Add(sg);
                }
            }

            return (outlist);
        }

        List<string> NoBracketStringsExtract(List<string> inlist)
        {
            List<string> outlist = new List<string>();
            foreach (string sg in inlist)
            {
                if (sg.Contains(">") == false && sg.Contains("<") == false)
                {
                    outlist.Add(sg);
                }
            }

            return (outlist);
        }

        public void BuildPresentationAttributes()
        {

            PresentationAttributeInfo.Add("alignment-baseline", new SvgAttributeInfo
            {
                Name = "alignment-baseline",
                DataType = new List<string> { "<auto>" , "baseline" , "before-edge" ,
                    "text-before-edge" , "middle" , "central" , "after-edge" ,
                    "text-after-edge" , "ideographic" , "alphabetic" , "hanging" ,
                    "mathematical" , "top" , "center" , "bottom" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "<none>",
            });

            PresentationAttributeInfo.Add("baseline-shift", new SvgAttributeInfo
            {
                Name = "baseline-shift",
                DataType = new List<string> { "<auto>", "baseline", "super",
                    "sub", "percentage", "<length>", "<inherit>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "<auto>",
            });


            PresentationAttributeInfo.Add("clip-path", new SvgAttributeInfo
            {
                Name = "clip-path",
                DataType = new List<string> { "<url>", "basic-shape", "geometry-box", "<none>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "<auto>",
            });

            PresentationAttributeInfo.Add("clip-rule", new SvgAttributeInfo
            {
                Name = "clip-rule",
                DataType = new List<string> { "FillRuleValues", "<inherit>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "nonzero",
            });

            PresentationAttributeInfo.Add("color-interpolation", new SvgAttributeInfo
            {
                Name = "color-interpolation",
                DataType = new List<string> { "sRGB", "linearRGB", "<inherit>", "<auto>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "sRGB",
            });

            PresentationAttributeInfo.Add("color-interpolation-filters", new SvgAttributeInfo
            {
                Name = "color-interpolation-filters",
                DataType = new List<string> { "sRGB", "linearRGB", "<inherit>", "<auto>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "linearRGB",
            });

            PresentationAttributeInfo.Add("color-rendering", new SvgAttributeInfo
            {
                Name = "color-rendering",
                DataType = new List<string> { "optimizeSpeed", "optimizeQuality", "<inherit>", "<auto>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "nonzero",
            });

            PresentationAttributeInfo.Add("color-profile", new SvgAttributeInfo
            {
                Name = "color-profile",
                DataType = new List<string> { "<auto>", "sRGB", "name", "<url>", "<inherit>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "<auto>",
            });

            PresentationAttributeInfo.Add("direction", new SvgAttributeInfo
            {
                Name = "direction",
                DataType = new List<string> { "ltr", "rtl" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "ltr",
            });

            PresentationAttributeInfo.Add("display", new SvgAttributeInfo
            {
                Name = "display",
                DataType = new List<string>
                { 
/* <display-outside> values */
                    "block",
                    "inline",
                    "run-in",
/* <display-inside> values */
                    "flow",
                    "flow-root",
                    "table",
                    "flex",
                    "grid",
                    "ruby",
/* <display-outside> plus <display-inside> values */
                    "block flow",
                    "inline table",
                    "flex run-in",
                    /* <display-listitem> values */
                    "list-item",
                    "list-item block",
                    "list-item inline",
                    "list-item flow",
                    "list-item flow-root",
                    "list-item block flow",
                    "list-item block flow-root",
                    "flow list-item block",
                    /* <display-internal> values */
                    "table-row-group",
                    "table-header-group",
                    "table-footer-group",
                    "table-row",
                    "table-cell",
                    "table-column-group",
                    "table-column",
                    "table-caption",
                    "ruby-base",
                    "ruby-text",
                    "ruby-base-container",
                    "ruby-text-container",
/* <display-box> values */
                    "contents",
                    "<none>",
/* <display-legacy> values */
                    "inline-block",
                    "inline-table",
                    "inline-flex",
                    "inline-grid",
/* Global values */                 
                    "initial",
                    "unset", "<inherit>", "<none>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "inline",
            });

            PresentationAttributeInfo.Add("fill", new SvgAttributeInfo
            {
                Name = "fill",
                DataType = new List<string> { "<color>", "<url>", "<none>", "context-fill", "context-stroke" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "#000000",
            });

            PresentationAttributeInfo.Add("stroke", new SvgAttributeInfo
            {
                Name = "stroke",
                DataType = new List<string> { "<color>", "<url>", "<none>", "context-fill", "context-stroke" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "#000000",
            });

            PresentationAttributeInfo.Add("fill-opacity", new SvgAttributeInfo
            {
                Name = "fill-opacity",
                DataType = new List<string> { "<number>", },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "1",
            });

            PresentationAttributeInfo.Add("fill-rule", new SvgAttributeInfo
            {
                Name = "fill-rule",
                DataType = new List<string> { "nonzero", "evenodd", "<inherit>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "nonzero"
            });

            PresentationAttributeInfo.Add("filter", new SvgAttributeInfo
            {
                Name = "filter",
                DataType = new List<string> { "<filter-function+>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "nonzero",
            });

            PresentationAttributeInfo.Add("mask", new SvgAttributeInfo
            {
                Name = "mask",
                DataType = new List<string> { "<url>" },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "<none>",
            });

            PresentationAttributeInfo.Add("opacity", new SvgAttributeInfo
            {
                Name = "opacity",
                DataType = new List<string> { "<number>", },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "1",
            });

            PresentationAttributeInfo.Add("pointer-events", new SvgAttributeInfo
            {
                Name = "pointer-events",
                DataType = new List<string> { "bounding-box", "visiblePainted", "visibleFill", "visibleStroke", "visible", "painted", "fill", "stroke", "all", "<none>", },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "visiblePainted",
            });

            PresentationAttributeInfo.Add("shape-rendering", new SvgAttributeInfo
            {
                Name = "shape-rendering",
                DataType = new List<string> { "<auto>", "optimizeSpeed", "crispEdges", "geometricPrecision", "<inherit>", },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "visiblePainted",
            });

            PresentationAttributeInfo.Add("stroke-dasharray", new SvgAttributeInfo
            {
                Name = "stroke-dasharray",
                DataType = new List<string> { "<length-percentage+>", "<none>", },
                DefaultUnit = "px",
                Animatable = true,
                DefaultValue = "<none>",
            });

            PresentationAttributeInfo.Add("stroke-dashoffset", new SvgAttributeInfo
            {
                Name = "stroke-dashoffset",
                DataType = new List<string> { "<length-percentage>", },
                DefaultUnit = "px",
                Animatable = true,
                DefaultValue = "0",
            });

            PresentationAttributeInfo.Add("stroke-linecap", new SvgAttributeInfo
            {
                Name = "stroke-linecap",
                DataType = new List<string> { "butt", "round", "square", },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "butt",
            });

            PresentationAttributeInfo.Add("stroke-linejoin", new SvgAttributeInfo
            {
                Name = "stroke-linejoin",
                DataType = new List<string> { "arcs", "bevel", "miter", "miter-clip", "round", },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "miter",
            });

            PresentationAttributeInfo.Add("stroke-miterlimit", new SvgAttributeInfo
            {
                Name = "stroke-miterlimit",
                DataType = new List<string> { "<number>", },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "4",
            });

            PresentationAttributeInfo.Add("stroke-opacity", new SvgAttributeInfo
            {
                Name = "stroke-opacity",
                DataType = new List<string> { "<number>", },
                DefaultUnit = "",
                Animatable = true,
                DefaultValue = "1",
            });

            PresentationAttributeInfo.Add("stroke-width", new SvgAttributeInfo
            {
                Name = "stroke-width",
                DataType = new List<string> { "<length-percentage>", },
                DefaultUnit = "px",
                Animatable = true,
                DefaultValue = "1",
            });

            PresentationAttributeInfo.Add("transform", new SvgAttributeInfo
            {
                Name = "transform",
                DataType = new List<string> { "<transform-list>", },
                DefaultUnit = "px",
                Animatable = true,
                DefaultValue = "<none>",
            });



            PresentationAttributeInfo.Add("vector-effect", new SvgAttributeInfo
            {
                Name = "vector-effect",
                DataType = new List<string> { "<url>", "<inherit>", },
                DefaultUnit = "px",
                Animatable = true,
                DefaultValue = "<none>",
            });



            PresentationAttributeInfo.Add("visibility", new SvgAttributeInfo
            {
                Name = "visibility",
                DataType = new List<string> { "<url>", "<inherit>", "visible", "hidden", "collapse", },
                DefaultUnit = "px",
                Animatable = true,
                DefaultValue = "<none>",
            });

        }

        public void BuildAnimationAttributes()
        {

            #region target attributes

            AnimationTargetAttributeInfo.Add("attributeName", new SvgAttributeInfo
            {
                Name = "attributeName",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "attributeName" },   // choose
                Animatable = false,
            });


            AnimationTargetAttributeInfo.Add("attributeType", new SvgAttributeInfo
            {
                Name = "attributeType",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "CSS", "XML", "<auto>", },
                Animatable = false,
            });

            #endregion

            #region Value attributes

            AnimationValueAttributeInfo.Add("from", new SvgAttributeInfo
            {
                Name = "from",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<value>", },
                Animatable = false,
            });

            AnimationValueAttributeInfo.Add("to", new SvgAttributeInfo
            {
                Name = "to",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<value>", },
                Animatable = false,
            });

            AnimationValueAttributeInfo.Add("calcMode", new SvgAttributeInfo
            {
                Name = "calcMode",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "discrete ", "linear", "paced", "spline", },
                Animatable = false,
            });

            AnimationValueAttributeInfo.Add("values", new SvgAttributeInfo
            {
                Name = "values",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<list>", },
                Animatable = false,
            });

            AnimationValueAttributeInfo.Add("keyTimes", new SvgAttributeInfo
            {
                Name = "keyTimes",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<list>", },
                Animatable = false,
            });

            AnimationValueAttributeInfo.Add("keySplines", new SvgAttributeInfo
            {
                Name = "keySplines",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<list>", },
                Animatable = false,
            });

            AnimationValueAttributeInfo.Add("by", new SvgAttributeInfo
            {
                Name = "by",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<list>", },
                Animatable = false,
            });

            AnimationValueAttributeInfo.Add("autoReverse", new SvgAttributeInfo
            {
                Name = "autoReverse",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<bool>", },
                Animatable = false,
            });

            AnimationValueAttributeInfo.Add("accelerate", new SvgAttributeInfo
            {
                Name = "accelerate",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<value>", },
                Animatable = false,
            });

            AnimationValueAttributeInfo.Add("decelerate", new SvgAttributeInfo
            {
                Name = "decelerate",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<value>", },
                Animatable = false,
            });

            #endregion

            #region Timing attributes

            AnimationTimingAttributeInfo.Add("begin", new SvgAttributeInfo
            {
                Name = "begin",
                DataType = new List<string> { "offset-value", "syncbase-value", "event-value", "repeat-value", "accessKey-value", "wallclock-sync-value", "indefinite" },
                DefaultUnit = "",
                Animatable = false,
                DefaultValue = "<none>",
            });

            AnimationTimingAttributeInfo.Add("end", new SvgAttributeInfo
            {
                Name = "end",
                DataType = new List<string> { "offset-value", "syncbase-value", "event-value", "repeat-value", "accessKey-value", "wallclock-sync-value", "indefinite" },
                DefaultUnit = "",
                Animatable = false,
                DefaultValue = "<none>",
            });

            AnimationTimingAttributeInfo.Add("dur", new SvgAttributeInfo
            {
                Name = "dur",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<clock-value>", "indefinite", },    // clock-value format

                Animatable = false,
            });

            AnimationTimingAttributeInfo.Add("repeat​Count", new SvgAttributeInfo
            {
                Name = "repeat​Count",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number>", "indefinite", },
                Animatable = false,
            });

            AnimationTimingAttributeInfo.Add("min", new SvgAttributeInfo
            {
                Name = "min",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<clock-value>", },
                Animatable = false,
            });


            AnimationTimingAttributeInfo.Add("max", new SvgAttributeInfo
            {
                Name = "max",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<clock-value>", },
                Animatable = false,
            });

            AnimationTimingAttributeInfo.Add("restart", new SvgAttributeInfo
            {
                Name = "restart",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "always", "whenNotActive", "never", },
                Animatable = false,

            });

            AnimationTimingAttributeInfo.Add("repeatDur", new SvgAttributeInfo
            {
                Name = "repeatDur",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<clock-value>", "indefinite", },
                Animatable = false,
            });

            #endregion

            #region Addition Attributes

            AnimationAdditionAttributeInfo.Add("additive", new SvgAttributeInfo
            {
                Name = "additive",
                DefaultUnit = "",
                DefaultValue = "replace",
                DataType = new List<string> { "replace", "sum", },
                Animatable = false,
            });

            AnimationTimingAttributeInfo.Add("fill", new SvgAttributeInfo
            {
                Name = "fill",
                DefaultUnit = "",
                DefaultValue = "remove",
                DataType = new List<string> { "freeze", "remove", },
                Animatable = false,
            });

            AnimationAdditionAttributeInfo.Add("accumulate", new SvgAttributeInfo
            {
                Name = "accumulate",
                DefaultUnit = "",
                DefaultValue = "<none>",
                DataType = new List<string> { "<none>", "sum", },
                Animatable = false,
            });

            #endregion

            #region Event Attributes

            AnimationEventAttributeInfo.Add("onbegin", new SvgAttributeInfo
            {
                Name = "onbegin",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { },
                Animatable = false,
            });

            AnimationEventAttributeInfo.Add("onend", new SvgAttributeInfo
            {
                Name = "onend",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { },
                Animatable = false,
            });

            AnimationEventAttributeInfo.Add("onrepeat", new SvgAttributeInfo
            {
                Name = "onrepeat",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { },
                Animatable = false,
            });

            #endregion

            foreach (KeyValuePair<string, SvgAttributeInfo> ita in AnimationTimingAttributeInfo)
            {
                AddAttribute("animate", ita.Value);
                AddAttribute("animateMotion", ita.Value);
                AddAttribute("set", ita.Value);
                AddAttribute("animateTransform", ita.Value);
                AddAttribute("animateColor", ita.Value);
            }

            foreach (KeyValuePair<string, SvgAttributeInfo> ita in AnimationValueAttributeInfo)
            {
                AddAttribute("animate", ita.Value);
                AddAttribute("animateMotion", ita.Value);
                AddAttribute("animateTransform", ita.Value);
                AddAttribute("animateColor", ita.Value);
            }

            foreach (KeyValuePair<string, SvgAttributeInfo> ita in AnimationAdditionAttributeInfo)
            {
                AddAttribute("animate", ita.Value);
                AddAttribute("animateMotion", ita.Value);
                AddAttribute("animateTransform", ita.Value);
                AddAttribute("animateColor", ita.Value);
            }

            foreach (KeyValuePair<string, SvgAttributeInfo> ita in AnimationTargetAttributeInfo)
            {
                AddAttribute("animate", ita.Value);
                AddAttribute("set", ita.Value);
                AddAttribute("animateTransform", ita.Value);
                AddAttribute("animateColor", ita.Value);
            }

            foreach (KeyValuePair<string, SvgAttributeInfo> ita in AnimationEventAttributeInfo)
            {
                AddAttribute("animate", ita.Value);
                AddAttribute("animateMotion", ita.Value);
                AddAttribute("animateTransform", ita.Value);
                AddAttribute("animateColor", ita.Value);
            }

            AddAttribute("animateMotion", new SvgAttributeInfo
            {
                Name = "path",
                DefaultUnit = "",
                DefaultValue = "linear",
                DataType = new List<string> { "discrete", "linear", "paced", "spline", },
                Animatable = false,
            });

            AddAttribute("animateMotion", new SvgAttributeInfo
            {
                Name = "keyPoints",
                DefaultUnit = "",
                DefaultValue = "linear",
                DataType = new List<string> { "<list>", },
                Animatable = false,
            });

            AddAttribute("animateMotion", new SvgAttributeInfo
            {
                Name = "rotate",
                DefaultUnit = "degree",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<number>", "auto-reverse", "<auto>", },
                Animatable = false,
            });

            AddAttribute("animateMotion", new SvgAttributeInfo
            {
                Name = "origin",
                DefaultUnit = "",
                DefaultValue = "linear",
                DataType = new List<string> { "<list>", },
                Animatable = false,
            });

            AddAttribute("animateMotion", new SvgAttributeInfo
            {
                Name = "calkMode",
                DefaultUnit = "",
                DefaultValue = "linear",
                DataType = new List<string> { "discrete | linear | paced | spline", },
                Animatable = false,
            });

            AddAttribute("animateTransform", new SvgAttributeInfo
            {
                Name = "type",
                DefaultUnit = "",
                DefaultValue = "linear",
                DataType = new List<string> { "translate", "scale", "rotate", "skewX", "skewY" },
                Animatable = false,
            });

            AddAttribute("animate", "href", "", "", false);
            AddAttribute("animateMotion", "href", "", "", false);
            AddAttribute("animateTransform", "href", "", "", false);
            AddAttribute("set", "href", "", "", false);
        }

        public void BuildFilterAttributes()
        {
            #region filterprimitives

            FilterPrimitiveAttributeInfo.Add("x", new SvgAttributeInfo
        {
            Name = "x",
            DefaultUnit = "%",
            DefaultValue = "0",
            DataType = new List<string> { "<length-percentage>" },   // choose
            Animatable = false,
        });

        FilterPrimitiveAttributeInfo.Add("y", new SvgAttributeInfo
        {
            Name = "y",
            DefaultUnit = "%",
            DefaultValue = "0",
            DataType = new List<string> { "<length-percentage>" },   // choose
            Animatable = false,
        });

        FilterPrimitiveAttributeInfo.Add("height", new SvgAttributeInfo
        {
            Name = "height",
            DefaultUnit = "%",
            DefaultValue = "100",
            DataType = new List<string> { "<length-percentage>" },   // choose
            Animatable = false,
        });

        FilterPrimitiveAttributeInfo.Add("width", new SvgAttributeInfo
        {
            Name = "width",
            DefaultUnit = "%",
            DefaultValue = "100",
            DataType = new List<string> { "<length-percentage>" },   // choose
            Animatable = false,
        });

        FilterPrimitiveAttributeInfo.Add("result", new SvgAttributeInfo
        {
            Name = "result",
            DefaultUnit = "",
            DefaultValue = "",
            DataType = new List<string> { "<filter-primitive-reference>" },   // choose
            Animatable = false,
        });

        foreach( string fe in SvgAttributeInfo.FilterPrimitiveElementNames)
        {
            foreach( KeyValuePair<string, SvgAttributeInfo> it in FilterPrimitiveAttributeInfo)
            {
                AddAttribute(fe, it.Value);
            }
        }

        #endregion

        }

        /// <summary>
        /// Add the svg elements and lists those that can contain others as perrmission
        /// </summary>
        public void BuildElementInfo()
        {
            AddEfo("circle");
            AddPermited("circle", ElementInfo.DescriptiveElements);
            AddPermited("circle", ElementInfo.AnimationElements);
            AddEfo("ellipse");
            AddPermited("ellipse", ElementInfo.DescriptiveElements);
            AddPermited("ellipse", ElementInfo.AnimationElements);
            AddEfo("rect");
            AddPermited("rect", ElementInfo.DescriptiveElements);
            AddPermited("rect", ElementInfo.AnimationElements);
            AddEfo("line");
            AddPermited("line", ElementInfo.DescriptiveElements);
            AddPermited("line", ElementInfo.AnimationElements);
            AddEfo("path");
            AddPermited("path", ElementInfo.DescriptiveElements);
            AddPermited("path", ElementInfo.AnimationElements);
            AddEfo("pattern");
            AddPermited("pattern", ElementInfo.ShapeElements);
            AddPermited("pattern", ElementInfo.StructuralElements);
            AddPermited("pattern", ElementInfo.GradientElements);
            AddPermited("pattern", ElementInfo.DescriptiveElements);
            AddPermited("pattern", ElementInfo.AnimationElements);
            List<string> lo1 = new List<string>
            {
                "a", "altGlyphDef", "clipPath", "color-profile", "cursor", "filter", "font",
                "font-face", "foreignObject", "image", "marker", "mask", "pattern", "script", "style", "switch", "text",
                "view"
            };
            AddPermited("pattern", lo1);

            AddEfo("polygon");
            AddPermited("polygon", ElementInfo.DescriptiveElements);
            AddPermited("polygon", ElementInfo.AnimationElements);

            AddEfo("linearGradient");
            AddPermited("linearGradient", ElementInfo.DescriptiveElements);
            List<string> lo2 = new List<string>
            {
                "animate", "animateTransform", "set", "stop"
            };
            AddPermited("linearGradient", lo2);

            AddEfo("image");
            AddPermited("image", ElementInfo.DescriptiveElements);
            AddPermited("image", ElementInfo.AnimationElements);

            AddEfo("defs");
            AddPermited("defs", ElementInfo.ShapeElements);
            AddPermited("defs", ElementInfo.StructuralElements);
            AddPermited("defs", ElementInfo.GradientElements);
            AddPermited("defs", ElementInfo.DescriptiveElements);
            AddPermited("defs", ElementInfo.AnimationElements);
            AddPermited("defs", lo1);

            AddEfo("clipPath");
            AddPermited("clipPath", ElementInfo.ShapeElements);
            AddPermited("clipPath", ElementInfo.AnimationElements);
            AddPermited("clipPath", ElementInfo.DescriptiveElements);
            AddPermited("clipPath", "text");
            AddPermited("clipPath", "use");

            AddEfo("animate");
            AddPermited("animate", ElementInfo.DescriptiveElements);
            AddEfo("animateMotion");
            AddPermited("animateMotion", ElementInfo.DescriptiveElements);
            AddPermited("animateMotion", "mpath");

            AddEfo("animateTransform");
            AddPermited("animateTransform", ElementInfo.DescriptiveElements);
            AddEfo("desc");
            AddEfo("metadata");

            AddEfo("polyline");
            AddPermited("polyline", ElementInfo.DescriptiveElements);
            AddPermited("polyline", ElementInfo.AnimationElements);


            AddEfo("radialGradient");
            AddPermited("radialGradient", ElementInfo.DescriptiveElements);
            AddPermited("linearGradient", lo2);

            AddEfo("text");
            AddPermited("text", ElementInfo.DescriptiveElements);
            AddPermited("text", ElementInfo.AnimationElements);
            AddPermited("text", ElementInfo.TextContentElements);
            AddPermited("text", "a");

            AddEfo("textPath");
            AddPermited("textPath", ElementInfo.DescriptiveElements);
            List<string> lo3 = new List<string>
            {
                "a", "altGlyph", "animate", "animateColor", "set", "tref", "tspan"
            };
            AddPermited("textPath", lo3);

            AddEfo("title");


            AddEfo("tspan");
            AddPermited("tspan", ElementInfo.DescriptiveElements);
            AddPermited("tspan", lo3);

            AddEfo("g");
            AddPermited("g", ElementInfo.ShapeElements);
            AddPermited("g", ElementInfo.StructuralElements);
            AddPermited("g", ElementInfo.GradientElements);
            AddPermited("g", ElementInfo.DescriptiveElements);
            AddPermited("g", ElementInfo.AnimationElements);
            AddPermited("g", lo1);


            AddEfo("view");
            AddPermited("view", ElementInfo.DescriptiveElements);

            AddEfo("use");
            AddPermited("use", ElementInfo.DescriptiveElements);
            AddPermited("use", ElementInfo.AnimationElements);

            AddEfo("set");
            AddPermited("set", ElementInfo.DescriptiveElements);

            AddEfo("svg");
            AddPermited("svg", ElementInfo.ShapeElements);
            AddPermited("svg", ElementInfo.StructuralElements);
            AddPermited("svg", ElementInfo.GradientElements);
            AddPermited("svg", ElementInfo.DescriptiveElements);
            AddPermited("svg", ElementInfo.AnimationElements);
            AddPermited("svg", lo1);

            AddEfo("symbol");
            AddPermited("symbol", ElementInfo.ShapeElements);
            AddPermited("symbol", ElementInfo.StructuralElements);
            AddPermited("symbol", ElementInfo.GradientElements);
            AddPermited("symbol", ElementInfo.DescriptiveElements);
            AddPermited("symbol", ElementInfo.AnimationElements);
            AddPermited("symbol", lo1);

            AddEfo("stop");
            AddPermited("stop", "animate");
            AddPermited("stop", "animateColor");
            AddPermited("stop", "set");

            AddEfo("style");


            AddEfo("mpath");
            AddPermited("mpath", ElementInfo.DescriptiveElements);

            AddEfo("marker");
            AddPermited("marker", ElementInfo.ShapeElements);
            AddPermited("marker", ElementInfo.StructuralElements);
            AddPermited("marker", ElementInfo.GradientElements);
            AddPermited("marker", ElementInfo.DescriptiveElements);
            AddPermited("marker", ElementInfo.AnimationElements);
            AddPermited("marker", lo1);

            AddEfo("mask");
            AddPermited("mask", ElementInfo.ShapeElements);
            AddPermited("mask", ElementInfo.StructuralElements);
            AddPermited("mask", ElementInfo.GradientElements);
            AddPermited("mask", ElementInfo.DescriptiveElements);
            AddPermited("mask", ElementInfo.AnimationElements);
            AddPermited("mask", lo1);



            AddEfo("feBlend");
            AddPermited("feBlend", "set");
            AddPermited("feBlend", "animate");

            AddEfo("feColorMatrix");
            AddPermited("feColorMatrix", "set");
            AddPermited("feColorMatrix", "animate");

            AddEfo("feComponentTransfer");
            AddPermited("feComponentTransfer", "feFuncA");
            AddPermited("feComponentTransfer", "feFuncB");
            AddPermited("feComponentTransfer", "feFuncR");
            AddPermited("feComponentTransfer", "feFuncG");


            AddEfo("feComposite");
            AddPermited("feComposite", "set");
            AddPermited("feComposite", "animate");

            AddEfo("feConvolveMatrix");
            AddPermited("feConvolveMatrix", "set");
            AddPermited("feConvolveMatrix", "animate");

            AddEfo("feDiffuseLighting");
            AddPermited("feDiffuseLighting", ElementInfo.DescriptiveElements);
            AddPermited("feDiffuseLighting", ElementInfo.LightSourceElements);

            AddEfo("feDisplacementMap");
            AddPermited("feDisplacementMap", "set");
            AddPermited("feDisplacementMap", "animate");

            AddEfo("feDistantLight");
            AddPermited("feDistantLight", "set");
            AddPermited("feDistantLight", "animate");

            AddEfo("feDropShadow");
            AddPermited("feDropShadow", "set");
            AddPermited("feDropShadow", "animate");
            AddPermited("feDropShadow", "script");

            AddEfo("feFlood");
            AddPermited("feFlood", "set");
            AddPermited("feFlood", "animate");
            AddPermited("feFlood", "animateColor");

            AddEfo("feFuncA");
            AddPermited("feFuncA", "set");
            AddPermited("feFuncA", "animate");
            AddEfo("feFuncB");
            AddPermited("feFuncB", "set");
            AddPermited("feFuncB", "animate");
            AddEfo("feFuncG");
            AddPermited("feFuncG", "set");
            AddPermited("feFuncG", "animate");
            AddEfo("feFuncR");
            AddPermited("feFuncR", "set");
            AddPermited("feFuncR", "animate");

            AddEfo("feGaussianBlur");
            AddPermited("feGaussianBlur", "set");
            AddPermited("feGaussianBlur", "animate");

            AddEfo("feImage");
            AddPermited("feImage", "set");
            AddPermited("feImage", "animateTransform");
            AddPermited("feImage", "animate");

            AddEfo("feMerge");
            AddPermited("feMerge", "feMergeNode");

            AddEfo("feMergeNode");
            AddPermited("feMergeNode", "set");
            AddPermited("feMergeNode", "animate");

            AddEfo("feMorphology");
            AddPermited("feMorphology", "set");
            AddPermited("feMorphology", "animate");

            AddEfo("feOffset");
            AddPermited("feOffset", "set");
            AddPermited("feOffset", "animate");

            AddEfo("feSpotLight");
            AddPermited("feSpotLight", "set");
            AddPermited("feSpotLight", "animate");

            AddEfo("feSpecularLighting");
            AddPermited("feSpecularLighting", ElementInfo.LightSourceElements);
            AddPermited("feSpecularLighting", ElementInfo.DescriptiveElements);

            AddEfo("feSpotLight");
            AddPermited("feSpotLight", "set");
            AddPermited("feSpotLight", "animate");

            AddEfo("feTile");
            AddPermited("feTile", "set");
            AddPermited("feTile", "animate");

            AddEfo("feTurbulence");
            AddPermited("feTurbulence", "set");
            AddPermited("feTurbulence", "animate");

            AddEfo("filter");
            AddPermited("filter", "set");
            AddPermited("filter", "animate");
        }

        public void AddAttributeInfo()
        {
            BuildFilterAttributes();
            BuildAnimationAttributes();

            #region circle
            AddAttribute("circle", new SvgAttributeInfo
            {
                Name = "cx",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("circle", new SvgAttributeInfo
            {
                Name = "cy",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("circle", new SvgAttributeInfo
            {
                Name = "r",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length>" },
                Animatable = true,

            });

            AddAttribute("circle", new SvgAttributeInfo
            {
                Name = "pathLength",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<number>" },
                Animatable = true,

            });

            AddAttribute("circle", new SvgAttributeInfo
            {
                Name = "fill",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            AddAttribute("circle", new SvgAttributeInfo
            {
                Name = "stroke",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,
            });

            #endregion 

            #region ellipse
            AddAttribute("ellipse", new SvgAttributeInfo
            {
                Name = "cx",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("ellipse", new SvgAttributeInfo
            {
                Name = "cy",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("ellipse", new SvgAttributeInfo
            {
                Name = "rx",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("ellipse", new SvgAttributeInfo
            {
                Name = "ry",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("ellipse", new SvgAttributeInfo
            {
                Name = "pathLength",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<number>" },
                Animatable = true,

            });

            AddAttribute("ellipse", new SvgAttributeInfo
            {
                Name = "fill",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            AddAttribute("ellipse", new SvgAttributeInfo
            {
                Name = "stroke",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });
            #endregion

            #region radialGradient

            AddAttribute("radialGradient", new SvgAttributeInfo
            {
                Name = "cx",
                DefaultUnit = "%",
                DefaultValue = "50",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("radialGradient", new SvgAttributeInfo
            {
                Name = "cy",
                DefaultUnit = "%",
                DefaultValue = "50",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("radialGradient", new SvgAttributeInfo
            {
                Name = "fr",
                DefaultUnit = "%",
                DefaultValue = "50",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("radialGradient", new SvgAttributeInfo
            {
                Name = "fx",
                DefaultUnit = "%",
                DefaultValue = "50",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("radialGradient", new SvgAttributeInfo
            {
                Name = "fy",
                DefaultUnit = "%",
                DefaultValue = "50",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("radialGradient", new SvgAttributeInfo
            {
                Name = "gradientUnits",
                DefaultUnit = "",
                DefaultValue = "objectBoundingBox",
                DataType = new List<string> { "userSpaceOnUse", "objectBoundingBox" },
                Animatable = true,

            });

            AddAttribute("radialGradient", new SvgAttributeInfo
            {
                Name = "gradient​Transform",
                DefaultUnit = "",
                DefaultValue = "<none>",
                DataType = new List<string> { "<transform-list>" },
                Animatable = true,

            });

            AddAttribute("radialGradient", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "<none>",
                DefaultValue = "<none>",
                DataType = new List<string> { "<url>" },
                Animatable = true,

            });

            AddAttribute("radialGradient", new SvgAttributeInfo
            {
                Name = "r",
                DefaultUnit = "%",
                DefaultValue = "50",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("radialGradient", new SvgAttributeInfo
            {
                Name = "spreadMethod",
                DefaultUnit = "",
                DefaultValue = "pad",
                DataType = new List<string> { "pad", "reflect", "repeat", },  // SpreadValues
                Animatable = true,

            });

            #endregion

            #region rect

            AddAttribute("rect", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("rect", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("rect", new SvgAttributeInfo
            {
                Name = "height",
                DefaultUnit = "px",
                DefaultValue = "",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("rect", new SvgAttributeInfo
            {
                Name = "width",
                DefaultUnit = "px",
                DefaultValue = "",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("rect", new SvgAttributeInfo
            {
                Name = "rx",
                DefaultUnit = "px",
                DefaultValue = "",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,
            });

            AddAttribute("rect", new SvgAttributeInfo
            {
                Name = "ry",
                DefaultUnit = "px",
                DefaultValue = "",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,
            });

            AddAttribute("rect", new SvgAttributeInfo
            {
                Name = "pathLength",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<number>" },
                Animatable = true,

            });

            AddAttribute("rect", new SvgAttributeInfo
            {
                Name = "fill",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            AddAttribute("rect", new SvgAttributeInfo
            {
                Name = "stroke",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            #endregion

            #region polygon
            AddAttribute("polygon", new SvgAttributeInfo
            {
                Name = "points",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<number+>" },
                Animatable = true,

            });

            AddAttribute("polygon", new SvgAttributeInfo
            {
                Name = "pathLength",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<number>" },
                Animatable = true,

            });

            AddAttribute("polygon", new SvgAttributeInfo
            {
                Name = "fill",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            AddAttribute("polygon", new SvgAttributeInfo
            {
                Name = "stroke",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            #endregion

            #region line
            AddAttribute("line", new SvgAttributeInfo
            {
                Name = "x1",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", "<number>" },
                Animatable = true,
            });

            AddAttribute("line", new SvgAttributeInfo
            {
                Name = "x2",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", "<number>" },
                Animatable = true,
            });

            AddAttribute("line", new SvgAttributeInfo
            {
                Name = "y1",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", "<number>" },
                Animatable = true,
            });

            AddAttribute("line", new SvgAttributeInfo
            {
                Name = "y2",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", "<number>" },
                Animatable = true,
            });

            AddAttribute("line", new SvgAttributeInfo
            {
                Name = "pathLength",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<number>" },
                Animatable = true,
            });

            AddAttribute("line", new SvgAttributeInfo
            {
                Name = "stroke",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            #endregion

            #region a

            AddAttribute("a", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>" },
                Animatable = true,
            });

            #endregion

            #region linearGradient
            AddAttribute("linearGradient", new SvgAttributeInfo
            {
                Name = "gradient​Units",
                DefaultUnit = "",
                DefaultValue = "objectBoundingBox",
                DataType = new List<string> { "userSpaceOnUse", "objectBoundingBox" },
                Animatable = true,
            });

            AddAttribute("linearGradient", new SvgAttributeInfo
            {
                Name = "gradientTransform",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<transform-list>" },
                Animatable = true,
            });

            AddAttribute("linearGradient", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>" },
                Animatable = true,
            });

            AddAttribute("linearGradient", new SvgAttributeInfo
            {
                Name = "spread​Method",
                DefaultUnit = "",
                DefaultValue = "pad",
                DataType = new List<string> { "pad", "reflect", "repeat" },
                Animatable = true,
            });

            AddAttribute("linearGradient", new SvgAttributeInfo
            {
                Name = "x1",
                DefaultUnit = "%",
                DefaultValue = "0",
                DataType = new List<string> { "<length>", },
                Animatable = true,
            });

            AddAttribute("linearGradient", new SvgAttributeInfo
            {
                Name = "y1",
                DefaultUnit = "%",
                DefaultValue = "0",
                DataType = new List<string> { "<length>", },
                Animatable = true,
            });

            #endregion

            #region clip-path

            AddAttribute("clip-path", new SvgAttributeInfo
            {
                Name = "clipPathUnits",
                DefaultUnit = "",
                DefaultValue = "userSpaceOnUse",
                DataType = new List<string> { "userSpaceOnUse", "objectBoundingBox", },
                Animatable = true,
            });



            #endregion

            #region image

            AddAttribute("image", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,
            });

            AddAttribute("image", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,
            });

            AddAttribute("image", new SvgAttributeInfo
            {
                Name = "width",
                DefaultUnit = "px",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<auto>", "<length-percentage>", },
                Animatable = true,
            });


            AddAttribute("image", new SvgAttributeInfo
            {
                Name = "height",
                DefaultUnit = "px",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<auto>", "<length-percentage>", },
                Animatable = true,
            });

            AddAttribute("image", new SvgAttributeInfo
            {
                Name = "preserveAspectRatio",
                DefaultUnit = "",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<align>", },
                Animatable = true,
            });

            AddAttribute("image", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>", },
                Animatable = true,
            });

            #endregion

            #region marker

            AddAttribute("marker", new SvgAttributeInfo
            {
                Name = "markerHeight",
                DefaultUnit = "px",
                DefaultValue = "3",
                DataType = new List<string> { "<length>", },
                Animatable = true,
            });

            AddAttribute("marker", new SvgAttributeInfo
            {
                Name = "markerWidth",
                DefaultUnit = "px",
                DefaultValue = "3",
                DataType = new List<string> { "<length>", },
                Animatable = true,
            });

            AddAttribute("marker", new SvgAttributeInfo
            {
                Name = "markerUnits",
                DefaultUnit = "",
                DefaultValue = "strokeWidth",
                DataType = new List<string> { "userSpaceOnUse", "strokeWidth", },
                Animatable = true,
            });

            AddAttribute("marker", new SvgAttributeInfo
            {
                Name = "orient",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<auto>", "auto-start-reverse", "<angle>", },
                Animatable = true,
            });

            AddAttribute("marker", new SvgAttributeInfo
            {
                Name = "preserveAspectRatio",
                DefaultUnit = "px",
                DefaultValue = "xMidYMid meet",
                DataType = new List<string> { "<none>", "<align>", },
                Animatable = true,
            });

            AddAttribute("marker", new SvgAttributeInfo
            {
                Name = "refX",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "left", "center", "right", "<coordinate>", },
                Animatable = true,
            });

            AddAttribute("marker", new SvgAttributeInfo
            {
                Name = "refY",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "top", "center", "bottom", "<coordinate>", },
                Animatable = true,
            });

            AddAttribute("marker", new SvgAttributeInfo
            {
                Name = "viewBox",
                DefaultUnit = "px",
                DefaultValue = "",
                DataType = new List<string> { "<number+>", },
                Animatable = true,
            });


            #endregion

            #region mask

            AddAttribute("mask", new SvgAttributeInfo
            {
                Name = "height",
                DefaultUnit = "%",
                DefaultValue = "120",
                DataType = new List<string> { "<length>", },
                Animatable = true,
            });

            AddAttribute("mask", new SvgAttributeInfo
            {
                Name = "width",
                DefaultUnit = "%",
                DefaultValue = "120",
                DataType = new List<string> { "<length>", },
                Animatable = true,
            });

            AddAttribute("mask", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "%",
                DefaultValue = "-10",
                DataType = new List<string> { "<length>", },
                Animatable = true,
            });

            AddAttribute("mask", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "%",
                DefaultValue = "-10",
                DataType = new List<string> { "<length>", },
                Animatable = true,
            });

            AddAttribute("mask", new SvgAttributeInfo
            {
                Name = "maskUnits",
                DefaultUnit = "",
                DefaultValue = "objectBoundingBox",
                DataType = new List<string> { "userSpaceOnUse", "objectBoundingBox", },
                Animatable = true,
            });

            AddAttribute("mask", new SvgAttributeInfo
            {
                Name = "maskContentUnits",
                DefaultUnit = "%",
                DefaultValue = "userSpaceOnUse",
                DataType = new List<string> { "userSpaceOnUse", "objectBoundingBox", },
                Animatable = true,
            });

            #endregion

            #region mpath
            AddAttribute("mpath", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>", },
                Animatable = false,
            });

            #endregion

            #region path

            AddAttribute("path", new SvgAttributeInfo
            {
                Name = "d",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<string>", },
                Animatable = true,
            });


            AddAttribute("path", new SvgAttributeInfo
            {
                Name = "pathlength",
                DefaultUnit = "",
                DefaultValue = "<none>",
                DataType = new List<string> { "<number>", "<none>", },
                Animatable = true,
            });

            AddAttribute("path", new SvgAttributeInfo
            {
                Name = "fill",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            AddAttribute("path", new SvgAttributeInfo
            {
                Name = "stroke",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            #endregion

            #region pattern

            AddAttribute("pattern", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,
            });

            AddAttribute("pattern", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,
            });

            AddAttribute("pattern", new SvgAttributeInfo
            {
                Name = "height",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,
            });

            AddAttribute("pattern", new SvgAttributeInfo
            {
                Name = "width",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,
            });

            AddAttribute("pattern", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>", },
                Animatable = true,
            });

            AddAttribute("pattern", new SvgAttributeInfo
            {
                Name = "patternTransform",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<transform-list>", },
                Animatable = true,
            });

            AddAttribute("pattern", new SvgAttributeInfo
            {
                Name = "patternContentUnits",
                DefaultUnit = "",
                DefaultValue = "userSpaceOnUse",
                DataType = new List<string> { "userSpaceOnUse", "objectBoundingBox", },
                Animatable = true,
            });

            AddAttribute("pattern", new SvgAttributeInfo
            {
                Name = "patternUnits",
                DefaultUnit = "",
                DefaultValue = "objectBoundingBox",
                DataType = new List<string> { "userSpaceOnUse", "objectBoundingBox", },
                Animatable = true,
            });

            AddAttribute("pattern", new SvgAttributeInfo
            {
                Name = "viewBox",
                DefaultUnit = "",
                DefaultValue = "o",
                DataType = new List<string> { "<number+>", "<none>", },
                Animatable = true,
            });

            AddAttribute("pattern", new SvgAttributeInfo
            {
                Name = "preserveAspectRatio",
                DefaultUnit = "",
                DefaultValue = "xMidYMid meet",
                DataType = new List<string> { "<align>", "<none>", },
                Animatable = true,
            });


            #endregion

            #region polyline
            AddAttribute("polyline", new SvgAttributeInfo
            {
                Name = "points",
                DefaultUnit = "px",
                DefaultValue = "",
                DataType = new List<string> { "<number+>" },
                Animatable = true,

            });

            AddAttribute("polyline", new SvgAttributeInfo
            {
                Name = "pathLength",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<number>", "<none>", },
                Animatable = true,

            });

            AddAttribute("polyline", new SvgAttributeInfo
            {
                Name = "fill",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            AddAttribute("polyline", new SvgAttributeInfo
            {
                Name = "stroke",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            #endregion

            #region stop

            AddAttribute("stop", new SvgAttributeInfo
            {
                Name = "offset",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number>" },
                Animatable = true,

            });

            AddAttribute("stop", new SvgAttributeInfo
            {
                Name = "stop-color",
                DefaultUnit = "p",
                DefaultValue = "#000000",
                DataType = new List<string> { "currentColor", "<color>", "<icccolor> ", "<inherit>" },
                Animatable = true,

            });

            AddAttribute("stop", new SvgAttributeInfo
            {
                Name = "stop-opacity",
                DefaultUnit = "",
                DefaultValue = "1.0",
                DataType = new List<string> { "<number>", "<inherit>" },
                Animatable = true,

            });

            #endregion

            #region svg

            AddAttribute("svg", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("svg", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("svg", new SvgAttributeInfo
            {
                Name = "width",
                DefaultUnit = "px",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<length-percentage>", "<auto>", },
                Animatable = true,

            });

            AddAttribute("svg", new SvgAttributeInfo
            {
                Name = "height",
                DefaultUnit = "px",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<length-percentage>", "<auto>", },
                Animatable = true,

            });

            AddAttribute("svg", new SvgAttributeInfo
            {
                Name = "viewBox",
                DefaultUnit = "",
                DefaultValue = "o",
                DataType = new List<string> { "<number+>", "<none>", },
                Animatable = true,
            });

            AddAttribute("svg", new SvgAttributeInfo
            {
                Name = "preserveAspectRatio",
                DefaultUnit = "",
                DefaultValue = "xMidYMid meet",
                DataType = new List<string> { "<align>", "<none>", },
                Animatable = true,
            });

            #endregion

            #region symbol

            AddAttribute("symbol", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("symbol", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("symbol", new SvgAttributeInfo
            {
                Name = "width",
                DefaultUnit = "px",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<length-percentage>", "<auto>", },
                Animatable = true,

            });

            AddAttribute("symbol", new SvgAttributeInfo
            {
                Name = "height",
                DefaultUnit = "px",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<length-percentage>", "<auto>", },
                Animatable = true,

            });

            AddAttribute("symbol", new SvgAttributeInfo
            {
                Name = "viewBox",
                DefaultUnit = "",
                DefaultValue = "o",
                DataType = new List<string> { "<number+>", "<none>", },
                Animatable = true,
            });

            AddAttribute("symbol", new SvgAttributeInfo
            {
                Name = "preserveAspectRatio",
                DefaultUnit = "",
                DefaultValue = "xMidYMid meet",
                DataType = new List<string> { "<align>", "<none>", },
                Animatable = true,
            });

            AddAttribute("symbol", new SvgAttributeInfo
            {
                Name = "refx",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", "left", "center", "right" },
                Animatable = true,

            });

            AddAttribute("symbol", new SvgAttributeInfo
            {
                Name = "refy",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", "top", "center", "bottom" },
                Animatable = true,

            });

            #endregion

            #region text

            AddAttribute("text", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("text", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("text", new SvgAttributeInfo
            {
                Name = "dx",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<length-percentage>", "<none>", },
                Animatable = true,

            });

            AddAttribute("text", new SvgAttributeInfo
            {
                Name = "dy",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<length-percentage>", "<none>", },
                Animatable = true,

            });

            AddAttribute("text", new SvgAttributeInfo
            {
                Name = "rotate",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<number+>", "<none>", },
                Animatable = true,

            });

            AddAttribute("text", new SvgAttributeInfo
            {
                Name = "lengthAdjust",
                DefaultUnit = "px",
                DefaultValue = "spacing",
                DataType = new List<string> { "spacing", "spacingAndGlyphs", },
                Animatable = true,

            });

            AddAttribute("text", new SvgAttributeInfo
            {
                Name = "textLength",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<length-percentage>", "<none>", },
                Animatable = true,

            });

            AddAttribute("text", new SvgAttributeInfo
            {
                Name = "fill",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            AddAttribute("text", new SvgAttributeInfo
            {
                Name = "stroke",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            #endregion

            #region textpath

            AddAttribute("textpath", new SvgAttributeInfo
            {
                Name = "lengthAdjust",
                DefaultUnit = "px",
                DefaultValue = "spacing",
                DataType = new List<string> { "spacing", "spacingAndGlyphs", },
                Animatable = true,

            });

            AddAttribute("textpath", new SvgAttributeInfo
            {
                Name = "path",
                DefaultUnit = "px",
                DefaultValue = "",
                DataType = new List<string> { "<number+>", },
                Animatable = true,

            });

            AddAttribute("textpath", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>", },
                Animatable = true,

            });

            AddAttribute("textpath", new SvgAttributeInfo
            {
                Name = "textLength",
                DefaultUnit = "px",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<length-percentage>", "<auto>", },
                Animatable = true,

            });

            AddAttribute("textpath", new SvgAttributeInfo
            {
                Name = "method",
                DefaultUnit = "",
                DefaultValue = "align",
                DataType = new List<string> { "align", "stretch", },
                Animatable = true,

            });

            AddAttribute("textpath", new SvgAttributeInfo
            {
                Name = "side",
                DefaultUnit = "",
                DefaultValue = "left",
                DataType = new List<string> { "left", "right", },
                Animatable = true,

            });

            AddAttribute("textpath", new SvgAttributeInfo
            {
                Name = "spacing",
                DefaultUnit = "px",
                DefaultValue = "exact",
                DataType = new List<string> { "exact", "<auto>", },
                Animatable = true,

            });

            AddAttribute("textpath", new SvgAttributeInfo
            {
                Name = "startOffset",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", "<number>", },
                Animatable = true,

            });

            AddAttribute("textpath", new SvgAttributeInfo
            {
                Name = "fill",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            AddAttribute("textpath", new SvgAttributeInfo
            {
                Name = "stroke",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            #endregion

            #region tspan

            AddAttribute("tspan", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("tspan", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("tspan", new SvgAttributeInfo
            {
                Name = "dx",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<length-percentage>", "<none>", },
                Animatable = true,

            });

            AddAttribute("tspan", new SvgAttributeInfo
            {
                Name = "dy",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<length-percentage>", "<none>", },
                Animatable = true,

            });

            AddAttribute("tspan", new SvgAttributeInfo
            {
                Name = "rotate",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<number+>", "<none>", },
                Animatable = true,

            });

            AddAttribute("tspan", new SvgAttributeInfo
            {
                Name = "lengthAdjust",
                DefaultUnit = "px",
                DefaultValue = "spacing",
                DataType = new List<string> { "spacing", "spacingAndGlyphs", },
                Animatable = true,

            });

            AddAttribute("tspan", new SvgAttributeInfo
            {
                Name = "textLength",
                DefaultUnit = "px",
                DefaultValue = "<none>",
                DataType = new List<string> { "<length-percentage>", "<number>", "<none>", },
                Animatable = true,

            });

            AddAttribute("tspan", new SvgAttributeInfo
            {
                Name = "fill",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            AddAttribute("tspan", new SvgAttributeInfo
            {
                Name = "stroke",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<paint>" },
                Animatable = true,

            });

            #endregion

            #region use

            AddAttribute("use", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("use", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "px",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,
            });

            AddAttribute("use", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>", },
                Animatable = true,
            });

            AddAttribute("use", new SvgAttributeInfo
            {
                Name = "width",
                DefaultUnit = "px",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<length-percentage>", "<auto>", },
                Animatable = true,

            });

            AddAttribute("use", new SvgAttributeInfo
            {
                Name = "height",
                DefaultUnit = "px",
                DefaultValue = "<auto>",
                DataType = new List<string> { "<length-percentage>", "<auto>", },
                Animatable = true,

            });

            AddAttribute("use", "href", "", "", true);

            #endregion

            #region feblend

            AddAttribute("feblend", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feblend", new SvgAttributeInfo
            {
                Name = "in2",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feblend", new SvgAttributeInfo
            {
                Name = "mode",
                DefaultUnit = "",
                DefaultValue = "normal",
                DataType = new List<string> { " normal", "multiply", "screen ", "darken", "lighten", },
                Animatable = true,

            });

            #endregion

            #region feColorMatrix

            AddAttribute("feColorMatrix", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feColorMatrix", new SvgAttributeInfo
            {
                Name = "type",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "matrix", "saturate", "hueRotate", "luminanceToAlpha", },
                Animatable = true,

            });

            AddAttribute("feColorMatrix", new SvgAttributeInfo
            {
                Name = "values",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number>", "<number+>", "<none>", },
                Animatable = true,

            });

            #endregion

            #region feComponentTransfer

            AddAttribute("feComponentTransfer", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });


            #endregion

            #region feComposite

            AddAttribute("feComposite", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feComposite", new SvgAttributeInfo
            {
                Name = "in2",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feComposite", new SvgAttributeInfo
            {
                Name = "operator",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "over", "in", "out", "atop", "or", "arithmetic", },
                Animatable = true,

            });

            AddAttribute("feComposite", new SvgAttributeInfo
            {
                Name = "k1",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });


            AddAttribute("feComposite", new SvgAttributeInfo
            {
                Name = "k2",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });


            AddAttribute("feComposite", new SvgAttributeInfo
            {
                Name = "k3",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });


            AddAttribute("feComposite", new SvgAttributeInfo
            {
                Name = "k4",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            #endregion

            #region feConvolveMatrix

            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "order",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number-optional-number>", },
                Animatable = true,

            });

            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "kernelMatrix",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number+>", },
                Animatable = true,

            });


            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "divisor",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "bias",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });


            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "bias",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });


            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "targetY",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });


            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "targetX",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });  // 

            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "edge​Mode",
                DefaultUnit = "",
                DefaultValue = "duplicate",
                DataType = new List<string> { "duplicate", "wrap", "none", },
                Animatable = true,

            });

            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "kernelUnitLength",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number-optional-number>", },
                Animatable = true,

            });

            AddAttribute("feConvolveMatrix", new SvgAttributeInfo
            {
                Name = "preserve​Alpha",
                DefaultUnit = "",
                DefaultValue = "false",
                DataType = new List<string> { "true", "false", },
                Animatable = true,

            });


            #endregion

            #region feDiffuseLighting


            AddAttribute("feDiffuseLighting", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feDiffuseLighting", new SvgAttributeInfo
            {
                Name = "surfaceScale",
                DefaultUnit = "",
                DefaultValue = "1",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feDiffuseLighting", new SvgAttributeInfo
            {
                Name = "diffuseConstant",
                DefaultUnit = "",
                DefaultValue = "1",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feDiffuseLighting", new SvgAttributeInfo
            {
                Name = "kernelUnitLength",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number-optional-number>", },
                Animatable = true,

            });


            #endregion

            #region feDisplacementMap

            AddAttribute("feDisplacementMap", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feDisplacementMap", new SvgAttributeInfo
            {
                Name = "in2",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feDisplacementMap", new SvgAttributeInfo
            {
                Name = "scale",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feDisplacementMap", new SvgAttributeInfo
            {
                Name = "xChannelSelector",
                DefaultUnit = "",
                DefaultValue = "A",
                DataType = new List<string> { "R ", "G", "B ", "A", },
                Animatable = true,

            });

            AddAttribute("feDisplacementMap", new SvgAttributeInfo
            {
                Name = "yChannelSelector",
                DefaultUnit = "",
                DefaultValue = "A",
                DataType = new List<string> { "R ", "G", "B ", "A", },
                Animatable = true,

            });



            #endregion

            #region feDistantLight
            //  azimuth
            AddAttribute("feDistantLight", new SvgAttributeInfo
            {
                Name = "azimuth",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            //     elevation
            AddAttribute("feDistantLight", new SvgAttributeInfo
            {
                Name = "elevation",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            #endregion

            #region feDropShadow

            AddAttribute("feDropShadow", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feDropShadow", new SvgAttributeInfo
            {
                Name = "stdDeviation",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number-optional-number>", },
                Animatable = true,

            });

            AddAttribute("feDropShadow", new SvgAttributeInfo
            {
                Name = "dx",
                DefaultUnit = "",
                DefaultValue = "2",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feDropShadow", new SvgAttributeInfo
            {
                Name = "dy",
                DefaultUnit = "",
                DefaultValue = "2",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            #endregion

            #region feFlood

            AddAttribute("feFlood", new SvgAttributeInfo
            {
                Name = "flood-color",
                DefaultUnit = "",
                DefaultValue = "#000000",
                DataType = new List<string> { "<inherit>", "currentColor", "<color>", },
                Animatable = true,

            });

            AddAttribute("feFlood", new SvgAttributeInfo
            {
                Name = "flood-opacity",
                DefaultUnit = "",
                DefaultValue = "1",
                DataType = new List<string> { "<inherit>", "number", },
                Animatable = true,

            });

            #endregion

            #region feGaussianBlur

            AddAttribute("feGaussianBlur", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feGaussianBlur", new SvgAttributeInfo
            {
                Name = "stdDeviation",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number-optional-number>", },
                Animatable = true,

            });

            AddAttribute("feGaussianBlur", new SvgAttributeInfo
            {
                Name = "edge​Mode",
                DefaultUnit = "",
                DefaultValue = "duplicate",
                DataType = new List<string> { "duplicate", "wrap", "<none>", },
                Animatable = true,

            });

            #endregion

            #region feImage

            AddAttribute("feImage", new SvgAttributeInfo
            {
                Name = "preserveAspectRatio",
                DefaultUnit = "",
                DefaultValue = "xMidYMid meet",
                DataType = new List<string> { "<align>", },
                Animatable = true,
            });

            AddAttribute("feImage", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>", },
                Animatable = true,
            });


            #endregion

            #region feMergeNode


            AddAttribute("feMergeNode", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            #endregion

            #region feMorphology

            AddAttribute("feMorphology", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feMorphology", new SvgAttributeInfo
            {
                Name = "operator",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "erode", "dilate", },
                Animatable = true,

            });

            AddAttribute("feMorphology", new SvgAttributeInfo
            {
                Name = "radius",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number-optional-number>", },
                Animatable = true,

            });

            #endregion

            #region feOffset

            AddAttribute("feOffset", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feOffset", new SvgAttributeInfo
            {
                Name = "dx",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feOffset", new SvgAttributeInfo
            {
                Name = "dy",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            #endregion

            #region fePointLight

            AddAttribute("fePointLight", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "%",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("fePointLight", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "%",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });

            AddAttribute("fePointLight", new SvgAttributeInfo
            {
                Name = "z",
                DefaultUnit = "%",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>", },
                Animatable = true,

            });



            #endregion

            #region feSpecularLighting

            AddAttribute("feSpecularLighting", new SvgAttributeInfo
            {
                Name = "in",
                DefaultUnit = "",
                DefaultValue = "SourceGraphic",
                DataType = new List<string> { "SourceGraphic", "SourceAlpha", "BackgroundImage", "BackgroundAlpha", "FillPaint", "StrokePaint", "<filter-primitive-reference>", },
                Animatable = true,

            });

            AddAttribute("feSpecularLighting", new SvgAttributeInfo
            {
                Name = "surfaceScale",
                DefaultUnit = "",
                DefaultValue = "1",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feSpecularLighting", new SvgAttributeInfo
            {
                Name = "specular​Constant",
                DefaultUnit = "",
                DefaultValue = "1",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feSpecularLighting", new SvgAttributeInfo
            {
                Name = "specularExponent",
                DefaultUnit = "",
                DefaultValue = "1",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feSpecularLighting", new SvgAttributeInfo
            {
                Name = "kernelUnitLength",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number-optional-number>", },
                Animatable = true,

            });

            #endregion

            #region feSpotLight

            AddAttribute("feSpotLight", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "%",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feSpotLight", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "%",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feSpotLight", new SvgAttributeInfo
            {
                Name = "z",
                DefaultUnit = "%",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feSpotLight", new SvgAttributeInfo
            {
                Name = "pointsAtX",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feSpotLight", new SvgAttributeInfo
            {
                Name = "pointsAtY",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feSpotLight", new SvgAttributeInfo
            {
                Name = "pointsAtZ",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });


            AddAttribute("feSpotLight", new SvgAttributeInfo
            {
                Name = "limiting​Cone​Angle",
                DefaultUnit = "deg",
                DefaultValue = "0",
                DataType = new List<string> { "<angle>", },
                Animatable = true,

            });

            AddAttribute("feSpotLight", new SvgAttributeInfo
            {
                Name = "specularExponent",
                DefaultUnit = "",
                DefaultValue = "1",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });



            #endregion

            #region feTurbulence

            AddAttribute("feTurbulence", new SvgAttributeInfo
            {
                Name = "baseFrequency",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<number-optional-number>", },
                Animatable = true,

            });

            AddAttribute("feTurbulence", new SvgAttributeInfo
            {
                Name = "numOctaves",
                DefaultUnit = "",
                DefaultValue = "1",
                DataType = new List<string> { "<integer>", },
                Animatable = true,

            });

            AddAttribute("feTurbulence", new SvgAttributeInfo
            {
                Name = "seed",
                DefaultUnit = "",
                DefaultValue = "0",
                DataType = new List<string> { "<number>", },
                Animatable = true,

            });

            AddAttribute("feTurbulence", new SvgAttributeInfo
            {
                Name = "stitchTiles",
                DefaultUnit = "",
                DefaultValue = "noStitch",
                DataType = new List<string> { "noStitch", "stitch", },
                Animatable = true,

            });

            AddAttribute("feTurbulence", new SvgAttributeInfo
            {
                Name = "type",
                DefaultUnit = "",
                DefaultValue = "fractalNoise",
                DataType = new List<string> { "fractalNoise", "turbulence", },
                Animatable = true,

            });



            #endregion

            #region filter

            AddAttribute("filter", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "%",
                DefaultValue = "-10",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("filter", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "%",
                DefaultValue = "-10",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("filter", new SvgAttributeInfo
            {
                Name = "height",
                DefaultUnit = "%",
                DefaultValue = "100",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("filter", new SvgAttributeInfo
            {
                Name = "width",
                DefaultUnit = "%",
                DefaultValue = "100",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("filter", new SvgAttributeInfo
            {
                Name = "filterUnits",
                DefaultUnit = "",
                DefaultValue = "objectBoundingBox",
                DataType = new List<string> { "userSpaceOnUse", "objectBoundingBox", },
                Animatable = true,

            });

            AddAttribute("filter", new SvgAttributeInfo
            {
                Name = "primitive​Units",
                DefaultUnit = "",
                DefaultValue = "userSpaceOnUse",
                DataType = new List<string> { "userSpaceOnUse", "objectBoundingBox" },
                Animatable = true,

            });

            AddAttribute("filter", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>" },
                Animatable = true,

            });


            #endregion

            #region foreignObject

            AddAttribute("foreignObject", new SvgAttributeInfo
            {
                Name = "x",
                DefaultUnit = "%",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("foreignObject", new SvgAttributeInfo
            {
                Name = "y",
                DefaultUnit = "%",
                DefaultValue = "0",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("foreignObject", new SvgAttributeInfo
            {
                Name = "height",
                DefaultUnit = "%",
                DefaultValue = "100",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            AddAttribute("foreignObject", new SvgAttributeInfo
            {
                Name = "width",
                DefaultUnit = "100",
                DefaultValue = "%",
                DataType = new List<string> { "<length-percentage>" },
                Animatable = true,

            });

            #endregion


            #region script

            AddAttribute("script", new SvgAttributeInfo
            {
                Name = "type",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<value>" },
                Animatable = true,

            });

            AddAttribute("script", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>" },
                Animatable = true,

            });

            #endregion

            #region discard

            AddAttribute("discard", new SvgAttributeInfo
            {
                Name = "href",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<url>" },
                Animatable = true,

            });

            AddAttribute("discard", new SvgAttributeInfo
            {
                Name = "begin",
                DefaultUnit = "",
                DefaultValue = "",
                DataType = new List<string> { "<begin-value-list>" },
                Animatable = true,

            });

            #endregion


            AddAttribute("discard", "href");


        }
        #endregion
    }
}
