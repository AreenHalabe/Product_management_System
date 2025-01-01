
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace WebApplication1.Models
{
    public class AdminModel
    {
        public AdminModel() { 
            Transactions = new HashSet<TransactionModel>();
            Notifications = new HashSet<NotificationModel>();
        }

        public int AdminId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

        public  ICollection<NotificationModel> Notifications { get; set; } 
        public  ICollection<TransactionModel> Transactions { get; set; } 
    }
}
