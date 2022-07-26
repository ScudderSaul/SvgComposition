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
public void CreateElement6019()
{
SvgElement el1 = CreateAnElement(true, el0, "foofoo", "", "svg",0);
SvgAttribute at1 = CreateAnAttribute(el1, "id", "foofoo", "", SvgAttributeType.Value, "px");
SvgAttribute at2 = CreateAnAttribute(el1, "viewBox", "0, 0, 400, 400", "", SvgAttributeType.Value, "px");
SvgAttribute at3 = CreateAnAttribute(el1, "width", "400", "", SvgAttributeType.LengthPercent, "");
SvgAttribute at4 = CreateAnAttribute(el1, "height", "400", "", SvgAttributeType.LengthPercent, "");
SvgAttribute at5 = CreateAnAttribute(el1, "x", "100", "", SvgAttributeType.LengthPercent, "");
SvgAttribute at6 = CreateAnAttribute(el1, "y", "20", "", SvgAttributeType.LengthPercent, "");
SvgElement el2 = CreateAnElement(false, el1, "", "", "rect",0);
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
SvgElement el3 = CreateAnElement(false, el1, "rect2", "", "rect",0);
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
SvgElement el4 = CreateAnElement(false, el3, "", "", "animateMotion",0);
SvgAttribute at27 = CreateAnAttribute(el4, "path", "M 250,80 H 50 Q 30,80 30,50 Q 30,20 50,20 H 250 Q 280,20,280,50 Q 280,80,250,80Z", "", SvgAttributeType.Value, "px");
SvgAttribute at28 = CreateAnAttribute(el4, "dur", "3s", "", SvgAttributeType.Value, "px");
SvgAttribute at29 = CreateAnAttribute(el4, "repeatCount", "indefinite", "", SvgAttributeType.Text, "px");
SvgAttribute at30 = CreateAnAttribute(el4, "rotate", "auto", "", SvgAttributeType.Text, "px");
     }
}
