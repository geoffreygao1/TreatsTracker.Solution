using Microsoft.AspNetCore.Mvc;
using TreatsTracker.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;


namespace TreatsTracker.Controllers;

public class HomeController : Controller
{
  private readonly TreatsTrackerContext _db;
  private readonly UserManager<ApplicationUser> _userManager;

  public HomeController(UserManager<ApplicationUser> userManager, TreatsTrackerContext db)
  {
    _userManager = userManager;
    _db = db;
  }

  [HttpGet("/")]
  public async Task<ActionResult> Index()
  {
    return View();
  }
}
