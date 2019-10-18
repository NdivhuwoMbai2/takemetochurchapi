using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TakeMeToChurchAPI.Models;

namespace TakeMeToChurchAPI.dbAccessLayer {
    //
    public class dbaccessor :Ipersondbaccess{
        private MySqlConnection connection;
        public dbaccessor(){
           connection= dbHelper.Initialize();
        }
        public int addPerson(Person person)
        {
            person.birthday = String.IsNullOrEmpty(person.birthday) == true ? null : DateTime.Parse(person.birthday).ToString();

            person.motherid = person.motherid.HasValue == true ? person.motherid.Value : 0 ;
            person.fatherid = person.fatherid.HasValue == true ? person.fatherid.Value : 0 ;

            string query = "INSERT INTO Person (name,surname,birthday,telephonenumber,location,isAlive,motherid,fatherid,isMarried,idchurch ) " +
              $"                VALUES('{person.name}', '{person.surname}','{person.birthday}','{person.telephonenumber}','{person.location}','{person.isAlive}','{person.motherid}','{person.fatherid}','{person.isMarried}','{person.idchurch}')";
            if (dbHelper.OpenConnection(this.connection) == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                return cmd.ExecuteNonQuery();
                dbHelper.CloseConnection(this.connection);
            }
            else
            {
                return 0;
            }
        }
        public int updateLocation(int? personId, string location)
        {

            string query = $"UPDATE Person SET location='{location}' WHERE idperson='{personId}'";
            if (dbHelper.OpenConnection(this.connection) == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                return cmd.ExecuteNonQuery();
                dbHelper.CloseConnection(this.connection);
            }
            else
            {
                return 0;
            }
        }
        public List<Person> GetPeople(){            
            string query = "Select person.*, (SELECT COUNT(*) FROM person as p WHERE p.fatherid=person.idperson AND YEAR(p.birthday)>2010) as numberofkids From person ";
            List<Person> people = new List<Person>();
            if (dbHelper.OpenConnection(this.connection) == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();      
                while (dataReader.Read())
                {
                    people.Add(MapPerson(dataReader));
                    }
                dataReader.Close();

                dbHelper.CloseConnection(connection);
                return people;
            }
            else{
                return null;
            }
        }
        public Person getPerson(int personId){
            string query = "Select * From person WHERE idperson="+personId;
            Person people = null;
            if (dbHelper.OpenConnection(this.connection) == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    people = MapPerson(dataReader);
                }
                dataReader.Close();
                dbHelper.CloseConnection(this.connection);
                return people;
            }
            else
            {
                return null;
            }
        }
        public List<Person> getChildren(int? motherid, int? fatherid)
        {
            string query = "";
            if(motherid.HasValue && fatherid.HasValue){
                query = "Select * From Person WHERE motherrid=" + motherid + " AND fatherid=" + fatherid;
            }else if(motherid.HasValue){
                query = "Select * From Person WHERE motherrid=" + motherid;
            }
            else if (fatherid.HasValue)
            {
                query = "Select * From Person WHERE  fatherid=" + fatherid;
            }
            
            List<Person> people = new List<Person>();
            if (dbHelper.OpenConnection(this.connection) == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    people.Add(MapPerson(dataReader));
                }
                dataReader.Close();
                dbHelper.CloseConnection(this.connection);
                return people;

            }else{
                return null;
            }
        }
        public List<Person> getLocations(){
            string query = "Select idperson, location From person Group by location ";
            List<Person> people = new List<Person>();
            if (dbHelper.OpenConnection(this.connection) == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    people.Add(new Person()
                    {
                        idperson = int.Parse(dataReader["idperson"].ToString()),
                        location = dataReader["location"].ToString()
                    });
                }
                dataReader.Close();
                dbHelper.CloseConnection(this.connection);
                return people;
            }
            else
            {
                return null;
            }
        } 
        public Person MapPerson(MySqlDataReader dataReader)
        {
            Person person = new Person();
            person.idperson = int.Parse(dataReader["idperson"].ToString());
            person.name = dataReader["name"].ToString();
            person.surname = dataReader["surname"].ToString();
            person.birthday = dataReader["birthday"].ToString();
            person.telephonenumber = dataReader["telephonenumber"].ToString();
            person.location = dataReader["location"].ToString();
            if(dataReader["isAlive"] != DBNull.Value){
                person.isAlive = int.Parse(dataReader["isAlive"].ToString());
            }           
            person.motherid = dataReader["motherid"] != DBNull.Value ? (int?) int.Parse(dataReader["motherid"].ToString()) : null;
            person.fatherid = dataReader["fatherid"] != DBNull.Value ? (int?) int.Parse(dataReader["fatherid"].ToString()) : null;
            person.numberofkids =dataReader["numberofkids"] != DBNull.Value ? (int?)int.Parse(dataReader["numberofkids"].ToString()) : null;
            person.isMarried = dataReader["isMarried"] != DBNull.Value ? (int?)int.Parse(dataReader["isMarried"].ToString()) : null;
            person.idchurch =dataReader["idchurch"] != DBNull.Value ? (int?)int.Parse(dataReader["idchurch"].ToString()):null;
            return person;
        }
    }
}