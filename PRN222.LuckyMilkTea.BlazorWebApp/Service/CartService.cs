using PRN222.MilkTeaShop.Repository.Models;

namespace PRN222.LuckyMilkTea.BlazorWebApp.Service
{
    public class CartService
    {
        public List<CartItem> CartItems { get; private set; } = new();

        public void AddToCart(Product product, int sizeId, string sizeName, List<string> toppings, decimal totalPrice, int quantity)
        {
            var sortedToppings = toppings != null ?
                new List<string>(toppings.OrderBy(t => t)) :
                new List<string>();

            var existingItem = CartItems.FirstOrDefault(c =>
                c.Product.Id == product.Id &&
                c.SizeId == sizeId &&
                ToppingsMatch(c.Toppings, sortedToppings));

            if (existingItem != null)
            {
                existingItem.Quantity += quantity; // Cộng thêm quantity thay vì chỉ tăng 1
            }
            else
            {
                CartItems.Add(new CartItem
                {
                    Product = product,
                    SizeId = sizeId,
                    SizeName = sizeName,
                    Toppings = sortedToppings,
                    TotalPrice = totalPrice / quantity, // Lưu giá đơn vị
                    Quantity = quantity
                });
            }
        }

        // compage topping
        private bool ToppingsMatch(List<string> list1, List<string> list2)
        {
            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                    return false;
            }

            return true;
        }
        public void RemoveFromCart(CartItem item)
        {
            CartItems.Remove(item);
        }

        public void ClearCart()
        {
            CartItems.Clear();
        }
    }

    public class CartItem
    {
        public Product Product { get; set; } = default!;
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public List<string> Toppings { get; set; } = new();
        public List<string> ToppingName { get; set; } = new();
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}