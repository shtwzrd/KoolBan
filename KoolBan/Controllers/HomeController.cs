using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web.Mvc;
using System.Windows.Forms;
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

        [Route("")]
        [Route("index")]
        [Route("{id}")]
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
            return RedirectToAction("Index");

        }

        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Validate hidden form field from Login.
        [Route("login")]
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

        [Route("about")]
        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        [Route("create/{id}")]
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