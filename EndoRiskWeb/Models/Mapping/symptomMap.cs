using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class symptomMap : EntityTypeConfiguration<symptom>
    {
        public symptomMap()
        {
            // Primary Key
            this.HasKey(t => new { t.idSymptom });

            // Properties
            this.Property(t => t.symptom1)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.abbr)
                .IsRequired()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("symptoms", "endorisk");
            this.Property(t => t.idSymptom).HasColumnName("idSymptom");
            this.Property(t => t.symptom1).HasColumnName("symptom");
            this.Property(t => t.abbr).HasColumnName("abbr");
        }
    }
}
