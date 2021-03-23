using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Data;
using Orders.DTO;
using Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly Context _context;

        public OrdersController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder_ById(int id)
        {
            var ord = await _context.Orders.FindAsync(id);
            if (ord != null)
            {
                return ord;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(OrderDTO ord)
        {

            var values = await _context.Customers.FindAsync(ord.Customer_Id);
            if(values == null)
            {
                return NotFound();
            }
            var cust = await _context.Customers.SingleOrDefaultAsync(x => x.id == ord.Customer_Id);
            cust.number_orders +=1;
            await _context.SaveChangesAsync();
            Order order= new Order{
                customer_Id = ord.Customer_Id,
                product_Id = ord.Product_Id,
                quantity = ord.Quantity
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = order.id }, order);
        }
    }
}
