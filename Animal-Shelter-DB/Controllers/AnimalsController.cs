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

    [HttpGet("/Animals/CreateForm")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpGet("/Animals/List")]
    public ActionResult List()
    {
      List<Animal> allAnimals = Animal.GetAllAnimals();

      return View(allAnimals);
    }

    [HttpPost("/Animals/List")]
    public ActionResult AddAnimal(string newtype, string newname, string newgender, int newage)
    {
      Animal newAnimal = new Animal(newtype, newname, newgender, newage);
      newAnimal.SaveAnimal();

      List<Animal> allAnimals = Animal.GetAllAnimals();
      return View("List", allAnimals);
    }

    [HttpGet("/Animals/Deleted")]
    public ActionResult ClearAnimals()
    {
      Animal.DeleteAllAnimals();
      List<Animal> allAnimals = Animal.GetAllAnimals();
      return View("List", allAnimals);
    }
  }
}
