using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvgComposition.Model;

namespace SvgComposition.ElementControls
{
   public interface ISvgElementControl
    {
       string SvgElementTypeName { get; set; }

       void Create(ref SvgElement parent);

       void AddAttribute(string name);

       void Save(ref SvgElement ee);

       void Load(SvgElement ee);

       SvgElement SvgElementData { get; set; }

       ObservableCollection<string> PermitedChildElements { get; }

       void BuildPermittedContent();

       ObservableCollection<string> PossibleAttributes { get; }

       void BuildUnusedAttributeList();

    }
}
