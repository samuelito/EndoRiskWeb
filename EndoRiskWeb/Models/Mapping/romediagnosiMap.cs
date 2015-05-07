using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class romediagnosiMap : EntityTypeConfiguration<romediagnosi>
    {
        public romediagnosiMap()
        {
            // Primary Key
            this.HasKey(t => t.idDiagnosis);

            // Properties
            this.Property(t => t.disease)
                .HasMaxLength(255);

            this.Property(t => t.diagnosis)
                .HasMaxLength(5000);

            this.Property(t => t.rome)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("romediagnosis", "endorisk");
            this.Property(t => t.idDiagnosis).HasColumnName("idDiagnosis");
            this.Property(t => t.disease).HasColumnName("disease");
            this.Property(t => t.diagnosis).HasColumnName("diagnosis");
            this.Property(t => t.rome).HasColumnName("rome");
        }
    }
}
