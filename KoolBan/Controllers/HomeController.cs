using System.Collections.Generic;
using System.Web.Mvc;
using KoolBan.Models;

namespace KoolBan.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var ondeck = new Column
            {
                ColumnName = "On Deck",
                ColumnId = 0,
                ProjectId = 0,
                Notes = new List<Note>()
            };

            var implement = new Column
            {
                ColumnName = "Implement",
                ColumnId = 1,
                ProjectId = 0,
                Notes = new List<Note>()
            };

            var test = new Column
            {
                ColumnName = "Test",
                ColumnId = 2,
                ProjectId = 0,
                Notes = new List<Note>()
            };

            var done = new Column
            {
                ColumnName = "Done",
                ColumnId = 3,
                ProjectId = 0,
                Notes = new List<Note>()
            };

            var embrace = new Note
            {
                Description = "embrace frictionless metrics",
                NoteColor = NoteColor.Pink,
                Logo =  "glyphicon glyphicon-cog",
                Column = null, 
                ColumnId = 0
            };

            ondeck.Notes.Add(embrace);  

            var actualize = new Note
            {
                Description = "synergistically actualize sustainable initiatives in order to promote sustainable synergy within the market of sticky e-commerce-based revolutionary metrics",
                NoteColor = NoteColor.Crimson,
                Column = null, 
                ColumnId = 0
            };

            ondeck.Notes.Add(actualize);  

            var network = new Note
            {
                Description = "interactively network mission-critical e-business",
                NoteColor = NoteColor.Cobalt,
                Column = null, 
                ColumnId = 0
            };

            ondeck.Notes.Add(network);  

            var architect = new Note
            {
                Description = "progressively architect intermandated technologies",
                NoteColor = NoteColor.Cyan,
                Column = null, 
                ColumnId = 1
            };

            implement.Notes.Add(architect);

            var viral = new Note
            {
                Description = "compellingly monetize viral methods of empowerment",
                NoteColor = NoteColor.Brown,
                Column = null, 
                ColumnId = 1
            };

            implement.Notes.Add(viral);
            implement.Capacity = 2;
            implement.MaxCapacity = 4;

            var drive = new Note
            {
                Description = "progressively drive user friendly internal or 'organic' sources",
                NoteColor = NoteColor.Violet,
                Column = null, 
                ColumnId = 2
            };

            test.Notes.Add(drive);

            var fabricate = new Note
            {
                Description = "appropriately fabricate cross-platform outsourcing",
                NoteColor = NoteColor.Orange,
                Column = null, 
                ColumnId = 2
            };

            test.Notes.Add(fabricate);

            var procrastinate = new Note
            {
                Description = "dynamically procrastinate real-time networks",
                NoteColor = NoteColor.Red,
                Column = null, 
                ColumnId = 2
            };

            test.Notes.Add(procrastinate);
            test.Capacity = 3;
            test.MaxCapacity = 3;

            var productize = new Note
            {
                Description = "compellingly monetize viral methods of empowerment",
                NoteColor = NoteColor.Cyan,
                Column = null, 
                ColumnId = 3
            };

            done.Notes.Add(productize);

            var project = new Project { Name = "Demo",
                IsPublic = false, Password = "", Columns = new List<Column>() };

            project.Columns.Add(ondeck);
            project.Columns.Add(implement);
            project.Columns.Add(test);
            project.Columns.Add(done);

            return View(project);
        }
    }
}
