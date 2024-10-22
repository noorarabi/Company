using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Company.Models;

namespace Company.Data
{
        public class CompanyDbContext : IdentityDbContext
        {
            public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
            {

            }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Company.Models.Menu> Menu { get; set; } = default!;
            public DbSet<Company.Models.Slider> Slider { get; set; } = default!;
            public DbSet<Train>Trains { get; set; }
            public DbSet<Cv> Cvs { get; set; }
    }

    }

