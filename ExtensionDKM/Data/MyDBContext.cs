using Microsoft.EntityFrameworkCore;
using ExtensionDKM.Models;

namespace ExtensionDKM.Data
{
    public class MyDBContext:DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
