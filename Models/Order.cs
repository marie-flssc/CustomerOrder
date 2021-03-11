using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public int customer_Id { get; set; }
        public int product_Id { get; set; }
        public int quantity { get; set; }
    }
}
