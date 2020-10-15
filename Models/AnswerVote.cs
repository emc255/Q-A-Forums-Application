using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject_StackOverFlowV2.Models {
    public class AnswerVote {
        public int Id { get; set; }
        public bool Vote { get; set; }

        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public AnswerVote() {

        }
        public AnswerVote(int answerId, string userId, bool vote) {
            AnswerId = answerId;
            UserId = userId;
            Vote = vote;
        }
    }

    
}