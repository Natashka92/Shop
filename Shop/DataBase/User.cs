using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("User")]
    public class User
    {
        public User()
        {
            Descriptions = new HashSet<Description>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public String UserName { get; set; }

        [Required]
        [MaxLength(20)]
        public String Login { get; set; }

        [Required]
        [MaxLength(60)]
        public String Email { get; set; }

        [Required]
        public bool Distribution { get; set; }

        [Required]
        [MaxLength(15)]
        public String Phone { get; set; }

        public virtual ICollection<Adress> Adress { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Description> Descriptions { get; set; }
    }
}
