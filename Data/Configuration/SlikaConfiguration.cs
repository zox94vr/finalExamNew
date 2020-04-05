using FinalExamNew.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Data.Configuration
{
    public class SlikaConfiguration : IEntityTypeConfiguration<Slika>
    {
        public void Configure(EntityTypeBuilder<Slika> builder)
        {
            builder.HasKey(s => new { s.SlikaId, s.OglasId });
            builder.ToTable("Slika");

            builder.HasOne(s => s.Oglas).WithMany(o => o.Slike);
        }
    }
}
