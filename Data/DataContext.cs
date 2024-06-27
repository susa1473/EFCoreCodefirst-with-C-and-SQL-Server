using Microsoft.EntityFrameworkCore;
using EFCoreCodefirst.Models;

namespace EFCoreCodefirst.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Address> adresses {  get; set; }
        public DbSet<Person> persons { get; set; }

    }
}
