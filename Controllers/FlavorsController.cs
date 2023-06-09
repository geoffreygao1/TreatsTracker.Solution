using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TreatsTracker.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TreatsTracker.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly TreatsTrackerContext _db;

    public FlavorsController(TreatsTrackerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors
                            .ToList();
      return View(model);
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Create()
    {
      return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      if (!ModelState.IsValid)
      {
        return View(flavor);
      }
      else
      {
        _db.Flavors.Add(flavor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Flavor thisFlavor = _db.Flavors
          .Include(flavor => flavor.FlavorTreatJoinEntities)
          .ThenInclude(join => join.Treat)
          .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Edit(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Flavors.Update(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Delete(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public ActionResult AddTreat(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.TreatCount = _db.Treats.ToList().Count;
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(thisFlavor);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int treatId)
    {
#nullable enable
      FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => (join.TreatId == treatId && join.FlavorId == flavor.FlavorId));
#nullable disable
      if (joinEntity == null && treatId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { TreatId = treatId, FlavorId = flavor.FlavorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = flavor.FlavorId });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult DeleteJoin(int joinId, int flavorId)
    {
      FlavorTreat joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreats.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = flavorId });
    }

  }
}