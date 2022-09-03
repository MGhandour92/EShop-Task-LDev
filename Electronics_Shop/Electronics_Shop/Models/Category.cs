using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Electronics_Shop.Models
{
    public class Category
    {
        [Key]
        public short Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        [Display(Name = "Category Name")] 
        public string Name { get; set; }


        //Relationships
        public ICollection<Product> Products { get; set; }
    }
}
