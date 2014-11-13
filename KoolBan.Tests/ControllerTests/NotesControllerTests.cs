using KoolBan.Controllers;
using KoolBan.Models;
using KoolBan.Models.Abstract;
using Moq;
using NUnit.Framework;

namespace KoolBan.Tests.ControllerTests
{
    [TestFixture]
    public class NotesControllerTests
    {
        [Test]
        public void Test_Create()
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();

            NotesController controller = new NotesController(noteRepositoryMock.Object);

            Note note = new Note
            {
                NoteId = 1,
                Description = "Description",
                ColumnId = 1
            };

            // Act
            controller.CreateNote(note);

            // Assert
            noteRepositoryMock.Verify(x => x.Create(note));
            noteRepositoryMock.Verify(x => x.Save());
        }

        [Test]
        public void Test_Update()
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();

            NotesController controller = new NotesController(noteRepositoryMock.Object);

            Note note = new Note
            {
                NoteId = 1,
                Description = "Description",
                ColumnId = 1
            };

            // Act
            controller.UpdateNote(note);

            // Assert
            noteRepositoryMock.Verify(x => x.Edit(note));
            noteRepositoryMock.Verify(x => x.Save());
        }

        [Test]
        public void Test_Delete()
        {
            // Arrange
            var noteRepositoryMock = new Mock<INoteRepository>();

            NotesController controller = new NotesController(noteRepositoryMock.Object);


            // Act
            controller.DeleteNote(1);

            // Assert
            noteRepositoryMock.Verify(x => x.Delete(1));
            noteRepositoryMock.Verify(x => x.Save());
        }
    }
}
