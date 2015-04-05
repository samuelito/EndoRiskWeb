using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class romechoiceMap : EntityTypeConfiguration<romechoice>
    {
        public romechoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.idRomeChoices);

            // Properties
            this.Property(t => t.romeChoice1)
                .HasMaxLength(45);

            this.Property(t => t.romeOption)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("romechoices", "endorisk");
            this.Property(t => t.idRomeChoices).HasColumnName("idRomeChoices");
            this.Property(t => t.romeChoice1).HasColumnName("romeChoice");
            this.Property(t => t.romeOption).HasColumnName("romeOption");
            this.Property(t => t.value).HasColumnName("value");
        }
    }
}
