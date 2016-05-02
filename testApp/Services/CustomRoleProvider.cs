using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using testApp.Repositories;
using MongoDB.Bson;

namespace testApp.Services
{
    public class CustomRoleProvider : RoleProvider
    {
        RoleRepository roleRep;
        UserRepository userRep;
        public CustomRoleProvider()
            : base()
        {
            roleRep = new RoleRepository();
            userRep = new UserRepository();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get;
            set;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return roleRep.GetRoles().Select(x => x.Name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = userRep.FindUser(username);
            if (user != null && user.Roles != null)
            {
                return user.Roles.Select(x => x.Name).ToArray();
            }
            return new string[0];
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            string[] roles = GetRolesForUser(username);
            foreach (var role in roles)
            {
                if (role == roleName)
                {
                    return true;
                }
            }
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}