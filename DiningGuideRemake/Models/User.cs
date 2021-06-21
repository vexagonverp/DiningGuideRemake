using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiningGuideRemake.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idUser { get; set; }

        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [MinLength(10)]
        [MaxLength(20)]
        [StringLength(300)]
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public int? Type { get; set; }

        public Rate Rate { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
