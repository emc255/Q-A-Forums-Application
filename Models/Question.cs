using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject_StackOverFlowV2.Models {
    public class Question {
        public Question() {
        }
        public Question(string title, string context, DateTime date, string userId) {
            Title = title;
            Context = context;
            DateTime = date;
            UserId = userId;
            TotalVote = 0;
            Answers = new HashSet<Answer>();
            QuestionComments = new HashSet<QuestionComment>();
            QuestionTags = new HashSet<QuestionTag>();
            QuestionVotes = new HashSet<QuestionVote>();
            
        }
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public string Title { get; set; }
        public string Context { get; set; }
        public string UserId { get; set; }
        public int TotalVote { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<QuestionVote> QuestionVotes { get; set; }
        public virtual ICollection<QuestionTag> QuestionTags { get; set; }  
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionComment> QuestionComments { get; set; }
    }
}