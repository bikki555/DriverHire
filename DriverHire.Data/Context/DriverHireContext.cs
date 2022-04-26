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
            modelBuilder.Entity<DriverForm>().HasOne(df => df.ApplicationUser)
                .WithOne(au => au.DriverForm)
                .HasForeignKey<DriverForm>(df => df.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //booking and applicationuser//
            modelBuilder.Entity<Booking>().HasOne(bo => bo.Customer)
               .WithMany(au => au.Customers)
                .HasForeignKey(bo => bo.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Booking>().HasOne(bo => bo.Driver)
               .WithMany(au => au.Drivers)
                .HasForeignKey(bo => bo.DriverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>().HasOne(bo => bo.CancelBy)
               .WithMany(au => au.CancelBys)
                .HasForeignKey(bo => bo.CancelById)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeedBack>().HasOne(fb => fb.Booking)
              .WithMany(bo => bo.FeedBacks)
               .HasForeignKey(bo => bo.BookingId)
               .OnDelete(DeleteBehavior.NoAction);


        }

        //all table should be defined here test table demo
        #region DbSet

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Booking> Booking { get; set; }

        public DbSet<DriverForm> DriverForm { get; set; }
        public DbSet<Register> Register { get; set; }

        public DbSet<Payment> Payment { get; set; }
        public DbSet<FeedBack> FeedBack { get; set; }

        #endregion
    }

}

