using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgComposition.Data
{
    class LenPerUnits : ObservableCollection<string>
    {
        public LenPerUnits()
        {
            Add("em"); // The default font size - usually the height of a character.
            Add("ex"); // The height of the character x
            Add("px"); // Pixels
            Add("pt"); // Points (1 / 72 of an inch)
            Add("pc"); // Picas (1 / 6 of an inch)
            Add("cm"); // Centimeters
            Add("mm"); // Millimeters
            Add("in"); // Inches
            Add("%");
        }

    }

}
