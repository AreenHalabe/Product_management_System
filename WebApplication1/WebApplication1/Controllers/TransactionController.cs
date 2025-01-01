using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly PhrmacyTwoContext context = new PhrmacyTwoContext();

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ShowHeader"] = true;
            var adminEmail = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "Admin");
            }
            List<TransactionModel> list = new List<TransactionModel>();
            try
            {
                list = (from obj in context.Transactions
                   .Include(t => t.Medicine) // Eager load Medicine
                   .Include(t => t.Customer) // Eager load Customer
                   .Include(t => t.Supplier) // Eager load Supplier
                        select new TransactionModel
                        {
                            MedicineName = obj.Medicine.Name,
                            SupplierName = obj.Supplier.Name,
                            CustomerName = obj.Customer != null ? obj.Customer.Name : "No Customer",
                            TransactionDate = obj.TransactionDate,
                            Quantity = obj.Quantity,
                            TransactionType = obj.TransactionType == false ? "purchase" : "sale",
                        }).ToList();

                return View(list);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            
        }

        [HttpGet]
        public IActionResult Add() {
            ViewData["ShowHeader"] = true;

            var adminEmail = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult>Add_Transaction(TransactionModel transaction)
        {
            var adminEmail = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "Admin");
            }

            try
            {
                Medicine medicine = new Medicine();
                medicine = await context.Medicines.Where(x => x.Name == transaction.MedicineName).FirstOrDefaultAsync();
                if (medicine != null)
                {
                    Supplier supplier = new Supplier();
                    supplier = await context.Suppliers.Where(x => x.Name == transaction.SupplierName).FirstOrDefaultAsync();

                    if (supplier != null)
                    {
                        Transaction NewTransaction = new Transaction();

                        if (transaction.TransactionType == "sale")
                        {
                            Customer customer = new Customer();
                            customer = await context.Customers.Where(x => x.Name == transaction.CustomerName).FirstOrDefaultAsync();

                            if (customer != null)
                            {
                                NewTransaction.Customer = customer;
                                NewTransaction.TransactionType = true;

                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Customer not found.");
                                return View("Validation", transaction);
                            }

                        }
                        else
                        {
                            NewTransaction.TransactionType = false;

                        }

                        NewTransaction.Supplier = supplier;
                        NewTransaction.Medicine = medicine;
                        NewTransaction.TransactionDate = transaction.TransactionDate;
                        NewTransaction.Quantity = transaction.Quantity;
                        context.Transactions.Add(NewTransaction);
                        await context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }


                    else
                    {
                        ModelState.AddModelError(string.Empty, "Supplier not found.");
                    }


                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Medicine cannot be null.");
                }

                return View("Validation", transaction);

            }

            catch (Exception ex) {
                return View(ex.Message);
            }

            

        }


        

        
    }
}
