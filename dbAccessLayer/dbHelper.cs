using MySql.Data.MySqlClient;

namespace TakeMeToChurchAPI.dbAccessLayer
{
    public static class dbHelper
    {
        private static string server;
        private static string database;
        private static string uid;
        private static string password;
        public static MySqlConnection Initialize()
        {
            server = "localhost";
            database = "churchdb";
            uid = "root";
            password = "Pass@123";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            return new MySqlConnection(connectionString);
        }


        public static bool OpenConnection(MySqlConnection connection)
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }
        public static bool CloseConnection(MySqlConnection connection)
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }
    }
}