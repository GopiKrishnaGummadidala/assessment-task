using EIM_assessment.Controllers;
using EIM_assessment.Models;
using EIM_assessment.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EIM_assessment.Tests
{
    public class BoardControllerTests
    {
        private BoardsController controller;
        private Mock<IBoardRepository> boardRepoMock;
        private List<Board> boardsMockData;

        [SetUp]
        public void Setup()
        {
            boardRepoMock = new Mock<IBoardRepository>();
            controller = new BoardsController(boardRepoMock.Object);
            boardsMockData = new List<Board> { new Board { Id = 1, Name = "Board 1", CreatedAt = DateTime.Now }, new Board { Id = 2, Name = "Board 2", CreatedAt = DateTime.Now } };
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

        [Test]
        public async Task Post_SaveBoards()
        {
            boardRepoMock.Setup(x => x.SaveBoards(It.IsAny<List<Board>>())).ReturnsAsync(true);

            var res = await controller.Post(boardsMockData);

            boardRepoMock.Verify(x => x.SaveBoards(boardsMockData), Times.Once);
        }

        [Test]
        public async Task Post_SavePostToBoard()
        {
            boardRepoMock.Setup(x => x.SavePostToBoard(It.IsAny<PostIt>())).ReturnsAsync(true);
            var post = new PostIt() { BoardId = 1, Id = 1, Text = "Sample post", CreatedAt = DateTime.Now };
            var res = await controller.SavePostToBoard(post);

            boardRepoMock.Verify(x => x.SavePostToBoard(post), Times.Once);
        }

        [Test]
        public void Post_Null_SavePostToBoard()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
              return controller.SavePostToBoard(null);
            });
        }

        [Test]
        public void Get_PostsOfBoard()
        {
            var posts = new List<PostIt> { new PostIt() { BoardId = 1, Id = 1, Text = "Sample post", CreatedAt = DateTime.Now }, new PostIt() { BoardId = 1, Id = 2, Text = "Sample post 2", CreatedAt = DateTime.Now } };
            boardRepoMock.Setup(x => x.GetPosts(It.IsAny<int>())).Returns(posts);

            var res = controller.GetPosts(1);

            Assert.AreEqual(res.Count, posts.Count);
        }

        [Test]
        public void DeletePost_ZeroBoardId_ThrowsOutOfRangeException()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
              return  controller.DeletePost(0,1);
            });
        }
        [Test]
        public void DeletePost_ZeroPostId_ThrowsOutOfRangeException()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return controller.DeletePost(1, 0);
            });
        }

        [Test]
        public async Task DeletePost_Positive_DeletePost()
        {
            boardRepoMock.Setup(x => x.DeletePost(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
            var res = await controller.DeletePost(1, 1);
            Assert.IsTrue(res);
        }

        [Test]
        public void DeleteBoard_ZeroBoardId_ThrowsOutOfRangeException()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return controller.DeleteBoard(0);
            });
        }

        public async Task DeleteBoard_Positive()
        {
            boardRepoMock.Setup(x => x.DeleteBoard(It.IsAny<int>())).ReturnsAsync(true);
            var res = await controller.DeleteBoard(1);
            Assert.IsTrue(res);
        }
    }
}
