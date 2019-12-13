using CrowDo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CrowDo.Services
{
    public class DbProject
    {
        public List<Project> GetListOfProjects()
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.ToList();
            }
        }
        public Project GetProject(string id)
        {
            using (var db = new CrowDoDB())
            {
                return db.Projects.Where(p => p.ProjectID.Equals(id)).FirstOrDefault();
            }
        }
        public void AddProject(Project project)
        {
            using (var db = new CrowDoDB())
            {
                
                db.Projects.Add(project);
                db.SaveChanges();
            }
        }
        public void EditProject(Project project, int id)
        {
            using (var db = new CrowDoDB())
            {
                User u = db.Users.Where(uu => uu.UserID.Equals(id)).FirstOrDefault();
                if (project != null)
                {
                    db.Update(project);
                    db.SaveChanges();
                }
            }
        }
    }
}