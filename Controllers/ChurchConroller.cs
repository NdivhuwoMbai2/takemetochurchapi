using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TakeMeToChurchAPI.dbAccessLayer;
using TakeMeToChurchAPI.Models;
using Newtonsoft.Json;

namespace TakeMeToChurchAPI.Controllers
{
    [Route("api/church")]
    [ApiController]
    [AllowAnonymous]
    [DisableCors()]
    public class ChurchConroller : ControllerBase
    {
        churchaccesslayer churchacess;
        public ChurchConroller()
        {
            churchacess = new churchaccesslayer();
        }
        [Route("getallchurches")]
        [HttpGet]
        public List<Church> GetAllChurches(){
            return churchacess.GetAllChurches();
        }
        [Route("addchurch")]
        [HttpPost]
        public int Addchurch(Church church){
            return churchacess.Addchurch(church);
        }
        
    }
}