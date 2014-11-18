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
            Project project = _projectRepository.Find(id); // Get the project
            if (project != null) // Does it exist?
            {
                // Checks if the password the user typed in is the same as the project's password
                if (project.IsPrivate && !PasswordHash.ValidatePassword((string)Session["project-Authentication"], project.Password))
                {
                    // Store the project's name and password for later use
                    Session["project-password"] = project.Password; 
                    Session["project-name"] = (string)project.ProjectId;
                    return RedirectToAction("Login");
                }
                // If passwords match, clear the user's password and move to project
                Session["project-Authentication"] = "";
                return View(project);
            }
            // Default project
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
            Session["project-Authentication"] = pwd; // Store the user's password

            // Check's if the user's password matches the project's password
            if (PasswordHash.ValidatePassword(pwd, (string)Session["project-password"]))
            {
                // If match, redirect to project
                return RedirectToAction("Index", new { id = (string)Session["project-name"] }); 
            }

            Session["project-Authentication"] = ""; // Else repeat

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}