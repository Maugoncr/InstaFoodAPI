using System;
using System.Collections.Generic;

namespace InstaFoodAPI.Models
{
    public partial class LogBook
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public string User { get; set; }
        public string Host { get; set; }
        public DateTime? Modify { get; set; }
        public string Table { get; set; }
    }
}
