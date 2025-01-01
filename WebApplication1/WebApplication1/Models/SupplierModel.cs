using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SupplierModel
    {
        public SupplierModel() {
            Notifications = new HashSet<NotificationModel>();
            Transactions = new HashSet<TransactionModel>();
        }
        public int SupplierId { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact is required.")]
        [RegularExpression(@"^05\d{8}$", ErrorMessage = "Contact must start with '05' and be exactly 10 numeric digits.")]
        public string Contact { get; set; } = string.Empty;

        public  ICollection<NotificationModel> Notifications { get; set; }
        public  ICollection<TransactionModel> Transactions { get; set; }
    }
}
