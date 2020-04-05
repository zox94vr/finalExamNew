using FinalExamNew.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Data.Configuration
{
    public class KljucneReciOglasaConfiguration : IEntityTypeConfiguration<KljucneReciOglasa>
    {
        public void Configure(EntityTypeBuilder<KljucneReciOglasa> builder)
        {
            builder.HasKey(kro => kro.KljucneReciOglasaId);
            builder.ToTable("KljucneReciOglasa");

            builder.HasOne(kro => kro.Oglas).WithMany(o => o.KljucneReciOglasa);
            builder.HasOne(kro => kro.KljucnaRec).WithMany(kr => kr.KljucneReciOglasa);
        }
    }
}
