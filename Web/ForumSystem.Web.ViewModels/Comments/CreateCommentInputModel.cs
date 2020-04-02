using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public int PostId { get; set; }

        public string Content { get; set; }
    }
}
