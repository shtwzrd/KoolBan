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
        [Route("home")]
        [Route("home/index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
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
        [Route("home/about")]
        public ActionResult About()
        {
            return View();
        }
    }
}
