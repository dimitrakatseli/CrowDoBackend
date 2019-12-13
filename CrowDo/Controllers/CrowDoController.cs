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
        public CrowDoController (DTOExcel dtoExcel,ILogger<CrowDoController> logger,DataManipulation dataM)
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


    }
}
