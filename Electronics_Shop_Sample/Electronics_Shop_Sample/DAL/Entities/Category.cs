using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Category
    {
        [Key]
        public short Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string Name { get; set; }


        //Relationships
        public ICollection<Product> Products { get; set; }
    }
}
