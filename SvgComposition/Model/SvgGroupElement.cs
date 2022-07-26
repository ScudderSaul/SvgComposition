using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SvgComposition.Model
{
    public class SvgGroupElement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string SvgId { get; set; }

        public virtual ICollection<SvgGroupElement> SvgGroupElements
        {
            get;
            set;
        }

       
    }
}