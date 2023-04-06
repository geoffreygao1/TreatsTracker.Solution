using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TreatsTracker.Models
{
  public class Treat
  {
    public int TreatId { get; set; }
    public string Name { get; set; }
    public List<FlavorTreat> FlavorTreatJoinEntities { get; }
    public ApplicationUser User { get; set; }

  }
}