using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class endochoiceMap : EntityTypeConfiguration<endochoice>
    {
        public endochoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.idChoice);

            // Properties
            this.Property(t => t.choiceSet)
                .HasMaxLength(5);

            this.Property(t => t.choiceOption)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("endochoices", "endorisk");
            this.Property(t => t.idChoice).HasColumnName("idChoice");
            this.Property(t => t.choiceSet).HasColumnName("choiceSet");
            this.Property(t => t.choiceOption).HasColumnName("choiceOption");
        }
    }
}
