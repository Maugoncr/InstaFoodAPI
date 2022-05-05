using System;
using System.Collections.Generic;

namespace InstaFoodAPI.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Products = new HashSet<Product>();
        }

        public int IdRest { get; set; }
        public string NameRest { get; set; }
        public int IdProv { get; set; }
        public int IdTown { get; set; }
        public int IdDistrict { get; set; }
        public string Adress { get; set; }
        public string Schedule { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? IdPay1 { get; set; }
        public string NumPay1 { get; set; }
        public int? IdPay2 { get; set; }
        public string NumPay2 { get; set; }
        public int? IdPay3 { get; set; }
        public string NumPay3 { get; set; }
        public int? Strike { get; set; }
        public bool? Active { get; set; }

        public virtual District IdDistrictNavigation { get; set; }
        public virtual Pay IdPay1Navigation { get; set; }
        public virtual Pay IdPay2Navigation { get; set; }
        public virtual Pay IdPay3Navigation { get; set; }
        public virtual Province IdProvNavigation { get; set; }
        public virtual Town IdTownNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
