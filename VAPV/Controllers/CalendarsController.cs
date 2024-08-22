using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VAPV.DTO;

namespace VAPV.Controllers {
    public class CalendarsController : Controller {
        private readonly string _calendarFilePath = Path.Combine(Directory.GetCurrentDirectory(), "calendar.json");

        //Získává data pro aktuální den a dva okolní dny, vrací view a do něj zpracovaná data
        public IActionResult Index() {
            var today = DateTime.Today;
            var calendarDays = GetCalendarDaysForThreeDays(today);
            return View(calendarDays);
        }

        //
        private List<CalendarDayDTO> GetCalendarDaysForThreeDays(DateTime startDate) {
            var allDays = LoadCalendarDays();
            return allDays.Where(day => day.Date >= startDate.AddDays(-1) && day.Date <= startDate.AddDays(1)).ToList();
        }

        //
        private List<CalendarDayDTO> LoadCalendarDays() {
            var json = System.IO.File.ReadAllText(_calendarFilePath);
            var days = JsonConvert.DeserializeObject<List<CalendarDayDTO>>(json);
            Console.WriteLine("Načteno dní: " + days.Count); // Přidá log pro kontrolu počtu načtených dní
            return days;
        }
    }
}
