using CatchUs.Model;
using System.Collections.Generic;

namespace CatchUs.Data
{
    public interface IPasswordRecoveryRepository
    {
        List<PasswordRecovery> GetAllPasswordsRecovery();
        PasswordRecovery GetPasswordRecoveryByEmail(string email);
        void InsertPasswordRecovery(PasswordRecovery entity);
        void DeletePasswordRecovery(int id);
        bool IsPasswordRecoveryEmailExist(string email);
    }
}
