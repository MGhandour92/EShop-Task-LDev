using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }

        [Required]
        public byte DisountToApply { get; set; } = 0;


        //Relationships
        public short CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
