using FinalExamNew.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Data.Configuration
{
    public class CenaConfiguration : IEntityTypeConfiguration<Cena>
    {
        public void Configure(EntityTypeBuilder<Cena> builder)
        {
            builder.HasKey(k => k.CenaId);
            builder.ToTable("Cena");
            builder.HasMany(m => m.Oglasi).WithOne(con => con.Cena); 
            builder.HasMany(m => m.TipoviOglasa).WithOne(m => m.Cena);
        }
    }
}
