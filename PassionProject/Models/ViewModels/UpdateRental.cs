using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models.ViewModels
{
    public class UpdateRental
    {
        //This viewmodel is a class which stores information that we need to add new to /Rental/New/


        //the Tools list 
        public IEnumerable<ToolDto> toolsoptions { get; set; }

        //the Customers list 
        public IEnumerable<CustomerDto> customersoptions { get; set; }

        public RentalDto selectedrental { get; set; }
    }
}