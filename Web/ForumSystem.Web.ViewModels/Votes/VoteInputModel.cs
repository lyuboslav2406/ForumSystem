namespace ForumSystem.Web.ViewModels.Votes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class VoteInputModel
    {
        public int PostId { get; set; }

        public bool IsUpVote { get; set; }
    }
}
