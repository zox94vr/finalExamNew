using FinalExamNew.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<FinalExamNewUser>
    {
        public void Configure(EntityTypeBuilder<FinalExamNewUser> builder)
        {
            builder.HasKey(k => k.Id);
            builder.ToTable("AspNetUsers");

            builder.HasMany(k => k.Oglasi).WithOne(k=>k.User);
        }
    }
}
