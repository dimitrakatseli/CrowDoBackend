using CrowDo.Entities;
using CrowDo.Services.CrowDo.Services;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static CrowDo.Services.FundingDto;

namespace CrowDo.Services
{
    public class DTOExcel
    {

        private static readonly string filename = @"C:\Users\KDIMITRA\Desktop\accenture\project\demodataForCrowdo.xlsx";

        public static void LoadFromExcelUsers()
        {

            List<UserDto> usersList = new List<UserDto>();
            XSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            { hssfwb = new XSSFWorkbook(file); }
            ISheet sheet = hssfwb.GetSheet("Users");
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {//null is when the row only contains empty cells 
                if (sheet.GetRow(row) != null)
                {
                    UserDto user = new UserDto
                    {
                        Code = sheet.GetRow(row).GetCell(0).StringCellValue,
                        FirstName = sheet.GetRow(row).GetCell(1).StringCellValue,
                        LastName = sheet.GetRow(row).GetCell(2).StringCellValue,
                        Address = sheet.GetRow(row).GetCell(3).StringCellValue
                    };
                    usersList.Add(user);
                }
            }
            AllExcellData.UsersList = new List<UserDto>();
            AllExcellData.UsersList = usersList;
            //return usersList;

        }
        public static void LoadFromExcelProjects()
        {
            AllExcellData.ProjectList = new List<ProjectDto>();
            List<ProjectDto> projectList = new List<ProjectDto>();
            XSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            { hssfwb = new XSSFWorkbook(file); }
            ISheet sheet = hssfwb.GetSheet("Projects");
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {//null is when the row only contains empty cells 
                if (sheet.GetRow(row) != null)
                {
                    ProjectDto project = new ProjectDto
                    {
                        Code = sheet.GetRow(row).GetCell(0).StringCellValue,
                        Creator = sheet.GetRow(row).GetCell(1).StringCellValue,
                        Title = sheet.GetRow(row).GetCell(2).StringCellValue,
                        StartDate = sheet.GetRow(row).GetCell(3).DateCellValue,
                        Packages = sheet.GetRow(row).GetCell(4).StringCellValue,
                        NumberOfPacks = sheet.GetRow(row).GetCell(5).StringCellValue

                    };

                    AllExcellData.ProjectList.Add(project);

                }
            }
            //return usersList;

        }

        public static void LoadFromExcelPackages()
        {
            AllExcellData.PackageList = new List<PackageDto>();

            XSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            { hssfwb = new XSSFWorkbook(file); }
            ISheet sheet = hssfwb.GetSheet("Packages");
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {//null is when the row only contains empty cells 
                if (sheet.GetRow(row) != null)
                {


                    PackageDto package = new PackageDto
                    {
                        Code = sheet.GetRow(row).GetCell(0).StringCellValue,
                        Title = sheet.GetRow(row).GetCell(1).StringCellValue,
                        Cost = sheet.GetRow(row).GetCell(2).NumericCellValue,
                        Details = sheet.GetRow(row).GetCell(3).StringCellValue,
                        Reward = sheet.GetRow(row).GetCell(4).StringCellValue

                    };
                    AllExcellData.PackageList.Add(package);

                }
            }
        }
        public static void LoadFromExcelFunding()
        {
            AllExcellData.FundingList = new List<FundingDto>();

            XSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            { hssfwb = new XSSFWorkbook(file); }
            ISheet sheet = hssfwb.GetSheet("Funding");
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {//null is when the row only contains empty cells 
                if (sheet.GetRow(row) != null)
                {


                    FundingDto fund = new FundingDto
                    {
                        User = sheet.GetRow(row).GetCell(0).StringCellValue,
                        Project = sheet.GetRow(row).GetCell(1).StringCellValue,
                        Quantity = Convert.ToInt32(sheet.GetRow(row).GetCell(3).NumericCellValue),
                        Package = sheet.GetRow(row).GetCell(2).StringCellValue,


                    };
                    AllExcellData.FundingList.Add(fund);

                }
            }
        }

