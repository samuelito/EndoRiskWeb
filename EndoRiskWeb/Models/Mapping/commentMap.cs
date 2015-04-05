using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class commentMap : EntityTypeConfiguration<comment>
    {
        public commentMap()
        {
            // Primary Key
            this.HasKey(t => t.idComment);

            // Properties
            this.Property(t => t.title)
                .HasMaxLength(45);

            this.Property(t => t.content)
                .HasMaxLength(255);

            this.Property(t => t.email)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("comment", "endorisk");
            this.Property(t => t.idComment).HasColumnName("idComment");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.content).HasColumnName("content");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.time).HasColumnName("time");
        }
    }
}
