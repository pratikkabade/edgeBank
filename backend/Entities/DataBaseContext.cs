using System;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Users> User { get; set; }

        // GIVING PREDIFINED DATA TO DATABASE 
        // CREATING ADMIN USER
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1, Email = "admin@gmail.com", Password = "Passcode1", Role = Roles.Admin }
                );
        }

    }
}
