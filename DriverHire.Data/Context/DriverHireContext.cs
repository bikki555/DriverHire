using DriverHire.Entity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DriverHire.Data.Context
{
    public partial class DriverHireContext : IdentityDbContext<ApplicationUser,
                                                        ApplicationRole,
                                                        string,
                                                        IdentityUserClaim<string>,
                                                        IdentityUserRole<string>,
                                                        IdentityUserLogin<string>,
                                                        IdentityRoleClaim<string>,
                                                        IdentityUserToken<string>>
    {
        public DriverHireContext(DbContextOptions<DriverHireContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //creating table configurations//
        }

            //all table should be defined here test table demo
            #region DbSet
            
            public DbSet<Booking> Booking { get; set; }

            public DbSet<DriverForm> DriverForm { get; set; }
            #endregion
        }

    }

