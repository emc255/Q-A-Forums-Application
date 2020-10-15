using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject_StackOverFlowV2.Models {
    public class QuestionComment {
        public int Id { get; set; }
        public string Context { get; set; }
        
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public QuestionComment(string context, string userId, int questionId) {
            Context = context;
            UserId = userId;
            QuestionId = questionId;

        }
        public QuestionComment() {
            

        }
    }
}