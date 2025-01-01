using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly PhrmacyTwoContext context = new PhrmacyTwoContext();
        public IActionResult Index()
        {


            var adminEmail = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "Admin");
            }

            try {
                List<CustomerModel> list = new List<CustomerModel>();
                list = (from obj in context.Customers
                        .Include(t => t.Transactions)
                        select new CustomerModel
                        {
                            CustomerId = obj.CustomerId,
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
        public async Task<IActionResult> AddCustomer(CustomerModel customer)
        {

            if (!ModelState.IsValid) { 
                return View() ;
            }
            try {
                Customer NewCustomer = new Customer
                {
                    Name = customer.Name,
                    Contact = customer.Contact
                };
                context.Customers.Add(NewCustomer);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                return View(ex.Message);
            }
           
        }


        [HttpGet]
        public async Task<IActionResult> Update(int CustomerId)
        {

            var adminEmail = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                Customer customer = await context.Customers
               .Where(x => x.CustomerId == CustomerId)
               .FirstOrDefaultAsync();

                if (customer == null)
                {
                    return NotFound();
                }

                CustomerModel customerModel = new CustomerModel
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Contact = customer.Contact
                };
                await context.SaveChangesAsync();


                return View(customerModel);

            }
            catch (Exception ex) {
                return View(ex.Message);

            }

        }


        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try {
                Customer NewCustomer = new Customer();

                NewCustomer = await context.Customers.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefaultAsync();

                if (NewCustomer == null)
                {
                    return NotFound();
                }
                NewCustomer.Name = customer.Name;
                NewCustomer.Contact = customer.Contact;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception ex) {
                return View("error in this page" );
            }
            
        }

        public async Task<IActionResult> Delete(int CustomerId)
        {
            Customer customer = await context.Customers.Where(x => x.CustomerId == CustomerId).FirstOrDefaultAsync();

            try
            {

                if (customer == null)
                {
                    return NotFound();
                }

                context.Customers.Remove(customer);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                ViewBag.ErrorMessage = "There was an error with the deleting  data.";
                return View(customer);
            }
            

        }
    }
}
