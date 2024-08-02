namespace HomeyBackend.Core.Models
{
    public class PlaceDetailNumberRooms
    {
        public int Id { get; set; }
        public Place Place { get; set; }
        public int NumberOfSalons { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBeds { get; set; }
        public int NumberOfBaths { get; set; }

    }
}
