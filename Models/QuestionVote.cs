using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject_StackOverFlowV2.Models {
    public class QuestionVote {
        public int Id { get; set; }
        public bool Vote { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public QuestionVote() {

        }
        public QuestionVote(int questionId, string userId, bool vote) {
            QuestionId = questionId;
            UserId = userId;
            Vote = vote;
        }

    }
}