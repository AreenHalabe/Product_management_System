using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class MedicineModel
    {
        public MedicineModel() { 
            Notifications = new HashSet<NotificationModel>();
            Transactions = new HashSet<TransactionModel>();
        }
        public int MedicineId { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must be a string (letters only).")]
        public string Name { get; set; }=string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be an number and greater than 0.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be an number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Expiry Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime ExpiryDate { get; set; }
        public  ICollection<NotificationModel> Notifications { get; set; }
        public  ICollection<TransactionModel> Transactions { get; set; }
    }
}


