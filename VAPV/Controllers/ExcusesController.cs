using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VAPV.Services;
using VAPV.DTO;
using System.Collections.Generic;

namespace VAPV.Controllers {
    public class ExcusesController : Controller {
        private readonly ExcuseService _excuseService;
        //DI
        public ExcusesController(ExcuseService excuseService) {
            _excuseService = excuseService;
        }

        //metoda je volána, když uživatel přistoupí na výchozí stránku (/Excuses/Index.cshtml)
        //načte seznamu všech výmluv z databáze a zároveň předá počet všech výmluv
        public async Task<IActionResult> Index() {
            var excuses = await _excuseService.GetExcusesAsync();
            ViewBag.ExcuseCount = await _excuseService.GetExcuseCountAsync();
            return View(excuses);
        }

        //zobrazí formulář pro vyhledávání výmluv, je volána při GET požadavku na /Excuses/Search.cshtml
        [HttpGet]
        public async Task<IActionResult> Search() {
            ViewBag.Categories = await _excuseService.GetCategoriesAsync();
            return View(new List<ExcuseDTO>());
        }

        //zpracovává výsledky vyhledávání po odeslání formuláře
        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm, string category) {
            var results = await _excuseService.SearchExcusesAsync(searchTerm, category);
            ViewBag.Categories = await _excuseService.GetCategoriesAsync();
            ViewBag.SearchCategory = category;
            return View(results);
        }

        //metoda vrací náhodnou výmluvu ve formě prostého textu
        //tato metoda je volána pomocí JavaScriptové funkce fetch() v metodě updateExcuse() v _Layout.cshtml
        [HttpGet]
        public async Task<IActionResult> GetRandomExcuse() {
            var excuse = await _excuseService.GetRandomExcuseAsync();
            return Content(excuse);
        }
    }
}
