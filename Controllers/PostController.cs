using CourseProject_StackOverFlowV2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject_StackOverFlowV2.Controllers
{
    [Authorize(Roles = "User")]
    public class PostController : Controller
    {
        // GET: Post
        [HttpGet]
        public ActionResult AskQuestion() {
            var tags = SystemHelper.getAllTag();
            ViewBag.tagId = new SelectList(tags, "Id", "Name");
            ViewBag.tagId2 = new SelectList(tags, "Id", "Name");
            ViewBag.tagId3 = new SelectList(tags, "Id", "Name");
            ViewBag.tagId4 = new SelectList(tags, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AskQuestion(string title, string context, int tagId, int tagId2, int tagId3, int tagId4) {
            var tags = SystemHelper.getAllTag();
            ViewBag.tagId = new SelectList(tags, "Id", "Name");
            ViewBag.tagId2 = new SelectList(tags, "Id", "Name");
            ViewBag.tagId3 = new SelectList(tags, "Id", "Name");
            ViewBag.tagId4 = new SelectList(tags, "Id", "Name");

            if (title == "" && context == "") {
                ViewBag.formError = "Incomplete Form";
                return View("AskQuestion");
            } else if (context == "") {
                ViewBag.formError = "Please Fill Description";
                return View("AskQuestion");
            } else if (title == "") {
                ViewBag.formError = "Please Fill Title";
                return View("AskQuestion");
            } else {
                var userId = User.Identity.GetUserId();
                SystemHelper.addQuestion(title, context, DateTime.Now, userId, tagId, tagId2, tagId3,tagId4);
            }
            //return RedirectToAction("sample", "Home");
           return RedirectToAction("AuthorizeUserPage", "Home");
        }

        [HttpGet]
        public ActionResult AnswerQuestion(int questionId) {
            var question = SystemHelper.getQuestionById(questionId);
            ViewBag.title = question.Title;
            ViewBag.userName = SystemHelper.getUserNameByQuestionId(questionId);
            ViewBag.description = question.Context;
            return View();
        }
        [HttpPost]
        public ActionResult AnswerQuestion(int questionId, string context) {
            var question = SystemHelper.getQuestionById(questionId);
            var userId = User.Identity.GetUserId();
            if (context != "") {
                SystemHelper.addAnswer(context, questionId, userId);
                return RedirectToAction("AuthorizeUserPage", "Home");
            } else {
                ViewBag.formError = "Please Fill Answer Box";
                ViewBag.title = question.Title;
                ViewBag.userName = SystemHelper.getUserNameByQuestionId(questionId);
                ViewBag.description = question.Context;
                return View("AnswerQuestion");
            }

        }

        [HttpGet]
        public ActionResult CommentToQuestion(int questionId) {
            var question = SystemHelper.getQuestionById(questionId);
            ViewBag.title = question.Title;
            ViewBag.userName = SystemHelper.getUserNameByQuestionId(questionId);
            ViewBag.description = question.Context;
            return View();
        }
        [HttpPost]
        public ActionResult CommentToQuestion(int questionId, string context) {
            var question = SystemHelper.getQuestionById(questionId);
            var userId = User.Identity.GetUserId();
            if (context != "") {
                SystemHelper.addCommentToQuestion(context, questionId, userId);
                return RedirectToAction("AuthorizeUserPage", "Home");
            } else {
                ViewBag.formError = "Please Fill Comment Box";
                ViewBag.title = question.Title;
                ViewBag.userName = SystemHelper.getUserNameByQuestionId(questionId);
                ViewBag.description = question.Context;
                return View("CommentToQuestion");
            }

        }
        [HttpGet]
        public ActionResult CommentToAnswer(int answerId) {
            var answer = SystemHelper.getAnswerById(answerId);
            ViewBag.userName = SystemHelper.getUserNameByAnswerId(answerId);
            ViewBag.description = answer.Context;
            return View();
        }
        [HttpPost]
        public ActionResult CommentToAnswer(int answerId, string context) {
            var answer = SystemHelper.getAnswerById(answerId);
            var userId = User.Identity.GetUserId();
            if (context != "") {
                SystemHelper.addCommentToAnswer(context, answerId, userId);
                return RedirectToAction("AuthorizeUserPage", "Home");
            } else {
                ViewBag.formError = "Please Fill Comment Box";
                ViewBag.userName = SystemHelper.getUserNameByAnswerId(answerId);
                ViewBag.description = answer.Context;
                return View("CommentToAnswer");
            }

        }
    }
}