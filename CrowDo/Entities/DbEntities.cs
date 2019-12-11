using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Entities
{
    public class DbEntities
    {
        class User
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Status Status { get; set; }
            public Role Role { get; set; }
            public int UserID { get; set; }
            public List<Project> Projects { get; set; }
            public List<Funding> Fundings { get; set; }
        }
        enum Status
        {
            Active,
            Inactive
        }
        enum Role
        {
            Backer,
            Creator,
            Administrator
        }
        class Project
        {
            public int ProjectID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public double Goal { get; set; }
            public List<Package> Packages { get; set; }
        }
        class Package
        {
            public int PackageID { get; set; }
            public string Title { get; set; }
            public double Cost { get; set; }
            public string Details { get; set; }
            public string Quantity { get; set; }
        }
        class Funding
        {
            public int FundingID { get; set; }
            public string NumPackages { get; set; }
            public double ProjectID { get; set; }
            public string PackageID { get; set; }
        }

    }
}
