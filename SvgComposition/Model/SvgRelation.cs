using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SvgComposition.Model
{
   public class SvgRelation
    {
        public int Id { get; set; }
        public Group SvgGroup { get; set; }

        public int? SvgGroupId { get; set; }
        
        public Group SvgChild { get; set; }
    }
}
