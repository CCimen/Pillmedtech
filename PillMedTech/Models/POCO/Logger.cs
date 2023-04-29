using System.ComponentModel.DataAnnotations;

namespace PillMedTech.Models.POCO
{
    public class Logger
    {
        [Key]
            public int Id { get; set; } // Primärnyckel
            public DateTime Time { get; set; } // Tidpunkt för loggningen
            // Create for EmployeeId
            public string EmployeeId { get; set; } // Anställningsnummer
            public string Ip { get; set; } // IP-adress
            public string Action { get; set; } // Vilken åtgärd som utfördes (inloggning, sjukskrivning, sökning, etc.)
    }

    }

