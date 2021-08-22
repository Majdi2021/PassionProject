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
    public class ToolController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static ToolController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/api/ToolsData/");
        }
        // GET: Tool/List
        public ActionResult List()
        {
            //objective: communicate with our rental data api to retrieve a list of tools
            //crul: https://localhost:44301/api/ToolsData/ListTools

            string url = "ListTools";

            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<ToolDto> tools = response.Content.ReadAsAsync<IEnumerable<ToolDto>>().Result;

            //Debug.WriteLine("Number of Rentals received : ");
            //Debug.WriteLine(rentals.Count());
            return View(tools);
        }

        // GET: Tool/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our rental data api to retrieve one tool
            //crul: https://localhost:44301/api/ToolsData/FindTool/{id}

            string url = "FindTool/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;
            ToolDto selectedtool = response.Content.ReadAsAsync<ToolDto>().Result;

            return View(selectedtool);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Tool/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Tool/Create
        [HttpPost]
        public ActionResult Create(Tool tool)
        {
            //objective: add a new tool into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/ToolData/AddTool 
            string url = "AddTool";


            string jsonpayload = jss.Serialize(tool);
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

        // GET: Tool/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindTool/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ToolDto selectedtool = response.Content.ReadAsAsync<ToolDto>().Result;
            return View(selectedtool);
        }

        // POST: Tool/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Tool tool)
        {
            string url = "UpdateTool/" + id;


            string jsonpayload = jss.Serialize(tool);
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

        // GET: Tool/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindTool/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ToolDto selectedtool = response.Content.ReadAsAsync<ToolDto>().Result;
            return View(selectedtool);
        }

        // POST: Tool/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Tool tool)
        {
            string url = "DeleteTool/" + id;


            string jsonpayload = jss.Serialize(tool);
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
