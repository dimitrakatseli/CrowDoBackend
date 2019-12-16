using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDo.Entities;
using CrowDo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CrowDo.Controllers
{
    [Route("API")]
    [ApiController]

    public class CrowDoController : ControllerBase
    {
        private readonly ILogger<CrowDoController> _logger;
        private DTOExcel _dtoExcel;
        private DataManipulation _dataM;
        public CrowDoController(DTOExcel dtoExcel, ILogger<CrowDoController> logger, DataManipulation dataM)
        {
            _dtoExcel = dtoExcel;
            _logger = logger;
            _dataM = dataM;
        }

        [HttpGet]
        public String Arxiki()
        {

            return "Initil Page";
        }


        [HttpGet("excell")]
        public IEnumerable<Package> PostExcell()
        {
            List<int> lint = new List<int>();
            //data transfer only first time
            //_dtoExcel.test();
            return DbEntities.LPack;
        }

        [HttpGet("projects")]
        public IEnumerable<Project> GetProjects()
        {
            return _dataM.GetProjectsFromDB();
        }

        
        [HttpGet("projects/{id}")]
        public Project GetProjectById(int id)
        {
            return _dataM.GetProjectsFromDB(id);
        }
        [HttpGet("projects1/{name}")]
        public Project GetProjectsByTitle(string name)
        {
            return _dataM.GetProjectFromDB(name);
        }
        
    
        [HttpGet("fundings/{id}")]
        public List<Funding> GetFundings(int id)
        {
            return _dataM.GetAllUserFunding(id);
        }
        
        [HttpPost("fund-project")]
        public Boolean FundAProject(Funding fund)
        {
            return _dataM.FundProject(fund);
        }
        
        [HttpDelete("delete/project/{id}")]
        public string DeleteProjectById(int id)
        {
            return _dataM.DeleteProject(id);
        }
        
        [HttpDelete("delete/member/{id}")]
        public string DeleteUser(int id)
        {
            return _dataM.DeleteUser(id);
        }
        
        [HttpPut("edit/project/{id}")]
        public string UpdateProject(Project project)
        {
            return _dataM.UpdateProject(project);
        }
        
        [HttpPost("project/new")]
        public void SetAnewProject(Project p)
        {
            _dataM.AddProject(p);
        }
      
        [HttpPost("register/member")]
        public string Register(User user)
        {
            return _dataM.Register(user);
        }
        
        [HttpGet("member/login/{username}/{password}")]
        public Boolean LogIn(User user)
        {
            return _dataM.Login(user);
        }
     
        [HttpGet("member/{id}")]
        public User GetUser(int id)
        {
            return _dataM.GetUser(id);
        }
        
        [HttpPut("edit/member/{id}")]
        public string EditUser(User user)
        {
            return _dataM.EditUser(user);
        }



    }
}
