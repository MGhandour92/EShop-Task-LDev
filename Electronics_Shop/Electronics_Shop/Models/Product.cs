using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Electronics_Shop.Models
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
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, 100)]
        public byte DisountToApply { get; set; } = 0;


        //Relationships
        public short CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
