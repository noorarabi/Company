using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    [Authorize]
    public class CvController : Controller
    {
        private readonly CompanyDbContext _context;

        public CvController(CompanyDbContext context)
        {
            _context = context;
        }

        [Authorize("Student")]
        public IActionResult Index(int id)
        {
            var x = _context.Trains.Find(id);
            ViewBag.Id = id;
            ViewBag.Name = x.TrainingName;
            
            return View();
        }
        [HttpPost]
        [Authorize("Student")]
        public async Task<IActionResult> Index(Cv cv, IFormFile Image,int id) {
            if (Image != null && Image.Length > 0)
            {
                if (Image.ContentType == "application/pdf" || Path.GetExtension(Image.FileName).ToLower() == ".pdf")
                {
                    var memorystream = new MemoryStream();
                    await Image.CopyToAsync(memorystream);
                    cv.CV = memorystream.ToArray();
                    ModelState.Remove("CV");
                }
                else
                {
                    ModelState.AddModelError("Image", "Only PDF files are allowed.");
                }
                //var memorystream = new MemoryStream();
                //await Image.CopyToAsync(memorystream);
                //memorystream.ToArray();
                //cv.CV = memorystream.ToArray();
                //ModelState.Remove("CV");

            }
        
            if (ModelState.IsValid)
            {

                cv.TrainId = id;
                _context.Add(cv);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
    }
            ViewBag.Id = id;    
            return View(cv);


}

    }
}
