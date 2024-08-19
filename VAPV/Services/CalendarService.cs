using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using VAPV.DTO;

namespace VAPV.Services {
    public class CalendarService {
        private readonly string _calendarFilePath = "calendar.json";

        private List<CalendarDayDTO> LoadCalendarDays() {
            var json = File.ReadAllText(_calendarFilePath);
            var days = JsonConvert.DeserializeObject<List<CalendarDayDTO>>(json, new JsonSerializerSettings {
                DateFormatString = "dd-MM-yyyy",
                Converters = new List<JsonConverter> { new CustomDateTimeConverter() }
            });
            Console.WriteLine("Načteno dní: " + days.Count);
            return days;
        }

        public List<CalendarDayDTO> GetCalendarDaysForThreeDays() {
            var days = LoadCalendarDays();
            var today = DateTime.Today;
            return days.Where(day => day.Date == today || day.Date == today.AddDays(1) || day.Date == today.AddDays(-1)).ToList();
        }
    }
}
