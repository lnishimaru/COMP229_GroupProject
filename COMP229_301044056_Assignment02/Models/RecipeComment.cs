using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models
{
    public class RecipeComment
    {
        public int RecipeCommentId { get; set; }
        public int RecipeID { get; set; }
        public string Comments { get; set; }
        public string UserId { get; set; }
        public DateTime CommentDateTime { get; set; } = DateTime.Now;
    }
}
