using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TakeMeToChurchAPI.dbAccessLayer;
using TakeMeToChurchAPI.Models;
using Newtonsoft.Json;

namespace TakeMeToChurchAPI.Controllers {
    [Route ("api/person")]
    [ApiController]
    [AllowAnonymous]
    [DisableCors()]
    public class PersonController : ControllerBase {
        dbaccessor db;
        public PersonController () {
            db = new dbaccessor();
        }
        [Route("addperson")]        
        [HttpPost]  
        public int AddPerson(Person person) {
            return db.addPerson(person);
        }
        
        [Route("getperson")]
        [HttpGet]
        public Person GetPerson(int personid){
            return db.getPerson(personid);
        }
         [Route("getchildren")]
         [HttpGet]
         public List<Person> getChildren(int? personid,int fatherid)
         {
             return db.getChildren(personid,fatherid);
         }

         [Route("getlocations")]
         [HttpGet]
         public List<Person> getlocations()
         {
             return db.getLocations();
         }
        [Route("getchurchmembers")]
        [HttpGet]
        public List<Person> getchurchmembers()
        {
            return db.GetPeople();
        }

        [Route("updatelocation")]
        [HttpPut]
        public int updatelocation(Person person)
        {
            return db.updateLocation(person.idperson, person.location);
        }

      


    }
}