using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject_StackOverFlowV2.Models {
    public class QuestionTag {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }

        public QuestionTag() {

        }
        public QuestionTag(int questionId, int tagId) {
            QuestionId = questionId;
            TagId = tagId;
        }
    }
}