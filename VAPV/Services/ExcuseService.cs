using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VAPV.DTO;
using VAPV.Models;

namespace VAPV.Services {
    public class ExcuseService {
        private ExcuseDbContext _excuseDbcontext;

        public ExcuseService(ExcuseDbContext excuseDbcontext) {
            _excuseDbcontext = excuseDbcontext;
        }

        //načtení všechn výmluvy z databáze, mapuje je na DTO a vrací je jako seznam.
        public async Task<List<ExcuseDTO>> GetExcusesAsync() {
            return await _excuseDbcontext.Excuses.Select(exc => new ExcuseDTO {
                Id = exc.Id,
                Category = exc.Category,
                Text = exc.Text
            }).ToListAsync();
        }

        //vyhledávat výmluvy podle zadaného textu nebo kategorie
        public async Task<List<ExcuseDTO>> SearchExcusesAsync(string searchTerm, string category) {
            var query = _excuseDbcontext.Excuses.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm)) {
                query = query.Where(exc => exc.Text.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(category)) {
                query = query.Where(exc => exc.Category == category);
            }

            return await query.Select(exc => new ExcuseDTO {
                Id = exc.Id,
                Category = exc.Category,
                Text = exc.Text
            }).ToListAsync();
        }

        //vrací počet všech výmluv v databázi
        public async Task<int> GetExcuseCountAsync() {
            return await _excuseDbcontext.Excuses.CountAsync();
        }

        //vrací seznam všech unikátních kategorií výmluv - Distinct() zajišťuje, že se každá kategorie v seznamu objeví pouze jednou  
        public async Task<List<string>> GetCategoriesAsync() {
            return await _excuseDbcontext.Excuses
                .Select(e => e.Category)
                .Distinct()
                .ToListAsync();
        }

        //vrací náhodnou výmluvu z databáze - Nejprve načte všechny texty výmluv, pak vybere náhodný text přes random.Next()
        public async Task<string> GetRandomExcuseAsync() {
            var excuses = await _excuseDbcontext.Excuses.Select(e => e.Text).ToListAsync();
            if (excuses.Count == 0) {
                return "Žádné výmluvy nenalezeny.";
            }
            var random = new Random();
            int index = random.Next(excuses.Count);
            return excuses[index];
        }
    }
}
