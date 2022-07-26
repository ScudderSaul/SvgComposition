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
 
namespace SvgComposition.ElementControls
{
void CreateElement5816()
{
   SvgElement ee1 = new SvgElement();
   ee1.TopSvg = true;
   ee1.SvgElementName = "OpenDoor";
   ee1.ElementType = svg;
   ee1.Id = SvgElementCreator.FindNextSvgElementId();
   ee1.ElementOrder = 0;
   SvgAttribute aa0 = new SvgAttribute();
   aa0.Id = SvgAttributeCreator.FindNextSvgAttributeId();
   aa0.AttributeName = "id";
   aa0.AttributeValue = "OpenDoor";
   aa0.AttributeType = Value;
   aa0.SvgUnit = "px";
   ee1.SvgAttributes.Add(aa0);
   SvgAttribute aa1 = new SvgAttribute();
   aa1.Id = SvgAttributeCreator.FindNextSvgAttributeId();
   aa1.AttributeName = "viewBox";
   aa1.AttributeValue = "0, 0, 1000, 1000";
   aa1.AttributeType = Value;
   aa1.SvgUnit = "px";
   ee1.SvgAttributes.Add(aa1);
   SvgAttribute aa2 = new SvgAttribute();
   aa2.Id = SvgAttributeCreator.FindNextSvgAttributeId();
   aa2.AttributeName = "width";
   aa2.AttributeValue = "100";
   aa2.AttributeType = LengthPercent;
   aa2.SvgUnit = "%";
   ee1.SvgAttributes.Add(aa2);
   SvgAttribute aa3 = new SvgAttribute();
   aa3.Id = SvgAttributeCreator.FindNextSvgAttributeId();
   aa3.AttributeName = "height";
   aa3.AttributeValue = "100";
   aa3.AttributeType = LengthPercent;
   aa3.SvgUnit = "%";
   ee1.SvgAttributes.Add(aa3);

        SvgElement ee2 = new SvgElement();
        ee2.SvgParentElementId = ee1.Id;
        ee2.SvgParentElement = ee1;
        ee1.SvgElements.Add(ee2);
        ee2.ElementType = circle;
        ee2.Id = SvgElementCreator.FindNextSvgElementId();
        ee2.ElementOrder = 0;
        SvgAttribute aa4 = new SvgAttribute();
        aa4.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa4.AttributeName = "cx";
        aa4.AttributeValue = "100";
        aa4.AttributeType = LengthPercent;
        aa4.SvgUnit = "";
        ee2.SvgAttributes.Add(aa4);
        SvgAttribute aa5 = new SvgAttribute();
        aa5.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa5.AttributeName = "cy";
        aa5.AttributeValue = "100";
        aa5.AttributeType = LengthPercent;
        aa5.SvgUnit = "";
        ee2.SvgAttributes.Add(aa5);
        SvgAttribute aa6 = new SvgAttribute();
        aa6.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa6.AttributeName = "r";
        aa6.AttributeValue = "80";
        aa6.AttributeType = LengthPercent;
        aa6.SvgUnit = "%";
        ee2.SvgAttributes.Add(aa6);
        SvgAttribute aa7 = new SvgAttribute();
        aa7.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa7.AttributeName = "stroke";
        aa7.AttributeValue = "#afeeee";
        aa7.AttributeType = Color;
        aa7.SvgUnit = "px";
        SvgColor ce0 = new SvgColor();
        ce0.Id = SvgAttributeCreator.FindNextSvgColorId();
        ce0.ResetColor(cc.Color());
        ce0.ColorIndex = cc.ColorIndex);
        ce0.SvgAttributeId = aa7.id);
        ce0.SvgAttribute = aa7);
        ee2.SvgAttributes.Add(aa7);
        SvgAttribute aa8 = new SvgAttribute();
        aa8.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa8.AttributeName = "stroke-opacity";
        aa8.AttributeValue = "1.0";
        aa8.AttributeType = Value;
        aa8.SvgUnit = "px";
        ee2.SvgAttributes.Add(aa8);
        SvgAttribute aa9 = new SvgAttribute();
        aa9.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa9.AttributeName = "stroke-width";
        aa9.AttributeValue = "1";
        aa9.AttributeType = LengthPercent;
        aa9.SvgUnit = "px";
        ee2.SvgAttributes.Add(aa9);
        SvgAttribute aa10 = new SvgAttribute();
        aa10.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa10.AttributeName = "fill";
        aa10.AttributeValue = "#4169e1";
        aa10.AttributeType = Color;
        aa10.SvgUnit = "px";
        SvgColor ce0 = new SvgColor();
        ce0.Id = SvgAttributeCreator.FindNextSvgColorId();
        ce0.ResetColor(cc.Color());
        ce0.ColorIndex = cc.ColorIndex);
        ce0.SvgAttributeId = aa10.id);
        ce0.SvgAttribute = aa10);
        ee2.SvgAttributes.Add(aa10);
        SvgAttribute aa11 = new SvgAttribute();
        aa11.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa11.AttributeName = "fill-opacity";
        aa11.AttributeValue = "1.0";
        aa11.AttributeType = Value;
        aa11.SvgUnit = "px";
        ee2.SvgAttributes.Add(aa11);

        SvgElement ee3 = new SvgElement();
        ee3.SvgParentElementId = ee2.Id;
        ee3.SvgParentElement = ee2;
        ee2.SvgElements.Add(ee3);
        ee3.ElementType = ellipse;
        ee3.Id = SvgElementCreator.FindNextSvgElementId();
        ee3.ElementOrder = 0;
        SvgAttribute aa12 = new SvgAttribute();
        aa12.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa12.AttributeName = "cx";
        aa12.AttributeValue = "400";
        aa12.AttributeType = LengthPercent;
        aa12.SvgUnit = "";
        ee3.SvgAttributes.Add(aa12);
        SvgAttribute aa13 = new SvgAttribute();
        aa13.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa13.AttributeName = "cy";
        aa13.AttributeValue = "550";
        aa13.AttributeType = LengthPercent;
        aa13.SvgUnit = "";
        ee3.SvgAttributes.Add(aa13);
        SvgAttribute aa14 = new SvgAttribute();
        aa14.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa14.AttributeName = "rx";
        aa14.AttributeValue = "300";
        aa14.AttributeType = LengthPercent;
        aa14.SvgUnit = "";
        ee3.SvgAttributes.Add(aa14);
        SvgAttribute aa15 = new SvgAttribute();
        aa15.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa15.AttributeName = "ry";
        aa15.AttributeValue = "100";
        aa15.AttributeType = LengthPercent;
        aa15.SvgUnit = "px";
        ee3.SvgAttributes.Add(aa15);
        SvgAttribute aa16 = new SvgAttribute();
        aa16.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa16.AttributeName = "stroke";
        aa16.AttributeValue = "#0000ff";
        aa16.AttributeType = Color;
        aa16.SvgUnit = "px";
        SvgColor ce0 = new SvgColor();
        ce0.Id = SvgAttributeCreator.FindNextSvgColorId();
        ce0.ResetColor(cc.Color());
        ce0.ColorIndex = cc.ColorIndex);
        ce0.SvgAttributeId = aa16.id);
        ce0.SvgAttribute = aa16);
        ee3.SvgAttributes.Add(aa16);
        SvgAttribute aa17 = new SvgAttribute();
        aa17.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa17.AttributeName = "stroke-opacity";
        aa17.AttributeValue = "1.0";
        aa17.AttributeType = Value;
        aa17.SvgUnit = "px";
        ee3.SvgAttributes.Add(aa17);
        SvgAttribute aa18 = new SvgAttribute();
        aa18.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa18.AttributeName = "stroke-width";
        aa18.AttributeValue = "1";
        aa18.AttributeType = LengthPercent;
        aa18.SvgUnit = "px";
        ee3.SvgAttributes.Add(aa18);
        SvgAttribute aa19 = new SvgAttribute();
        aa19.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa19.AttributeName = "fill";
        aa19.AttributeValue = "#8b008b";
        aa19.AttributeType = Color;
        aa19.SvgUnit = "px";
        SvgColor ce0 = new SvgColor();
        ce0.Id = SvgAttributeCreator.FindNextSvgColorId();
        ce0.ResetColor(cc.Color());
        ce0.ColorIndex = cc.ColorIndex);
        ce0.SvgAttributeId = aa19.id);
        ce0.SvgAttribute = aa19);
        ee3.SvgAttributes.Add(aa19);
        SvgAttribute aa20 = new SvgAttribute();
        aa20.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa20.AttributeName = "fill-opacity";
        aa20.AttributeValue = "1.0";
        aa20.AttributeType = Value;
        aa20.SvgUnit = "px";
        ee3.SvgAttributes.Add(aa20);

        SvgElement ee4 = new SvgElement();
        ee4.SvgParentElementId = ee3.Id;
        ee4.SvgParentElement = ee3;
        ee3.SvgElements.Add(ee4);
        ee4.ElementType = rect;
        ee4.Id = SvgElementCreator.FindNextSvgElementId();
        ee4.ElementOrder = 0;
        SvgAttribute aa21 = new SvgAttribute();
        aa21.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa21.AttributeName = "id";
        aa21.AttributeValue = "";
        aa21.AttributeType = Value;
        aa21.SvgUnit = "px";
        ee4.SvgAttributes.Add(aa21);
        SvgAttribute aa22 = new SvgAttribute();
        aa22.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa22.AttributeName = "x";
        aa22.AttributeValue = "0";
        aa22.AttributeType = LengthPercent;
        aa22.SvgUnit = "";
        ee4.SvgAttributes.Add(aa22);
        SvgAttribute aa23 = new SvgAttribute();
        aa23.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa23.AttributeName = "y";
        aa23.AttributeValue = "0";
        aa23.AttributeType = LengthPercent;
        aa23.SvgUnit = "";
        ee4.SvgAttributes.Add(aa23);
        SvgAttribute aa24 = new SvgAttribute();
        aa24.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa24.AttributeName = "width";
        aa24.AttributeValue = "0";
        aa24.AttributeType = LengthPercent;
        aa24.SvgUnit = "";
        ee4.SvgAttributes.Add(aa24);
        SvgAttribute aa25 = new SvgAttribute();
        aa25.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa25.AttributeName = "height";
        aa25.AttributeValue = "0";
        aa25.AttributeType = LengthPercent;
        aa25.SvgUnit = "";
        ee4.SvgAttributes.Add(aa25);
        SvgAttribute aa26 = new SvgAttribute();
        aa26.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa26.AttributeName = "stroke";
        aa26.AttributeValue = "#000000";
        aa26.AttributeType = Color;
        aa26.SvgUnit = "px";
        SvgColor ce0 = new SvgColor();
        ce0.Id = SvgAttributeCreator.FindNextSvgColorId();
        ce0.ResetColor(cc.Color());
        ce0.ColorIndex = cc.ColorIndex);
        ce0.SvgAttributeId = aa26.id);
        ce0.SvgAttribute = aa26);
        ee4.SvgAttributes.Add(aa26);
        SvgAttribute aa27 = new SvgAttribute();
        aa27.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa27.AttributeName = "stroke-opacity";
        aa27.AttributeValue = "1.0";
        aa27.AttributeType = Value;
        aa27.SvgUnit = "px";
        ee4.SvgAttributes.Add(aa27);
        SvgAttribute aa28 = new SvgAttribute();
        aa28.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa28.AttributeName = "stroke-width";
        aa28.AttributeValue = "1";
        aa28.AttributeType = LengthPercent;
        aa28.SvgUnit = "";
        ee4.SvgAttributes.Add(aa28);
        SvgAttribute aa29 = new SvgAttribute();
        aa29.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa29.AttributeName = "fill";
        aa29.AttributeValue = "#000000";
        aa29.AttributeType = Color;
        aa29.SvgUnit = "px";
        SvgColor ce0 = new SvgColor();
        ce0.Id = SvgAttributeCreator.FindNextSvgColorId();
        ce0.ResetColor(cc.Color());
        ce0.ColorIndex = cc.ColorIndex);
        ce0.SvgAttributeId = aa29.id);
        ce0.SvgAttribute = aa29);
        ee4.SvgAttributes.Add(aa29);
        SvgAttribute aa30 = new SvgAttribute();
        aa30.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa30.AttributeName = "fill-opacity";
        aa30.AttributeValue = "1.0";
        aa30.AttributeType = Value;
        aa30.SvgUnit = "px";
        ee4.SvgAttributes.Add(aa30);

        SvgElement ee5 = new SvgElement();
        ee5.SvgParentElementId = ee4.Id;
        ee5.SvgParentElement = ee4;
        ee4.SvgElements.Add(ee5);
        ee5.SvgElementName = "femorph";
        ee5.ElementType = filter;
        ee5.Id = SvgElementCreator.FindNextSvgElementId();
        ee5.ElementOrder = 0;
        SvgAttribute aa31 = new SvgAttribute();
        aa31.Id = SvgAttributeCreator.FindNextSvgAttributeId();
        aa31.AttributeName = "id";
        aa31.AttributeValue = "femorph";
        aa31.AttributeType = Value;
        aa31.SvgUnit = "px";
        ee5.SvgAttributes.Add(aa31);

             SvgElement ee6 = new SvgElement();
             ee6.SvgParentElementId = ee5.Id;
             ee6.SvgParentElement = ee5;
             ee5.SvgElements.Add(ee6);
             ee6.ElementType = feMorphology;
             ee6.Id = SvgElementCreator.FindNextSvgElementId();
             ee6.ElementOrder = 0;
             SvgAttribute aa32 = new SvgAttribute();
             aa32.Id = SvgAttributeCreator.FindNextSvgAttributeId();
             aa32.AttributeName = "operator";
             aa32.AttributeValue = "erode";
             aa32.AttributeType = Text;
             aa32.SvgUnit = "px";
             ee6.SvgAttributes.Add(aa32);
             SvgAttribute aa33 = new SvgAttribute();
             aa33.Id = SvgAttributeCreator.FindNextSvgAttributeId();
             aa33.AttributeName = "radius";
             aa33.AttributeValue = "0";
             aa33.SecondaryValue = "none";
             aa33.AttributeType = Value;
             aa33.SvgUnit = "px";
             ee6.SvgAttributes.Add(aa33);

     }
}
