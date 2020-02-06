using SQLite;
using System;

namespace CatchUs.Model
{
    [Table("Meeting")]
    public class Meeting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int OrganizerUser_Id { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerLastName { get; set; }
        public DateTime OrganizerAge { get; set; }
        public string ActivityName { get; set; }
        public string ActivityIcon { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Participants { get; set; }
        public string Description { get; set; }
        public string Chat { get; set; }
        public int Pin_Id { get; set; }
    }
}
