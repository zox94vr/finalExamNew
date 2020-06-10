using FinalExamNew.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Data.Configuration
{
    public class OglasConfiguration : IEntityTypeConfiguration<Oglas>
    {
        public void Configure(EntityTypeBuilder<Oglas> builder)
        {
            builder.HasKey(o => o.OglasId);
            builder.ToTable("Oglas");
            //builder.Ignore(m => m.CenaView);
            //builder.Ignore(m => m.DatumDoView);
            //builder.Ignore(m => m.DatumOdView);
            //builder.Ignore(m => m.KljucneReciView);
            //builder.Ignore(m => m.NaslovView);
            //builder.Ignore(m => m.TekstView);
            //builder.Ignore(m => m.TipoviOglasaView);
            //builder.Ignore(m => m.UploadFilesView);


            builder.HasMany(o => o.KljucneReciOglasa).WithOne(kro => kro.Oglas);
            builder.HasMany(o => o.Slike).WithOne(s => s.Oglas);
            builder.HasMany(o => o.Oglasavanja).WithOne(obj => obj.Oglas);
            builder.HasOne(o => o.User).WithMany(n => n.Oglasi).HasForeignKey(m=>m.UserId);
            builder.HasOne(o => o.Cena).WithMany(m=>m.Oglasi);
        }
    }
}
