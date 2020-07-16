using mapp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace mapp.api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<Users> User{ get; set; }
    }
}