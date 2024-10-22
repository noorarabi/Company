//using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Authorization;

namespace Company.Areas.SuperAdmain.Controllers
{
    [Area("SuperAdmain")]
   [Authorize]
    public class CommentsController : Controller
    {
        private readonly CompanyDbContext _context;

        public CommentsController(CompanyDbContext context)
        {
            _context = context;
        }
        [Authorize("SuperAdmain")]
        // GET: SuperAdmain/Comments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.ToListAsync());
        }
        [Authorize("SuperAdmain")]
        // GET: SuperAdmain/Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: SuperAdmain/Comments/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: SuperAdmain/Comments/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CommentId,Name,CommentText,img")] Comment comment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(comment);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(comment);
        //}

        //// GET: SuperAdmain/Comments/Edit/5
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

        //// POST: SuperAdmain/Comments/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: SuperAdmain/Comments/Delete/5
        [Authorize("SuperAdmain")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: SuperAdmain/Comments/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize("SuperAdmain")]
        public async Task<IActionResult> Delete(Comment comments)
        {
            var comment = await _context.Comments.FindAsync(comments.CommentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Comments");
        }

        //private bool CommentExists(int id)
        //{
        //    return _context.Comments.Any(e => e.CommentId == id);
        //}
    }
}
