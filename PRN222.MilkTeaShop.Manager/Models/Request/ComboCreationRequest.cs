using System.ComponentModel.DataAnnotations;

namespace PRN222.MilkTeaShop.Manager.Models.Request
{
	public class ComboCreationRequest
	{
		[Required(ErrorMessage = "Tên combo là bắt buộc.")]
		[StringLength(255, ErrorMessage = "Tên combo không được vượt quá 255 ký tự.")]
		public string Name { get; set; }

		[Display(Name = "Mô tả")]
		public string Description { get; set; }

		[Display(Name = "Giá")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
		public decimal? Price { get; set; }
		public IFormFile Image { get; set; }
		public List<ProductInComboCreationRequest> Products { get; set; } = new List<ProductInComboCreationRequest>();
	}

	public class ProductInComboCreationRequest
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }

		public int? SizeId { get; set; }

		public string? SizeName { get; set; }
		public string? Name { get; set; }
		public string? ImageUrl { get; set; }
		public int? Price { get; set; }
	}
}
