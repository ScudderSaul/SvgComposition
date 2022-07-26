using System;
using System.Collections.Generic;
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
using SvgComposition.ElementControls;




namespace SvgComposition
{
    public partial class ExampleGenerator
    {
        public SvgElement CreateAnElement(bool top, SvgElement par,
            string name, string content, string etype, int order)
        {
            SvgElement uu = new SvgElement();
            uu.TopSvg = top;
           
            if (top == false)
            {
                uu.SvgParentElementId = par.Id;
                var vv = from aa in SvgCompositionControl.Context.SvgElements
                    where aa.Id == par.Id
                    select aa;
                if (vv.Any())
                {
                    uu.SvgParentElement = vv.First();
                }
            }
            else
            {
                uu.SvgParentElement = null;
            }

            uu.Id = SvgElementCreator.FindNextSvgElementId();
            uu.ElementType = etype;
            uu.Content = content;
            uu.SvgElementName = name;
            uu.ElementOrder = order;
            SvgCompositionControl.Context.SvgElements.Add(uu);
            SvgCompositionControl.Context.SaveChanges();

            return (uu);
        }

        public SvgAttribute CreateAnAttribute(SvgElement epar, string nam, string aval,
            string sec, SvgAttributeType ty, string un)
        {
            SvgAttribute at = new SvgAttribute();
            at.Id = SvgAttributeCreator.FindNextSvgAttributeId();
            at.AttributeName = nam;
            at.AttributeValue = aval;
            at.SecondaryValue = sec;
            at.SvgUnit = un;
            at.AttributeType = ty;
            at.SvgElementId = epar.Id;

            var vv = from aa in SvgCompositionControl.Context.SvgElements
                where aa.Id == epar.Id
                select aa;

            if (vv.Any())
            {
                at.SvgElement = vv.First();

                SvgCompositionControl.Context.SvgAttributes.Add(at);
                SvgCompositionControl.Context.SaveChanges();

                var qq = from bb in SvgCompositionControl.Context.SvgAttributes
                    where bb.Id == at.Id
                    select bb;
                if (qq.Any())
                {

                    vv.First().SvgAttributes.Add(qq.First());
                    SvgCompositionControl.Context.SaveChanges();
                }
            }

            return (at);
        }

        public void CreateAColor(SvgAttribute isa, int ind,
            byte a, byte r, byte g, byte b )
        {
            SvgColor col = new SvgColor(a, r, g, b);
            col.Id = SvgAttributeCreator.FindNextSvgColorId();
            col.ColorIndex = ind;
            col.SvgAttributeId = isa.Id;

            var qq = from bb in SvgCompositionControl.Context.SvgAttributes
                where bb.Id == isa.Id
                select bb;
            if (qq.Any())
            {
                col.SvgAttribute = qq.First();

                SvgCompositionControl.Context.SvgColors.Add(col);
                SvgCompositionControl.Context.SaveChanges();

                qq.First().SvgColors.Add(col);
                SvgCompositionControl.Context.SaveChanges();
            }
        }

