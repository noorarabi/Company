using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Authorization;

namespace Company.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly CompanyDbContext _context;
       
        public CommentController(CompanyDbContext context)
        {
            _context = context;
        }

		// GET: Comment
		//public async Task<IActionResult> Index()
		//{
		//    return View(await _context.Comments.ToListAsync());
		//}

		// GET: Comment/Details/5
		//public async Task<IActionResult> Details(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var comment = await _context.Comments
		//        .FirstOrDefaultAsync(m => m.CommentId == id);
		//    if (comment == null)
		//    {
		//        return NotFound();
		//    }

		//    return View(comment);
		//}

		// GET: Comment/Create
		[Authorize("Student")]
		public IActionResult Create()
        {
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize("Student")]
		public async Task<IActionResult> Create([Bind("CommentId,Name,CommentText,img")] Comment comment,IFormFile Image)
        {
            if(Image !=null && Image.Length > 0) {
                var memorystream = new MemoryStream();
                await Image.CopyToAsync(memorystream);
                memorystream.ToArray();
                comment.img = memorystream.ToArray();
				ModelState.Remove("img");

			}
            if (ModelState.IsValid)
            {
                comment.Creation=DateTime.Now;
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View(comment);
        }

        // GET: Comment/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comments.FindAsync(id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(comment);
        //}

        // POST: Comment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CommentId,Name,CommentText,img")] Comment comment)
        //{
        //    if (id != comment.CommentId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(comment);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CommentExists(comment.CommentId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(comment);
        //}

        // GET: Comment/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comments
        //        .FirstOrDefaultAsync(m => m.CommentId == id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comment);
        //}

        //// POST: Comment/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var comment = await _context.Comments.FindAsync(id);
        //    if (comment != null)
        //    {
        //        _context.Comments.Remove(comment);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CommentExists(int id)
        //{
        //    return _context.Comments.Any(e => e.CommentId == id);
        //}
    }
}
