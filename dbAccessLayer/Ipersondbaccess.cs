using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TakeMeToChurchAPI.Models;

namespace TakeMeToChurchAPI.dbAccessLayer
{
    public interface Ipersondbaccess
    {
        int addPerson(Person person);
        int updateLocation(int? idperson, string location);
        List<Person> GetPeople();
        Person getPerson(int personId);
        List<Person> getChildren(int? motherid, int? fatherid);
        List<Person> getLocations();
        Person MapPerson(MySqlDataReader dataReader);
    }
}