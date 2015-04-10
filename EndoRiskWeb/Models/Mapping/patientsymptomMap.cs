using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class patientsymptomMap : EntityTypeConfiguration<patientsymptom>
    {
        public patientsymptomMap()
        {
            // Primary Key
            this.HasKey(t => new { t.idPatient, t.symptom });

            // Properties
            this.Property(t => t.idPatient)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.symptom)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.hasSymptom)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("patientsymptom", "endorisk");
            this.Property(t => t.idPatient).HasColumnName("idPatient");
            this.Property(t => t.symptom).HasColumnName("symptom");
            this.Property(t => t.hasSymptom).HasColumnName("hasSymptom");
        }
    }
}
