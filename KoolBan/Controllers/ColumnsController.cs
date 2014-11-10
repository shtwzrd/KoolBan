using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using KoolBan.Models;

namespace KoolBan.Controllers
{
    public class ColumnsController : Controller
    {
        private KoolBanContext db = new KoolBanContext();

        // GET: Columns
        public ActionResult Index()
        {
            var columns = db.Columns.Include(c => c.Project);
            return View(columns.ToList());
        }

        // GET: Columns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Column column = db.Columns.Find(id);
            if (column == null)
            {
                return HttpNotFound();
            }
            return View(column);
        }

        // GET: Columns/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectId");
            return View();
        }

        // POST: Columns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColumnId,ColumnName,ProjectId,Priority,Capacity")] Column column)
        {
            if (ModelState.IsValid)
            {
                db.Columns.Add(column);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Password", column.ProjectId);
            return View(column);
        }

        // GET: Columns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Column column = db.Columns.Find(id);
            if (column == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Password", column.ProjectId);
            return View(column);
        }

        // POST: Columns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColumnId,ColumnName,ProjectId,Priority,Capacity")] Column column)
        {
            if (ModelState.IsValid)
            {
                db.Entry(column).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Password", column.ProjectId);
            return View(column);
        }

        // GET: Columns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Column column = db.Columns.Find(id);
            if (column == null)
            {
                return HttpNotFound();
            }
            return View(column);
        }

        // POST: Columns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Column column = db.Columns.Find(id);
            db.Columns.Remove(column);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
