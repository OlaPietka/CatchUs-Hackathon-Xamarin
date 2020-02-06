using SQLite;

namespace CatchUs.Model
{
    [Table("Pin")]
    public class Pin
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Icon { get; set; }
    }
}
