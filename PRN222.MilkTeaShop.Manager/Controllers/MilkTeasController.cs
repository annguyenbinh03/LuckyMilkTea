using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Manager.Models.Request;
using PRN222.MilkTeaShop.Repository.DbContexts;
using PRN222.MilkTeaShop.Repository.Models;
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

            return View();
        }

        // POST: MilkTeas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,CategoryId,ImageUrl,Status,CreatedAt,UpdatedAt")] Product product)
        {


            return View();
        }

        // GET: MilkTeas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            return View();
        }

        // POST: MilkTeas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            return RedirectToAction(nameof(Index));
        }
    }
}
