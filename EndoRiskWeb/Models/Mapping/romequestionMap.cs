using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class romequestionMap : EntityTypeConfiguration<romequestion>
    {
        public romequestionMap()
        {
            // Primary Key
            this.HasKey(t => t.idRomeQuestion);

            // Properties
            this.Property(t => t.romeQuestion1)
                .HasMaxLength(255);

            this.Property(t => t.romeChoice)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("romequestion", "endorisk");
            this.Property(t => t.idRomeQuestion).HasColumnName("idRomeQuestion");
            this.Property(t => t.romeQuestion1).HasColumnName("romeQuestion");
            this.Property(t => t.romeChoice).HasColumnName("romeChoice");
        }
    }
}
