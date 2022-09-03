using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContactPhone { get; set; }

        [Required]
        //nvarchar
        [StringLength(800)] 
        public string DeliverToAddress { get; set; }


        //Relationships
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
