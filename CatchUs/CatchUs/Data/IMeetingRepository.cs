using CatchUs.Model;
using System.Collections.Generic;

namespace CatchUs.Data
{
    public interface IMeetingRepository
    {
        List<Meeting> GetAllMeetings();
        Meeting GetMeetingById(int id);
        void InsertMeeting(Meeting user);
        void DeleteMeeting(int id);
        void UpdateMeeting(string property, string newValue, int id);
    }
}
