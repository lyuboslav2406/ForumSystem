namespace ForumSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> CreateAsync(string title, string content, int categoryId, string userId)
        {
            var post = new Post
            {
                CategoryId = categoryId,
                Content = content,
                Title = title,
                UserId = userId,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
            return post.Id;
        }

        public async Task Delete(Post post)
        {

            this.postsRepository.HardDelete(post);
            await this.postsRepository.SaveChangesAsync();
        }

        public async Task<int> Edit(Post post)
        {
            this.postsRepository.Update(post);
            await this.postsRepository.SaveChangesAsync();
            return post.Id;
        }

        public IEnumerable<T> GetAll<T>(string search = null, int? take = null, int skip = 0, string userName = null)
        {
            var allPosts = this.postsRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                allPosts = allPosts.Take(take.Value);
            }

            if (search != null)
            {
                allPosts = allPosts.Where(a => a.Title.Contains(search));
            }

            if (userName != null)
            {
                allPosts = allPosts.Where(a => a.User.UserName == userName);
            }

            return allPosts.To<T>();
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0)
        {
            var query = this.postsRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(c => c.CategoryId == categoryId).Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            var post = this.postsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return post;
        }

        public int GetCount(string userName = null)
        {
            var allPostCount = this.postsRepository.All().Count();

            if (userName != null)
            {
                allPostCount = this.postsRepository.All().Where(u => u.User.UserName == userName).Count();
            }

            return allPostCount;
        }

        public int GetCountByCategoryId(int categoryId)
        {
            var categoryCount = this.postsRepository.All().Count(x => x.CategoryId == categoryId);
            return categoryCount;
        }

        public string GetUserNameByPostId(int id)
        {
            var postUserName = this.postsRepository.All().Where(p => p.Id == id).FirstOrDefault().User.UserName;

            return postUserName;
        }
    }
}
