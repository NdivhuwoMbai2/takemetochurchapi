using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TakeMeToChurchAPI.Models;

namespace TakeMeToChurchAPI.dbAccessLayer
{
    public class churchaccesslayer : Ichurchaccess
    {
        private MySqlConnection connection;
        public churchaccesslayer()
        {
            connection = dbHelper.Initialize();
        }
        public Church MapChurch(MySqlDataReader dataReader)
        {
            Church church = new Church();
            church.idchurch = int.Parse(dataReader["idchurch"].ToString());
            church.name = dataReader["name"].ToString();
            church.location = dataReader["location"].ToString();
            return church;
        }
        public List<Church> GetAllChurches()
        {
            string query = "Select * From church";
            List<Church> church = new List<Church>();
            if (dbHelper.OpenConnection(this.connection) == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    church.Add(MapChurch(dataReader));
                }
                dataReader.Close();
                dbHelper.CloseConnection(this.connection);
                return church;
            }
            else
            {
                return null;
            }
        }      

        public int Addchurch(Church church)
        {
            string query = "INSERT INTO church (name,location ) " +
              $" VALUES('{church.name}', '{church.location}')";
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
    }
}