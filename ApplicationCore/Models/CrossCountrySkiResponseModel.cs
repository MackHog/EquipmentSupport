namespace ApplicationCore.Models
{
    public class CrossCountrySkiRecommendation
    {
        public int SkiLength { get; }
        public string Info { get; }
        public string Text { get; }
        public CrossCountrySkiRecommendation(int skiLength, string info)
        {
            SkiLength = skiLength;
            Info = info;
            Text = $"Rekommenderad längd är {skiLength}";
        }
    }
}
