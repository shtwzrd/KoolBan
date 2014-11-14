using System.Web.Mvc;
using KoolBan.Models;
using KoolBan.Models.Abstract;
using KoolBan.Models.Security;

namespace KoolBan.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public HomeController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public ActionResult Index(string id = "Demo")
        {
            Project project = _projectRepository.Find(id);
            if (project != null)
            {
                if (project.IsPrivate && !PasswordHash.ValidatePassword((string)Session["project-Authentication"], project.Password))
                {
                    Session["project-password"] = project.Password;
                    Session["project-name"] = (string)project.ProjectId;
                    return RedirectToAction("Login");
                }
                Session["project-Authentication"] = "";
                return View(project);
            }
            return RedirectToRoute("Demo");
        }

//        [RequireHttps]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Validate hidden form field from Login.
//        [RequireHttps] // Enable for secure HTTPS connection
        public ActionResult Login(string pwd)
        {
            Session["project-Authentication"] = pwd;

            if (PasswordHash.ValidatePassword(pwd, (string)Session["project-password"]))
            {
                return RedirectToAction("Index", new { id = (string)Session["project-name"] });
            }

            Session["project-Authentication"] = "";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.Password = PasswordHash.CreateHash(project.Password);
                _projectRepository.Create(project);
                _projectRepository.Save();
                return RedirectToAction("Index", new { id = project.ProjectId });
            }

            return View("Index");
        }
    }
}