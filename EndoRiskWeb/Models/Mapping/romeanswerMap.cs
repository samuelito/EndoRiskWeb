using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class romeanswerMap : EntityTypeConfiguration<romeanswer>
    {
        public romeanswerMap()
        {
            // Primary Key
            this.HasKey(t => new { t.idRomeQuiz, t.idRomeQuestion });

            // Properties
            this.Property(t => t.idRomeQuiz)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.idRomeQuestion)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("romeanswers", "endorisk");
            this.Property(t => t.idRomeQuiz).HasColumnName("idRomeQuiz");
            this.Property(t => t.idRomeQuestion).HasColumnName("idRomeQuestion");
            this.Property(t => t.answer).HasColumnName("answer");
        }
    }
}
