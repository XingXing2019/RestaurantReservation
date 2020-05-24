using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace T3RMSWS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sitting> Sittings { get; set; }
        public DbSet<ReservationRequest> ReservationRequests { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<ReservationDate> ReservationDates { get; set; }
        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ReservationRequest>()
                .HasOne(x => x.Table)
                .WithOne(x => x.Reservation)
                .HasForeignKey<ReservationRequest>(x => x.TableId);

            builder.Entity<Table>()
                .HasOne(x => x.Reservation)
                .WithOne(x => x.Table)
                .HasForeignKey<Table>(x => x.ReservationId);
        }
    }
}
