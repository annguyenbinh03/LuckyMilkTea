using PRN222.MilkTeaShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.BusinessObjects
{
    public class MilkTeaModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
		public decimal PriceSizeS { get; set; }
		public decimal PriceSizeM { get; set; }
        public decimal PriceSizeL { get; set; }

		public Product ToProduct()
        {
            if (this == null) return null;

            return new Product
            {
                Id = Id,
                Name = Name,
                Description = Description,
                ImageUrl = ImageUrl,
                Status = Status,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt
            };
        }
    }
}
