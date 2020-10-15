using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject_StackOverFlowV2.Models {
    public class SystemHelper {
        static ApplicationDbContext db = new ApplicationDbContext();
        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(db)
            );


        //USERS
        public static ApplicationUser getUserById(string userId) {
            return db.Users.Where(x => x.Id == userId).FirstOrDefault();
        }
        public static string getUserNameById(string userId) {
            return db.Users.Where(x => x.Id == userId).Select(x => x.UserName).FirstOrDefault();
        }

        public static string getUserIdByQuestionId(int questionId) {
            return db.Questions.Where(x => x.Id == questionId).Select(x => x.UserId).FirstOrDefault();
        }

        //TAGS
        public static List<Tag> getAllTag() {
            return db.Tags.ToList();
        }
        public static Tag getTagById(int id) {
            return db.Tags.Where(x => x.Id == id).FirstOrDefault();
        }

        //NAMES
        public static string getUserNameByQuestionId(int questionId) {
            return db.Questions.Where(x => x.Id == questionId).Select(x => x.User.UserName).FirstOrDefault();
        }
        public static string getUserNameByAnswerId(int answerId) {
            return db.Answers.Where(x => x.Id == answerId).Select(x => x.User.UserName).FirstOrDefault();
        }

        //QUESTION
        public static Question getQuestionById(int questionId) {
            return db.Questions.Where(x => x.Id == questionId).FirstOrDefault();
        }

     
        public static int getQuestionIdByUserIdByTitleByContext(string userId, string title, string context) {
            return db.Questions.Where(x => x.UserId == userId && x.Title == title && x.Context == context).Select(x => x.Id).FirstOrDefault();
        }
        public static List<Question> getAllQuestionByUserId(string userId) {
            return db.Questions.Where(x => x.UserId == userId).ToList();
        }
        public static List<Question> getAllQuestionByTagId(int tagId) {
            var listQT = db.QuestionTags.Where(x => x.TagId == tagId).Select(x => x.QuestionId);
            List<Question> listQues = new List<Question>();
            foreach (var item in listQT) {
                var temp = db.Questions.Where(x => x.Id == item).FirstOrDefault();
                if (!listQues.Contains(temp)) {
                    listQues.Add(temp);
                }
            }
            return listQues;
        }
        public static List<Question> getAllQuestion() {

            ///return db.QuestionModels.OrderByDescending(x => x.DateTime).ToList();
            var a = db.Questions;
            var aa = a.Include("User").ToList();
            return aa;

        }

        //ANSWER
        public static Answer getAnswerById(int answerId) {
            return db.Answers.Where(x => x.Id == answerId).FirstOrDefault();
        }
        public static List<Answer> getAllAnswerByUserId(string userId) {
            return db.Answers.Where(x => x.UserId == userId).ToList();
        }

        //QUESTION VOTE
        public static QuestionVote getQuestionVoteByUserIdByQuestionId(int questionId, string userIdVote) {
            return db.QuestionVotes.Where(x => x.QuestionId == questionId && x.UserId == userIdVote).FirstOrDefault();
        }
        public static AnswerVote getAnswerVoteByUserIdByQuestionId(int answerId, string userIdVote) {
            return db.AnswerVotes.Where(x => x.AnswerId == answerId && x.UserId == userIdVote).FirstOrDefault();
        }


        //CALCULATE VOTING
        public static int getQuestionSumTotalVoteById(int questionId) {
            var question = getQuestionById(questionId);
            var voteSum = question.QuestionVotes.Where(x => x.Vote).Count();
            var voteFalse = question.QuestionVotes.Where(x => x.Vote == false).Count();
            return voteSum - voteFalse;
        } 
        public static int getAnswerSumTotalVoteById(int answerId) {
            var answer = getAnswerById(answerId);
            var voteSum = answer.AnswerVotes.Where(x => x.Vote).Count();
            var voteFalse = answer.AnswerVotes.Where(x => x.Vote == false).Count();
            return voteSum - voteFalse;
        }

        //CALCULATE REPUTATION
        public static int getUserReputation(string userId) {
            var user = getUserById(userId);
            var userQuestion = getAllQuestionByUserId(userId); 
            var userAnswer = getAllAnswerByUserId(userId);
            var TrueQuestionVote = userQuestion.SelectMany(x => x.QuestionVotes).Where(x => x.Vote == true).Count();
            var TrueAnswerVote = userAnswer.SelectMany(x => x.AnswerVotes).Where(x => x.Vote == true).Count();
            return (TrueQuestionVote + TrueAnswerVote) * 5;            
        }



        //STORED PROCEDURE
        public static List<AllQuestionOrderByDateViewModel> getAllQuestionOrderByDate() {
            return db.Database.SqlQuery<AllQuestionOrderByDateViewModel>("[dbo].[GetQuestionOrderByDate]").ToList();
        }

        public static QuestionMostReplyViewModel getAllQuestionWithMostAnswer() {
            return db.Database.SqlQuery<QuestionMostReplyViewModel>("[dbo].[GetAllQuestionWithMostAnswer]").FirstOrDefault();
        }
        


        //ADD POST
        public static void addQuestion(string title, string context, 
                DateTime date, string userId, int tag1, int tag2, int tag3,int tag4) {
            db.Questions.Add(new Question(
                    title,
                    context,
                    date,
                    userId
                ));
            db.SaveChanges();
            int questionId = getQuestionIdByUserIdByTitleByContext(userId, title, context);
            addTag(tag1, tag2, tag3, tag4, questionId);
            db.SaveChanges();

        }
        public static void addAnswer(string context, int questionId, string userId) {
            Answer answer = new Answer(context, questionId, userId);
            var question = getQuestionById(questionId);
            question.Answers.Add(answer);
            db.SaveChanges();
        }

        public static void addCommentToQuestion(string context, int questionId, string userId) {
            QuestionComment comment = new QuestionComment(context, userId, questionId);
            var question = getQuestionById(questionId);
            question.QuestionComments.Add(comment);
            db.SaveChanges();
        }

        public static void addCommentToAnswer(string context, int answerId, string userId) {
            var answer = db.Answers.Where(x => x.Id == answerId).FirstOrDefault();
            AnswerComment comment = new AnswerComment(context, userId, answerId);
            answer.AnswerComments.Add(comment);
            db.SaveChanges();
        }

        // IMPLEMENT VOTING
        public static void addUpVoteToQuestion(int questionId, string userIdVote) {         
            var question = getQuestionById(questionId);
            var user = getUserById(question.UserId);
            var questionVote = getQuestionVoteByUserIdByQuestionId(questionId, userIdVote);
            if (questionVote == null && userIdVote != question.UserId) {                
                question.QuestionVotes.Add(new QuestionVote(questionId,userIdVote,true));
                question.TotalVote = getQuestionSumTotalVoteById(question.Id);
                user.Reputation = getUserReputation(user.Id);
                db.SaveChanges();
            } else if (questionVote != null && userIdVote != question.UserId) {
                if (questionVote.Vote == false) {
                    questionVote.Vote = true;
                    question.TotalVote = getQuestionSumTotalVoteById(question.Id);
                    user.Reputation = getUserReputation(user.Id);
                    db.SaveChanges();

                }
            }        
        }

        public static void addDownVoteToQuestion(int questionId, string userIdVote) {
           
            var question = getQuestionById(questionId);
            var user = getUserById(question.UserId);
            var questionVote = getQuestionVoteByUserIdByQuestionId(questionId, userIdVote);
            if (questionVote == null && userIdVote != question.UserId) {
                question.QuestionVotes.Add(new QuestionVote(questionId, userIdVote, false));
                question.TotalVote = getQuestionSumTotalVoteById(question.Id);
                user.Reputation = getUserReputation(user.Id);
                db.SaveChanges();
            } else if (questionVote != null && userIdVote != question.UserId) {
                if(questionVote.Vote == true) {
                    questionVote.Vote = false;
                    question.TotalVote = getQuestionSumTotalVoteById(question.Id);
                    user.Reputation = getUserReputation(user.Id);
                    db.SaveChanges();
                }               
            }
        }

        public static void addUpVoteToAnswer(int answerId, string userIdVote) {
            var answer = getAnswerById(answerId);
            var user = getUserById(answer.UserId);
            var answerVote = getAnswerVoteByUserIdByQuestionId(answerId, userIdVote);
            if (answerVote == null && userIdVote != answer.UserId) {
                answer.AnswerVotes.Add(new AnswerVote(answerId, userIdVote, true));
                answer.TotalVote = getAnswerSumTotalVoteById(answerId);
                user.Reputation = getUserReputation(user.Id);
                db.SaveChanges();
            } else if (answerVote != null && userIdVote != answer.UserId) {
                if (answerVote.Vote == false) {
                    answerVote.Vote = true;
                    answer.TotalVote = getAnswerSumTotalVoteById(answerId);
                    user.Reputation = getUserReputation(user.Id);
                    db.SaveChanges();
                }
            }
        }

        public static void addDownVoteToAnswer(int answerId, string userIdVote) {
            var answer = getAnswerById(answerId);
            var user = getUserById(answer.UserId);
            var answerVote = getAnswerVoteByUserIdByQuestionId(answerId, userIdVote);
            if (answerVote == null && userIdVote != answer.UserId) {
                answer.AnswerVotes.Add(new AnswerVote(answerId, userIdVote, false));
                answer.TotalVote = getAnswerSumTotalVoteById(answerId);
                user.Reputation = getUserReputation(user.Id);
                db.SaveChanges();
            } else if (answerVote != null && userIdVote != answer.UserId) {
                if (answerVote.Vote == true) {
                    answerVote.Vote = false;
                    answer.TotalVote = getAnswerSumTotalVoteById(answerId);
                    user.Reputation = getUserReputation(user.Id);
                    db.SaveChanges();
                }
            }
        }
        

        //ADD QuetionTag
        public static void addTag(int tag1, int tag2, int tag3, int tag4, int questionId) {         
            List<int> listTagId = new List<int>();
            listTagId.Add(tag1);
            listTagId.Add(tag2);
            listTagId.Add(tag3);
            listTagId.Add(tag4);
            List<int> tempId = new List<int>();
            if(listTagId.Count > 0) {
                foreach (var id in listTagId) {
                    if(id != 1 && !tempId.Contains(id)) {
                        tempId.Add(id);
                        addQuestionTag(questionId, id);
                    }                  
                }
            }         
        }

        public static void addQuestionTag(int questionId, int tagId) {
            db.QuestionTags.Add(new QuestionTag(questionId, tagId));
            db.SaveChanges();
        }
        

    }
}