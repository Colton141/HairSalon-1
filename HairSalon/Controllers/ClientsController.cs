using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {

    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpGet("/clients/new")]
    public ActionResult New()
    {
       return View();
    }

    [HttpPost("/clients")]
    public ActionResult Create(string clientName)
    {
      Client newClient = new Client(clientName);
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      return RedirectToAction("Index", allClients);
    }


    [HttpGet("/clients/{clientId}")]
    public ActionResult Show(int stylistId, int clientId)
    {
      Client client = Client.Find(clientId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("client", client);
      model.Add("stylist", stylist);
      return View(model);
    }

    [HttpGet("/clients/{clientId}/edit")]
    public ActionResult Edit(int stylistId, int clientId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      Client client = Client.Find(clientId);
      model.Add("client", client);
      return View(model);
    }


    [HttpPost("/clients/{clientId}")]
    public ActionResult Update(int stylistId, int clientId, string newName)
    {
      Client client = Client.Find(clientId);
      client.Edit(newName);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      model.Add("client", client);
      return View("Show", model);
    }

    [HttpPost("/clients/delete")]
    public ActionResult Clear()
    {
      Client.ClearAll();
      return RedirectToAction("Index");
    }

    [HttpPost("/clients/{clientId}/delete")]
    public ActionResult Delete(int clientId)
    {
      Client client = Client.Find(clientId);
      client.Delete();
      return RedirectToAction("Index");
    }

  }
}
