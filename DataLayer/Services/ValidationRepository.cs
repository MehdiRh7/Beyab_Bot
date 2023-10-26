using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ValidationRepository:IValidationRepository
    {
        BeyabContext db;
        public ValidationRepository(BeyabContext context)
        {
            this.db = context;
        }
        public IEnumerable<RequestValidation> GetAllValidations()
        {
            return db.Validations.ToList();
        }

        public bool InsertValidation(RequestValidation validation)
        {
            try
            {
                db.Validations.Add(validation);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteValidation(int validationId)
        {
            try
            {
                var validation = db.Validations.Find(validationId);
                DeleteValidation(validation);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteValidation(RequestValidation validation)
        {
            try
            {
                db.Entry(validation).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateValidation(RequestValidation validation)
        {
            try
            {
                db.Entry(validation).State = EntityState.Modified;
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
