using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class severityMap : EntityTypeConfiguration<severity>
    {
        public severityMap()
        {
            // Primary Key
            this.HasKey(t => t.idSeverity);

            // Properties
            this.Property(t => t.severity1)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("severity", "endorisk");
            this.Property(t => t.idSeverity).HasColumnName("idSeverity");
            this.Property(t => t.idQuiz).HasColumnName("idQuiz");
            this.Property(t => t.severity1).HasColumnName("severity");
        }
    }
}
