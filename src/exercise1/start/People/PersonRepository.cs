using System;
using System.Collections.Generic;
using People.Models;
using SQLite;

namespace People
{
    public class PersonRepository
    {
        public string StatusMessage { get; set; }

        private SQLiteConnection Connection { get; set; }

        public PersonRepository(string dbPath)
        {
            Connection = new SQLiteConnection(dbPath);
            Connection.CreateTable<Person>();
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = Connection.Insert(new Person { Name = name });

                this.StatusMessage = string.Format("{0} record(s) added [Name: {1}]", result, name);
            }
            catch (Exception ex)
            {
                this.StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }
        }

        public List<Person> GetAllPeople()
        {
            try
            {
                return Connection.Table<Person>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}
