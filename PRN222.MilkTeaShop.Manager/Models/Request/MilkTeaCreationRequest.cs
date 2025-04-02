namespace PRN222.MilkTeaShop.Manager.Models.Request
{
    public class MilkTeaCreationRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? PriceSizeS { get; set; }
        public decimal? PriceSizeM { get; set; }
        public decimal? PriceSizeL { get; set; }
        public IFormFile Image { get; set; }
    }
}
