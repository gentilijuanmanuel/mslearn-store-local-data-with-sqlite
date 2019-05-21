using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using People.Models;
using SQLite;

namespace People
{
    public class PersonRepository
    {
        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection Connection { get; set; }

        public PersonRepository(string dbPath)
        {
            Connection = new SQLiteAsyncConnection(dbPath);
            Connection.CreateTableAsync<Person>().Wait();
        }

        public async Task AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = await Connection.InsertAsync(new Person { Name = name });

                this.StatusMessage = string.Format("{0} record(s) added [Name: {1}]", result, name);
            }
            catch (Exception ex)
            {
                this.StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }
        }

        public async Task<List<Person>> GetAllPeople()
        {
            try
            {
                return await Connection.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}
