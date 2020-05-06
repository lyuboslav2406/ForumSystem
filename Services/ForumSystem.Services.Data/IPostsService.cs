using ForumSystem.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumSystem.Services.Data
{
    public interface IPostsService
    {
        Task<int> CreateAsync(string title, string content, int categoryId, string userId);

        Task<int> Edit(Post post);

        Task Delete(Post post);

        T GetById<T>(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);

        IEnumerable<T> GetAll<T>(string search = null, int? take = null, int skip = 0, string userName = null);

        int GetCount(string userName = null);

        string GetUserNameByPostId(int id);
    }
}