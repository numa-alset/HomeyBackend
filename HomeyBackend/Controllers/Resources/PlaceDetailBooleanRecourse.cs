using HomeyBackend.Core.Models;

namespace HomeyBackend.Controllers.Resources
{
    public class PlaceDetailBooleanRecourse
    {
        public bool Wifi { get; set; } = false;
        public bool Furntiure { get; set; } = false;
        public bool Garden { get; set; } = false;
        public bool Pool { get; set; } = false;
        public bool Elevator { get; set; } = false;
        public bool SolarPower { get; set; } = false;
    }
}
