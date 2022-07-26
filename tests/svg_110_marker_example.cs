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
public void CreateElement8852()
{
SvgElement el1 = CreateAnElement(true, el0, "marker example", "", "svg",0);
SvgAttribute at1 = CreateAnAttribute(el1, "id", "marker example", "", SvgAttributeType.Value, "px");
SvgAttribute at2 = CreateAnAttribute(el1, "viewBox", "0, 0, 100, 100", "", SvgAttributeType.Value, "px");
SvgAttribute at3 = CreateAnAttribute(el1, "width", "400", "", SvgAttributeType.LengthPercent, "");
SvgAttribute at4 = CreateAnAttribute(el1, "height", "400", "", SvgAttributeType.LengthPercent, "");
SvgElement el2 = CreateAnElement(false, el1, "", "", "defs",0);
SvgElement el3 = CreateAnElement(false, el2, "arrow", "", "marker",0);
SvgAttribute at5 = CreateAnAttribute(el3, "markerHeight", "6", "", SvgAttributeType.LengthPercent, "");
SvgAttribute at6 = CreateAnAttribute(el3, "markerWidth", "6", "", SvgAttributeType.LengthPercent, "");
SvgAttribute at7 = CreateAnAttribute(el3, "orient", "auto-start-reverse", "", SvgAttributeType.Text, "px");
SvgAttribute at8 = CreateAnAttribute(el3, "refX", "5", "", SvgAttributeType.Value, "px");
SvgAttribute at9 = CreateAnAttribute(el3, "refY", "5", "", SvgAttributeType.Value, "px");
SvgAttribute at10 = CreateAnAttribute(el3, "viewBox", "0, 0, 10, 10", "", SvgAttributeType.Value, "px");
SvgAttribute at11 = CreateAnAttribute(el3, "id", "arrow", "", SvgAttributeType.Value, "px");
SvgElement el4 = CreateAnElement(false, el3, "", "", "path",0);
SvgAttribute at12 = CreateAnAttribute(el4, "d", " M 0 0 L 10 5 L 0 10 z", "", SvgAttributeType.Value, "px");
SvgAttribute at13 = CreateAnAttribute(el4, "id", "", "", SvgAttributeType.Value, "px");
SvgElement el5 = CreateAnElement(false, el2, "dot", "", "marker",0);
SvgAttribute at14 = CreateAnAttribute(el5, "markerHeight", "5", "", SvgAttributeType.LengthPercent, "");
SvgAttribute at15 = CreateAnAttribute(el5, "markerWidth", "5", "", SvgAttributeType.LengthPercent, "");
SvgAttribute at16 = CreateAnAttribute(el5, "refX", "5", "", SvgAttributeType.Value, "px");
SvgAttribute at17 = CreateAnAttribute(el5, "refY", "5", "", SvgAttributeType.Value, "px");
SvgAttribute at18 = CreateAnAttribute(el5, "viewBox", "0, 0, 10, 10", "", SvgAttributeType.Value, "px");
SvgAttribute at19 = CreateAnAttribute(el5, "id", "dot", "", SvgAttributeType.Value, "px");
SvgElement el6 = CreateAnElement(false, el5, "", "", "circle",0);
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
SvgElement el7 = CreateAnElement(false, el1, "", "", "polyline",0);
SvgAttribute at29 = CreateAnAttribute(el7, "points", " 10,10 10,90 90,90", "", SvgAttributeType.Value, "px");
SvgAttribute at30 = CreateAnAttribute(el7, "id", "", "", SvgAttributeType.Value, "px");
SvgAttribute at31 = CreateAnAttribute(el7, "fill", "none", "", SvgAttributeType.Text, "px");
CreateAColor(at31, 0, 255, 0, 0, 0);
SvgAttribute at32 = CreateAnAttribute(el7, "stroke", "#000000", "", SvgAttributeType.Color, "px");
CreateAColor(at32, 0, 255, 0, 0, 0);
SvgAttribute at33 = CreateAnAttribute(el7, "marker-start", "url(#arrow)", "", SvgAttributeType.Url, "px");
SvgAttribute at34 = CreateAnAttribute(el7, "marker-end", "url(#arrow)", "", SvgAttributeType.Url, "px");
SvgElement el8 = CreateAnElement(false, el1, "", "", "polyline",0);
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
}
