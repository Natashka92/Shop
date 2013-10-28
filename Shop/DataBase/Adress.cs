using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Adress")]
    public class Adress
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AdressId { get; set; }
        
        [Required]
        [MaxLength(10)]
        public String Country { get; set; }

        [Required]
        [MaxLength(10)]
        public String Town { get; set; }

        [Required]
        [MaxLength(15)]
        public String Street { get; set; }
    }
}
