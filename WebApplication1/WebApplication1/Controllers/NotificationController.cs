using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int medicineId)
        {
            var adminIdString = HttpContext.Session.GetString("AdminId");
            NotificationModel notification = new NotificationModel();


            if (!string.IsNullOrEmpty(adminIdString) && int.TryParse(adminIdString, out int adminId))
            {
                notification.AdminId = adminId;
            }

            notification.MedicineId = medicineId;


            



            return View(notification);
        }
    }
}
