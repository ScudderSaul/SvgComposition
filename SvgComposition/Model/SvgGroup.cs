using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SvgComposition.Model
{

    public enum SvgGroupType
    {
    
        Group,
        Svg,
    }

    [Table("SvgGroup")]
    public class SvgGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string SvgGroupName { get; set; } = string.Empty;

        [Required]
        public SvgGroupType GroupType { get; set; }

        public virtual ICollection<SvgElement> SvgElements { get; set; } = new HashSet<SvgElement>();
        public int GroupOrder { get; set; }

        public int? SvgGroupId { get; set; }

        public SvgGroup SvgGroupUp { get; set; }

        public virtual ICollection<SvgGroup> SvgGroups { get; set; } = new HashSet<SvgGroup>();
    }
}