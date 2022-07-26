using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvgComposition.Model
{
    [Table("SvgDoc")]
    public class SvgDoc
    {
        public SvgDoc()
        {
            SvgElements = new HashSet<SvgElement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string SvgDocName { get; set; }

        public virtual ICollection<SvgElement> SvgElements
        {
            get;
            set;
        }
    }
}