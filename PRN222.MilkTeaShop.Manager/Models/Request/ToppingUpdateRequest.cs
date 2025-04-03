namespace PRN222.MilkTeaShop.Manager.Models.Request
{
	public class ToppingUpdateRequest
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public IFormFile? Image { get; set; }
		public string ImageUrl { get; set; }
	}
}
