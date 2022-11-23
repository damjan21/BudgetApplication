using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Constants;
using Core.Domain.Entites.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructures.Identity.EntityFramework.Data
{
    public class IdentityDbContextCustom : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

        public IdentityDbContextCustom(DbContextOptions<IdentityDbContextCustom> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContextCustom).Assembly);

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = Guid.Parse("854c5246-3d55-45f7-abfe-aea30efc5e28"),
                    Name = ApplicationUserRole.ADMIN,
                    NormalizedName = ApplicationUserRole.ADMIN
                }
            );

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = Guid.Parse("5310feb4-a1e1-4439-b511-fd2293f33af0"),
                    Name = ApplicationUserRole.CUSTOMER,
                    NormalizedName = ApplicationUserRole.CUSTOMER
                }
            );
        }
    }
}
