using CatchUs.Model;
using System.Collections.Generic;

namespace CatchUs.Data
{
    public interface IUserMeetingRepository
    {
        List<UserMeeting> GetAllUserMeetings();
        List<UserMeeting> GetAllMeetingsFromUser(int id);
        List<UserMeeting> GetAllUsersFromMeeting(int id);
        void InsertUserMeeting(UserMeeting userMeeting);
        void DeleteUserMeetingByUserId(int id);
        void DeleteUserMeetingByMeetingId(int id);
    }
}
