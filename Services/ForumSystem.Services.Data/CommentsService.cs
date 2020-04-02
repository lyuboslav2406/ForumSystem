using ForumSystem.Data.Common.Repositories;
using ForumSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Services.Data
{
    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(int postId, string userId, string content, int? parentId = null)
        {
            var newComment = new Comment
            {
                Content = content,
                ParentId = parentId,
                PostId = postId,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(newComment);

            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
