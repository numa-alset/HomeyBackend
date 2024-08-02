namespace HomeyBackend.Core.Models
{

    public class PlaceDetailBoolean
    {
        public int Id { get; set; }
        public Place Place { get; set; }
        public bool Wifi { get; set; }
        public bool Furntiure { get; set; }
        public bool Garden { get; set; }
        public bool Pool { get; set; }
        public bool Elevator { get; set; }
        public bool SolarPower { get; set; }

    }
}
