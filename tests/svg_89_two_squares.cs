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
public void CreateElement8641()
{
SvgElement el1 = CreateAnElement(true, el0, "two squares", "", "svg",0);
SvgAttribute at1 = CreateAnAttribute(el1, "id", "two squares", "", SvgAttributeType.Value, "px");
SvgAttribute at2 = CreateAnAttribute(el1, "width", "200", "", SvgAttributeType.LengthPercent, "");
SvgAttribute at3 = CreateAnAttribute(el1, "height", "200", "", SvgAttributeType.LengthPercent, "");
SvgElement el2 = CreateAnElement(false, el1, "", "", "defs",0);
SvgElement el3 = CreateAnElement(false, el2, "offset", "", "filter",0);
SvgAttribute at4 = CreateAnAttribute(el3, "id", "offset", "", SvgAttributeType.Value, "px");
SvgAttribute at5 = CreateAnAttribute(el3, "width", "180", "", SvgAttributeType.LengthPercent, "");
SvgAttribute at6 = CreateAnAttribute(el3, "height", "180", "", SvgAttributeType.LengthPercent, "");
SvgElement el4 = CreateAnElement(false, el3, "", "", "feOffset",0);
SvgAttribute at7 = CreateAnAttribute(el4, "in", "SourceGraphic", "", SvgAttributeType.Text, "px");
SvgAttribute at8 = CreateAnAttribute(el4, "dx", "60", "", SvgAttributeType.Value, "px");
SvgAttribute at9 = CreateAnAttribute(el4, "dy", "60", "", SvgAttributeType.Value, "px");
SvgElement el5 = CreateAnElement(false, el1, "", "", "rect",0);
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
SvgElement el6 = CreateAnElement(false, el1, "", "", "rect",0);
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
}
