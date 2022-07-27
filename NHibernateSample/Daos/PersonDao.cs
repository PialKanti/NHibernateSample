using NHibernateSample.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHibernateSample.Daos
{
    public class PersonDao
    {
        private static PersonDao personDao;

        public static PersonDao GetInstance()
        {
            if(personDao == null)
            {
                personDao = new PersonDao();
            }
            return personDao;
        }

        public Person AddOrUpdate(Person person)
        {
            var sessionFactory = FluentSessionFactory.GetInstance();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(person);
                    transaction.Commit();
                }
            }

            return person;
        }

        public void Delete(int id)
        {
            var sessionFactory = FluentSessionFactory.GetInstance();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Person person = session.QueryOver<Person>().Where(p => p.Id == id).SingleOrDefault();
                    session.Delete(person);
                    transaction.Commit();
                }
            }
        }

        public IList<Person> GetAllPersons()
        {
            IList<Person> personList = new List<Person>();

            var sessionFactory = FluentSessionFactory.GetInstance();

            using (var session = sessionFactory.OpenSession())
            {
                personList = session.CreateCriteria<Person>().List<Person>();
            }

            return personList;
        }
    }
}
