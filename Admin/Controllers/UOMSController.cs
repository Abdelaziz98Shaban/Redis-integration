using Data.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UOMSController : Controller
    {
        private readonly IUOMService _uOMService;

        public UOMSController(IUOMService uOMService)
        {
            _uOMService = uOMService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uOMService.GetAllUOMVMAsync());
        }

        // GET: BrandsController/Create
        public async Task<IActionResult> Create()
        {
            return View(new UOMVM());
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UOMVM UOMVM)
        {

            if (ModelState.IsValid)
            {
                try
                {


                    var result = await _uOMService.AddAsync(UOMVM);

                    if (result)
                        return RedirectToAction(nameof(Index));

                }
                catch
                {
                }

            }
            return View(UOMVM);
        }

        // GET: BrandsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var UOMVM = await _uOMService.GetUOMVMAsync(id);
            if (UOMVM == null)
            {
                return NotFound();

            }
            return View(UOMVM);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UOMVM UOMVM)
        {
            if (id != UOMVM.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _uOMService.UpdateAsync(UOMVM);

                    if (result)
                        return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _uOMService.AnyAsync(UOMVM.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(UOMVM);
        }



        public async Task<JsonResult> JsonDelete(int? id)
        {
            if (id == null)
            {
                return Json("Failed");

            }
            var result = await _uOMService.DeleteAsync(id.Value);

            if (result)
            {

                return Json("Removed");
            }
            return Json("Failed");
        }
    }
}
