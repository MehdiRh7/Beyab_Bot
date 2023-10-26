using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IPersonRepository:IDisposable
    {
        IEnumerable<Person> GetAllPerson();
        Person GetPersonById(int id);
        bool InsertPerson(Person person);
        bool DeletePerson(Person person);
        bool DeletePerson(int id); 
        bool UpdatePerson(Person person);
        void Save();

    }
}
