using FinalExamNew.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Data.Configuration
{
    public class KljucnaRecConfiguration : IEntityTypeConfiguration<KljucnaRec>
    {
        public void Configure(EntityTypeBuilder<KljucnaRec> builder)
        {
            builder.HasKey(k => k.KljucnaRecId);
            builder.ToTable("KljucnaRec");
            builder.HasMany(k => k.KljucneReciOglasa).WithOne(kr => kr.KljucnaRec);
        }
    }
}
