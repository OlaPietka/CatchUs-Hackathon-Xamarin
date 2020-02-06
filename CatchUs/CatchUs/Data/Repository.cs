using CatchUs.Model;
using CatchUs.Services;
using System.Collections.Generic;

namespace CatchUs.Data
{
    public class Repository : IUserRepository, IMeetingRepository, IUserMeetingRepository, IPinRepository, IPasswordRecoveryRepository
    {
        DatabaseController database;

        public Repository()
        {
            database = new DatabaseController();
            foreach(var c in database.GetAllPins())
            {
                database.DeletePin(c.Id);
            }
        }

        #region User Querys
        public List<User> GetAllUsers()
        {
            return database.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return database.GetUserById(id);
        }

        public User GetUserByEmail(string email)
        {
            return database.GetUserByEmail(email);
        }

        public void InsertUser(User user)
        {
            database.InsertUser(user);
        }

        public void DeleteUser(int id)
        {
            database.DeleteUser(id);
        }

        public void UpdateUser(string property, string newValue, int id)
        {
            database.UpdateUser(property, newValue, id);
        }

        public bool IsUserEmailExist(string email)
        {
            foreach (User u in GetAllUsers())
            {
                if (u.Email.Equals(email))
                    return true;
            }

            return false;
        }
        #endregion

        #region Meeting Querys
        public List<Meeting> GetAllMeetings()
        {
            return database.GetAllMeetings();
        }

        public Meeting GetMeetingById(int id)
        {
            return database.GetMeetingById(id);
        }

        public void InsertMeeting(Meeting meeting)
        {
            database.InsertMeeting(meeting);
        }

        public void DeleteMeeting(int id)
        {
            database.DeleteMeeting(id);
        }

        public void UpdateMeeting(string property, string newValue, int id)
        {
            database.UpdateMeeting(property, newValue, id);
        }
        #endregion

        #region UserMeeting Querys
        public List<UserMeeting> GetAllUserMeetings()
        {
            return database.GetAllUserMeetings();
        }

        public List<UserMeeting> GetAllMeetingsFromUser(int id)
        {
            return database.GetAllMeetingsFromUser(id);
        }

        public List<UserMeeting> GetAllUsersFromMeeting(int id)
        {
            return database.GetAllUsersFromMeeting(id);
        }

        public void InsertUserMeeting(UserMeeting userMeeting)
        {
            database.InsertUserMeeting(userMeeting);
        }

        public void DeleteUserMeetingByUserId(int id)
        {
            database.DeleteUserMeetingByUserId(id);
        }

        public void DeleteUserMeetingByMeetingId(int id)
        {
            database.DeleteUserMeetingByMeetingId(id);
        }
        #endregion

        #region Pin Querys
        public List<Pin> GetAllPins()
        {
            return database.GetAllPins();
        }

        public Pin GetPinById(int id)
        {
            return database.GetPinById(id);
        }

        public int GetIdByPin(Pin pin)
        {
            foreach (Pin p in database.GetAllPins())
            {
                if (p.Latitude == pin.Latitude && p.Longitude == pin.Longitude && p.Icon == pin.Icon)
                    return p.Id;
            }

            return -1;
        }

        public void InsertPin(Pin pin)
        {
            database.InsertPin(pin);
        }

        public void DeletePin(int id)
        {
            database.DeletePin(id);
        }
        #endregion

        #region PasswordRecovery Querys
        public List<PasswordRecovery> GetAllPasswordsRecovery()
        {
            return database.GetAllPasswordsRecovery();
        }

        public PasswordRecovery GetPasswordRecoveryByEmail(string email)
        {
            return database.GetPasswordRecoveryByEmail(email);
        }

        public void InsertPasswordRecovery(PasswordRecovery passwordRecovery)
        {
            database.InsertPasswordRecovery(passwordRecovery);
        }

        public void DeletePasswordRecovery(int id)
        {
            database.DeletePasswordRecovery(id);
        }

        public bool IsPasswordRecoveryEmailExist(string email)
        {
            foreach (PasswordRecovery p in database.GetAllPasswordsRecovery())
            {
                if (p.Email == email)
                    return true;
            }

            return false;
        }
        #endregion
    }
}
