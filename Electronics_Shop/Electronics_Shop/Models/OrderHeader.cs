using System.ComponentModel.DataAnnotations;

namespace Electronics_Shop.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string ContactPhone { get; set; }

        [Required]
        //nvarchar
        [StringLength(800)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Deliver to Address")]
        public string DeliverToAddress { get; set; }


        //Relationships
        public List<OrderLine> OrderLines { get; set; }
    }
}
