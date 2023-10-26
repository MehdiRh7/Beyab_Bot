using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PersonRepository : IPersonRepository
    {
        BeyabContext db;
        public PersonRepository(BeyabContext context)
        {
            this.db = context;
        }
        public IEnumerable<Person> GetAllPerson()
        {
            return db.Person.ToList();
        }

        public Person GetPersonById(int id)
        {
            return db.Person.Find(id);
        }

        public bool InsertPerson(Person person)
        {
            try
            {
                db.Person.Add(person);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeletePerson(Person person)
        {
            try
            {
                db.Entry(person).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePerson(int id)
        {
            try
            {
                var person = db.Person.Find(id);
                DeletePerson(person);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePerson(Person person)
        {
            try
            {
                db.Entry(person).State = EntityState.Modified;
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
