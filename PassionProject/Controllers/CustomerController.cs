using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject.Models;
using System.Web.Script.Serialization;

namespace PassionProject.Controllers
{
    public class CustomerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static CustomerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/api/CustomersData/");
        }
        // GET: Customer/List
        public ActionResult List()
        {
            //objective: communicate with our rental data api to retrieve a list of customers
            //crul: https://localhost:44301/api/CustomersData/ListCustomers

            string url = "ListCustomers";

            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<CustomerDto> customers = response.Content.ReadAsAsync<IEnumerable<CustomerDto>>().Result;

            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our rental data api to retrieve one tool
            //crul: https://localhost:44301/api/CustomersData/FindCustomer/{id}

            string url = "FindCustomer/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;
            CustomerDto selectedcustomer = response.Content.ReadAsAsync<CustomerDto>().Result;

            return View(selectedcustomer);
        }
        public ActionResult Error()
        {

            return View();
        }

        // GET: Customer/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            //objective: add a new customer into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/CustomerData/AddCustomer 
            string url = "AddCustomer";


            string jsonpayload = jss.Serialize(customer);
            Debug.WriteLine(jsonpayload);

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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindCustomer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CustomerDto selectedcustomer = response.Content.ReadAsAsync<CustomerDto>().Result;
            return View(selectedcustomer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            string url = "UpdateCustomer/" + id;

            string jsonpayload = jss.Serialize(customer);
            Debug.WriteLine(jsonpayload);
            Debug.WriteLine(jsonpayload);

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

        // GET: Customer/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindCustomer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CustomerDto selectedcustomer = response.Content.ReadAsAsync<CustomerDto>().Result;
            return View(selectedcustomer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {
            string url = "DeleteCustomer/" + id;


            string jsonpayload = jss.Serialize(customer);
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
