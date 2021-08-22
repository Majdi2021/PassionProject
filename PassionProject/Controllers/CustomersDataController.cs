using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class CustomersDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CustomersData/ListCustomers
        [HttpGet]
        public IEnumerable<CustomerDto> ListCustomers()
        {
            List<Customer> customers = db.Customers.ToList();
            List<CustomerDto> CustomerDtos = new List<CustomerDto>();

            customers.ForEach(a => CustomerDtos.Add(new CustomerDto()
            {
                CustomerId = a.CustomerId,
                CustomerName = a.CustomerName,
                CustomerPhone = a.CustomerPhone

            }));

            return CustomerDtos;
        }

        // GET: api/CustomersData/FindCustomer/5
        [ResponseType(typeof(Customer))]
        [HttpGet]
        public IHttpActionResult FindCustomer(int id)
        {
            Customer Customer = db.Customers.Find(id);
            CustomerDto CustomerDto = new CustomerDto()
            {
                CustomerId = Customer.CustomerId,
                CustomerName = Customer.CustomerName,
                CustomerPhone = Customer.CustomerPhone

            };
            if (Customer == null)
            {
                return NotFound();
            }

            return Ok(CustomerDto);
        }

        // Post: api/CustomersData/UpdateCustomer/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CustomersData/AddCustomer
        [ResponseType(typeof(Customer))]
        [HttpPost]
        public IHttpActionResult AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
        }

        // POST: api/CustomersData/DeleteCustomer/5
        [ResponseType(typeof(Customer))]
        [HttpPost]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerId == id) > 0;
        }
    }
}