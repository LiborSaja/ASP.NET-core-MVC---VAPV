using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VAPV.Services;
using VAPV.DTO;
using System.Collections.Generic;

namespace VAPV.Controllers {
    public class ExcusesController : Controller {
        private readonly ExcuseService _excuseService;

        public ExcusesController(ExcuseService excuseService) {
            _excuseService = excuseService;
        }

        public async Task<IActionResult> Index() {
            var excuses = await _excuseService.GetExcusesAsync();
            ViewBag.ExcuseCount = await _excuseService.GetExcuseCountAsync();
            return View(excuses);
        }

        [HttpGet]
        public async Task<IActionResult> Search() {
            ViewBag.Categories = await _excuseService.GetCategoriesAsync();
            return View(new List<ExcuseDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm, string category) {
            var results = await _excuseService.SearchExcusesAsync(searchTerm, category);
            ViewBag.Categories = await _excuseService.GetCategoriesAsync();
            ViewBag.SearchCategory = category;
            return View(results);
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomExcuse() {
            var excuse = await _excuseService.GetRandomExcuseAsync();
            return Content(excuse);
        }
    }
}
