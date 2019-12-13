using CrowDo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Services
{
    public class DataManipulation {
        public List<Project> GetProjectsFromDB()
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.ToList();
            }
        }
        public Project GetProjectsFromDB(int id)
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(proj => proj.ProjectID == id ).FirstOrDefault();
            }
        }
        public Project GetProjectFromDB(string name)
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(proj => ((proj.Title.Equals(name))&&(proj.Status==Status.Active))).FirstOrDefault();
            }
        }

        public string DeleteProject(int id)
        {
            using (var db = new CrowDoDB())
            {
                Project p = db.Projects.Where(proj => proj.ProjectID == id).FirstOrDefault();
                if (p == null) 
                    return "not found";
                p.Status = Status.Inactive;
                db.SaveChanges();
            }
            return "Project is deleted";
        }
        public string DeleteUser(int id)
        {
            using (var db = new CrowDoDB())
            {
                User user = db.Users.Where(usr => usr.UserID == id).FirstOrDefault();
                if (user == null)
                    return "not found";
                else
                {
                    user.Status = Status.Inactive;
                    db.SaveChanges();
                }
            }
            return "User is deleted";
        }
        public string UpdateProject(int id, Project proj)
        {
            using (var db = new CrowDoDB())
            {
                Project p = db.Projects.Where(proj => proj.ProjectID == id).FirstOrDefault();
                if (p == null) 
                    return "not found";
                else
                {
                    p.Title = proj.Title;
                    p.StartDate = proj.StartDate;
                    p.Packages = proj.Packages;
                    p.NumOfViews = proj.NumOfViews;
                    p.Description = proj.Description;
                    p.EndDate = proj.EndDate;
                    db.SaveChanges();
                    return "Project is updated";
                }
            }

        }
        public void AddProject(Project proj)
        {
            using (var db = new CrowDoDB())
            {
                User usr = db.Users.Where(u => u.UserID.Equals(proj.User.UserID)).FirstOrDefault();
                if (usr == null) 
                    return ;
                proj.User = usr;
                db.Projects.Add(proj);
                db.SaveChanges();
                
            }
        }
        public Boolean FundProject(Funding fund)
        {
            using (var db = new CrowDoDB())
            {
                User user = db.Users.Where(usr => usr.UserID.Equals(fund.User.UserID)).FirstOrDefault();
                if (user == null)
                    return false;
                fund.User = user;
                Project project = db.Projects.Where(proj => proj.Code.Equals(fund.Project.Code)).FirstOrDefault();
                fund.Project = project;
                Package package = db.Packages.Where(pack => pack.Code.Equals(fund.Package.Code)).FirstOrDefault();
                fund.Package = package;
                db.Fundings.Add(fund);
                db.SaveChanges();
                return true;
            }
        }
        public List<Funding> GetFundedProjectsById(int projId)
        {
            using (var db = new CrowDoDB())
            {
                return db.Fundings
                    .Where(funds => funds.ProjectID == projId)
                    .ToList();
            }
        }
        public string Login(string username, string password)
        {
            using (var db = new CrowDoDB())
            {
                User user = db.Users.Where(usr => usr.UserName == username && usr.Password==password).FirstOrDefault();
                if (user == null)
                    return "not found";
                else
                    return "OK";
            }
        }
        
        public string SignUp(User user)
        {
            using (var db = new CrowDoDB())
            {
                if (db.Users.Where(usr=>usr.UserName.Equals(user.UserName)).ToList()==null)
                    return "Exists";
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return "Created";
                }
            }
        }
        public User GetUser(int id)
        {
            using (var db = new CrowDoDB())
            {
                return db.Users.Where(usr => (usr.UserID == id) && (usr.Status ==Status.Active)).FirstOrDefault();
            }
        }
        public string EditUser(int id, User user)
        {
            using (var db = new CrowDoDB())
            {
                User usr = db.Users.Where(us => us.UserID == id).FirstOrDefault();
                if (usr == null) 
                    return "NOT FOUND";
                else
                {
                    usr.Password = user.Password;
                    usr.Email = user.Email;
                    usr.FirstName = user.FirstName;
                    usr.LastName = user.LastName;
                    db.SaveChanges();
                    return "Updated";
                }
            }
        }
    }
}

