
namespace WebApplication1.Models
{
    public class NotificationModel
    {
        public NotificationModel() { 
            Admin = new AdminModel();
            Medicine = new MedicineModel();
            Supplier = new SupplierModel();
        }
        public int NotificationId1 { get; set; }
        public int MedicineId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public int AdminId { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool Status { get; set; }

        public  AdminModel Admin { get; set; }

        public MedicineModel Medicine { get; set; }
        public  SupplierModel Supplier { get; set; }
    }
}
