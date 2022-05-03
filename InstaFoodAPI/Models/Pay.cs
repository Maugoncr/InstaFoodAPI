using System;
using System.Collections.Generic;

namespace InstaFoodAPI.Models
{
    public partial class Pay
    {
        public Pay()
        {
            RestaurantIdPay1Navigations = new HashSet<Restaurant>();
            RestaurantIdPay2Navigations = new HashSet<Restaurant>();
            RestaurantIdPay3Navigations = new HashSet<Restaurant>();
        }

        public int IdPay { get; set; }
        public string MethodPay { get; set; }

        public virtual ICollection<Restaurant> RestaurantIdPay1Navigations { get; set; }
        public virtual ICollection<Restaurant> RestaurantIdPay2Navigations { get; set; }
        public virtual ICollection<Restaurant> RestaurantIdPay3Navigations { get; set; }
    }
}
