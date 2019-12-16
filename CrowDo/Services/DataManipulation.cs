using CrowDo.Entities;
using Microsoft.EntityFrameworkCore;
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
                return db.Projects.Where(proj=>proj.Status == Status.Active).ToList();
            }
        }
        public Project GetProjectsFromDB(int id)
        {
            using (var db = new CrowDoDB())
            {
                AddView(id);
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
        public string UpdateProject(Project proj)
        {
            using (var db = new CrowDoDB())
            {
                Project p = db.Projects.Where(proje => proje.ProjectID == proj.ProjectID).FirstOrDefault();
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
        public User Login(User user)
        {
            using (var db = new CrowDoDB())
            {
                User usr = db.Users.Where(usr => usr.UserName == user.UserName && usr.Password==user.Password).FirstOrDefault();
                return usr;
            }
        }
        
        public User Register(User user)
        {
            using (var db = new CrowDoDB())
            {
                if (db.Users.Where(usr=>usr.UserName.Equals(user.UserName)).ToList()==null)
                    return null;
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return user;
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
        public string EditUser(User user)
        {
            using (var db = new CrowDoDB())
            {
                User usr = db.Users.Where(us => us.UserID == user.UserID).FirstOrDefault();
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

        public void AddFunding(Funding fund)
        {
            using (var db = new CrowDoDB())
            {
                db.Fundings.Add(fund);
                db.SaveChanges();
            }
        }
        public void AddUser(User user)
        {
            using (var db = new CrowDoDB())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public List<Funding> GetAllUserFunding(int id)
        {
            using (var db = new CrowDoDB())
            {
                return db.Fundings.Where(i => i.UserID.Equals(id))
                    .Include(it => it.Project)
                    .Include(it => it.Package)
                    .ToList();
            }

        }
        public List<Project> GetAllUserProjects(int id)
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(i => i.UserID.Equals(id))
                    .Include(it => it.Packages)
                    .ToList();
            }

        }
        public void AddPackages(Package pack)
        {
            using (var db = new CrowDoDB())
            {

                db.Packages.Add(pack);
                db.SaveChanges();

            }
        }

        public string AddView(int id)
        {
            using (var db = new CrowDoDB())
            {
                Project p = db.Projects.Where(proje => proje.ProjectID == id).FirstOrDefault();
                if (p == null)
                    return "not found";
                else
                {
                    p.NumOfViews = p.NumOfViews+1;
                    db.SaveChanges();
                    return "Updated View";
                }
            }
        }


    }
}

