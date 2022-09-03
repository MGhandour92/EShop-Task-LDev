namespace Electronics_Shop.Models
{
    public class OrderViewModel
    {
        public OrderHeader OrderHeader { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