        public static UserDto FindExcellUser(string code)
        {
            foreach (UserDto user in AllExcellData.UsersList)
            {
                if (user.Code.Equals(code))
                {
                    return user;
                }
            }
            return null;
        }

        public static void NumOfPacksToList()
        {

            List<string> strlist = new List<string>();
            foreach (ProjectDto proj in AllExcellData.ProjectList)
            {
                proj.NumberOfPacksL = new List<int>();
                strlist = proj.NumberOfPacks.Split(',').ToList();
                foreach (string str in strlist)
                    proj.NumberOfPacksL.Add(Convert.ToInt32(str));

            }

        }

        public static void SplitPackages()
        {
            foreach (ProjectDto proj in AllExcellData.ProjectList)
            {
                List<string> strlist = new List<string>();
                proj.PackagesL = new List<string>();
                strlist = proj.Packages.Split(',').ToList();
                foreach (string str in strlist)
                    proj.PackagesL.Add(str);

            }
        }


        public static void TransformData()
        {

            List<Package> lpack = new List<Package>();
            DbEntities.LPack = new List<Package>();

            //Packages
            foreach (PackageDto packDto in AllExcellData.PackageList)
            {
                Package pkg = new Package()
                {
                    Code = packDto.Code,
                    Title = packDto.Title,
                    Cost = packDto.Cost,
                    Details = packDto.Details,
                    Quantity = packDto.Quantity,
                    Reward = packDto.Reward
                };
                lpack.Add(pkg);
            }
            DbEntities.LPack = lpack;
            List<Project> lproj = new List<Project>();
            DbEntities.LProject = new List<Project>();
            foreach (ProjectDto projDto in AllExcellData.ProjectList)
            {
                Project proj = new Project()
                {
                    Code = projDto.Code,
                    Title = projDto.Title,
                    StartDate = projDto.StartDate,
                    Goal = 500.99

                };

                DbEntities.LProject.Add(proj);
                /*
                List<Package> packListSearch = new List<Package>();

                foreach(String spack in projDto.PackagesL)
                {
                    foreach(Package pack in DbEntities.LPack)
                    {
                        if (pack.Code.Equals(spack)){
                            packListSearch.Add(pack);
                        }
                    }
                    
                }
                proj.Packages = packListSearch;


                lproj.Add(proj);

            }
            */
            }
            DbEntities.LProject = lproj;
            DbEntities.LFunding = new List<Funding>();
            foreach (FundingDto dtofund in AllExcellData.FundingList)
            {
                Funding fund = new Funding
                {
                    NumPackages = dtofund.Quantity
                };
                DbEntities.LFunding.Add(fund);
            }

            DbEntities.LUser = new List<User>();
            int counter = 0;
            foreach (UserDto dtoUser in AllExcellData.UsersList)
            {
                User user = new User()
                {
                    FirstName = dtoUser.FirstName,
                    LastName = dtoUser.LastName,
                    Code = dtoUser.Code,
                    UserName = "test" + counter++,
                    Role = Role.Creator,
                    Password = "12345678",
                    Status = Status.Active,

                };
                DbEntities.LUser.Add(user);

            }
        }

        public static void test()
        {
            LoadFromExcelUsers();
            LoadFromExcelProjects();
            LoadFromExcelPackages();
            LoadFromExcelFunding();
            NumOfPacksToList();
            SplitPackages();
            TransformData();
            foreach (Package pack in DbEntities.LPack)
            {
                DbPackage.AddPackages(pack);
            }



            foreach (User user in DbEntities.LUser)
            {
                DbUser.AddUser(user);
            }

            using (CrowDoDB db = new CrowDoDB())
            {
                foreach (ProjectDto proj in AllExcellData.ProjectList)
                {
                    string userCode = proj.Creator;
                    User user = db.Users.Where(u => u.Code.Equals(userCode)).First();
                    if (user == null)
                    {
                        continue;
                    }

                    Project p = new Project
                    {
                        Title = proj.Title,
                        User = user
                    };

                    db.Projects.Add(p);
                }

                db.SaveChanges();
            }
            foreach (Funding fund in DbEntities.LFunding)
            {
                DbFunding.AddFunding(fund);
            }


        }





    }
}


