using EIM_assessment.Controllers;
using EIM_assessment.Models;
using EIM_assessment.Repository;
using Moq;
using NUnit.Framework;
using System;

namespace EIM_assessment.Tests
{
    public class BoardControllerTests
    {
        [Test]
        public void Constructor_CreatesController()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.NotNull(controller);
        }

        [Test]
        public void GetAll_DoesLookupThroughRepository()
        {
            var boardRepo = new Mock<IBoardRepository>();
            var controller = new BoardsController(boardRepo.Object);

            controller.GetAll();

            boardRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void Find_NegativeId_ThrowsOutOfRangeException()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                controller.Find(-1);
            });
        }

        [Test]
        public void Find_ZeroId_ThrowsOutOfRangeException()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                controller.Find(0);
            });
        }

        [Test]
        public void Find_ValidId_DoesLookupThroughRepository()
        {
            var boardRepo = new Mock<IBoardRepository>();
            boardRepo.Setup(x => x.Find(It.IsAny<int>())).Returns(new Board());

            var controller = new BoardsController(boardRepo.Object);

            controller.Find(1);

            boardRepo.Verify(x => x.Find(1), Times.Once);
        }
    }
}
