using SQLite;

namespace CatchUs.Model
{
    [Table("UserMeeting")]
    public class UserMeeting
    {
        public int User_Id { get; set; }
        public int Meeting_Id { get; set; }
    }
}
