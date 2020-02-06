using CatchUs.Model;
using System.Collections.Generic;


namespace CatchUs.Data
{
    public interface IPinRepository
    {
        List<Pin> GetAllPins();
        Pin GetPinById(int id);
        int GetIdByPin(Pin pin);
        void InsertPin(Pin pin);
        void DeletePin(int id);
    }
}
