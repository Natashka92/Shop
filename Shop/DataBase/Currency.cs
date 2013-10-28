using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Currency")]
    public class Currency : INamedEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CurrencyId { get; set; }

        [MaxLength(3)]
        [Required]
        public string Name { get; set; }
        [Required]
        public double Rate { get; set; }
    }
}
