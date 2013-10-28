using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Tag")]
    public class Tag : INamedEntity
    {
        public Tag()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }

        [Required]
        [MaxLength(10)]
        public String Name { get; set; }

        [Required]
        public int Value { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
