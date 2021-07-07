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
        private BoardsController controller;
        private Mock<IBoardRepository> boardRepoMock;

        [SetUp]
        public void Setup()
        {
            boardRepoMock = new Mock<IBoardRepository>();
            controller = new BoardsController(boardRepoMock.Object);
        }

        [Test]
        public void Constructor_CreatesController()
        {
            var controller = new BoardsController(boardRepoMock.Object);
            Assert.NotNull(controller);
        }

        [Test]
        public void GetAll_DoesLookupThroughRepository()
        {
            controller.GetAll();
            boardRepoMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void Find_NegativeId_ThrowsOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                controller.Find(-1);
            });
        }

        [Test]
        public void Find_ZeroId_ThrowsOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                controller.Find(0);
            });
        }

        [Test]
        public void Find_ValidId_DoesLookupThroughRepository()
        {
            boardRepoMock.Setup(x => x.Find(It.IsAny<int>())).Returns(new Board());

            controller.Find(1);

            boardRepoMock.Verify(x => x.Find(1), Times.Once);
        }
    }
}
