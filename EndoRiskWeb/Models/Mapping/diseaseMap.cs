using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class diseaseMap : EntityTypeConfiguration<disease>
    {
        public diseaseMap()
        {
            // Primary Key
            this.HasKey(t => t.idDiseases);

            // Properties
            this.Property(t => t.disease1)
                .HasMaxLength(50);

            this.Property(t => t.criteria)
                .HasMaxLength(3);

            this.Property(t => t.comparedValue)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("diseases", "endorisk");
            this.Property(t => t.idDiseases).HasColumnName("idDiseases");
            this.Property(t => t.disease1).HasColumnName("disease");
            this.Property(t => t.idRomeQuestion).HasColumnName("idRomeQuestion");
            this.Property(t => t.criteria).HasColumnName("criteria");
            this.Property(t => t.comparedValue).HasColumnName("comparedValue");
        }
    }
}
