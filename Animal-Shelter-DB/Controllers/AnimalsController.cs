using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    [HttpGet("/Animals")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/Animals/Tigers")]
    public ActionResult Tigers()
    {
      Tiger newTiger1 = new Tiger("Fuzzy", "Male", 12);
      Tiger newTiger2 = new Tiger("Maude", "Female", 15);

      newTiger1.SaveTiger();
      newTiger2.SaveTiger();

      List<Tiger> allTigers = Tiger.GetAllTigers();

      return View(allTigers);
    }
  }
}
