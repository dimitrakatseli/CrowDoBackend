using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Services
{
    public class UserDto
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }

    public class ProjectDto
    {
        public string Code { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public string Packages { get; set; }
        public List<string> PackagesL { get; set; }
        public string NumberOfPacks { get; set; }
        public List<int> NumberOfPacksL { get; set; }

    }
    public class PackageDto
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public double Cost { get; set; }
        public string Details { get; set; }
        public string Reward { get; set; }
        public int Quantity { get; set; }

    }

    public class FundingDto
    {
        public string User { get; set; }
        public string Project { get; set; }
        public string Package { get; set; }
        public int Quantity { get; set; }
    }

    public class AllExcellData
    {
        public static List<UserDto> UsersList { get; set; }
        public static List<ProjectDto> ProjectList { get; set; }
        public static List<PackageDto> PackageList { get; set; }
        public static List<FundingDto> FundingList { get; set; }



    }
}
