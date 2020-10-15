using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseProject_StackOverFlowV2.Models {
    public class Answer {
        public Answer() {

        }
        public Answer(string context, int questionId, string userId) {
            Context = context;
            QuestionId = questionId;
            UserId = userId;
            TotalVote = 0;
            AnswerComments = new HashSet<AnswerComment>();
            AnswerVotes = new HashSet<AnswerVote>();
          
        }
        public int Id { get; set; }
        public string Context { get; set; }
        public bool BestAnswer { get; set; }
        public int TotalVote { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<AnswerComment> AnswerComments { get; set; }
        public virtual ICollection<AnswerVote> AnswerVotes { get; set; }
        
    }
}