using Insurance1CompAppServices.Repositories;
using Insurance1CompAppServices.Repositories.LocalRepositories.Ef;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance1CompAppServices.Repositories.LocalRepositories
{
    public class CountryGwpRepository : ICountryGwpRepository
    {
        private readonly AppDbContext _context;

        public CountryGwpRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, List<double>>> GetGwpDataAsync(string country, List<string> lobs, int yearFrom, int yearTo)
        {
            var query = await _context.GwpRecords
                .Where(g => g.CountryCode == country
                        && lobs.Contains(g.LineOfBusinessCode)
                        && g.Year >= yearFrom && g.Year <= yearTo)
                .ToListAsync();

            var grouped = query
                .GroupBy(g => g.LineOfBusinessCode)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(x => x.Year).Select(x => (double)x.Value).ToList()
                );

            return grouped;
        }
    }
}
