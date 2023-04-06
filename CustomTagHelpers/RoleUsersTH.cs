using TreatsTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TreatsTracker.CustomTagHelpers
{
  [HtmlTargetElement("td", Attributes = "i-role")]
  public class RoleUsersTH : TagHelper
  {
    private UserManager<ApplicationUser> userManager;
    private RoleManager<IdentityRole> roleManager;

    public RoleUsersTH(UserManager<ApplicationUser> usermgr, RoleManager<IdentityRole> rolemgr)
    {
      userManager = usermgr;
      roleManager = rolemgr;
    }

  }
}