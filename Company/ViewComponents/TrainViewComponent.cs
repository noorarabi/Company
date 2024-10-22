using Company.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Company.ViewComponents
{
    
        public class TrainViewComponent : ViewComponent
        {
            private readonly CompanyDbContext db;
            public TrainViewComponent(CompanyDbContext _db)
            {
                db = _db;
            }
        
            public async Task<IViewComponentResult> InvokeAsync(string query)
            {

            //     var results = string.IsNullOrEmpty(name)
            //? await db.Trains.OrderByDescending(x => x.TrainId).ToListAsync()
            //: await db.Trains
            //          .Where(x => EF.Functions.Like(x.TrainingField.ToLower(), "%" + name.ToLower() + "%")) // استخدام EF.Functions.Like للبحث بدون حساسية لحالة الأحرف
            //          .OrderByDescending(x => x.TrainId)
            //          .ToListAsync();

            //     return View(results);
            var results = string.IsNullOrEmpty(query) ?
                     await db.Trains.OrderByDescending(x => x.TrainId).ToListAsync() :
                     await db.Trains.Where(x => x.TrainingField.ToLower().Contains(query.ToLower()))
                               .OrderByDescending(x => x.TrainId)
                               .ToListAsync();

            //return View(db.Trains.OrderByDescending(x=>x.TrainId).Take(10).ToList());
            return View(results);
        }
    }
    }

