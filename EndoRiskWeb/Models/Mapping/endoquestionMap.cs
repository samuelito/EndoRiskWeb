using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class endoquestionMap : EntityTypeConfiguration<endoquestion>
    {
        public endoquestionMap()
        {
            // Primary Key
            this.HasKey(t => t.idQuestion);

            // Properties
            this.Property(t => t.endoQuestion1)
                .HasMaxLength(255);

            this.Property(t => t.abbr)
                .HasMaxLength(5);

            this.Property(t => t.choiceSet)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("endoquestion", "endorisk");
            this.Property(t => t.idQuestion).HasColumnName("idQuestion");
            this.Property(t => t.endoQuestion1).HasColumnName("endoQuestion");
            this.Property(t => t.abbr).HasColumnName("abbr");
            this.Property(t => t.choiceSet).HasColumnName("choiceSet");
        }
    }
}
