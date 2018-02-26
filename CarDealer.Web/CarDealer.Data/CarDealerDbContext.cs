namespace CarDealer.Data
{
    using CarDealer.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CarDealerDbContext : IdentityDbContext<User>
    {
        public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options)
            : base(options)
        { }

        public DbSet<Cutomer> Customers { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Part> Parts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Sale>()
                .HasOne(s => s.Car)
                .WithMany(sa => sa.Sales)
                .HasForeignKey(s => s.CarId);

            builder.Entity<Sale>()
                .HasOne(c => c.Cutomer)
                .WithMany(s => s.Sales)
                .HasForeignKey(s => s.CustomerId);

            builder.Entity<Part>()
                .HasOne(p => p.Supplier)
                .WithMany(p => p.Parts)
                .HasForeignKey(p => p.SupplierId);

            builder
                .Entity<PartCar>()
                .HasKey(pc => new
                {
                    pc.PartId,
                    pc.CarId
                });

            builder
                .Entity<PartCar>()
                .HasOne(pc => pc.Car)
                .WithMany(p => p.Parts)
                .HasForeignKey(p => p.CarId);

            builder
                .Entity<PartCar>()
                .HasOne(p => p.Part)
                .WithMany(pc => pc.Cars)
                .HasForeignKey(pc=>pc.PartId);

            base.OnModelCreating(builder);

        }
    }
}

