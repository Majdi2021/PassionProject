using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Rental
    {
        [Key] public int RentalId { get; set; }

        //A tool belongs to one rental, and one customer
        //A rental can have many tools, and one customer
        //A customer can have many tools, and one rental

        [ForeignKey("Customer")] public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Tool")] public int ToolId { get; set; }
        public virtual Tool Tool { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime? ReturnDate { get; set; }

    }

    public class RentalDto
    {
        public int RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int ToolId { get; set; }
        public string ToolName { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }


    }
}