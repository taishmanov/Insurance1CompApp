using CsvHelper;
using Insurance1CompAppServices.Repositories.LocalRepositories.Ef.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance1CompAppServices.DataInitialization
{
    public class CsvImporter
    {
        public IEnumerable<GwpRecord> Import(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Read();
            csv.ReadHeader();

            var records = new List<GwpRecord>();

            while (csv.Read())
            {
                var country = csv.GetField("country");
                var lob = csv.GetField("lineOfBusiness");

                for (int year = 2008; year <= 2015; year++)
                {
                    var field = $"Y{year}";
                    if (decimal.TryParse(csv.GetField(field), out decimal value))
                    {
                        records.Add(new GwpRecord
                        {
                            CountryCode = country,
                            LineOfBusinessCode = lob,
                            Year = year,
                            Value = value
                        });
                    }
                }
            }

            return records;
        }
    }
}
