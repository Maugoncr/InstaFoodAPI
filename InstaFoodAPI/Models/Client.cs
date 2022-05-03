using System;
using System.Collections.Generic;

namespace InstaFoodAPI.Models
{
    public partial class Client
    {
        public Client()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int IdClient { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Strike { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
