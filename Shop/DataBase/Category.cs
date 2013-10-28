using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Category")]
    public class Category : INamedEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public Category Parent { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
