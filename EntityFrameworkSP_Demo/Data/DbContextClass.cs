using EntityFrameworkSP_Demo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkSP_Demo.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {   
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<ImageUpload> ImageUploads { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Order> Order { get; set; }
        
    }
}
