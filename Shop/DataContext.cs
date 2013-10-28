using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Shop.DataBase;

namespace Shop
{
    public class DataContext : DbContext
    {
        public  DataContext(string name)
            : base(name)
        {
        }
       
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies  { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<ParamName> ParamNames { get; set; }
        public DbSet<ParamValue> ParamValues { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorName> VendorNames { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Adress> Adresses  { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}
