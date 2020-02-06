using SQLite;
using System;

namespace CatchUs.Model
{
    [Table("User")]
    public class User
    {
        int? likes;
        int? organisedMeetings;
        int? participateIn;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public int? Likes
        {
            get
            {
                if (likes == null)
                    return 0;

                return likes;
            }
            set
            {
                likes = value;
            }
        }
        public int? OrganisedMeetings
        {
            get
            {
                if (organisedMeetings == null)
                    return 0;

                return organisedMeetings;
            }
            set
            {
                organisedMeetings = value;
            }
        }
        public int? ParticipateIn
        {
            get
            {
                if (participateIn == null)
                    return 0;

                return participateIn;
            }
            set
            {
                participateIn = value;
            }
        }
        public DateTime Joined { get; set; }


    }
}
