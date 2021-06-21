using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiningGuideRemake.Models
{
    public class Rate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IDReview { get; set; }

        public int IDRestaurant { get; set; }

        public int IDUser { get; set; }

        [Column("Rating")]
        public double Rating1 { get; set; }

        public string Review { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
