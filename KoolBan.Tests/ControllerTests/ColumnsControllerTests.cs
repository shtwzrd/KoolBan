using KoolBan.Controllers;
using KoolBan.Models;
using KoolBan.Models.Abstract;
using Moq;
using NUnit.Framework;

namespace KoolBan.Tests.ControllerTests
{
    [TestFixture]
    public class ColumnsControllerTests
    {
        [Test]
        public void Test_Create()
        {
            // Arrange
            var columnRepositoryMock = new Mock<IColumnRepository>();

            ColumnsController controller = new ColumnsController(columnRepositoryMock.Object);

            Column column = new Column
            {
                ColumnId = 1,
                ColumnName = "MyColumn",
                ProjectId = "MyProject"
            };

            // Act
            controller.CreateColumn(column);

            // Assert
            columnRepositoryMock.Verify(x => x.Create(column));
            columnRepositoryMock.Verify(x => x.Save());
        }

        [Test]
        public void Test_Update()
        {
            // Arrange
            var columnRepositoryMock = new Mock<IColumnRepository>();

            ColumnsController controller = new ColumnsController(columnRepositoryMock.Object);

            Column column = new Column
            {
                ColumnId = 1,
                ColumnName = "MyColumn",
                ProjectId = "MyProject"
            };

            // Act
            controller.UpdateColumn(column);

            // Assert
            columnRepositoryMock.Verify(x => x.Edit(column));
            columnRepositoryMock.Verify(x => x.Save());
        }

        [Test]
        public void Test_Delete()
        {
            // Arrange
            var columnRepositoryMock = new Mock<IColumnRepository>();

            ColumnsController controller = new ColumnsController(columnRepositoryMock.Object);

            
            // Act
            controller.DeleteColumn(1);

            // Assert
            columnRepositoryMock.Verify(x => x.Delete(1));
            columnRepositoryMock.Verify(x => x.Save());
        }
    }
}
