using CrowDo.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CrowDo.Services
{
    public class DbUser
    {
        public static List<User> GetListOfUsers()
        {
            using (var db = new CrowDoDB())
            {

                return db.Users.ToList();
            }

        }


        public static User GetUser(string id)
        {

            using (var db = new CrowDoDB())
            {

                return db.Users.Where(i => i.UserID.Equals(id)).FirstOrDefault();

            }

        }


        public static void AddUser(User u)
        {

            using (var db = new CrowDoDB())
            {

                db.Users.Add(u);
                db.SaveChanges();

            }


        }

            public static string UpdateUser(string id, User user)
        {

            using (var db = new CrowDoDB())
            {

                User u = db.Users.Where(uu => uu.UserID.Equals(id)).FirstOrDefault();
                if (u != null)
                {

                    db.Update(u);
                    db.SaveChanges();

                }
                                    
            }

            return "updated";

        }


        public void DeleteUser(string id)

        {
            using (var db = new CrowDoDB())
            {

                User u = db.Users.Where(uu => uu.UserID.Equals(id)).FirstOrDefault();
                u.Status = Status.Inactive;
                db.SaveChanges();
            }

          

        }


       

    }
}
