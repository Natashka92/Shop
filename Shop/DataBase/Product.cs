using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Product")]
    public class Product : INamedEntity
    {
        public Product()
        {
            Tags = new HashSet<Tag>();
            Descriptions = new HashSet<Description>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Url { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Vendor Vendor { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Picture> Picture { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(13)]
        public string Barcode { get; set; }

        public double Discount { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Parameter> Parameter { get; set; }
        public virtual ICollection<Description> Descriptions { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
