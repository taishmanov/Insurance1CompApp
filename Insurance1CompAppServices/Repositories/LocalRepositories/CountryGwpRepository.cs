using Insurance1CompAppServices.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance1CompAppServices.Repositories.LocalRepositories
{
    public class CountryGwpRepository : ICountryGwpRepository
    {
        public async Task<Dictionary<string, List<double>>> GetGwpDataAsync(string country, List<string> lobs)
        {
            // Replace with actual DB logic
            var mockData = new Dictionary<string, List<double>>
            {
                { "property", new List<double> { 100, 200, 300, 400, 500, 600, 700, 800 } },
                { "transport", new List<double> { 200, 300, 400, 500, 600, 700, 800, 900 } },
                { "liability", new List<double> { 300, 400, 500, 600, 700, 800, 900, 1000 } }
            };
            var result = new Dictionary<string, List<double>>();
            foreach (var lob in lobs)
            {
                if (mockData.ContainsKey(lob))
                    result[lob] = mockData[lob];
            }
            return await Task.FromResult(result);
        }
    }
}
