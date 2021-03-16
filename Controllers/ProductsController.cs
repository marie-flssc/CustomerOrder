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
    public class ProductsController : ControllerBase
    {
        private readonly Context _context;

        public ProductsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(ProductDTO prod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roduct = new Product()
            {
                price = prod.Price,
                name = prod.Name,
                description = prod.Description
            };
            await _context.Products.AddAsync(roduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = roduct.id }, prod);
        }
    }
}
