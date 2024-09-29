using AuthServer.Core.Entitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Data
{
    //IdentityUser kullandıgımızdan dolayi 2 farklı tablo olacak
    //bunu engellememiz lazım. Bu sebeple DbContext den değil, IdentityContext den miras aldık

    //ilgili IdentityUser class, Default gelen role, ve PK ları hangi tip tutmasi icin parametre
    public class AppDbContext : IdentityDbContext<UserApp, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        //UserApp kendisi yapıyor, Identity oldugu icin

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Configuration dosyalarındaki IEntitytypeConfiguration interfaceleri arayıp
            //otomatik olarak calistiracak, kendi dizinindeki assembly leri bulcak
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);


            base.OnModelCreating(builder);
        }
    }
}
