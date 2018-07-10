using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using AnimalShelter;

namespace AnimalShelter.Models
{
  public class Tiger
  {
    private int _id;
    private string _name;
    private string _gender;
    private int _age;

    public Tiger(string newName, string newGender, int newAge, int id = 0)
    {
      _name = newName;
      _gender = newGender;
      _age = newAge;
      _id = id;
    }

    public override bool Equals(System.Object otherTiger)
    {
      if (!(otherTiger is Tiger))
      {
        return false;
      }
      else
      {
        Tiger newTiger = (Tiger) otherTiger;
        bool idEquality = (this.GetTigerId() == newTiger.GetTigerId());
        bool nameEquality = (this.GetTigerName() == newTiger.GetTigerName());
        bool genderEquality = (this.GetTigerGender() == newTiger.GetTigerGender());
        bool ageEquality = (this.GetTigerAge() == newTiger.GetTigerAge());
        return (idEquality && nameEquality && genderEquality && ageEquality);
      }
    }
    public override int GetHashCode()
    {
        return this.GetTigerName().GetHashCode();
    }

    public int GetTigerId()
    {
      return _id;
    }
    public void SetTigerId(int newId)
    {
      _id = newId;
    }
    public string GetTigerName()
    {
      return _name;
    }
    public void SetTigerName(string newName)
    {
      _name = newName;
    }
    public string GetTigerGender()
    {
      return _gender;
    }
    public void SetTigerGender(string newGender)
    {
      _gender = newGender;
    }
    public int GetTigerAge()
    {
      return _age;
    }
    public void SetTigerAge(int newAge)
    {
      _age = newAge;
    }

    public void SaveTiger()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO tigers (name, gender, age) VALUES (@TigerName, @TigerGender, @TigerAge);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@TigerName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter gender = new MySqlParameter();
      gender.ParameterName = "@TigerGender";
      gender.Value = this._gender;
      cmd.Parameters.Add(gender);

      MySqlParameter age = new MySqlParameter();
      age.ParameterName = "@TigerAge";
      age.Value = this._age;
      cmd.Parameters.Add(age);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Tiger> GetAllTigers()
    {
      List<Tiger> allTigers = new List<Tiger> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tigers;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int tigerId = rdr.GetInt32(0);
        string tigerName = rdr.GetString(1);
        string tigerGender = rdr.GetString(2);
        int tigerAge = rdr.GetInt32(3);
        Tiger newTiger = new Tiger(tigerName, tigerGender, tigerAge, tigerId);
        allTigers.Add(newTiger);
      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allTigers;
    }

    public static void DeleteAllTigers()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM tigers;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Tiger FindTiger(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tigers WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int tigerId = 0;
      string tigerName = "";
      string tigerGender = "";
      int tigerAge = 0;

      while (rdr.Read())
      {
         tigerId = rdr.GetInt32(0);
         tigerName = rdr.GetString(1);
         tigerGender = rdr.GetString(2);
         tigerAge = rdr.GetInt32(3);
      }

      Tiger foundTiger = new Tiger(tigerName, tigerGender, tigerAge, tigerId);

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }

      return foundTiger;
    }

  }
}
