using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIM_assessment.Models;
using EIM_assessment.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EIM_assessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardRepository _boardRepository;

        public BoardsController(IBoardRepository boardRepository)
        {
            this._boardRepository = boardRepository;
        }

        [HttpGet]
        public IEnumerable<Board> GetAll()
        {
            return _boardRepository.GetAll();
        }

        [HttpPost]
        public async Task<bool> Post(List<Board> boards)
        {
            return await _boardRepository.SaveBoards(boards);
        }

        [HttpPost("{post}")]
        public async Task<bool> SavePostToBoard(PostIt post)
        {
            return await _boardRepository.SavePostToBoard(post);
        }

        [HttpGet("{boardId}")]
        public IEnumerable<PostIt> SavePostToBoard(int boardId)
        {
            return _boardRepository.GetPosts(boardId);
        }

        [HttpDelete("{postId}")]
        public IEnumerable<PostIt> DeletePost(int postId)
        {
            return _boardRepository.DeletePost(postId);
        }

        [HttpDelete("{boardId}")]
        public IEnumerable<Board> DeleteBoard(int boardId)
        {
            return _boardRepository.DeleteBoard(boardId);
        }

        [HttpGet("{id}")]
        public Board Find(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Board ID must be greater than zero.");

            return _boardRepository.Find(id);
        }
    }
}
