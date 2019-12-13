using CrowDo.Entities;
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

        public void LoadFromExcelUsers()
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
        public void LoadFromExcelProjects()
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

        public void LoadFromExcelPackages()
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
        public void LoadFromExcelFunding()
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

        public UserDto FindExcellUser(string code)
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

        public void NumOfPacksToList()
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

        public void SplitPackages()
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


        public void TransformData()
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
                    Quantity = 3,
                    Reward = packDto.Reward
                };
                lpack.Add(pkg);
            }
            DbEntities.LPack = lpack;



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

        public void test()
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

            SaveDTOProjects();
            SaveDTOFundings();

        }

        public void SaveDTOFundings()
        {
            Funding dbfund;
            using (CrowDoDB db = new CrowDoDB())
            {
                foreach (FundingDto fund in AllExcellData.FundingList)
                {

                    User user = db.Users.Where(x => x.Code.Equals(fund.User)).FirstOrDefault();
                    Console.WriteLine(user.Code);
                    if (user == null) continue;
                    Project project = db.Projects.Where(x => x.Code.Equals(fund.Project)).FirstOrDefault();
                    Console.WriteLine(project.Code);
                    if (project == null) continue;
                    Package package = db.Packages.Where(x => x.Code.Equals(fund.Package)).FirstOrDefault();
                    Console.WriteLine(package.Code);
                    if (package == null) continue;
                    dbfund = new Funding()
                    {
                        NumPackages = fund.Quantity,
                        User = user,
                        Package = package,
                        Project = project


                    };
                    db.Fundings.Add(dbfund);
                }
                db.SaveChanges();
            }
        }


        public void SaveDTOProjects()
        {
            using (CrowDoDB db = new CrowDoDB())
            {
                Package package;
                foreach (ProjectDto proj in AllExcellData.ProjectList)
                {

                    string userCode = proj.Creator;
                    User user = db.Users.Where(u => u.Code.Equals(userCode)).First();
                    if (user == null) continue;

                    Project p = new Project()
                    {
                        Title = proj.Title,
                        StartDate = proj.StartDate,
                        User = user,
                        Packages = new List<Package>(),
                        Code = proj.Code,
                        NumOfViews =0,
                        Goal = 40000

                   


                    };





                    foreach (String pack in proj.PackagesL)
                    {
                        package = db.Packages.Where(x => x.Code.Equals(pack)).FirstOrDefault();
                        if (package == null) continue;
                        p.Packages.Add(package);
                    }



                    db.Projects.Add(p);

                }
                db.SaveChanges();

            }


        }

    }



}




