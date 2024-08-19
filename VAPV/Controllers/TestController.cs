using Microsoft.AspNetCore.Mvc;

namespace VAPV.Controllers {
    public class TestController : Controller {
        public IActionResult Index() {
            return Content("Test controller is working");
        }
    }
}
