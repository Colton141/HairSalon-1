using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _name;
    private int _id;

    public Stylist(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());
        return (idEquality && nameEquality);
      }
    }

      public string GetName()
      {
        return _name;
      }

      public int GetId()
      {
        return _id;
      }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";
        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@name";
        name.Value = this._name;
        cmd.Parameters.Add(name);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static List<Stylist> GetAll()
      {
        List<Stylist> allStylists = new List<Stylist> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int StylistId = rdr.GetInt32(0);
          string StylistName = rdr.GetString(1);
          Stylist newStylist = new Stylist(StylistName, StylistId);
          allStylists.Add(newStylist);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allStylists;
      }

      public static Stylist Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int StylistId = 0;
        string StylistName = "";
        while(rdr.Read())
        {
          StylistId = rdr.GetInt32(0);
          StylistName = rdr.GetString(1);
        }
        Stylist newStylist = new Stylist(StylistName, StylistId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newStylist;

      }

      public List<Client> GetClients()
      {
        List<Client> allStylistClients = new List<Client> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist.id;";
        MySqlParameter stylist_id = new MySqlParameter();
        stylist_id.ParameterName = "@stylist.id";
        stylist_id.Value = this._id;
        cmd.Parameters.Add(stylist_id);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int clientId = rdr.GetInt32(0);
          string clientName = rdr.GetString(1);
          int clientStylistId = rdr.GetInt32(2);
          Client newClient = new Client(clientName, clientStylistId, clientId);
          allStylistClients.Add(newClient);
        }

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allStylistClients;
      }

      public List<Specialty> GetSpecialties()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(@"SELECT stylist_id FROM stylists_specialties WHERE stylist_id = @StylistId;", conn);
        cmd.Parameters.AddWithValue("@StylistId", _id);
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<int> specialtyIds = new List<int> {};
        while(rdr.Read())
        {
          int specialtyId = rdr.GetInt32(0);
          specialtyIds.Add(specialtyId);
        }
        rdr.Dispose();
        List<Specialty> specialties = new List<Specialty> {};
        foreach (int specialtyId in specialtyIds)
        {
          MySqlCommand specialtyQuery = new MySqlCommand(@"SELECT * FROM specialties WHERE id = @SpecialtyId;", conn);
          specialtyQuery.Parameters.AddWithValue("@SpecialtyId", specialtyId);
          MySqlDataReader specialtyQueryRdr = specialtyQuery.ExecuteReader() as MySqlDataReader;
          while(specialtyQueryRdr.Read())
          {
            int thisSpecialtyId = specialtyQueryRdr.GetInt32(0);
            string specialtyName = specialtyQueryRdr.GetString(1);
            Specialty foundSpecialty = new Specialty(specialtyName, thisSpecialtyId);
            specialties.Add(foundSpecialty);
          }
          specialtyQueryRdr.Dispose();
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return specialties;
      }

      public static void ClearAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM stylists;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }

      }

      public void DeleteStylist(int stylistId)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(@"DELETE FROM stylists WHERE id = @thisId", conn);
        cmd.Parameters.AddWithValue("@thisId", _id);
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }


      }

      public void AddSpecialty (Specialty newSpecialty)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(@"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);", conn);
        cmd.Parameters.AddWithValue("@StylistId", _id);
        cmd.Parameters.AddWithValue("@SpecialtyId", newSpecialty.GetId());
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
  }
}
