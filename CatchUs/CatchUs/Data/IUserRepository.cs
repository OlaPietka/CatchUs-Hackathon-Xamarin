using CatchUs.Model;
using System.Collections.Generic;

namespace CatchUs.Data
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByEmail(string email);
        void InsertUser(User user);
        void DeleteUser(int id);
        void UpdateUser(string property, string newValue, int id);
        bool IsUserEmailExist(string email);
    }
}