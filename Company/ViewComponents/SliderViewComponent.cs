using Company.Data;
using Microsoft.AspNetCore.Mvc;

namespace Company.ViewComponents
{
    
    
        public class SliderViewComponent : ViewComponent
        {
            private readonly CompanyDbContext db;
            public SliderViewComponent(CompanyDbContext _db)
            {
                db = _db;
            }
            public IViewComponentResult Invoke()
            {
                return View(db.Slider);
            }
        }
    }

