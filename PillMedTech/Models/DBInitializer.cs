
namespace PillMedTech.Models
{
  public class DBInitializer
  {
    public static void EnsurePopulated(IServiceProvider services)
    {
      var context = services.GetRequiredService<ApplicationDbContext>();

      if (!context.Employees.Any())
      {
        context.Employees.AddRange(
          new Employee { EmployeeId = "EMP330", EmployeeName = "Ada Öqvist", SecurityNo = "19890620-4443", Adress = "Svartsjövägen 12", Mail = "ada@pillmedtech.com", Phone = "070-12345678" },
          new Employee { EmployeeId = "EMP430", EmployeeName = "Charlie Jansson", SecurityNo = "19921114-4438", Adress = "Storgatan 44", Mail = "charlie@pillmedtech.com", Phone = "070-12456789" },
          new Employee { EmployeeId = "EMP530", EmployeeName = "Bertil Gustavsson", SecurityNo = "19780211-4475", Adress = "Blåsjögatan 2", Mail = "bertil@pillmedtech.com", Phone = "070-12567890" },
          new Employee { EmployeeId = "HRS270", EmployeeName = "Amelia Gundersson", SecurityNo = "19650404-4465", Adress = "Lilla gränd 6", Mail = "amelia@pillmedtech.com", Phone = "070-12678901" },
          new Employee { EmployeeId = "ITS980", EmployeeName = "Tove Berg", SecurityNo = "20000105-4442", Adress = "Storsjövägen 78", Mail = "tove@pillmedtech.com", Phone = "070-12789012" }
          );
        context.SaveChanges();
      }

      if (!context.Childrens.Any())
      {
        context.Childrens.AddRange(
           new Children { ChildName = "Alice", SecurityNo = "20140620-4482", EmployeeId = "EMP330" },
           new Children { ChildName = "Maj", SecurityNo = "20160413-4421", EmployeeId = "EMP330" },
           new Children { ChildName = "Noah", SecurityNo = "20181010-4471", EmployeeId = "EMP330" },
           new Children { ChildName = "William", SecurityNo = "20171202-4492", EmployeeId = "EMP430" }
          );
        context.SaveChanges();
      }

      if (!context.SickErrands.Any())
      {
        context.SickErrands.AddRange(
          new SickErrand { EmployeeID = "EMP330", ChildName = "Alice", TypeOfAbsence = "VAB", HomeFrom = new DateTime(2021, 09, 16), HomeUntil = new DateTime(2021, 09, 17) },
          new SickErrand { EmployeeID = "EMP430", ChildName = "ej aktuellt", TypeOfAbsence = "Sjuk med intyg", HomeFrom = new DateTime(2021, 08, 10), HomeUntil = new DateTime(2021, 08, 20) },
          new SickErrand { EmployeeID = "EMP530", ChildName = "ej aktuellt", TypeOfAbsence = "Sjuk utan intyg", HomeFrom = new DateTime(2021, 07, 22), HomeUntil = new DateTime(2021, 07, 23) },
          new SickErrand { EmployeeID = "EMP430", ChildName = "William", TypeOfAbsence = "VAB", HomeFrom = new DateTime(2021, 09, 16), HomeUntil = new DateTime(2021, 09, 17) },
          new SickErrand { EmployeeID = "EMP330", ChildName = "Maj", TypeOfAbsence = "VAB", HomeFrom = new DateTime(2021, 06, 15), HomeUntil = new DateTime(2021, 06, 18) },
          new SickErrand { EmployeeID = "EMP530", ChildName = "ej aktuellt", TypeOfAbsence = "Sjuk utan intyg", HomeFrom = new DateTime(2021, 05, 12), HomeUntil = new DateTime(2021, 05, 13) }
          );
        context.SaveChanges();
      }


    }
  }
}
