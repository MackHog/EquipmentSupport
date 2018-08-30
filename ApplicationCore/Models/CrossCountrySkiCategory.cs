namespace ApplicationCore.Models
{
    public class CrossCountrySkiCategory
    {
        public string Name { get; set; }
        public int FromAge { get; set; }
        public int? ToAge { get; set; }
        public string Style { get; set; }
        public int AdditionalLength { get; set; }
        public int? MaxLength { get; set; }
        public int? AdditionalMinLength { get; set; }
        public string Info { get; set; }
    }
}
