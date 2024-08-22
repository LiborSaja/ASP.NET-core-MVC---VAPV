using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using VAPV.DTO;

namespace VAPV.Services {
    public class CalendarService {
        private readonly string _calendarFilePath = "calendar.json";

        //načítá data ze JSON a deserializuje je do seznamu objektů dle vlastností v CalendarDayDTO, vlastního konvertor pro datum
        private List<CalendarDayDTO> LoadCalendarDays() {
            var json = File.ReadAllText(_calendarFilePath);
            var days = JsonConvert.DeserializeObject<List<CalendarDayDTO>>(json, new JsonSerializerSettings {
                DateFormatString = "dd-MM-yyyy",
                Converters = new List<JsonConverter> { new CustomDateTimeConverter() }
            });
            Console.WriteLine("Načteno dní: " + days.Count);
            return days;
        }

        //vrací seznam tří dnů, aktuálního, předchozího a následujícího
        public List<CalendarDayDTO> GetCalendarDaysForThreeDays() {
            var days = LoadCalendarDays();   // Načtení všech dnů z JSON souboru
            var today = DateTime.Today;     // Získání dnešního data bez času
            return days.Where(day => day.Date == today || day.Date == today.AddDays(1) || day.Date == today.AddDays(-1)).ToList();//porovnání a vracení
        }
    }
}
