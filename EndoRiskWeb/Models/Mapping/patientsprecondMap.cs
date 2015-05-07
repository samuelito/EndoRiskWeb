using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class patientsprecondMap : EntityTypeConfiguration<patientsprecond>
    {
        public patientsprecondMap()
        {
            // Primary Key
            this.HasKey(t => new { t.idCondition });

            // Properties
            // this.Property(t => t.idQuiz)
            //     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.idCondition)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.preCondition)
                .HasMaxLength(255);

            this.Property(t => t.preAbbr)
                .HasMaxLength(10);


            this.Property(t => t.haspreCond)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("patientsprecond", "endorisk");
            this.Property(t => t.idCondition).HasColumnName("idCondition");
            this.Property(t => t.idQuiz).HasColumnName("idQuiz");
            this.Property(t => t.preCondition).HasColumnName("preCondition");
            this.Property(t => t.preAbbr).HasColumnName("preAbbr");
            this.Property(t => t.haspreCond).HasColumnName("haspreCond");
        }
    }
}
