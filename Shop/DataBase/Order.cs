using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public virtual User User { get; set; }
        [Required]
        public int UserId { get; set; }

        public virtual Adress Adress { get; set; }
        [Required]
        public int AdressId { get; set; }

        [Required]
        public DateTime OrderData { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        public virtual Currency Currency { get; set; }
        [Required]
        public int CurrencyId { get; set; }

        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
