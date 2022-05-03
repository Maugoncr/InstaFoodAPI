using System;
using System.Collections.Generic;

namespace InstaFoodAPI.Models
{
    public partial class Town
    {
        public Town()
        {
            Districts = new HashSet<District>();
            Purchases = new HashSet<Purchase>();
            Restaurants = new HashSet<Restaurant>();
        }

        public int IdTown { get; set; }
        public string NameTown { get; set; }
        public int IdProv { get; set; }

        public virtual Province IdProvNavigation { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
