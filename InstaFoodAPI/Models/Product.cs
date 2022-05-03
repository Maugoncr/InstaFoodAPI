using System;
using System.Collections.Generic;

namespace InstaFoodAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int IdProduct { get; set; }
        public string Name { get; set; }
        public int IdRest { get; set; }
        public int Price { get; set; }
        public string Detail { get; set; }
        public bool? Status { get; set; }
        public bool? Express { get; set; }
        public DateTime? Published { get; set; }
        public string ImgUrl { get; set; }

        public virtual Restaurant IdRestNavigation { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
