using System.Data.Entity;

namespace SvgComposition.Model
{
    public partial class SvgModel : DbContext
    {
        public SvgModel() : base(SvgCompositionPackage.ConnectionOther)  // base(@"Server=(localdb)\mssqllocaldb;Database=CssScriptClasses.db;Trusted_Connection=True;")  // "CssScriptClasses.db"
        {
            Database.CreateIfNotExists();
        }

        public virtual DbSet<SvgToolStorage> SvgToolStorages { get; set; }
        public virtual DbSet<SvgColor> SvgColors { get; set; }
        public virtual DbSet<SvgAttribute> SvgAttributes { get; set; }
        public virtual DbSet<SvgElement> SvgElements { get; set; }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SvgElement>()
                .HasMany(e => e.SvgAttributes)
                .WithOptional(e => e.SvgElement)
                .HasForeignKey(e => e.SvgElementId);

            modelBuilder.Entity<SvgElement>()
                .HasMany(e => e.SvgElements)
                .WithOptional(e => e.SvgParentElement)
                .HasForeignKey(e => e.SvgParentElementId);

            modelBuilder.Entity<SvgAttribute>()
                .HasMany(e => e.SvgColors)
                .WithOptional(e => e.SvgAttribute)
                .HasForeignKey(e => e.SvgAttributeId);

        }

        

    }
}
