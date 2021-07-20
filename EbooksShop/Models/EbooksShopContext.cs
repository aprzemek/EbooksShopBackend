using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EbooksShop.Models
{
    public class EbooksShopContext : DbContext 
    {

        public EbooksShopContext(DbContextOptions<EbooksShopContext> options) 
            : base (options)
        { 
        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
