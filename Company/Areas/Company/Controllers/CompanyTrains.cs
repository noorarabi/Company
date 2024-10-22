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
    public class CompanyTrains : Controller
    {
        private readonly CompanyDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;    
        public CompanyTrains(CompanyDbContext context,UserManager<IdentityUser>userManager)
        {
            _context = context;
            _userManager = userManager;  
        }

        [Authorize("Admain")]
        public async Task<IActionResult> Index()
        {
            var user=await _userManager.GetUserAsync(User); 

            return View(await _context.Trains.Where(c=>c.UserId==user!.Id).ToListAsync());
        }
        [Authorize("Admain")]
        // GET: SuperAdmain/Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Trains
                .FirstOrDefaultAsync(m => m.TrainId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }
        [Authorize("Admain")]
        public async Task <IActionResult> Create()
        {
          
            return View();
        }

        // POST: SuperAdmain/Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admain")]
        public async Task<IActionResult> Create(Train comment, IFormFile Image)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("Users");
            if (Image != null && Image.Length > 0)
            {
                var memorystream = new MemoryStream();
                await Image.CopyToAsync(memorystream);
                memorystream.ToArray();
                comment.TrainingImg = memorystream.ToArray();
                ModelState.Remove("TrainImg");

            }
            if (ModelState.IsValid)
            {
                var user=await _userManager.GetUserAsync(User);
                comment.UserId = user!.Id;
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        [Authorize("Admain")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Trains.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: SuperAdmain/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admain")]
        public async Task<IActionResult> Edit(Train slider, IFormFile Image)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("Users");

            if (slider.TrainId==null)
            {
                return NotFound();
            }
            if (Image != null && Image.Length > 0)
            {
                var memorystream = new MemoryStream();
                await Image.CopyToAsync(memorystream);
                memorystream.ToArray();
                slider.TrainingImg= memorystream.ToArray();
                ModelState.Remove("TrainingImg");

            }

            if (ModelState.IsValid)
            {

                var user = await _userManager.GetUserAsync(User);
               slider.UserId = user!.Id;
                _context.Trains.Update(slider);
                    await _context.SaveChangesAsync();
                
               
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }


        [Authorize("Admain")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Trains
                .FirstOrDefaultAsync(m => m.TrainId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: SuperAdmain/Comments/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize("Admain")]
        public async Task<IActionResult> Delete(Train comments)
        {
            var comment = await _context.Trains.FindAsync(comments.TrainId);
            if (comment != null)
            {
                _context.Trains.Remove(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "CompanyTrains");
            }

           
            return View(comment);
        }

    }
}
