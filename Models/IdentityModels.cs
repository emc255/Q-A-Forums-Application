using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace CourseProject_StackOverFlowV2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() {
            Questions = new HashSet<Question>();
            QuestionComments = new HashSet<QuestionComment>();
            QuestionVotes = new HashSet<QuestionVote>();
            Answers = new HashSet<Answer>();
            AnswerComments = new HashSet<AnswerComment>();
            AnswerVotes = new HashSet<AnswerVote>();
            Reputation = 0;
           
          
        }
        public int Reputation { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<QuestionComment> QuestionComments { get; set; }
        public virtual ICollection<QuestionVote> QuestionVotes { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<AnswerComment> AnswerComments { get; set; }
        public virtual ICollection<AnswerVote> AnswerVotes { get; set; }
       
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionComment> QuestionComments { get; set; }
        public DbSet<QuestionVote> QuestionVotes { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerComment> AnswerComments { get; set; }
        public DbSet <AnswerVote> AnswerVotes { get; set; }

        public DbSet<QuestionTag> QuestionTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
       
        public ApplicationDbContext()
            : base("StackOverFlowV2BConnectionString", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder) {
        //    base.OnModelCreating(modelBuilder);

        //    Global turn off delete behaviour on foreign keys
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}

      
    }
   
}