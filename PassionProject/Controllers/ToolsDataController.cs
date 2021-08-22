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
    public class ToolsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ToolsData/ListTools
        [HttpGet]
        public IEnumerable<ToolDto> ListTools()
        {
            List<Tool> Tools = db.Tools.ToList();
            List<ToolDto> ToolDtos = new List<ToolDto>();

            Tools.ForEach(a => ToolDtos.Add(new ToolDto()
            {
                ToolId = a.ToolId,
                ToolName = a.ToolName
               
            }));

            return ToolDtos;
        }

        // GET: api/ToolsData/FindTool/5
        [ResponseType(typeof(Tool))]
        [HttpGet]
        public IHttpActionResult FindTool(int id)
        {
            Tool Tool = db.Tools.Find(id);
            ToolDto ToolDto = new ToolDto()
            {
                ToolId = Tool.ToolId,
                ToolName = Tool.ToolName

            };
            if (Tool == null)
            {
                return NotFound();
            }

            return Ok(ToolDto);
        }

        // POST: api/ToolsData/UpdateTool/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTool(int id, Tool tool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tool.ToolId)
            {
                return BadRequest();
            }

            db.Entry(tool).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolExists(id))
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

        // POST: api/ToolsData/AddTool
        [ResponseType(typeof(Tool))]
        [HttpPost]
        public IHttpActionResult AddTool(Tool tool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tools.Add(tool);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tool.ToolId }, tool);
        }

        // POST: api/ToolsData/DeleteTool/5
        [ResponseType(typeof(Tool))]
        [HttpPost]
        public IHttpActionResult DeleteTool(int id)
        {
            Tool tool = db.Tools.Find(id);
            if (tool == null)
            {
                return NotFound();
            }

            db.Tools.Remove(tool);
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

        private bool ToolExists(int id)
        {
            return db.Tools.Count(e => e.ToolId == id) > 0;
        }
    }
}