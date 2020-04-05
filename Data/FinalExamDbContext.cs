using FinalExamNew.Areas.Identity.Data;
using FinalExamNew.Dal;
using FinalExamNew.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Data
{
    public class FinalExamDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new KljucnaRecConfiguration());
            builder.ApplyConfiguration(new KljucneReciOglasaConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new OglasConfiguration());
            builder.ApplyConfiguration(new OglasavanjeConfiguration());
            builder.ApplyConfiguration(new SlikaConfiguration());
            builder.ApplyConfiguration(new TipOglasaConfiguration());
            //base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        //private readonly string _connectionString;
        //public string ConnectionString => _connectionString;
        public DbSet<KljucnaRec> KljucneReci { get; set; }
        public DbSet<KljucneReciOglasa> KljucneReciOglasa { get; set; }
        public DbSet<FinalExamNewUser> Korisnici { get; set; }
        public DbSet<Oglas> Oglasi { get; set; }
        public DbSet<Oglasavanje> Oglasavanje { get; set; }
        public DbSet<Slika> Slike { get; set; }
        public DbSet<TipOglasa> TipoviOglasa { get; set; }
        public DbSet<Cena> Cene { get; set; }
        public FinalExamDbContext(DbContextOptions<FinalExamDbContext> options) : base(options)
        { }

    }
}
