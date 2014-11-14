using System.Web.Mvc;
using KoolBan.Models;
using KoolBan.Models.Abstract;
using Newtonsoft.Json;

namespace KoolBan.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpPost]
        public JsonResult CreateProject(Project project)
        {
            if (ModelState.IsValid)
            {
                _projectRepository.Create(project);
                _projectRepository.Save();

                return Json(new { result = "HttpPost Successful" });
            }

            return Json(new { result = "HttpPost Failed" });
        }

        [HttpGet]
        public JsonResult ReadProject(string projectId)
        {
            var project = _projectRepository.Find(projectId);

            var result = new JsonNetResult
            {
                Data = project,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Settings = { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            };
            return result;
        }

        [HttpPost]
        public JsonResult UpdateProject(Project project)
        {
            if (ModelState.IsValid)
            {
                _projectRepository.Edit(project);
                _projectRepository.Save();

                return Json(new { result = "HttpPost Successful" });
            }

            return Json(new { result = "HttpPost Failed" });
        }
    }
}
