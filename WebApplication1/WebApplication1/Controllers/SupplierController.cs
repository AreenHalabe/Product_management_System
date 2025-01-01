using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SupplierController : Controller
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


            List<SupplierModel> list = new List<SupplierModel>();

            try
            {
                list = (from obj in context.Suppliers.ToList()
                        select new SupplierModel
                        {
                            SupplierId = obj.SupplierId,
                            Name = obj.Name,
                            Contact = obj.Contact,
                        }).ToList();
                return View(list);

            }
            catch (Exception ex) { 
                return View(ex.Message);
            }

            
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

        public IActionResult AddSupplier(SupplierModel supplier)
        {
            if (!ModelState.IsValid) {
                return View();
            }
            try
            {
                Supplier NewSupplier = new Supplier();

                NewSupplier.Name = supplier.Name;
                NewSupplier.Contact = supplier.Contact;

                context.Suppliers.Add(NewSupplier);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                return View(ex.Message);
            }
            
        }

        [HttpGet]
        public IActionResult UpdateSupplier(int SupplierId)
        {
            var adminEmail = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                var supplierEntity = context.Suppliers
                  .Where(x => x.SupplierId == SupplierId)
                  .FirstOrDefault();

                if (supplierEntity == null)
                {
                    return NotFound();
                }

                SupplierModel supplier = new SupplierModel
                {
                    SupplierId = supplierEntity.SupplierId,
                    Name = supplierEntity.Name,
                    Contact = supplierEntity.Contact,
                };

                return View(supplier);
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
          
            
        }

        [HttpPost]
        public async Task<IActionResult> Update(SupplierModel supplier)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                Supplier newSupplier = await context.Suppliers.Where(x => x.SupplierId == supplier.SupplierId).FirstOrDefaultAsync();
                Console.WriteLine(newSupplier);
                newSupplier.Name = supplier.Name;
                newSupplier.Contact = supplier.Contact;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            catch(Exception ex)
            {
                return View(ex.Message);
            }
           
        }


        public async Task<IActionResult> Delete(int SupplierId)
        {
            try
            {
                Supplier supplier = await context.Suppliers.Where(x => x.SupplierId == SupplierId).FirstOrDefaultAsync();
                context.Suppliers.Remove(supplier);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }

           
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SupplierModel supplier)
        {

            if (!ModelState.IsValid)
            {
                return View(supplier);
            }

            try
            {
                var FindSupplier = await context.Suppliers.Where(x => x.Name == supplier.Name && x.Contact == supplier.Contact).FirstOrDefaultAsync();
                if (FindSupplier == null)
                {
                    TempData["ErrorMessage"] = "Invalid login attempt.";
                    return View(supplier);
                }
                HttpContext.Session.SetString("SupplierName", supplier.Name);

                return View("Index");
            }
            catch (Exception ex) { 
                return View(ex.Message);
            }

            
        }

    }
}
