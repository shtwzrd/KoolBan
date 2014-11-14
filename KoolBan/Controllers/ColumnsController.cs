
using System.Web.Mvc;
using KoolBan.Models;
using KoolBan.Models.Abstract;
using Newtonsoft.Json;

namespace KoolBan.Controllers
{
    public class ColumnsController : Controller
    {
        private readonly IColumnRepository _columnRepository;

        public ColumnsController(IColumnRepository columnRepository)
        {
            _columnRepository = columnRepository;
        }

        [HttpPost]
        public JsonResult CreateColumn(Column column)
        {
            if (ModelState.IsValid)
            {
                _columnRepository.Create(column);
                _columnRepository.Save();

                return Json(new { result = "HttpPost Successful" });
            }

            return Json(new { result = "HttpPost Failed" });
        }

        [HttpGet]
        public JsonResult ReadColumn(int columnId)
        {
            Column column = _columnRepository.Find(columnId);

            var result = new JsonNetResult
            {
                Data = column,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Settings = { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            };
            return result;
        }

        [HttpPost]
        public JsonResult UpdateColumn(Column column)
        {
            if (ModelState.IsValid)
            {
                _columnRepository.Edit(column);
                _columnRepository.Save();

                return Json(new { result = "HttpPost Successful" });
            }

            return Json(new { result = "HttpPost Failed" });
        }

        [HttpPost]
        public JsonResult DeleteColumn(int columnId)
        {
            _columnRepository.Delete(columnId);
            _columnRepository.Save();

            return Json(new { result = "HttpPost Successful" });
        }
    }
}