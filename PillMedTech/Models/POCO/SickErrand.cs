using System.ComponentModel.DataAnnotations;

namespace PillMedTech.Models
{
  public class SickErrand
  {
    public int SickErrandID { get; set; }

    [Display(Name = "Användarid:")]
    [Required(ErrorMessage = "Du måste fylla i ditt anställningsid")]
    public string EmployeeID { get; set; }

    public string TypeOfAbsence { get; set; }

    [Display(Name = "Barnets namn:")]
    [Required(ErrorMessage = "Du måste välja ditt barns namn")]
    public string ChildName { get; set; }

    [Display(Name = "Sjuk från:")]
    [Required(ErrorMessage = "Du måste välja datum när din sjukskrivning börjar")]
    [DataType(DataType.Date)]
    public DateTime HomeFrom { get; set; }

    [Display(Name = "Sjuk till:")]
    [Required(ErrorMessage = "Du måste välja datum när din sjukskrivning börjar")]
    [DataType(DataType.Date)]
    public DateTime HomeUntil { get; set; }

  }
}
