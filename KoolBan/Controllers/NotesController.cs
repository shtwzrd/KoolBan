using System.Web.Mvc;
using KoolBan.Models;
using KoolBan.Models.Abstract;
using Newtonsoft.Json;

namespace KoolBan.Controllers
{
    public class NotesController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpPost]
        public JsonResult CreateNote(Note note)
        {
            if (ModelState.IsValid)
            {
                _noteRepository.Create(note);
                _noteRepository.Save();

                return Json(new { result = "HttpPost Successful" });
            }

            return Json(new { result = "HttpPost Failed" });
        }

        [HttpGet]
        public JsonResult ReadNote(int noteId)
        {
            Note note = _noteRepository.Find(noteId);

            var result = new JsonNetResult
            {
                Data = note,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Settings = { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            };
            return result;
        }

        [HttpPost]
        public JsonResult UpdateNote(Note note)
        {
            if (ModelState.IsValid)
            {
                _noteRepository.Edit(note);
                _noteRepository.Save();

                return Json(new { result = "HttpPost Successful" });
            }

            return Json(new { result = "HttpPost Failed" });
        }

        [HttpPost]
        public JsonResult DeleteNote(int noteId)
        {
            _noteRepository.Delete(noteId);
            _noteRepository.Save();

            return Json(new {result = "HttpPost Successful" });
        }
    }

}