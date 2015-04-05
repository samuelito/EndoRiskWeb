using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class romedependencyMap : EntityTypeConfiguration<romedependency>
    {
        public romedependencyMap()
        {
            // Primary Key
            this.HasKey(t => new { t.disease, t.preDisease });

            // Properties
            this.Property(t => t.disease)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.preDisease)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("romedependencies", "endorisk");
            this.Property(t => t.disease).HasColumnName("disease");
            this.Property(t => t.preDisease).HasColumnName("preDisease");
        }
    }
}
