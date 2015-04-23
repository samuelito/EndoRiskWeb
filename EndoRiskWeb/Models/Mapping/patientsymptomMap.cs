using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class patientsymptomMap : EntityTypeConfiguration<patientsymptom>
    {
        public patientsymptomMap()
        {
            // Primary Key
            this.HasKey(t => t.idSymp);

            // Properties
           // this.Property(t => t.idQuiz)
           //     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.symptom)
                .HasMaxLength(50);

            this.Property(t => t.hasSymptom)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("patientsymptom", "endorisk");
            this.Property(t => t.idSymp).HasColumnName("idSymp");
            this.Property(t => t.idQuiz).HasColumnName("idQuiz");
            this.Property(t => t.symptom).HasColumnName("symptom");
            this.Property(t => t.hasSymptom).HasColumnName("hasSymptom");
        }
    }
}
