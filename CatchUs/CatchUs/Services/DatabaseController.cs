using CatchUs.Model;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CatchUs.Services
{
    public class DatabaseController
    {

        SQLiteConnection database;

        public DatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<User>();
            database.CreateTable<Pin>();
            database.CreateTable<Meeting>();
            database.CreateTable<UserMeeting>();
            database.CreateTable<PasswordRecovery>();
        }

        #region User Querys
        public List<User> GetAllUsers()
        {
            return database.Query<User>("SELECT * FROM User");
        }

        public User GetUserById(int id)
        {
            return database.Table<User>().FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return database.Table<User>().FirstOrDefault(u => u.Email == email);
        }

        public void InsertUser(User user)
        {
            database.Insert(user);
        }

        public void DeleteUser(int id)
        {
            database.Delete<User>(id);
        }

        public void UpdateUser(string property, string newValue, int id)
        {
            database.Query<User>($"UPDATE User SET {property} = '{newValue}' WHERE Id = {id}");
        }
        #endregion

        #region Meeting Querys
        public List<Meeting> GetAllMeetings()
        {
            return database.Query<Meeting>("SELECT * FROM Meeting");
        }

        public Meeting GetMeetingById(int id)
        {
            return database.Table<Meeting>().FirstOrDefault(u => u.Id == id);
        }

        public void InsertMeeting(Meeting meeting)
        {
            database.Insert(meeting);
        }

        public void DeleteMeeting(int id)
        {
            database.Delete<Meeting>(id);
        }

        public void UpdateMeeting(string property, string newValue, int id)
        {
            database.Query<Meeting>($"UPDATE Meeting SET {property} = '{newValue}' WHERE Id = {id}");
        }
        #endregion

        #region UserMeeting Querys
        public List<UserMeeting> GetAllUserMeetings()
        {
            return database.Query<UserMeeting>($"SELECT * FROM UserMeeting");
        }

        public List<UserMeeting> GetAllMeetingsFromUser(int id)
        {
            return database.Query<UserMeeting>($"SELECT Meeting_Id FROM UserMeeting WHERE User_Id = {id}");
        }

        public List<UserMeeting> GetAllUsersFromMeeting(int id)
        {
            return database.Query<UserMeeting>($"SELECT User_Id FROM UserMeeting WHERE Meeting_Id = {id}");
        }

        public void InsertUserMeeting(UserMeeting userMeeting)
        {
            database.Insert(userMeeting);
        }

        public void DeleteUserMeetingByUserId(int id)
        {
            database.Query<UserMeeting>($"DELETE FROM UserMeeting WHERE User_Id = {id}");
        }

        public void DeleteUserMeetingByMeetingId(int id)
        {
            database.Query<UserMeeting>($"DELETE FROM UserMeeting WHERE Meeting_Id = {id}");
        }
        #endregion

        #region Pin Querys
        public List<Pin> GetAllPins()
        {
            return database.Query<Pin>($"SELECT * FROM Pin");
        }

        public Pin GetPinById(int id)
        {
            return database.Table<Pin>().FirstOrDefault(u => u.Id == id);
        }

        public void InsertPin(Pin pin)
        {
            database.Insert(pin);
        }

        public void DeletePin(int id)
        {
            database.Delete<Pin>(id);
        }
        #endregion

        #region PasswordRecovery Querys
        public List<PasswordRecovery> GetAllPasswordsRecovery()
        {
            return database.Query<PasswordRecovery>("SELECT * FROM PasswordRecovery");
        }

        public PasswordRecovery GetPasswordRecoveryByEmail(string email)
        {
            return database.Table<PasswordRecovery>().FirstOrDefault(u => u.Email == email);
        }

        public void InsertPasswordRecovery(PasswordRecovery pass)
        {
            database.Insert(pass);
        }

        public void DeletePasswordRecovery(int id)
        {
            database.Delete<PasswordRecovery>(id);
        }
        #endregion
    }
}
