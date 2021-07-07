using EIM_assessment.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EIM_assessment.Repository
{
    public interface IBoardRepository
    {
        IQueryable<Board> GetAll();
        Task<bool> SaveBoards(List<Board> boards);
        Task<bool> SavePostToBoard(PostIt post);
        List<PostIt> GetPosts(int boardId);
        IQueryable<PostIt> DeletePost(int postId);
        IQueryable<Board> DeleteBoard(int boardId);
        Board Find(int id);

    }

    public class BoardRepository : IBoardRepository
    {
        private List<Board> _boards;

        public BoardRepository()
        {
            _boards = GetBoardsFromFile();
        }

        private List<Board> GetBoardsFromFile()
        {
            var filePath = Application.Configuration["DataFile"];
            if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            var json = System.IO.File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<Board>>(json);
        }

        public IQueryable<Board> GetAll()
        {
            return _boards.AsQueryable();
        }

        public Board Find(int id)
        {
            return _boards.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> SaveBoards(List<Board> boards)
        {
            _boards = boards;
            return await SaveToFile();
        }

        public bool SavePostToBoard(PostIt post)
        {
            throw new NotImplementedException();
        }

        public List<PostIt> GetPosts(int boardId)
        {
            return boards.FirstOrDefault(b => b.Id == boardId)?.Posts;
        }

        public IQueryable<PostIt> DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Board> DeleteBoard(int boardId)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> SaveToFile()
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(boards);
                var filePath = Application.Configuration["DataFile"];
                if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                // Write the specified text asynchronously to a new file named "WriteTextAsync.txt".
                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    await outputFile.WriteAsync(json);
                    return true;
                }
            }
            catch(Exception ex)
            {
                // to do exception Logging
                return false;
            }
        }
    }
}
