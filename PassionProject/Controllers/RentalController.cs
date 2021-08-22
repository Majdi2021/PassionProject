using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject.Models;
using PassionProject.Models.ViewModels;
using System.Web.Script.Serialization;


namespace PassionProject.Controllers
{
    public class RentalController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static RentalController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/api/");
        }


        // GET: Rental/List
        public ActionResult List()
        {
            //objective: communicate with our rental data api to retrieve a list of rentals
            //crul: https://localhost:44301/api/RentalsData/ListRentals

            string url = "RentalsData/ListRentals";

            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<RentalDto> rentals = response.Content.ReadAsAsync<IEnumerable<RentalDto>>().Result;

            //Debug.WriteLine("Number of Rentals received : ");
            //Debug.WriteLine(rentals.Count());
            return View(rentals);
        }

        // GET: Rental/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our rental data api to retrieve one rental
            //crul: https://localhost:44301/api/RentalsData/FindRental/{id}

            string url = "RentalsData/FindRental/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;
            RentalDto selectedrental = response.Content.ReadAsAsync<RentalDto>().Result;

            //Debug.WriteLine("Number of Rentals received : ");
            return View(selectedrental);
        }

        public ActionResult Error()
        {
            
            return View();
        }


        // GET: Rental/New
        public ActionResult New()
        {

            NewRental viewModel = new NewRental();
            //information about all Tools in the system.
            //GET api/ToolsData/ListTool

            string url = "ToolsData/ListTools";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<ToolDto> toolsoptions = response.Content.ReadAsAsync<IEnumerable<ToolDto>>().Result;

            viewModel.toolsoptions = toolsoptions;

             url = "CustomersData/ListCustomers";
             response = client.GetAsync(url).Result;
             IEnumerable<CustomerDto> customeroptions = response.Content.ReadAsAsync<IEnumerable<CustomerDto>>().Result;

            viewModel.customersoptions = customeroptions;

            return View(viewModel);
        }

       

        // POST: Rental/Create
        [HttpPost]
        public ActionResult Create(Rental rental)
        {
            //objective: add a new rental into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/RentalData/AddRental 
            string url = "RentalsData/AddRental";
            string jsonpayload = jss.Serialize(rental);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Rental/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateRental ViewModel = new UpdateRental();

            //the existing animal information
            string url = "RentalsData/FindRental/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RentalDto selectedrental = response.Content.ReadAsAsync<RentalDto>().Result;
            ViewModel.selectedrental = selectedrental;

            //information about all Tools in the system.
            //GET api/ToolsData/ListTool

             url = "ToolsData/ListTools";
             response = client.GetAsync(url).Result;
            IEnumerable<ToolDto> toolsoptions = response.Content.ReadAsAsync<IEnumerable<ToolDto>>().Result;

            ViewModel.toolsoptions = toolsoptions;

            url = "CustomersData/ListCustomers";
            response = client.GetAsync(url).Result;
            IEnumerable<CustomerDto> customeroptions = response.Content.ReadAsAsync<IEnumerable<CustomerDto>>().Result;

            ViewModel.customersoptions = customeroptions;

            return View(ViewModel);

        }

        // POST: Rental/UpdateRental/5
        [HttpPost]
        public ActionResult Update(int id, Rental rental)
        {
            string url = "RentalsData/UpdateRental/" + id;
            string jsonpayload = jss.Serialize(rental);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            
            if (response.IsSuccessStatusCode)
            {
                //Debug.WriteLine(jsonpayload);
                return RedirectToAction("List");

            }
            else
            {
                Debug.WriteLine(response);
                return RedirectToAction("Error");
            }
        }


        // GET: Rental/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "RentalsData/FindRental/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RentalDto selectedrental = response.Content.ReadAsAsync<RentalDto>().Result;
            return View(selectedrental);
        }

        // POST: Rental/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Rental rental)
        {
            string url = "RentalsData/DeleteRental/" + id;


            string jsonpayload = jss.Serialize(rental);
            //Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
