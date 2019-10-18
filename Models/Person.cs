using System;

namespace TakeMeToChurchAPI.Models
{
    public class Person
    {
        public int? idperson{ get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string birthday { get; set; }
        public string telephonenumber{ get; set; }
        public string location { get; set; }
        public int? isAlive{ get; set; }
        public int? motherid{ get; set; }
        public int? fatherid{ get; set; }
        public int? isMarried{ get; set; }
        public int? numberofkids{ get; set; }
        public int? idchurch{ get; set; }

    }
    
}