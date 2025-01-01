using WebApplication1.Entity;

namespace WebApplication1.Models
{
    public class TransactionModel
    {
        public TransactionModel() { 
            Customer = new CustomerModel();
            Medicine = new MedicineModel();
            Supplier = new SupplierModel();

        }
        public int TransactionId { get; set; }
        public string MedicineName { get; set; }= string.Empty;
        public string SupplierName { get; set; }=string.Empty;
        public string? CustomerName { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Quantity { get; set; }
        public string TransactionType { get; set; } = string.Empty;




        public  CustomerModel Customer { get; set; }
        public  MedicineModel Medicine { get; set; }
        public  SupplierModel Supplier { get; set; }
    }
}
