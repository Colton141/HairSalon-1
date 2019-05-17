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
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(@"SELECT stylist_id FROM stylists_clients WHERE stylist_id = @StylistId;", conn);
        cmd.Parameters.AddWithValue("@StylistId", _id);
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<int> clientIds = new List<int> {};
        while(rdr.Read())
        {
          int clientId = rdr.GetInt32(0);
          clientIds.Add(clientId);
        }
        rdr.Dispose();
        List<Client> clients = new List<Client> {};
        foreach (int clientId in clientIds)
        {
          MySqlCommand clientQuery = new MySqlCommand(@"SELECT * FROM clients WHERE id = @SpecialtyId;", conn);
          clientQuery.Parameters.AddWithValue("@SpecialtyId", clientId);
          MySqlDataReader clientQueryRdr = clientQuery.ExecuteReader() as MySqlDataReader;
          while(clientQueryRdr.Read())
          {
            int thisClientId = clientQueryRdr.GetInt32(0);
            string clientName = clientQueryRdr.GetString(1);
            Client foundClient = new Client(clientName, thisClientId);
            clients.Add(foundClient);
          }
          clientQueryRdr.Dispose();
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return clients;
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
        Stylist selectedStylist = Stylist.Find(stylistId);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Client> stylistClient = selectedStylist.GetClients();
        List<Specialty> stylistSpecialty = selectedStylist.GetSpecialties();
        model.Add("stylist", selectedStylist);

        foreach (Specialty client in stylistSpecialty)
        {
          client.Delete();
        }

        foreach (Specialty specialty in stylistSpecialty)
        {
          specialty.Delete();
        }

        MySqlCommand cmd = new MySqlCommand(@"DELETE FROM stylists WHERE id = @thisId", conn);
        cmd.Parameters.AddWithValue("@thisId", _id);
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }


      }

      public void Delete()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("DELETE FROM stylists WHERE id = @StylistId; DELETE FROM stylists_clients WHERE stylist_id = @StylistId; DELETE FROM stylists_specialties WHERE stylist_id = @StylistId;", conn);
        cmd.Parameters.AddWithValue("@StylistId", this.GetId());
        cmd.ExecuteNonQuery();
        if (conn != null)
        {
          conn.Close();
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

      public void AddClient (Client newClient)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(@"INSERT INTO stylists_clients (stylist_id, client_id) VALUES (@StylistId, @ClientId);", conn);
        cmd.Parameters.AddWithValue("@StylistId", _id);
        cmd.Parameters.AddWithValue("@ClientId", newClient.GetId());
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public void Edit(string newName)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@newName";
        name.Value = newName;
        cmd.Parameters.Add(name);
        cmd.ExecuteNonQuery();
        _name = newName;

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
  }
}
