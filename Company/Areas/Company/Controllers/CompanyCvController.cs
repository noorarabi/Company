using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Areas.Company.Controllers
{
    [Area("Company")]
    [Authorize]
    public class CompanyCvController : Controller
    {
        private readonly CompanyDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CompanyCvController(CompanyDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize("Admain")]
        public async Task <IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var Train = await _context.Trains.Where(c => c.UserId == user!.Id).ToListAsync();

            var allCvs = new List<Cv>();
            foreach (var i in Train) {
              var cv=await _context.Cvs.Where(m=>m.TrainId==i.TrainId).ToListAsync();
                allCvs.AddRange(cv);
            }
            return View(allCvs);
        }
        [Authorize("Admain")]
        public async Task<IActionResult> Details(int id)
        {
                var cv = await _context.Cvs.FindAsync(id);
               
               return View(cv);
        }
        [Authorize("Admain")]
        public async Task<IActionResult> Delete(int id)
        {
            var cv = await _context.Cvs.FindAsync(id);

            return View(cv);
        }
        [Authorize("Admain")]
        [HttpPost]
        public async Task<IActionResult> Delete(Cv cv)
        {
            var CV = await _context.Cvs.FindAsync(cv.CvId);
            if (ModelState.IsValid)
            {
                _context.Cvs.Remove(CV);
                _context.SaveChanges();
                return RedirectToAction("Index", "CompanyCv");
                }
            return View(CV);
        }
public async Task<IActionResult>ShowPdf(int id)
        {

            var cv= _context.Cvs.FirstOrDefault(cv=>cv.CvId==id);        
            if(cv==null)
            {
                return NotFound();
            }
            byte[] fileBytes = cv.CV; // تأكد من أن cv.CV تحتوي على بيانات ثنائية للملف

            // استخدام Content-Disposition لضمان عرض الملف في المتصفح
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = cv.Student_Name + ".pdf",  // اسم الملف
                Inline = true  // عرض الملف في المتصفح بدلاً من تحميله
            };

            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(fileBytes, "application/pdf");
            //return File(cv.CV, "Application/Pdf");
        }
    }
}
