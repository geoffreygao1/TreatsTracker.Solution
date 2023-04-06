using System.ComponentModel.DataAnnotations;

namespace TreatsTracker.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}