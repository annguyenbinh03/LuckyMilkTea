namespace PRN222.MilkTeaShop.Manager.Models.Response
{
	public class ProductViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal? Price { get; set; } // Giá (cho topping)
		public string ImageUrl { get; set; }
		public List<ProductSizeViewModel> Sizes { get; set; } // Danh sách size (cho milktea)
		public bool IsMilkTea { get; set; } // Xác định xem là MilkTea hay Topping
	}
	public class ProductSizeViewModel
	{
		public int SizeId { get; set; }
		public string SizeName { get; set; }
		public decimal Price { get; set; }
	}
}
