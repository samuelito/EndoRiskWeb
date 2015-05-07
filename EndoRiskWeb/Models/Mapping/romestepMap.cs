using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class romestepMap : EntityTypeConfiguration<romestep>
    {
         public romestepMap()
        {
            // Primary Key
            this.HasKey(t => t.idStep);

            // Properties
            this.Property(t => t.disease3)
                .HasMaxLength(50);

            this.Property(t => t.method)
                .HasMaxLength(50);

            

            // Table & Column Mappings
            this.ToTable("romesteps", "endorisk");
            this.Property(t => t.idStep).HasColumnName("idStep");
            this.Property(t => t.steps).HasColumnName("steps");
            this.Property(t => t.disease3).HasColumnName("disease");
            this.Property(t => t.method).HasColumnName("method");
            this.Property(t => t.quantityN).HasColumnName("quantityN");
        }
    }
}