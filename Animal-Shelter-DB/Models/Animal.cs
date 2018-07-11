using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using AnimalShelter;

namespace AnimalShelter.Models
{
  public class Animal
  {
    private int _id;
    private string _type;
    private string _name;
    private string _gender;
    private int _age;

    public Animal(string newType, string newName, string newGender, int newAge, int id = 0)
    {
      _type = newType;
      _name = newName;
      _gender = newGender;
      _age = newAge;
      _id = id;
    }

    public override bool Equals(System.Object otherAnimal)
    {
      if (!(otherAnimal is Animal))
      {
        return false;
      }
      else
      {
        Animal newAnimal = (Animal) otherAnimal;
        bool idEquality = (this.GetAnimalId() == newAnimal.GetAnimalId());
        bool typeEquality = (this.GetAnimalType() == newAnimal.GetAnimalType());
        bool nameEquality = (this.GetAnimalName() == newAnimal.GetAnimalName());
        bool genderEquality = (this.GetAnimalGender() == newAnimal.GetAnimalGender());
        bool ageEquality = (this.GetAnimalAge() == newAnimal.GetAnimalAge());
        return (idEquality && nameEquality && genderEquality && ageEquality);
      }
    }
    public override int GetHashCode()
    {
        return this.GetAnimalName().GetHashCode();
    }

    public int GetAnimalId()
    {
      return _id;
    }
    public void SetAnimalId(int newId)
    {
      _id = newId;
    }
    public string GetAnimalName()
    {
      return _name;
    }
    public void SetAnimalName(string newName)
    {
      _name = newName;
    }
    public string GetAnimalGender()
    {
      return _gender;
    }
    public void SetAnimalGender(string newGender)
    {
      _gender = newGender;
    }
    public int GetAnimalAge()
    {
      return _age;
    }
    public void SetAnimalAge(int newAge)
    {
      _age = newAge;
    }
    public string GetAnimalType()
    {
      return _type;
    }
    public void SetAnimalType(string newType)
    {
      _type = newType;
    }

    public void SaveAnimal()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO animals (type, name, gender, age) VALUES (@AnimalType, @AnimalName, @AnimalGender, @AnimalAge);";

      MySqlParameter type = new MySqlParameter();
      type.ParameterName = "@AnimalType";
      type.Value = this._type;
      cmd.Parameters.Add(type);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@AnimalName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter gender = new MySqlParameter();
      gender.ParameterName = "@AnimalGender";
      gender.Value = this._gender;
      cmd.Parameters.Add(gender);

      MySqlParameter age = new MySqlParameter();
      age.ParameterName = "@AnimalAge";
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

    public static List<Animal> GetAllAnimals()
    {
      List<Animal> allAnimals = new List<Animal> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM animals;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalType = rdr.GetString(1);
        string animalName = rdr.GetString(2);
        string animalGender = rdr.GetString(3);
        int animalAge = rdr.GetInt32(4);
        Animal newAnimal = new Animal(animalType, animalName, animalGender, animalAge, animalId);
        allAnimals.Add(newAnimal);
      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allAnimals;
    }

    public static void DeleteAllAnimals()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM animals;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Animal FindAnimal(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM animals WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int animalId = 0;
      string animalType = "";
      string animalName = "";
      string animalGender = "";
      int animalAge = 0;

      while (rdr.Read())
      {
         animalId = rdr.GetInt32(0);
         animalType = rdr.GetString(1);
         animalName = rdr.GetString(2);
         animalGender = rdr.GetString(3);
         animalAge = rdr.GetInt32(4);
      }

      Animal foundAnimal = new Animal(animalType, animalName, animalGender, animalAge, animalId);

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }

      return foundAnimal;
    }

  }
}
