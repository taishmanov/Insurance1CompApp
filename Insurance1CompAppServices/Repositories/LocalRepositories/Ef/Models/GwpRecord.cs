namespace Insurance1CompAppServices.Repositories.LocalRepositories.Ef.Models
{
    public class GwpRecord
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string LineOfBusinessCode { get; set; }
        public int Year { get; set; }
        public decimal Value { get; set; }
    }
}
