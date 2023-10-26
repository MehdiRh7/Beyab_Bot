using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IOnlinesRepository:IDisposable
    {
        IEnumerable<Onlines> GetAllOnlines();
        Onlines GetOnlineById(int id);
        bool InsertOnline(Onlines online);
        bool DeleteOnline(Onlines online);
        bool DeleteOnline(int id);
        bool UpdateOnline(Onlines online);
        void Save();
    }
}
