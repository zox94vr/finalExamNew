using FinalExamNew.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Data.Configuration
{
    public class TipOglasaConfiguration : IEntityTypeConfiguration<TipOglasa>
    {
        public void Configure(EntityTypeBuilder<TipOglasa> builder)
        {
            builder.HasKey(t => t.TipOglasaId);
            builder.ToTable("TipOglasa");

            builder.HasOne(t => t.Cena).WithMany(m=>m.TipoviOglasa);
            builder.HasMany(t => t.Oglasavanja).WithOne(o => o.TipOglasa);
        }
    }
}
