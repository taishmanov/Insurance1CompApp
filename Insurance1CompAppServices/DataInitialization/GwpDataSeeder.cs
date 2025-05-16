using Insurance1CompAppServices.Repositories.LocalRepositories.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance1CompAppServices.DataInitialization
{
    public class GwpDataSeeder
    {
        private readonly AppDbContext _dbContext;
        private readonly CsvImporter _importer;

        public GwpDataSeeder(AppDbContext dbContext, CsvImporter importer)
        {
            _dbContext = dbContext;
            _importer = importer;
        }

        public void Seed(string csvPath)
        {
            if (!_dbContext.GwpRecords.Any())
            {
                var records = _importer.Import(csvPath);
                _dbContext.GwpRecords.AddRange(records);
                _dbContext.SaveChanges();
            }
        }
    }
}
