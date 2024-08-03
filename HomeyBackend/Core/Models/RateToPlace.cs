namespace HomeyBackend.Core.Models
{
    public class RateToPlace
    {
        public Guid Id { get; set; }
        public int TotalNumberOfRates { get; set; }
        public int SumOfRates { get; set; }
        public Place Place { get; set; }
        public int PlaceId { get; set; }

        public void Increase(int rate)
        {
            this.TotalNumberOfRates += 1;
            this.SumOfRates += rate;
        }
        public void Decrease(int rate)
        {
            this.TotalNumberOfRates -= 1;
            this.SumOfRates -= rate;
        }
    }
}
