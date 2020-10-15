using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject_StackOverFlowV2.Models {
    public class AnswerComment {
        public int Id { get; set; }
        public string Context { get; set; }

        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public AnswerComment(string context, string userId, int answerId) {
            Context = context;
            UserId = userId;
            AnswerId = answerId;
        }
        public AnswerComment() {
          
        }
    }
}