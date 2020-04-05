using ForumSystem.Data.Models;
using ForumSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Web.ViewModels.Posts
{
    public class PostAllModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }

    }
}
