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
        public int ContactPhone { get; set; }

        [Required]
        //nvarchar
        [StringLength(800)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Deliver to Address")]
        public string DeliverToAddress { get; set; }


        //Relationships
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
