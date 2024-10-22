using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Areas.SuperAdmain.Controllers
{
	[Area("SuperAdmain")]
    [Authorize]
	public class AdmainPanelsController : Controller
    {
        [Authorize("SuperAdmain")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
