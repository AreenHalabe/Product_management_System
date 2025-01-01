using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using WebApplication1.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MidicenController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult AddMidicen(midicen NewMidince)
        {
            PhrmacyTwoContext context = new PhrmacyTwoContext();
            Supplier supplier = new Supplier();

            supplier.Name =NewMidince.Name;
            supplier.Contact =NewMidince.Contact;
            context.Suppliers.Add(supplier);
            context.SaveChanges();
            Console.WriteLine(NewMidince.Name ,"......." ,NewMidince.Contact);

            return RedirectToAction("GetALlSupplier"); 
        }


        public IActionResult GetALlSupplier() {
            PhrmacyTwoContext context = new PhrmacyTwoContext();

            List<midicen> list = new List<midicen>();
            list = (from obj in context.Suppliers.ToList()
                    select new midicen
                    {
                        SupplierId = obj.SupplierId,
                        Name = obj.Name,
                        Contact = obj.Contact
                    }).ToList();

            return View(list);
        }

        public IActionResult Update(int SupId) {
            PhrmacyTwoContext context = new PhrmacyTwoContext();
           midicen suppM = new midicen();

            suppM = (from x in context.Suppliers.Where(y => y.SupplierId == SupId)
                     select new midicen
                     {
                        Name = x.Name,
                        Contact = x.Contact
                    }).FirstOrDefault();

            return View(suppM);
        }
    }
}
