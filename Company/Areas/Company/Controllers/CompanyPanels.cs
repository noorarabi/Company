using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Areas.Company.Controllers
{
    [Area("Company")]
    [Authorize]
    public class CompanyPanels : Controller
    {
        [Authorize("Admain")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
