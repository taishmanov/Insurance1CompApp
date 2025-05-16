using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance1CompAppServices.Repositories
{
    public interface ICountryGwpRepository
    {
        Task<Dictionary<string, List<double>>> GetGwpDataAsync(string country, List<string> lobs);
    }
}
