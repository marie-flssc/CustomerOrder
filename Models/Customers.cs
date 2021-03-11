using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Models
{
    public class Customers
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int number_orders { get; set; }
    }
}
