using System.ComponentModel.DataAnnotations.Schema;

namespace SvgComposition.Model
{
    public class AttributeListPart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int Index { get; set; }
        public int IntValue { get; set; }
        public int DoubleValue { get; set; }
        public int? SvgAttributeId { get; set; }
        public SvgAttribute SvgAttribute { get; set; }
    }
}