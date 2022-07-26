using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SvgComposition.Model
{

    [Table("SvgToolStorage")]
    public class SvgToolStorage
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime TimeSet { get; set; } = DateTime.Now;

        public SvgElement SavedElement { get; set; } = null;

    }
}