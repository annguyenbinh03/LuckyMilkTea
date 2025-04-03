using PRN222.MilkTeaShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.BusinessObjects
{
    public class ComboModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public string ImageUrl { get; set; }

        public int? SoldCount { get; set; }

        public string Status { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<ProductInCombo> Products { get; set; }
    }
    public class ProductInCombo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public decimal? Price { get; set; }
        public string? Size { get; set; }
    }
}
