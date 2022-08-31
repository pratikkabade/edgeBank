namespace BackendAPI.Helpers;

using Microsoft.EntityFrameworkCore;
using BackendAPI.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<User> Users { get; set; }
}