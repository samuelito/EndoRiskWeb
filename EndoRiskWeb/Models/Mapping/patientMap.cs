using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class patientMap : EntityTypeConfiguration<patient>
    {
        public patientMap()
        {
            // Primary Key
            this.HasKey(t => new { t.idQuiz, t.idPatient });

            // Properties
            this.Property(t => t.idQuiz)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.idPatient)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.verified)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("patients", "endorisk");
            this.Property(t => t.idQuiz).HasColumnName("idQuiz");
            this.Property(t => t.idPatient).HasColumnName("idPatient");
            this.Property(t => t.risk).HasColumnName("risk");
            this.Property(t => t.verified).HasColumnName("verified");
            this.Property(t => t.time).HasColumnName("time");
        }
    }
}
