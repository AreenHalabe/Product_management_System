using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MedicineController : Controller
    {
        private readonly PhrmacyTwoContext context = new PhrmacyTwoContext();

        [HttpGet]
        public IActionResult Index()
        {
            var adminEmail = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "Admin");
            }

            List<MedicineModel> list = new List<MedicineModel>();

            list = (from obj in context.Medicines
                    .Include(t => t.Transactions)
                    .Include(t => t.NotificationIds)
                    select new MedicineModel
                    {
                        MedicineId=obj.MedicineId,
                        Name = obj.Name,
                        Price = obj.Price,
                        ExpiryDate = obj.ExpiryDate,
                        Quantity = obj.Quantity,
                    }).ToList();
            return View(list);
        }

        [HttpGet]

        public IActionResult Add()
        {
            var adminEmail = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddMedicine(MedicineModel medicine)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with the model to show validation errors
                return View(medicine);
            }

            Medicine NewMedicine = new Medicine();
            NewMedicine.Name = medicine.Name;
            NewMedicine.Price = medicine.Price;
            NewMedicine.Quantity = medicine.Quantity;
            NewMedicine.ExpiryDate = medicine.ExpiryDate;
            context.Medicines.Add(NewMedicine);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public  IActionResult Update(int MedicineId)
        {
            var adminEmail = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "Admin");
            }
            try {

                var MedicineEntity =  context.Medicines.Where(x => x.MedicineId == MedicineId).FirstOrDefault();

                if (MedicineEntity == null)
                {
                    return NotFound();
                }

                MedicineModel medicine = new MedicineModel
                {
                    MedicineId = MedicineEntity.MedicineId,
                    Name = MedicineEntity.Name,
                    Price = MedicineEntity.Price,
                    Quantity = MedicineEntity.Quantity,
                    ExpiryDate = MedicineEntity.ExpiryDate,

                };

                return View(medicine);
            }
            catch (Exception ex) { 
                return View(ex.Message);
            }
            
            
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMedicine(MedicineModel medicine)
        {
            if (!ModelState.IsValid)
            {
                return View(medicine);
            }
            try
            {
                Medicine NewMedicine = await context.Medicines.Where(x => x.MedicineId == medicine.MedicineId).FirstOrDefaultAsync();
                NewMedicine.Name = medicine.Name;
                NewMedicine.Price = medicine.Price;
                NewMedicine.Quantity = medicine.Quantity;
                NewMedicine.ExpiryDate = medicine.ExpiryDate;
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            catch (Exception ex) { 
            return View(ex.Message);
            }
            

        }



        public async Task<IActionResult> Delete(int MedicineId)
        {
            try
            {
                Medicine medicine = await context.Medicines.Where(x => x.MedicineId == MedicineId).FirstOrDefaultAsync();
                context.Medicines.Remove(medicine);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            catch (Exception ex) {
                return View(ex.Message);
            }
            
        }

    }
}


    
