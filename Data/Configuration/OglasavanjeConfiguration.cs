using FinalExamNew.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Data.Configuration
{
    public class OglasavanjeConfiguration : IEntityTypeConfiguration<Oglasavanje>
    {
        public void Configure(EntityTypeBuilder<Oglasavanje> builder)
        {
            builder.HasKey(o => o.OglasId);
            builder.ToTable("Oglasavanje");

            builder.HasOne(o => o.Oglas).WithMany(o => o.Oglasavanja);
            builder.HasOne(o => o.TipOglasa).WithMany(t => t.Oglasavanja);
        }
    }
}
