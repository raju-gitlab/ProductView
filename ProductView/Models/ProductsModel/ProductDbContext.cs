using ProductView.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductView.Models
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Products> ProductsLists { get; set; }
        public DbSet<CategoryModel> categoryModels { get; set; }

    }
}