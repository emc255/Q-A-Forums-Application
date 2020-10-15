using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject_StackOverFlowV2.Models {
    public class AccountHelper {
        static ApplicationDbContext db = new ApplicationDbContext();
        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(db)
            );
        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(db)
            );
        
        public static bool addRole(string roleName) {
            if (!roleManager.RoleExists(roleName)) {
                roleManager.Create(new IdentityRole { Name = roleName });
                return true;
            }
            return false;
        }

        public static bool checkIfUserIsRole(string userId, string role) {
            var result = userManager.IsInRole(userId, role);
            return result;
        }

        public static bool addUserToRole(string userId, string role) {
            if (checkIfUserIsRole(userId, role)) {
                return false;
            } else {
                userManager.AddToRole(userId, role);
                return true;
            }
        }

    }
}