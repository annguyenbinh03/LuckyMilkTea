using Microsoft.AspNetCore.Mvc;
using PRN222.MilkTeaShop.Manager.Models.Request;
using PRN222.MilkTeaShop.Service.BusinessObjects;
using PRN222.MilkTeaShop.Service.Services;

namespace PRN222.MilkTeaShop.Manager.Controllers
{
    public class MilkTeasController : Controller
    {
        private readonly IProductService _productService;
		private readonly CloudinaryService _cloudinaryService;

		public MilkTeasController(IProductService productService, CloudinaryService cloudinaryService)
        {
            _productService = productService;
            _cloudinaryService = cloudinaryService;
        }

        // GET: MilkTeas
        public async Task<IActionResult> Index(int? page,
            string? search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var (products, totalItems) = await _productService.GetMilkTeas(search, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.Search = search;
            return View(products);

        }

        // GET: MilkTeas/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: MilkTeas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,PriceSizeS,PriceSizeM,PriceSizeL,Image")] MilkTeaCreationRequest request)
        {
            int categoryId = 1;

            string? imageUrl = null;

            if (request.Image != null && request.Image.Length > 0)
            {
                using var stream = request.Image.OpenReadStream();
                imageUrl = await _cloudinaryService.UploadImageAsync(stream, request.Name);
                if (imageUrl == null)
                {
                    ModelState.AddModelError("Image", "Không thể tải ảnh lên.");
                    return View(request);
                }
            }

            var model = new MilkTeaModel
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = imageUrl,
                PriceSizeS = request.PriceSizeS,
                PriceSizeM = request.PriceSizeM,
                PriceSizeL = request.PriceSizeL
            };
            try
            {
                await _productService.CreateMilkTea(model);
			} catch (Exception e)
            {
				ViewBag.Error = e.Message;
			}

            return RedirectToAction(nameof(Index));
        }

        // GET: MilkTeas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _productService.GetMilkTea((int)id);

            if(model == null)
            {
                return NotFound();
            }

            var request = new MilkTeaUpdateRequest
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                PriceSizeS = model.PriceSizeS,
                PriceSizeM = model.PriceSizeM,
                PriceSizeL = model.PriceSizeL,
                ImageUrl = model.ImageUrl,
            };

            return View(request);
        }

        // POST: MilkTeas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description,PriceSizeS,PriceSizeM,PriceSizeL,Image")] MilkTeaUpdateRequest request)
        {
			if (request.Image != null && request.Image.Length > 0)
			{
                string? url = null; 
				using var stream = request.Image.OpenReadStream();
				url = await _cloudinaryService.UploadImageAsync(stream, request.Name);
				if (url == null)
				{
					ModelState.AddModelError("Image", "Không thể tải ảnh lên.");
					return View(request);
				}
                request.ImageUrl = url;
			}

			var model = new MilkTeaModel
			{
                Id = request.Id,
				Name = request.Name,
				Description = request.Description,
				ImageUrl = request.ImageUrl,
				PriceSizeS = request.PriceSizeS,
				PriceSizeM = request.PriceSizeM,
				PriceSizeL = request.PriceSizeL
			};
			try
			{
				await _productService.UpdateMilkTea(model);
			}
			catch (Exception e)
			{
				ViewBag.Error = e.Message;
			}

			return RedirectToAction(nameof(Index));
		}

        // GET: MilkTeas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            await _productService.Delete((int)id);
            return RedirectToAction(nameof(Index));
		}

		// GET: MilkTeas/Active/5	
		public async Task<IActionResult> Active(int? id)
		{
			if (id == null)
				return NotFound();
			await _productService.Active((int)id);
			return RedirectToAction(nameof(Index));
		}
	}
}
