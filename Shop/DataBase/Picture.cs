using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Picture")]
    public class Picture
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PictureId { get; set; }
        [Required]
        public String PictureUrl { get; set; }

        [Required]
        public int ProductId { get; set; }
        //public virtual Product Product { get; set; }
    }
}
