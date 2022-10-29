using ProductView.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductView.Models
{
    [Table("ListProducts")]
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
    }
    public class ListProducts
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}