        // texture filter
        public void CreateElement7354()
        {
            SvgElement el1 = CreateAnElement(true, null, "Filtertexture", "", "svg", 0);
            SvgAttribute at1 = CreateAnAttribute(el1, "id", "Filtertexture", "", SvgAttributeType.Value, "px");
            SvgAttribute at2 = CreateAnAttribute(el1, "width", "470", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at3 = CreateAnAttribute(el1, "height", "250", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at4 = CreateAnAttribute(el1, "viewBox", "0, 0, 470, 250", "", SvgAttributeType.Value, "px");
            SvgElement el2 = CreateAnElement(false, el1, "conform", "", "filter", 0);
            SvgAttribute at5 = CreateAnAttribute(el2, "id", "conform", "", SvgAttributeType.Value, "px");
            SvgAttribute at6 = CreateAnAttribute(el2, "x", "-50", "", SvgAttributeType.LengthPercent, "%");
            SvgAttribute at7 = CreateAnAttribute(el2, "y", "-50", "", SvgAttributeType.LengthPercent, "%");
            SvgAttribute at8 = CreateAnAttribute(el2, "width", "200", "", SvgAttributeType.LengthPercent, "%");
            SvgAttribute at9 = CreateAnAttribute(el2, "height", "200", "", SvgAttributeType.LengthPercent, "%");
            SvgElement el3 = CreateAnElement(false, el2, "", "", "feImage", 0);
            SvgAttribute at10 = CreateAnAttribute(el3, "href", "https://dragonsawaken.net/leho/GameOfThronesCoins/content/images/large/DSC03787.jpg", "", SvgAttributeType.Value, "px");
            SvgAttribute at11 = CreateAnAttribute(el3, "x", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at12 = CreateAnAttribute(el3, "y", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at13 = CreateAnAttribute(el3, "preserveAspectRatio", "none", "", SvgAttributeType.Text, "px");
            SvgAttribute at14 = CreateAnAttribute(el3, "height", "100", "", SvgAttributeType.LengthPercent, "%");
            SvgAttribute at15 = CreateAnAttribute(el3, "width", "100", "", SvgAttributeType.LengthPercent, "%");
            SvgElement el4 = CreateAnElement(false, el2, "", "", "feColorMatrix", 0);
            SvgAttribute at16 = CreateAnAttribute(el4, "type", "saturate", "", SvgAttributeType.Text, "px");
            SvgAttribute at17 = CreateAnAttribute(el4, "result", "IMAGE", "", SvgAttributeType.Value, "px");
            SvgAttribute at18 = CreateAnAttribute(el4, "values", " 0", "", SvgAttributeType.Value, "px");
            SvgElement el5 = CreateAnElement(false, el2, "", "", "feGaussianBlur", 0);
            SvgAttribute at19 = CreateAnAttribute(el5, "in", "IMAGE", "", SvgAttributeType.Value, "px");
            SvgAttribute at20 = CreateAnAttribute(el5, "stdDeviation", "0.25", "none", SvgAttributeType.Value, "px");
            SvgAttribute at21 = CreateAnAttribute(el5, "result", "MAP", "", SvgAttributeType.Value, "px");
            SvgElement el6 = CreateAnElement(false, el2, "", "", "feDisplacementMap", 0);
            SvgAttribute at22 = CreateAnAttribute(el6, "in", "SourceGraphic", "", SvgAttributeType.Text, "px");
            SvgAttribute at23 = CreateAnAttribute(el6, "in2", "MAP", "", SvgAttributeType.Value, "px");
            SvgAttribute at24 = CreateAnAttribute(el6, "scale", "15", "", SvgAttributeType.Value, "px");
            SvgAttribute at25 = CreateAnAttribute(el6, "xChannelSelector", "R", "", SvgAttributeType.Text, "px");
            SvgAttribute at26 = CreateAnAttribute(el6, "yChannelSelector", "R", "", SvgAttributeType.Text, "px");
            SvgAttribute at27 = CreateAnAttribute(el6, "result", "TEXTURED_TEXT", "", SvgAttributeType.Value, "px");
            SvgElement el7 = CreateAnElement(false, el2, "", "", "feImage", 0);
            SvgAttribute at28 = CreateAnAttribute(el7, "href", "https://dragonsawaken.net/leho/GameOfThronesCoins/content/images/large/DSC03787.jpg", "", SvgAttributeType.Value, "px");
            SvgAttribute at29 = CreateAnAttribute(el7, "x", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at30 = CreateAnAttribute(el7, "y", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at31 = CreateAnAttribute(el7, "preserveAspectRatio", "none", "", SvgAttributeType.Text, "px");
            SvgAttribute at32 = CreateAnAttribute(el7, "height", "100", "", SvgAttributeType.LengthPercent, "%");
            SvgAttribute at33 = CreateAnAttribute(el7, "width", "100", "", SvgAttributeType.LengthPercent, "%");
            SvgAttribute at34 = CreateAnAttribute(el7, "result", "BG", "", SvgAttributeType.Value, "px");
            SvgElement el8 = CreateAnElement(false, el2, "", "", "feColorMatrix", 0);
            SvgAttribute at35 = CreateAnAttribute(el8, "in", "TEXTURED_TEXT", "", SvgAttributeType.Value, "px");
            SvgAttribute at36 = CreateAnAttribute(el8, "type", "matrix", "", SvgAttributeType.Text, "px");
            SvgAttribute at37 = CreateAnAttribute(el8, "values", " 1 0 0 0 0 0 1 0 0 0 0 0 1 0 0 0 0 0 .9 0", "", SvgAttributeType.Value, "px");
            SvgAttribute at38 = CreateAnAttribute(el8, "result", "Textured_Text_2", "", SvgAttributeType.Value, "px");
            SvgElement el9 = CreateAnElement(false, el2, "", "", "feBlend", 0);
            SvgAttribute at39 = CreateAnAttribute(el9, "in", "BG", "", SvgAttributeType.Value, "px");
            SvgAttribute at40 = CreateAnAttribute(el9, "in2", "Textured_Text_2", "", SvgAttributeType.Value, "px");
            SvgAttribute at41 = CreateAnAttribute(el9, "mode", "multiply", "", SvgAttributeType.Text, "px");
            SvgAttribute at42 = CreateAnAttribute(el9, "result", "BLENDED_TEXT", "", SvgAttributeType.Value, "px");
            SvgElement el10 = CreateAnElement(false, el2, "", "", "feMerge", 0);
            SvgElement el11 = CreateAnElement(false, el10, "", "", "feMergeNode", 0);
            SvgAttribute at43 = CreateAnAttribute(el11, "in", "BG", "", SvgAttributeType.Value, "px");
            SvgElement el12 = CreateAnElement(false, el10, "", "", "feMergeNode", 0);
            SvgAttribute at44 = CreateAnAttribute(el12, "in", "BLENDED_TEXT", "", SvgAttributeType.Value, "px");
            SvgElement el13 = CreateAnElement(false, el1, "", "Faceless Man", "text", 0);
            SvgAttribute at45 = CreateAnAttribute(el13, "dx", "60", "", SvgAttributeType.Value, "px");
            SvgAttribute at46 = CreateAnAttribute(el13, "dy", "200", "", SvgAttributeType.Value, "px");
            SvgAttribute at47 = CreateAnAttribute(el13, "font-size", "8", "", SvgAttributeType.LengthPercent, "em");
            SvgAttribute at48 = CreateAnAttribute(el13, "font-weight", "bold", "", SvgAttributeType.Text, "px");
            SvgAttribute at49 = CreateAnAttribute(el13, "fill", "#00d1af", "", SvgAttributeType.Color, "px");
            CreateAColor(at49, 0, 255, 0, 209, 175);
            SvgAttribute at50 = CreateAnAttribute(el13, "filter", "url(#conform)", "", SvgAttributeType.Text, "px");
        }

        // light effects
        public void CreateElement8619()
        {
            SvgElement el1 = CreateAnElement(true, null, "lighttest", "", "svg", 0);
            SvgAttribute at1 = CreateAnAttribute(el1, "id", "lighttest", "", SvgAttributeType.Value, "px");
            SvgAttribute at2 = CreateAnAttribute(el1, "width", "440", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at3 = CreateAnAttribute(el1, "height", "140", "", SvgAttributeType.LengthPercent, "px");
            SvgElement el2 = CreateAnElement(false, el1, "", "No Light", "text", 0);
            SvgAttribute at4 = CreateAnAttribute(el2, "x", "60", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at5 = CreateAnAttribute(el2, "y", "22", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at6 = CreateAnAttribute(el2, "text-anchor", "middle", "", SvgAttributeType.Text, "px");
            SvgElement el3 = CreateAnElement(false, el1, "", "", "circle", 0);
            SvgAttribute at7 = CreateAnAttribute(el3, "cx", "60", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at8 = CreateAnAttribute(el3, "cy", "80", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at9 = CreateAnAttribute(el3, "r", "50", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at10 = CreateAnAttribute(el3, "stroke", "#00ff00", "", SvgAttributeType.Color, "px");
            CreateAColor(at10, 0, 255, 0, 255, 0);
            SvgAttribute at11 = CreateAnAttribute(el3, "stroke-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at12 = CreateAnAttribute(el3, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "px");
            SvgAttribute at13 = CreateAnAttribute(el3, "fill", "#00ff00", "", SvgAttributeType.Color, "px");
            CreateAColor(at13, 0, 255, 0, 255, 0);
            SvgAttribute at14 = CreateAnAttribute(el3, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgElement el4 = CreateAnElement(false, el1, "", "fePointLight", "text", 0);
            SvgAttribute at15 = CreateAnAttribute(el4, "x", "170", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at16 = CreateAnAttribute(el4, "y", "22", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at17 = CreateAnAttribute(el4, "text-anchor", "middle", "", SvgAttributeType.Text, "px");
            SvgElement el5 = CreateAnElement(false, el1, "lightMe1", "", "filter", 0);
            SvgAttribute at18 = CreateAnAttribute(el5, "id", "lightMe1", "", SvgAttributeType.Value, "px");
            SvgElement el6 = CreateAnElement(false, el5, "", "", "feDiffuseLighting", 0);
            SvgAttribute at19 = CreateAnAttribute(el6, "in", "SourceGraphic", "", SvgAttributeType.Text, "px");
            SvgAttribute at20 = CreateAnAttribute(el6, "result", " light", "", SvgAttributeType.Value, "px");
            SvgAttribute at21 = CreateAnAttribute(el6, "lighting-color", "#ffffff", "", SvgAttributeType.Color, "px");
            CreateAColor(at21, 0, 255, 255, 255, 255);
            SvgElement el7 = CreateAnElement(false, el6, "", "", "fePointLight", 0);
            SvgAttribute at22 = CreateAnAttribute(el7, "x", "160", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at23 = CreateAnAttribute(el7, "y", "60", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at24 = CreateAnAttribute(el7, "z", "20", "", SvgAttributeType.Value, "px");
            SvgElement el8 = CreateAnElement(false, el5, "", "", "feComposite", 0);
            SvgAttribute at25 = CreateAnAttribute(el8, "in", "SourceGraphic", "", SvgAttributeType.Text, "px");
            SvgAttribute at26 = CreateAnAttribute(el8, "in2", "light", "", SvgAttributeType.Value, "px");
            SvgAttribute at27 = CreateAnAttribute(el8, "operator", "arithmetic", "", SvgAttributeType.Text, "px");
            SvgAttribute at28 = CreateAnAttribute(el8, "k1", "1", "", SvgAttributeType.Value, "px");
            SvgAttribute at29 = CreateAnAttribute(el8, "k2", "0", "", SvgAttributeType.Value, "px");
            SvgAttribute at30 = CreateAnAttribute(el8, "k3", "0", "", SvgAttributeType.Value, "px");
            SvgAttribute at31 = CreateAnAttribute(el8, "k4", "0", "", SvgAttributeType.Value, "px");
            SvgElement el9 = CreateAnElement(false, el1, "", "", "circle", 0);
            SvgAttribute at32 = CreateAnAttribute(el9, "cx", "170", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at33 = CreateAnAttribute(el9, "cy", "80", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at34 = CreateAnAttribute(el9, "r", "50", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at35 = CreateAnAttribute(el9, "stroke", "#00ff00", "", SvgAttributeType.Color, "px");
            CreateAColor(at35, 0, 255, 0, 255, 0);
            SvgAttribute at36 = CreateAnAttribute(el9, "stroke-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at37 = CreateAnAttribute(el9, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "px");
            SvgAttribute at38 = CreateAnAttribute(el9, "fill", "#00ff00", "", SvgAttributeType.Color, "px");
            CreateAColor(at38, 0, 255, 0, 255, 0);
            SvgAttribute at39 = CreateAnAttribute(el9, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at40 = CreateAnAttribute(el9, "filter", "url(#lightMe1)", "", SvgAttributeType.Text, "px");
            SvgElement el10 = CreateAnElement(false, el1, "", "feDistantLight", "text", 0);
            SvgAttribute at41 = CreateAnAttribute(el10, "x", "280", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at42 = CreateAnAttribute(el10, "y", "22", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at43 = CreateAnAttribute(el10, "text-anchor", "middle", "", SvgAttributeType.Text, "px");
            SvgElement el11 = CreateAnElement(false, el1, "lightMe2", "", "filter", 0);
            SvgAttribute at44 = CreateAnAttribute(el11, "id", "lightMe2", "", SvgAttributeType.Value, "px");
            SvgElement el12 = CreateAnElement(false, el11, "", "", "feDiffuseLighting", 0);
            SvgAttribute at45 = CreateAnAttribute(el12, "in", "SourceGraphic", "", SvgAttributeType.Text, "px");
            SvgAttribute at46 = CreateAnAttribute(el12, "result", " light", "", SvgAttributeType.Value, "px");
            SvgAttribute at47 = CreateAnAttribute(el12, "lighting-color", "#ffffff", "", SvgAttributeType.Color, "px");
            CreateAColor(at47, 0, 255, 255, 255, 255);
            SvgElement el13 = CreateAnElement(false, el12, "", "", "feDistantLight", 0);
            SvgAttribute at48 = CreateAnAttribute(el13, "elevation", "20", "", SvgAttributeType.Value, "px");
            SvgAttribute at49 = CreateAnAttribute(el13, "azimuth", "240", "", SvgAttributeType.Value, "px");
            SvgElement el14 = CreateAnElement(false, el11, "", "", "feComposite", 0);
            SvgAttribute at50 = CreateAnAttribute(el14, "in", "SourceGraphic", "", SvgAttributeType.Text, "px");
            SvgAttribute at51 = CreateAnAttribute(el14, "in2", "light", "", SvgAttributeType.Value, "px");
            SvgAttribute at52 = CreateAnAttribute(el14, "operator", "arithmetic", "", SvgAttributeType.Text, "px");
            SvgAttribute at53 = CreateAnAttribute(el14, "k1", "1", "", SvgAttributeType.Value, "px");
            SvgAttribute at54 = CreateAnAttribute(el14, "k2", "0", "", SvgAttributeType.Value, "px");
            SvgAttribute at55 = CreateAnAttribute(el14, "k3", "0", "", SvgAttributeType.Value, "px");
            SvgAttribute at56 = CreateAnAttribute(el14, "k4", "0", "", SvgAttributeType.Value, "px");
            SvgElement el15 = CreateAnElement(false, el1, "", "", "circle", 0);
            SvgAttribute at57 = CreateAnAttribute(el15, "cx", "280", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at58 = CreateAnAttribute(el15, "cy", "80", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at59 = CreateAnAttribute(el15, "r", "50", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at60 = CreateAnAttribute(el15, "stroke", "#00ff00", "", SvgAttributeType.Color, "px");
            CreateAColor(at60, 0, 255, 0, 255, 0);
            SvgAttribute at61 = CreateAnAttribute(el15, "stroke-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at62 = CreateAnAttribute(el15, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "px");
            SvgAttribute at63 = CreateAnAttribute(el15, "fill", "#00ff00", "", SvgAttributeType.Color, "px");
            CreateAColor(at63, 0, 255, 0, 255, 0);
            SvgAttribute at64 = CreateAnAttribute(el15, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at65 = CreateAnAttribute(el15, "filter", "url(#lightMe2)", "", SvgAttributeType.Text, "px");
            SvgElement el16 = CreateAnElement(false, el1, "", "feSpotLight", "text", 0);
            SvgAttribute at66 = CreateAnAttribute(el16, "x", "390", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at67 = CreateAnAttribute(el16, "y", "22", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at68 = CreateAnAttribute(el16, "text-anchor", "middle", "", SvgAttributeType.Text, "px");
            SvgElement el17 = CreateAnElement(false, el1, "lightMe3", "", "filter", 0);
            SvgAttribute at69 = CreateAnAttribute(el17, "id", "lightMe3", "", SvgAttributeType.Value, "px");
            SvgElement el18 = CreateAnElement(false, el17, "", "", "feDiffuseLighting", 0);
            SvgAttribute at70 = CreateAnAttribute(el18, "in", "SourceAlpha", "", SvgAttributeType.Text, "px");
            SvgAttribute at71 = CreateAnAttribute(el18, "result", " light", "", SvgAttributeType.Value, "px");
            SvgAttribute at72 = CreateAnAttribute(el18, "lighting-color", "#ffffff", "", SvgAttributeType.Color, "px");
            CreateAColor(at72, 0, 255, 255, 255, 255);
            SvgElement el19 = CreateAnElement(false, el18, "", "", "feSpotLight", 0);
            SvgAttribute at73 = CreateAnAttribute(el19, "x", "360", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at74 = CreateAnAttribute(el19, "y", "5", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at75 = CreateAnAttribute(el19, "z", "30", "", SvgAttributeType.Value, "px");
            SvgAttribute at76 = CreateAnAttribute(el19, "pointsAtX", "390", "", SvgAttributeType.Value, "px");
            SvgAttribute at77 = CreateAnAttribute(el19, "pointsAtY", "80", "", SvgAttributeType.Value, "px");
            SvgAttribute at78 = CreateAnAttribute(el19, "pointsAtZ", "0", "", SvgAttributeType.Value, "px");
            SvgAttribute at79 = CreateAnAttribute(el19, "limitingConeAngle", "20", "", SvgAttributeType.Value, "px");
            SvgElement el20 = CreateAnElement(false, el17, "", "", "feComposite", 0);
            SvgAttribute at80 = CreateAnAttribute(el20, "in", "SourceGraphic", "", SvgAttributeType.Text, "px");
            SvgAttribute at81 = CreateAnAttribute(el20, "in2", "light", "", SvgAttributeType.Value, "px");
            SvgAttribute at82 = CreateAnAttribute(el20, "operator", "arithmetic", "", SvgAttributeType.Text, "px");
            SvgAttribute at83 = CreateAnAttribute(el20, "k1", "1", "", SvgAttributeType.Value, "px");
            SvgAttribute at84 = CreateAnAttribute(el20, "k2", "0", "", SvgAttributeType.Value, "px");
            SvgAttribute at85 = CreateAnAttribute(el20, "k3", "0", "", SvgAttributeType.Value, "px");
            SvgAttribute at86 = CreateAnAttribute(el20, "k4", "0", "", SvgAttributeType.Value, "px");
            SvgElement el21 = CreateAnElement(false, el1, "", "", "circle", 0);
            SvgAttribute at87 = CreateAnAttribute(el21, "cx", "390", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at88 = CreateAnAttribute(el21, "cy", "80", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at89 = CreateAnAttribute(el21, "r", "50", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at90 = CreateAnAttribute(el21, "stroke", "#00ff00", "", SvgAttributeType.Color, "px");
            CreateAColor(at90, 0, 255, 0, 255, 0);
            SvgAttribute at91 = CreateAnAttribute(el21, "stroke-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at92 = CreateAnAttribute(el21, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "px");
            SvgAttribute at93 = CreateAnAttribute(el21, "fill", "#00ff00", "", SvgAttributeType.Color, "px");
            CreateAColor(at93, 0, 255, 0, 255, 0);
            SvgAttribute at94 = CreateAnAttribute(el21, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at95 = CreateAnAttribute(el21, "filter", "url(#lightMe3)", "", SvgAttributeType.Text, "px");
        }

        // marker
        public void CreateElement8852()
        {
            SvgElement el1 = CreateAnElement(true, null, "marker example", "", "svg", 0);
            SvgAttribute at1 = CreateAnAttribute(el1, "id", "marker example", "", SvgAttributeType.Value, "px");
            SvgAttribute at2 = CreateAnAttribute(el1, "viewBox", "0, 0, 100, 100", "", SvgAttributeType.Value, "px");
            SvgAttribute at3 = CreateAnAttribute(el1, "width", "400", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at4 = CreateAnAttribute(el1, "height", "400", "", SvgAttributeType.LengthPercent, "");
            SvgElement el2 = CreateAnElement(false, el1, "", "", "defs", 0);
            SvgElement el3 = CreateAnElement(false, el2, "arrow", "", "marker", 0);
            SvgAttribute at5 = CreateAnAttribute(el3, "markerHeight", "6", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at6 = CreateAnAttribute(el3, "markerWidth", "6", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at7 = CreateAnAttribute(el3, "orient", "auto-start-reverse", "", SvgAttributeType.Text, "px");
            SvgAttribute at8 = CreateAnAttribute(el3, "refX", "5", "", SvgAttributeType.Value, "px");
            SvgAttribute at9 = CreateAnAttribute(el3, "refY", "5", "", SvgAttributeType.Value, "px");
            SvgAttribute at10 = CreateAnAttribute(el3, "viewBox", "0, 0, 10, 10", "", SvgAttributeType.Value, "px");
            SvgAttribute at11 = CreateAnAttribute(el3, "id", "arrow", "", SvgAttributeType.Value, "px");
            SvgElement el4 = CreateAnElement(false, el3, "", "", "path", 0);
            SvgAttribute at12 = CreateAnAttribute(el4, "d", " M 0 0 L 10 5 L 0 10 z", "", SvgAttributeType.Value, "px");
            SvgAttribute at13 = CreateAnAttribute(el4, "id", "", "", SvgAttributeType.Value, "px");
            SvgElement el5 = CreateAnElement(false, el2, "dot", "", "marker", 0);
            SvgAttribute at14 = CreateAnAttribute(el5, "markerHeight", "5", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at15 = CreateAnAttribute(el5, "markerWidth", "5", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at16 = CreateAnAttribute(el5, "refX", "5", "", SvgAttributeType.Value, "px");
            SvgAttribute at17 = CreateAnAttribute(el5, "refY", "5", "", SvgAttributeType.Value, "px");
            SvgAttribute at18 = CreateAnAttribute(el5, "viewBox", "0, 0, 10, 10", "", SvgAttributeType.Value, "px");
            SvgAttribute at19 = CreateAnAttribute(el5, "id", "dot", "", SvgAttributeType.Value, "px");
            SvgElement el6 = CreateAnElement(false, el5, "", "", "circle", 0);
            SvgAttribute at20 = CreateAnAttribute(el6, "cy", "5", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at21 = CreateAnAttribute(el6, "r", "5", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at22 = CreateAnAttribute(el6, "stroke", "#000000", "", SvgAttributeType.Color, "px");
            CreateAColor(at22, 0, 255, 0, 0, 0);
            SvgAttribute at23 = CreateAnAttribute(el6, "stroke-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at24 = CreateAnAttribute(el6, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at25 = CreateAnAttribute(el6, "fill", "#ff0000", "", SvgAttributeType.Color, "px");
            CreateAColor(at25, 0, 255, 255, 0, 0);
            SvgAttribute at26 = CreateAnAttribute(el6, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at27 = CreateAnAttribute(el6, "id", "", "", SvgAttributeType.Value, "px");
            SvgAttribute at28 = CreateAnAttribute(el6, "cx", "5", "", SvgAttributeType.LengthPercent, " ");
            SvgElement el7 = CreateAnElement(false, el1, "", "", "polyline", 0);
            SvgAttribute at29 = CreateAnAttribute(el7, "points", " 10,10 10,90 90,90", "", SvgAttributeType.Value, "px");
            SvgAttribute at30 = CreateAnAttribute(el7, "id", "", "", SvgAttributeType.Value, "px");
            SvgAttribute at31 = CreateAnAttribute(el7, "fill", "none", "", SvgAttributeType.Text, "px");
            CreateAColor(at31, 0, 255, 0, 0, 0);
            SvgAttribute at32 = CreateAnAttribute(el7, "stroke", "#000000", "", SvgAttributeType.Color, "px");
            CreateAColor(at32, 0, 255, 0, 0, 0);
            SvgAttribute at33 = CreateAnAttribute(el7, "marker-start", "url(#arrow)", "", SvgAttributeType.Url, "px");
            SvgAttribute at34 = CreateAnAttribute(el7, "marker-end", "url(#arrow)", "", SvgAttributeType.Url, "px");
            SvgElement el8 = CreateAnElement(false, el1, "", "", "polyline", 0);
            SvgAttribute at35 = CreateAnAttribute(el8, "points", " 15,80 29,50 43,60 57,30 71,40 85,15", "", SvgAttributeType.Value, "px");
            SvgAttribute at36 = CreateAnAttribute(el8, "id", "", "", SvgAttributeType.Value, "px");
            SvgAttribute at37 = CreateAnAttribute(el8, "fill", "none", "", SvgAttributeType.Text, "px");
            CreateAColor(at37, 0, 255, 0, 0, 0);
            SvgAttribute at38 = CreateAnAttribute(el8, "stroke", "#777677", "", SvgAttributeType.Color, "px");
            CreateAColor(at38, 0, 255, 119, 118, 119);
            SvgAttribute at39 = CreateAnAttribute(el8, "marker-start", "url(#dot)", "", SvgAttributeType.Url, "px");
            SvgAttribute at40 = CreateAnAttribute(el8, "marker-mid", "url(#dot)", "", SvgAttributeType.Url, "px");
            SvgAttribute at41 = CreateAnAttribute(el8, "marker-end", "url(#dot)", "", SvgAttributeType.Url, "px");
        }

        // two squares
        public void CreateElement8641()
        {
            SvgElement el1 = CreateAnElement(true, null, "two squares", "", "svg", 0);
            SvgAttribute at1 = CreateAnAttribute(el1, "id", "two squares", "", SvgAttributeType.Value, "px");
            SvgAttribute at2 = CreateAnAttribute(el1, "width", "200", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at3 = CreateAnAttribute(el1, "height", "200", "", SvgAttributeType.LengthPercent, "");
            SvgElement el2 = CreateAnElement(false, el1, "", "", "defs", 0);
            SvgElement el3 = CreateAnElement(false, el2, "offset", "", "filter", 0);
            SvgAttribute at4 = CreateAnAttribute(el3, "id", "offset", "", SvgAttributeType.Value, "px");
            SvgAttribute at5 = CreateAnAttribute(el3, "width", "180", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at6 = CreateAnAttribute(el3, "height", "180", "", SvgAttributeType.LengthPercent, "");
            SvgElement el4 = CreateAnElement(false, el3, "", "", "feOffset", 0);
            SvgAttribute at7 = CreateAnAttribute(el4, "in", "SourceGraphic", "", SvgAttributeType.Text, "px");
            SvgAttribute at8 = CreateAnAttribute(el4, "dx", "60", "", SvgAttributeType.Value, "px");
            SvgAttribute at9 = CreateAnAttribute(el4, "dy", "60", "", SvgAttributeType.Value, "px");
            SvgElement el5 = CreateAnElement(false, el1, "", "", "rect", 0);
            SvgAttribute at10 = CreateAnAttribute(el5, "id", "", "", SvgAttributeType.Value, "px");
            SvgAttribute at11 = CreateAnAttribute(el5, "x", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at12 = CreateAnAttribute(el5, "y", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at13 = CreateAnAttribute(el5, "width", "100", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at14 = CreateAnAttribute(el5, "height", "100", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at15 = CreateAnAttribute(el5, "stroke", "#000000", "", SvgAttributeType.Color, "px");
            CreateAColor(at15, 0, 255, 0, 0, 0);
            SvgAttribute at16 = CreateAnAttribute(el5, "stroke-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at17 = CreateAnAttribute(el5, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at18 = CreateAnAttribute(el5, "fill", "#00ff00", "", SvgAttributeType.Color, "px");
            CreateAColor(at18, 0, 255, 0, 255, 0);
            SvgAttribute at19 = CreateAnAttribute(el5, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgElement el6 = CreateAnElement(false, el1, "", "", "rect", 0);
            SvgAttribute at20 = CreateAnAttribute(el6, "id", "", "", SvgAttributeType.Value, "px");
            SvgAttribute at21 = CreateAnAttribute(el6, "x", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at22 = CreateAnAttribute(el6, "y", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at23 = CreateAnAttribute(el6, "width", "100", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at24 = CreateAnAttribute(el6, "height", "100", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at25 = CreateAnAttribute(el6, "stroke", "#000000", "", SvgAttributeType.Color, "px");
            CreateAColor(at25, 0, 255, 0, 0, 0);
            SvgAttribute at26 = CreateAnAttribute(el6, "stroke-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at27 = CreateAnAttribute(el6, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at28 = CreateAnAttribute(el6, "fill", "#00ff00", "", SvgAttributeType.Color, "px");
            CreateAColor(at28, 0, 255, 0, 255, 0);
            SvgAttribute at29 = CreateAnAttribute(el6, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at30 = CreateAnAttribute(el6, "filter", "url(#offset)", "", SvgAttributeType.Text, "px");
        }

        // patterns
        public void CreateElement8546()
        {
            SvgElement el1 = CreateAnElement(true, null, "patternstest", "", "svg", 0);
            SvgAttribute at1 = CreateAnAttribute(el1, "id", "patternstest", "", SvgAttributeType.Value, "px");
            SvgAttribute at2 = CreateAnAttribute(el1, "width", "200", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at3 = CreateAnAttribute(el1, "height", "200", "", SvgAttributeType.LengthPercent, "");
            SvgElement el2 = CreateAnElement(false, el1, "", "", "defs", 0);
            SvgElement el3 = CreateAnElement(false, el2, "GradientOne", "", "linearGradient", 0);
            SvgAttribute at4 = CreateAnAttribute(el3, "id", "GradientOne", "", SvgAttributeType.Value, "px");
            SvgElement el4 = CreateAnElement(false, el3, "", "", "stop", 0);
            SvgAttribute at5 = CreateAnAttribute(el4, "offset", "5%", "", SvgAttributeType.Value, "px");
            SvgAttribute at6 = CreateAnAttribute(el4, "stop-color", "#ffffff", "", SvgAttributeType.Color, "px");
            CreateAColor(at6, 0, 255, 255, 255, 255);
            SvgElement el5 = CreateAnElement(false, el3, "", "", "stop", 0);
            SvgAttribute at7 = CreateAnAttribute(el5, "offset", "95%", "", SvgAttributeType.Value, "px");
            SvgAttribute at8 = CreateAnAttribute(el5, "stop-color", "#0000ff", "", SvgAttributeType.Color, "px");
            CreateAColor(at8, 0, 255, 0, 0, 255);
            SvgElement el6 = CreateAnElement(false, el2, "GradientTwo", "", "linearGradient", 0);
            SvgAttribute at9 = CreateAnAttribute(el6, "id", "GradientTwo", "", SvgAttributeType.Value, "px");
            SvgElement el7 = CreateAnElement(false, el6, "", "", "stop", 0);
            SvgAttribute at10 = CreateAnAttribute(el7, "offset", "5%", "", SvgAttributeType.Value, "px");
            SvgAttribute at11 = CreateAnAttribute(el7, "stop-color", "#ff0000", "", SvgAttributeType.Color, "px");
            CreateAColor(at11, 0, 255, 255, 0, 0);
            SvgElement el8 = CreateAnElement(false, el6, "", "", "stop", 0);
            SvgAttribute at12 = CreateAnAttribute(el8, "offset", "95%", "", SvgAttributeType.Value, "px");
            SvgAttribute at13 = CreateAnAttribute(el8, "stop-color", "#ffa500", "", SvgAttributeType.Color, "px");
            CreateAColor(at13, 0, 255, 255, 165, 0);
            SvgElement el9 = CreateAnElement(false, el1, "Pattern", "", "pattern", 0);
            SvgAttribute at14 = CreateAnAttribute(el9, "height", ".25", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at15 = CreateAnAttribute(el9, "width", ".25", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at16 = CreateAnAttribute(el9, "x", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at17 = CreateAnAttribute(el9, "y", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at18 = CreateAnAttribute(el9, "id", "Pattern", "", SvgAttributeType.Value, "px");
            SvgElement el10 = CreateAnElement(false, el9, "", "", "rect", 0);
            SvgAttribute at19 = CreateAnAttribute(el10, "id", "", "", SvgAttributeType.Value, "px");
            SvgAttribute at20 = CreateAnAttribute(el10, "x", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at21 = CreateAnAttribute(el10, "y", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at22 = CreateAnAttribute(el10, "width", "50", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at23 = CreateAnAttribute(el10, "height", "50", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at24 = CreateAnAttribute(el10, "fill", "#87ceeb", "", SvgAttributeType.Color, "px");
            CreateAColor(at24, 0, 255, 135, 206, 235);
            SvgAttribute at25 = CreateAnAttribute(el10, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgElement el11 = CreateAnElement(false, el9, "", "", "rect", 0);
            SvgAttribute at26 = CreateAnAttribute(el11, "id", "", "", SvgAttributeType.Value, "px");
            SvgAttribute at27 = CreateAnAttribute(el11, "x", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at28 = CreateAnAttribute(el11, "y", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at29 = CreateAnAttribute(el11, "width", "25", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at30 = CreateAnAttribute(el11, "height", "25", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at31 = CreateAnAttribute(el11, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at32 = CreateAnAttribute(el11, "fill", "url(#GradientTwo)", "none", SvgAttributeType.Url, "px");
            CreateAColor(at32, 0, 255, 135, 206, 235);
            SvgAttribute at33 = CreateAnAttribute(el11, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgElement el12 = CreateAnElement(false, el9, "", "", "circle", 0);
            SvgAttribute at34 = CreateAnAttribute(el12, "cx", "25", "", SvgAttributeType.LengthPercent, " ");
            SvgAttribute at35 = CreateAnAttribute(el12, "cy", "25", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at36 = CreateAnAttribute(el12, "r", "20", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at37 = CreateAnAttribute(el12, "fill", "url(#GradientOne)", "none", SvgAttributeType.Url, "px");
            CreateAColor(at37, 0, 255, 0, 0, 0);
            SvgAttribute at38 = CreateAnAttribute(el12, "fill-opacity", ".5", "", SvgAttributeType.Value, "px");
            SvgElement el13 = CreateAnElement(false, el1, "", "", "rect", 0);
            SvgAttribute at39 = CreateAnAttribute(el13, "id", "", "", SvgAttributeType.Value, "px");
            SvgAttribute at40 = CreateAnAttribute(el13, "width", "200", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at41 = CreateAnAttribute(el13, "height", "200", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at42 = CreateAnAttribute(el13, "stroke", "#000000", "", SvgAttributeType.Color, "px");
            CreateAColor(at42, 0, 255, 0, 0, 0);
            SvgAttribute at43 = CreateAnAttribute(el13, "stroke-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at44 = CreateAnAttribute(el13, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at45 = CreateAnAttribute(el13, "fill", "url(#Pattern)", "none", SvgAttributeType.Url, "px");
            CreateAColor(at45, 0, 255, 0, 0, 0);
            SvgAttribute at46 = CreateAnAttribute(el13, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
        }

        // simple animation
        public void CreateElement6019()
        {
            SvgElement el1 = CreateAnElement(true, null, "foofoo", "", "svg", 0);
            SvgAttribute at1 = CreateAnAttribute(el1, "id", "foofoo", "", SvgAttributeType.Value, "px");
            SvgAttribute at2 = CreateAnAttribute(el1, "viewBox", "0, 0, 400, 400", "", SvgAttributeType.Value, "px");
            SvgAttribute at3 = CreateAnAttribute(el1, "width", "400", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at4 = CreateAnAttribute(el1, "height", "400", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at5 = CreateAnAttribute(el1, "x", "100", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at6 = CreateAnAttribute(el1, "y", "20", "", SvgAttributeType.LengthPercent, "");
            SvgElement el2 = CreateAnElement(false, el1, "", "", "rect", 0);
            SvgAttribute at7 = CreateAnAttribute(el2, "id", "", "", SvgAttributeType.Value, "px");
            SvgAttribute at8 = CreateAnAttribute(el2, "x", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at9 = CreateAnAttribute(el2, "y", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at10 = CreateAnAttribute(el2, "width", "300", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at11 = CreateAnAttribute(el2, "height", "300", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at12 = CreateAnAttribute(el2, "stroke", "#000000", "", SvgAttributeType.Color, "px");
            CreateAColor(at12, 0, 255, 0, 0, 0);
            SvgAttribute at13 = CreateAnAttribute(el2, "stroke-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at14 = CreateAnAttribute(el2, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at15 = CreateAnAttribute(el2, "fill", "#000000", "", SvgAttributeType.Color, "px");
            CreateAColor(at15, 0, 255, 0, 0, 0);
            SvgAttribute at16 = CreateAnAttribute(el2, "fill-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgElement el3 = CreateAnElement(false, el1, "rect2", "", "rect", 0);
            SvgAttribute at17 = CreateAnAttribute(el3, "id", "rect2", "", SvgAttributeType.Value, "px");
            SvgAttribute at18 = CreateAnAttribute(el3, "x", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at19 = CreateAnAttribute(el3, "y", "0", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at20 = CreateAnAttribute(el3, "width", "30", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at21 = CreateAnAttribute(el3, "height", "20", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at22 = CreateAnAttribute(el3, "stroke", "#000000", "", SvgAttributeType.Color, "px");
            CreateAColor(at22, 0, 255, 0, 0, 0);
            SvgAttribute at23 = CreateAnAttribute(el3, "stroke-opacity", "1.0", "", SvgAttributeType.Value, "px");
            SvgAttribute at24 = CreateAnAttribute(el3, "stroke-width", "1", "", SvgAttributeType.LengthPercent, "");
            SvgAttribute at25 = CreateAnAttribute(el3, "fill", "#00bfff", "", SvgAttributeType.Color, "px");
            CreateAColor(at25, 0, 255, 0, 191, 255);
            SvgAttribute at26 = CreateAnAttribute(el3, "fill-opacity", ".5", "", SvgAttributeType.Value, "px");
            SvgElement el4 = CreateAnElement(false, el3, "", "", "animateMotion", 0);
            SvgAttribute at27 = CreateAnAttribute(el4, "path", "M 250,80 H 50 Q 30,80 30,50 Q 30,20 50,20 H 250 Q 280,20,280,50 Q 280,80,250,80Z", "", SvgAttributeType.Value, "px");
            SvgAttribute at28 = CreateAnAttribute(el4, "dur", "3s", "", SvgAttributeType.Value, "px");
            SvgAttribute at29 = CreateAnAttribute(el4, "repeatCount", "indefinite", "", SvgAttributeType.Text, "px");
            SvgAttribute at30 = CreateAnAttribute(el4, "rotate", "auto", "", SvgAttributeType.Text, "px");
        }
    }
}