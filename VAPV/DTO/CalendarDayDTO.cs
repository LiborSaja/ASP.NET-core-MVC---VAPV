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

//DateTime Date -> datum daného dne. Vlastnost je anotována atributem [JsonProperty("date")], což znamená, že při deserializaci JSON souboru se
//                tato hodnota bude mapovat z pole date v JSONu.
//List<string> InternationalDays -> seznam mezinárodních dnů pro daný den
//List<string> Excuses -> seznam výmluv pro daný den
//[JsonConverter(typeof(CustomDateTimeConverter))] -> zajišťuje správnou konverzi datových formátů mezi JSONem a DateTime v C#.