namespace ForumSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    public class PostDeleteModel : IMapTo<Post>, IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
