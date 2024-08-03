using System.ComponentModel.DataAnnotations;

namespace HomeyBackend.Controllers.Resources
{
    public class RateResource
    {
        [Range(0, 5)]
        public int UserRate { get; set; }
        public int PlaceId { get; set; }
    }
}
