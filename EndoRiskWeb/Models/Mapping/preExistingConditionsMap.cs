using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EndoRiskWeb.Models.Mapping
{
    public class preExistingConditionsMap : EntityTypeConfiguration<preExistingConditions>
    {
        public preExistingConditionsMap()
        {
            // Primary Key
            this.HasKey(t => new { idPreCond = t.idPreCond });

            // Properties
            this.Property(t => t.condition)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.abbr)
                .IsRequired()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("pre_existing_conditions", "endorisk");
            this.Property(t => t.idPreCond).HasColumnName("idPreCond");
            this.Property(t => t.condition).HasColumnName("condition");
            this.Property(t => t.abbr).HasColumnName("abbr");
        }
    }
}


