using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        

        [HttpGet]
        public IActionResult Login()
        {
            var adminEmail = HttpContext.Session.GetString("AdminId");
            if (!string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Index", "Transaction");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminModel admin)
        {

            if (!ModelState.IsValid)
            {
                return View(admin);
            }

            PhrmacyTwoContext context = new PhrmacyTwoContext();

            var FindAdmin = await context.Admins.Where(x => x.Name == admin.Name && x.Password==admin.Password).FirstOrDefaultAsync();

            if (FindAdmin == null) {
                TempData["ErrorMessage"] = "Invalid login attempt. Admin not found.";
                return View(admin);
            }
            HttpContext.Session.SetString("AdminId", admin.AdminId.ToString());
            return RedirectToAction("Index", "Transaction");
        }


        


        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
