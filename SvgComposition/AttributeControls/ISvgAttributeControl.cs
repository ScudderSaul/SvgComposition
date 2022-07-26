using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvgComposition.Model;

namespace SvgComposition.AttributeControls
{
    public interface ISvgAttributeControl
    {
        string SvgAttributeCurrentValue { get; set; }
        string LabelText { get; set; }

        bool CanAnimate { get; set; }

        SvgAttribute Create(ref SvgElement ele);
        void Load(SvgAttribute at);

        void Save(SvgAttribute at);

    }
}
