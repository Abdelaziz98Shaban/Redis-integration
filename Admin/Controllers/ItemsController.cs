using Data.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IUOMService _uOMService;

        public ItemsController(IItemService itemService, IUOMService uOMService)
        {
            _itemService = itemService;
            _uOMService = uOMService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _itemService.GetAllItemVMAsync());
        }

        // GET: BrandsController/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UOMS"] = await _uOMService.GetSelectListAsync();
            return View(new ItemVM());
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ItemVM ItemVM)
        {

            if (ModelState.IsValid)
            {
                try
                {


                    var result = await _itemService.AddAsync(ItemVM);

                    if (result)
                        return RedirectToAction(nameof(Index));

                }
                catch
                {
                }

            }
            ViewData["UOMS"] = await _uOMService.GetSelectListAsync(ItemVM.UomId);
            return View(ItemVM);
        }

        // GET: BrandsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var ItemVM = await _itemService.GetItemVMAsync(id);
            if (ItemVM == null)
            {
                return NotFound();

            }
            ViewData["UOMS"] = await _uOMService.GetSelectListAsync(ItemVM.UomId);
            return View(ItemVM);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ItemVM ItemVM)
        {
            if (id != ItemVM.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _itemService.UpdateAsync(ItemVM);

                    if (result)
                        return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _itemService.AnyAsync(ItemVM.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["UOMS"] = await _uOMService.GetSelectListAsync(ItemVM.UomId);
            return View(ItemVM);
        }



        public async Task<JsonResult> JsonDelete(int? id)
        {
            if (id == null)
            {
                return Json("Failed");

            }
            var result = await _itemService.DeleteAsync(id.Value);
       
            if (result)
            {

                return Json("Removed");
            }
            return Json("Failed");
        }
    }
}
