using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class romequestionnaireMap : EntityTypeConfiguration<romequestionnaire>
    {
        public romequestionnaireMap()
        {
            // Primary Key
            this.HasKey(t => new { t.idRomeQuiz, t.idPatient, t.idQuiz });

            // Properties
            this.Property(t => t.idRomeQuiz)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.idPatient)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.idQuiz)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("romequestionnaires", "endorisk");
            this.Property(t => t.idRomeQuiz).HasColumnName("idRomeQuiz");
            this.Property(t => t.idPatient).HasColumnName("idPatient");
            this.Property(t => t.idQuiz).HasColumnName("idQuiz");
        }
    }
}
