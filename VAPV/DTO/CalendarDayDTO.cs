using Newtonsoft.Json;

namespace VAPV.DTO {
    public class CalendarDayDTO {
        [JsonProperty("date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Date { get; set; }
        public string CzechName { get; set; }
        public List<string> InternationalDays { get; set; }
        public List<string> Excuses { get; set; }
    }

}
