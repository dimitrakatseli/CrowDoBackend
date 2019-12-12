using CrowDo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Services
{
    public class DbPackage
    {


        public static void AddPackages(Package pack)
        {
            using (var db = new CrowDoDB())
            {

                db.Packages.Add(pack);
                db.SaveChanges();

            }




        }
    }
}
