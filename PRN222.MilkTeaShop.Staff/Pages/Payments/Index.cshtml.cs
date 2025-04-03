using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.MilkTeaShop.Repository.DbContexts;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Service.Services.Interface;

namespace PRN222.MilkTeaShop.Staff.Pages.Payments
{
    public class IndexModel : PageModel
    {
        private readonly IPaymentService _paymentService;

        public IndexModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // Property to store list of payments
        public IList<Payment> Payments { get; set; }
        public string[] PaymentStatuses { get; } = new[] { "Pending", "Completed", "Failed" };
        // OnGetAsync method to fetch payments
        public async Task OnGetAsync()
        {
            Payments = (List<Payment>)await _paymentService.GetAllPaymentsAsync();
        }
        public async Task<IActionResult> OnPostUpdateStatusAsync(int paymentId, string newStatus)
        {
            if (string.IsNullOrEmpty(newStatus))
            {
                // Handle invalid status or error (optional)
                return RedirectToPage("./Index");
            }

            await _paymentService.UpdatePaymentStatusAsync(paymentId, newStatus);

            // Redirect back to the index page after updating
            return RedirectToPage("./Index");
        }
    }
}