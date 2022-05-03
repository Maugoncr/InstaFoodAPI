using System;
using System.Collections.Generic;

namespace InstaFoodAPI.Models
{
    public partial class District
    {
        public District()
        {
            Purchases = new HashSet<Purchase>();
            Restaurants = new HashSet<Restaurant>();
        }

        public int IdDistrict { get; set; }
        public string NameDistrict { get; set; }
        public int IdTown { get; set; }

        public virtual Town IdTownNavigation { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
