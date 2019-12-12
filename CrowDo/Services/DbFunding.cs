using CrowDo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Services
{

        public class DbFunding
        {
            public static void AddFunding(Funding fund)
            {
                using (var db = new CrowDoDB())
                {
                    db.Fundings.Add(fund);
                    db.SaveChanges();
                }
            }
        }
    
}
