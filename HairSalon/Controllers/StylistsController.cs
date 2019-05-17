using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {

    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName)
    {
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClient = selectedStylist.GetClients();
      List<Client> allClients = Client.GetAll();
      List<Specialty> stylistSpecialty = selectedStylist.GetSpecialties();
      List<Specialty> allSpecialties = Specialty.GetAll();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClient);
      model.Add("allClients", allClients);
      model.Add("specialties", stylistSpecialty);
      model.Add("allSpecialties", allSpecialties);
      return View(model);
    }
    //

    // [HttpPost("/stylists/{stylistId}/clients")]
    // public ActionResult Create(int stylistId, string clientName)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Stylist foundStylist = Stylist.Find(stylistId);
    //   Client newClient = new Client(clientName, stylistId);
    //   newClient.Save();
    //   foundStylist.GetClients();
    //   List<Client> stylistClient = foundStylist.GetClients();
    //   model.Add("clients", stylistClient);
    //   model.Add("stylist", foundStylist);
    //   return View("Show", foundStylist);
    // }

    [HttpPost("/stylists/delete")]
    public ActionResult DeleteAll()
    {
      Stylist.ClearAll();
      return RedirectToAction("Index");
    }

    [HttpPost("/stylists/{stylistId}/delete")]
    public ActionResult Delete(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      stylist.Delete();
      return RedirectToAction("Index");
    }



    [HttpPost("/stylists/{stylistId}/specialties/new")]
    public ActionResult AddSpecialty(int stylistId, int specialtyId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Specialty specialty = Specialty.Find(specialtyId);
      stylist.AddSpecialty(specialty);
      return RedirectToAction("Show",  new { id = stylistId });
    }

    [HttpPost("/stylists/{stylistId}/clients/new")]
    public ActionResult AddClient(int stylistId, int clientId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Client client = Client.Find(clientId);
      stylist.AddClient(client);
      return RedirectToAction("Show",  new { id = stylistId });
    }

    [HttpGet("/stylists/{stylistId}/edit")]
    public ActionResult Edit(int stylistId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      return View(model);
    }

    [HttpPost("/stylists/{stylistId}")]
    public ActionResult Update(int stylistId, string newName)
    {
      Stylist stylist = Stylist.Find(stylistId);
      stylist.Edit(newName);
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylist", stylist);
      return RedirectToAction("Index");
    }




  }
}
