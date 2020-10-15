using CourseProject_StackOverFlowV2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;


namespace CourseProject_StackOverFlowV2.Controllers {
    public class HomeController : Controller {
        // GET "EmpSearch"
        public ActionResult Index(int? page) {
            if (User.Identity.GetUserId() != null) {
                return RedirectToAction("AuthorizeUserPage");
            }
            ViewBag.tag = SystemHelper.getAllTag();
            List<Question> display = SystemHelper.getAllQuestion();

            return View(display.ToPagedList(page?? 1,3));
        }

        [Authorize(Roles = "User")]
        public ActionResult AuthorizeUserPage(int? page) {
            ViewBag.tag = SystemHelper.getAllTag();
            List<Question> display = SystemHelper.getAllQuestion();         
            return View(display.ToPagedList(page ?? 1, 3));
        }

        public ActionResult QuestionByTagName(int tagId, int? page) {
            ViewBag.tag = SystemHelper.getAllTag();
            List<Question> display = SystemHelper.getAllQuestionByTagId(tagId);
            return View(display.ToPagedList(page ?? 1, 3));
        }

        public ActionResult GetQuestionOrderByDate(int? page ) {
            ViewBag.tag = SystemHelper.getAllTag();
            List<AllQuestionOrderByDateViewModel> display = SystemHelper.getAllQuestionOrderByDate();
            return View(display.ToPagedList(page ?? 1, 3));
        }

        public ActionResult GetQuestionByWithMostAnswer() {
            ViewBag.tag = SystemHelper.getAllTag();
            QuestionMostReplyViewModel display = SystemHelper.getAllQuestionWithMostAnswer();
            return View(display);
        }

        [Authorize(Roles = "User")]
        public ActionResult AddUpVoteQuestion (int questionId, int? page, int? tagId) {
            var userId = User.Identity.GetUserId();
            SystemHelper.addUpVoteToQuestion(questionId, userId);
            if (tagId > 0) {
                return RedirectToAction("QuestionByTagName", new { tagId, page });
            } 
            return RedirectToAction("AuthorizeUserPage", new { page });
        }
        [Authorize(Roles = "User")]
        public ActionResult AddDownVoteQuestion(int questionId, int? page, int? tagId) {
            var userId = User.Identity.GetUserId();
            SystemHelper.addDownVoteToQuestion(questionId, userId);
            if (tagId > 0) {
                return RedirectToAction("QuestionByTagName", new { tagId, page });
            }
            return RedirectToAction("AuthorizeUserPage", new { page });
        }

        [Authorize(Roles = "User")]
        public ActionResult AddUpVoteAnswer(int answerId, int? page, int? tagId) {
            var userId = User.Identity.GetUserId();
            SystemHelper.addUpVoteToAnswer(answerId, userId);
            if (tagId > 0) {
                return RedirectToAction("QuestionByTagName", new { tagId, page });
            }
            return RedirectToAction("AuthorizeUserPage", new { page });
        }

        [Authorize(Roles = "User")]
        public ActionResult AddDownVoteAnswer(int answerId, int? page, int? tagId) {
            var userId = User.Identity.GetUserId();
            SystemHelper.addDownVoteToAnswer(answerId, userId);
            if (tagId > 0) {
                return RedirectToAction("QuestionByTagName", new { tagId, page });
            }
            return RedirectToAction("AuthorizeUserPage", new { page });
        }


    }
}