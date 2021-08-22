using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Tool
    {
        [Key]
        public int ToolId { get; set; }
        public string ToolName { get; set; }


        //A tool can be rented by one customer
        //public ICollection<Rental> Rentals { get; set; }




    }

    public class ToolDto
    {
        public int ToolId { get; set; }
        public string ToolName { get; set; }


    }
}