using Insurance1CompAppServices.Repositories;

namespace Insurance1CompAppServices
{
    public class CountryGwpService
    {
        private readonly ICountryGwpRepository _repository;

        public CountryGwpService(ICountryGwpRepository repository)
        {
            _repository = repository;
        }

        public async Task<Dictionary<string, double>> GetAverageGwpAsync(string country, List<string> lobs)
        {
            var data = await _repository.GetGwpDataAsync(country, lobs);
            var result = new Dictionary<string, double>();
            foreach (var lob in lobs)
            {
                if (data.ContainsKey(lob) && data[lob].Count > 0)
                {
                    var avg = data[lob].Average();
                    result[lob] = Math.Round(avg, 1);
                }
            }
            return result;
        }
    }
}
