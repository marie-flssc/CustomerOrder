using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }

    }
}
