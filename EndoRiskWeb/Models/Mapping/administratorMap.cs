using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class administratorMap : EntityTypeConfiguration<administrator>
    {
        public administratorMap()
        {
            // Primary Key
            this.HasKey(t => t.idAdmin);

            // Properties
            this.Property(t => t.email)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.password)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.firstname)
                .HasMaxLength(50);

            this.Property(t => t.lastname)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("administrator", "endorisk");
            this.Property(t => t.idAdmin).HasColumnName("idAdmin");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.password).HasColumnName("password");
            this.Property(t => t.firstname).HasColumnName("firstname");
            this.Property(t => t.lastname).HasColumnName("lastname");
            this.Property(t => t.subadmin).HasColumnName("subadmin");
        }
    }
}
