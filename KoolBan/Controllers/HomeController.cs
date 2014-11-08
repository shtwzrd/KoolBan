using System.Collections.Generic;
using System.Web.Mvc;
using KoolBan.Models;
using KoolBan.Models.Abstract;

namespace KoolBan.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public HomeController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        [Route("")]
        [Route("index")]
        [Route("{id}")]
        [HttpGet]
        public ActionResult Index(string id = "Demo")
        {
            Project project = _projectRepository.Find(id);
            if (project != null)
            {
                return View(project);
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        [Route("create/{id}")]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _projectRepository.Create(project);
                _projectRepository.Save();
                // TODO: Route to correct view
            }

            return View("Index");
        }

        [Route("about")]
        public ActionResult About()
        {
            return View();
        }
    }
}