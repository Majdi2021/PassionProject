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
    public class RentalsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RentalsData/ListRentals
        [HttpGet]
        public IEnumerable<RentalDto> ListRentals()
        {
            List<Rental> Rentals = db.Rentals.ToList();
            List<RentalDto> RentalDtos = new List<RentalDto>();

            Rentals.ForEach(a => RentalDtos.Add(new RentalDto()
            {
                RentalId = a.RentalId,
                RentalDate = a.RentalDate,
                ReturnDate = a.ReturnDate,
                ToolId = a.Tool.ToolId,
                ToolName = a.Tool.ToolName,
                CustomerId = a.Customer.CustomerId,
                CustomerName = a.Customer.CustomerName


            }));

            return RentalDtos;
        }

        // GET: api/RentalsData/FindRental/5
        [ResponseType(typeof(Rental))]
        [HttpGet]
        public IHttpActionResult FindRental(int id)
        {
            Rental Rental = db.Rentals.Find(id);
            RentalDto RentalDto = new RentalDto()
            {
                RentalId = Rental.RentalId,
                RentalDate = Rental.RentalDate,
                ReturnDate = Rental.ReturnDate,
                ToolName = Rental.Tool.ToolName,
                CustomerName = Rental.Customer.CustomerName

            };
            if (Rental == null)
            {
                return NotFound();
            }

            return Ok(RentalDto);
        }

        // PUT: api/RentalsData/UpdateRental/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRental(int id, Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rental.RentalId)
            {
                return BadRequest();
            }

            db.Entry(rental).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
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

        // POST: api/RentalsData/AddRental
        [ResponseType(typeof(Rental))]
        [HttpPost]
        public IHttpActionResult AddRental(Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rentals.Add(rental);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rental.RentalId }, rental);
        }

        // DELETE: api/RentalsData/DeleteRental/5
        [ResponseType(typeof(Rental))]
        [HttpPost]
        public IHttpActionResult DeleteRental(int id)
        {
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return NotFound();
            }

            db.Rentals.Remove(rental);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentalExists(int id)
        {
            return db.Rentals.Count(e => e.RentalId == id) > 0;
        }
    }
}