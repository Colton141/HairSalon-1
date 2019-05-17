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
      List<Specialty> stylistSpecialty = selectedStylist.GetSpecialties();
      List<Specialty> allSpecialties = Specialty.GetAll();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClient);
      model.Add("specialties", stylistSpecialty);
      model.Add("allSpecialties", allSpecialties);
      return View(model);
    }
    //

    [HttpPost("/stylists/{stylistId}/clients")]
    public ActionResult Create(int stylistId, string clientName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(stylistId);
      Client newClient = new Client(clientName, foundStylist.GetId());
      newClient.Save();
      List<Client> stylistClient = foundStylist.GetClients();
      model.Add("clients", stylistClient);
      model.Add("stylist", newClient);
      return View("Show", model);
    }

    [HttpPost("/stylists/delete")]
    public ActionResult DeleteAll()
    {
      Stylist.ClearAll();
      return RedirectToAction("Index");
    }

    [HttpPost("/stylists/{stylistId}/clients/delete")]
    public ActionResult Clear()
    {
      Client.ClearAll();
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


  }
}
