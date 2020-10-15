namespace CourseProject_StackOverFlowV2.Migrations
{
    using CourseProject_StackOverFlowV2.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CourseProject_StackOverFlowV2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CourseProject_StackOverFlowV2.Models.ApplicationDbContext context)
        {
            context.Tags.AddOrUpdate(x => x.Name, new Models.Tag { Id = 1, Name = "--------" });
            context.Tags.AddOrUpdate(x => x.Name, new Models.Tag { Id = 2, Name = "C#" });
            context.Tags.AddOrUpdate(x => x.Name, new Models.Tag { Id = 3, Name = "Java" });
            context.Tags.AddOrUpdate(x => x.Name, new Models.Tag { Id = 4, Name = "SQL" });
            context.Tags.AddOrUpdate(x => x.Name, new Models.Tag { Id = 5, Name = "JavaScript" });
            context.Tags.AddOrUpdate(x => x.Name, new Models.Tag { Id = 6, Name = "Kotlin" });

         

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
        
    }
}
