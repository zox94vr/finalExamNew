using System;
using FinalExamNew.Areas.Identity.Data;
using FinalExamNew.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FinalExamNew.Areas.Identity.IdentityHostingStartup))]
namespace FinalExamNew.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<FinalExamNewContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("FinalExamNewContextConnection")));

                services.AddDefaultIdentity<FinalExamNewUser>()
                    .AddEntityFrameworkStores<FinalExamNewContext>();
            });
        }
    }
}