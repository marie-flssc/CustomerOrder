﻿using Microsoft.AspNetCore.Mvc;
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
    public class CustomersController : ControllerBase
    {
        private readonly Context _context;

        public CustomersController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }


        
        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetCustomers_byId(int id)
        {
            var ords =  _context.Orders;
            List<Order> car = ords.Where(x => x.customer_Id == id).ToList();
            
            var cust = from customers in _context.Customers
            select new CustomerDTO
            {
                Id = customers.id,
                Name =customers.name,
                Number_orders = customers.number_orders,
                Cart = car
                
            };
            var current_cust = cust.ToList().Find(x => x.Id == id);

            if (current_cust == null)
            {
                return NotFound();
            }
            return current_cust;
        }

        [HttpPost]
        public async Task<ActionResult<Customers>> Post_CustV2(Customers values)
        {
            _context.Customers.Add(values);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomers", new { id = values.id }, values);
        }
    }
}
