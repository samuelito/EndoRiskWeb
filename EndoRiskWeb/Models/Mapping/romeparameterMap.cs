using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class romeparameterMap : EntityTypeConfiguration<romeparameter>
    {
        public romeparameterMap()
        {
            // Primary Key
            this.HasKey(t => t.idOrder);

            // Properties
            this.Property(t => t.disease4)
                .HasMaxLength(50);

            this.Property(t => t.que)
                .HasMaxLength(5);

            this.Property(t => t.cual)
                .HasMaxLength(50);



            // Table & Column Mappings
            this.ToTable("romeparameters", "endorisk");
            this.Property(t => t.idOrder).HasColumnName("idOrder");
            this.Property(t => t.step).HasColumnName("step");
            this.Property(t => t.disease4).HasColumnName("disease");
            this.Property(t => t.que).HasColumnName("que");
            this.Property(t => t.cual).HasColumnName("cual");
            this.Property(t => t.boolValue).HasColumnName("boolValue");
        }
    }
}