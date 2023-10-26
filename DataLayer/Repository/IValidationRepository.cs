using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IValidationRepository:IDisposable
    {
        IEnumerable<RequestValidation> GetAllValidations();
        bool InsertValidation(RequestValidation validation);
        bool DeleteValidation(int validationId);
        bool DeleteValidation(RequestValidation validation);
        bool UpdateValidation(RequestValidation validation);
        void Save();
    }
}
