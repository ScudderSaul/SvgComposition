using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace SvgComposition.Model
{
    public enum SvgElementType
    {
        Path,
        Text,
        Rect,
        Circle,
        Ellipse,
        Line,
        Polyline,
        Polygon,
        Image,
        Use,
        Group,
        Svg,
    }

    [Table("SvgElement")]
    public class SvgElement 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string SvgElementName { get; set; } = string.Empty;

        public bool TopSvg { get; set; } = false;

        [Required]
        public string ElementType { get; set; } // the type like circle, or line

        public int ElementOrder { get; set; }

        public string Content { get; set; }  // text content

        public virtual ICollection<SvgAttribute> SvgAttributes { get; set; } = new HashSet<SvgAttribute>();

        public virtual ICollection<SvgElement> SvgElements { get; set; } = new HashSet<SvgElement>();


        public int? SvgParentElementId { get; set; }
        public virtual SvgElement SvgParentElement { get; set; }
      
    }
}