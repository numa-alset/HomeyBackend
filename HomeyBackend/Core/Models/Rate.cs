using System.ComponentModel.DataAnnotations;

namespace HomeyBackend.Core.Models
{
    public class Rate
    {
        public int Id { get; set; }
        [Range(0, 5)]
        public int UserRate { get; set; }
        public int PlaceId { get; set; }
        public String UserId { get; set; }

    }
}
