using CrowDo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Services
{
    public class DbPackage
    {


        public static void AddPackages(Package pack, int id)
        {
            using (var db = new CrowDoDB())
            {
                Project p = db.Projects.Where(p => p.ProjectID.Equals(id)).FirstOrDefault();

                p.Packages.Add(pack);
                db.SaveChanges();

            }




        }
    }
}
