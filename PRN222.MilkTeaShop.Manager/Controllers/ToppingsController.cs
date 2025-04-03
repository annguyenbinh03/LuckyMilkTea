using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Manager.Models.Request;
using PRN222.MilkTeaShop.Repository.DbContexts;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Service.BusinessObjects;
using PRN222.MilkTeaShop.Service.Services;

namespace PRN222.MilkTeaShop.Manager.Controllers
{
    public class ToppingsController : Controller
    {
        private readonly IProductService _productService;
        private readonly CloudinaryService _cloudinaryService;

        public ToppingsController(IProductService productService, CloudinaryService cloudinaryService)
        {
            _productService = productService;
            _cloudinaryService = cloudinaryService;
        }

        // GET: Toppings
        public async Task<IActionResult> Index(int? page,
            string? search)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var (products, totalItems) = await _productService.GetToppings(search, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.Search = search;
            return View(products);
        }

        //GET: Toppings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Toppings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,Image")] ToppingCreationRequest request)
        {
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

            var model = new ToppingModel
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = imageUrl,
                Price = request.Price
            };

			try
			{
				await _productService.CreateTopping(model);
			}
			catch (Exception e)
			{
				ViewBag.Error = e.Message;
			}

			return RedirectToAction(nameof(Index));
		}

        // GET: Toppings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var model = await _productService.GetTopping((int)id);
			if (model == null)
            {
                return NotFound();
            }
			var request = new ToppingUpdateRequest
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description,
				Price = (decimal)model.Price,
				ImageUrl = model.ImageUrl,
			};
			return View(request);
        }

        // POST: Toppings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Image")] ToppingUpdateRequest request)
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

            var model = new ToppingModel
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Price = request.Price
            };
            try
            {
                await _productService.UpdateTopping(model);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Toppings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            await _productService.Delete((int)id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Toppings/Active/5	
        public async Task<IActionResult> Active(int? id)
        {
            if (id == null)
                return NotFound();
            await _productService.Active((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}
