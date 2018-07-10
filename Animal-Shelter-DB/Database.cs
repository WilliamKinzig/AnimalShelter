using System;
using MySql.Data.MySqlClient;
using AnimalShelter;

namespace AnimalShelter
{
    public class DB
    {
      public static MySqlConnection Connection()
      {
        MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
        return conn;
      }
    }
}
