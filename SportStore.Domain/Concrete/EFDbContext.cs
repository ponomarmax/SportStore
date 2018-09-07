using SportStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("SportStore") { Database.SetInitializer(new DbInitializer()); }
        public DbSet<Product> Products { get; set; }
    }
}
