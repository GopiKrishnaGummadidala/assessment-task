using EIM_assessment.Repository;
using Moq;
using NUnit.Framework;

namespace EIM_assessment.Tests
{
    public class BoardRepositoryTests
    {
        private Mock<IBoardRepository> boardRepoMock;

        [SetUp]
        public void Setup()
        {
            boardRepoMock = new Mock<IBoardRepository>();
        }
    }
}
