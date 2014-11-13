using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using KoolBan.Models;
using Newtonsoft.Json;

namespace KoolBan.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly KoolBanContext _db = new KoolBanContext();


        public JsonResult GetProjectColumnsJson(string projectId)
        {
            var data = _db.Projects.Find(projectId.ToLower()).Columns.Select(
                c => new Column
                {
                    ProjectId = c.ProjectId,
                    ColumnId = c.ColumnId,
                    ColumnName = c.ColumnName,
                    Capacity = c.Capacity,
                    Notes = c.Notes,
                    Priority = c.Priority
                });

            var result = new JsonNetResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Settings = { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            };
            return result;
        } 

        [HttpPost]
        public JsonResult SetProjectColumnsJson(Project project)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(project).State = EntityState.Modified;
                _db.SaveChanges();
                return Json(new {result = "HttpPost successful"});
            }
                return Json(new {result = "HttpPost failed"});
        } 



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
