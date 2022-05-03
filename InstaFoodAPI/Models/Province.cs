using System;
using System.Collections.Generic;

namespace InstaFoodAPI.Models
{
    public partial class Province
    {
        public Province()
        {
            Purchases = new HashSet<Purchase>();
            Restaurants = new HashSet<Restaurant>();
            Towns = new HashSet<Town>();
        }

        public int IdProv { get; set; }
        public string NameProv { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<Town> Towns { get; set; }
    }
}
