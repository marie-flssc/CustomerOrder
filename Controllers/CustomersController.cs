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
        /*
        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetCustomers_byId(int id)
        {
            var cust = from customers in _context.Customers
                       join order in _context.Orders on customers.id equals order.customer_Id
                       select new CustomerDTO
                       {
                           Id = 
                       };

            var student = from students in _context.Students
                          join students_descriptions in _context.students_Description on students.Id equals students_descriptions.Students_id
                          join library in _context.Library on students.Id equals library.Student_id
                          select new StudentDetailsDTO
                          {
                              Student_id = students.Id,
                              Age = students_descriptions.age,
                              First_name = students_descriptions.first_name,
                              Last_name = students_descriptions.last_name,
                              Address = students_descriptions.address,
                              Country = students_descriptions.country,
                              Grade = students.grade,
                              Books = book.Where(x => x.Book_id == library.Book_id).ToList()
                          };

            var student_by_id = student.ToList().Find(x => x.Student_id == id);

            if (student_by_id == null)
            {
                return NotFound();
            }
            return student_by_id;
        }*/

        [HttpPost]
        public async Task<ActionResult> AddCustomer(NewCustomer customer)
        {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             var cust = new Customers()
             {
                number_orders = customer.Number_orders,
                name = customer.Name
             };
             await _context.Customers.AddAsync(cust);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetCustomer", new { id = cust.id }, customer);
         }
    }
}
