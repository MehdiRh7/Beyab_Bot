using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OnlinesRepository:IOnlinesRepository
    {
        BeyabContext db;
        public OnlinesRepository(BeyabContext context)
        {
            this.db = context;
        }
        public IEnumerable<Onlines> GetAllOnlines()
        {
            return db.Onlines.ToList();
        }

        public Onlines GetOnlineById(int id)
        {
            return db.Onlines.Find(id);
        }

        public bool InsertOnline(Onlines online)
        {
            try
            {
                db.Onlines.Add(online);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteOnline(Onlines online)
        {
            try
            {
                db.Entry(online).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOnline(int id)
        {
            try
            {
                var online = db.Onlines.Find(id);
                DeleteOnline(online);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateOnline(Onlines online)
        {
            try
            {
                db.Entry(online).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
