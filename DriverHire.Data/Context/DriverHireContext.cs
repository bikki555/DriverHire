using DriverHire.Entity;
using DriverHire.Entity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DriverHire.Data.Context
{
    public partial class DriverHireContext : IdentityDbContext<IdentityUser,
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
            //if many then can moved to separate class//
            //driverform and applicationuser//
            //modelBuilder.Entity<DriverForm>().HasOne(df=>df.ApplicationUser)
            //    .WithOne(au => au.DriverForm)
            //    .HasForeignKey<ApplicationUser>(df =>df.Id)
            //    .OnDelete(DeleteBehavior.NoAction);
        }

        //all table should be defined here test table demo
        #region DbSet

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Booking> Booking { get; set; }

            public DbSet<DriverForm> DriverForm { get; set; }
            public DbSet<Register> Register { get; set; }
            
        #endregion
    }

    }

