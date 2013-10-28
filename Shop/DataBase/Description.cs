using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Description")]
    public class Description
    {
        public Description()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DescriptionId { get; set; }

        [Required]
        [MaxLength(200)]
        public String TextDescription { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
