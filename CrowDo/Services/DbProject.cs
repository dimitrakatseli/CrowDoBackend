using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Services
{
    public class DbProject
    {

        public void AddProject(int id, Project project)
        {
            using (var db = new CrowDoDB())
            {
                User u = db.Users.Where(uu => uu.UserID.Equals(id)).FirstOrDefault();

                u.Projects.Add(project);
                db.SaveChanges();


            }

        }

        public static void EditProject(Project project, int id)
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

