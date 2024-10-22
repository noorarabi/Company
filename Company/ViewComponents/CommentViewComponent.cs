using Company.Data;
using Microsoft.AspNetCore.Mvc;

namespace Company.ViewComponents
{
    public class CommentViewComponent:ViewComponent
    {
       private readonly CompanyDbContext db;
        public CommentViewComponent(CompanyDbContext _db)
        {
            db= _db;    
        }
        public IViewComponentResult Invoke()
        {
            return View(db.Comments);
        }
    }
}
