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
        public async Task<ActionResult> AddCustomer(OrderDTO orderdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ord = new Order()
            {
                customer_Id = orderdto.Customer_Id,
                product_Id = orderdto.Product_Id,
                quantity = orderdto.Quantity
            };
            await _context.Orders.AddAsync(ord);
            await _context.SaveChangesAsync();
            /*
            var cust = from customers in _context.Customers
            join order in _context.Orders on customers.id equals order.customer_Id
            select new CustomerDTO
            {
                Id =
            };
            Books = book.Where(x => x.Book_id == library.Book_id).ToList()
            await _context.Orders.AddAsync(ord);
            await _context.SaveChangesAsync();*/
            return CreatedAtAction("GetCustomer", new { id = ord.id }, orderdto);
        }
    }
}
