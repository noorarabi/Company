using Company.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.ViewComponents
{

    [Authorize]
    public class MenuViewComponent : ViewComponent
        {
            private readonly CompanyDbContext db;
        
            public MenuViewComponent(CompanyDbContext _db)
            {
                db = _db;
            }
        [Authorize("Student")]
            public IViewComponentResult Invoke()
            {
                return View(db.Menu);
            }
        }
    }

