using PRN222.MilkTeaShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.BusinessObjects
{
	public class ToppingModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string ImageUrl { get; set; }

		public string Status { get; set; }

		public DateTime? CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }
		public decimal Price { get; set; }
		public Product ToProduct()
		{
			if (this == null) return null;

			return new Product
			{
				Id = Id,
				Name = Name,
				Description = Description,
				Price = Price,
				ImageUrl = ImageUrl,
				Status = Status,
				CreatedAt = CreatedAt,
				UpdatedAt = UpdatedAt
			};
		}
	}
}
