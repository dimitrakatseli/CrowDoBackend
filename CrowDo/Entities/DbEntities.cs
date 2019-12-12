using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Entities
{

    public class User
    {
        public string? Code { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
        public int UserID { get; set; }
        public List<Project> Projects { get; set; }
        public List<Funding> Fundings { get; set; }
    }
    public enum Status
    {
        Active,
        Inactive
    }
    public enum Role
    {
        Backer,
        Creator,
        Administrator
    }
    public class Project
    {
        public string? Code { get; set; }
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Goal { get; set; }
        public List<Package> Packages { get; set; }
    }
    public class Package
    {
        public string? Code { get; set; }
        public int PackageID { get; set; }
        public string? Title { get; set; }
        public double Cost { get; set; }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public string Reward { get; set; }
    }
    public class Funding
    {

        public int FundingID { get; set; }
        public int? NumPackages { get; set; }
        public int? ProjectID { get; set; }
        public int? PackageID { get; set; }
    }

    public class DbEntities
    {
        public static List<Package> LPack { get; set; }
        public static List<User> LUser { get; set; }
        public static List<Project> LProject { get; set; }

        public static List<Funding> LFunding { get; set; }
    }


}
