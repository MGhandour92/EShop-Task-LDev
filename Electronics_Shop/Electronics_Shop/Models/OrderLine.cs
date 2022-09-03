using System.ComponentModel.DataAnnotations;

namespace Electronics_Shop.Models
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal AfterDiscountPrice { get; set; }


        //Relationships
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
