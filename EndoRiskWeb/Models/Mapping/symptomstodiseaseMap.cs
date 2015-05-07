using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class symptomstodiseaseMap : EntityTypeConfiguration<symptomstodisease>
    {
        public symptomstodiseaseMap()
        {
            // Primary Key
            this.HasKey(t => new { t.idRelation });

            // Properties
            this.Property(t => t.symptom2)
                .HasMaxLength(50);

            this.Property(t => t.disease2)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("symptomstodisease", "endorisk");
            this.Property(t => t.idRelation).HasColumnName("idRelation");
            this.Property(t => t.symptom2).HasColumnName("symptom");
            this.Property(t => t.disease2).HasColumnName("disease");
        }
    }
}