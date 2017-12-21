using SQLite;
using SQLiteDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.Data
{
    public class DB
    {
        readonly SQLiteAsyncConnection database;

        public DB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetItemsAsync()
        {
            return database.Table<Note>().ToListAsync();
        }

        public Task<List<Note>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Note>("SELECT * FROM [Note] WHERE [Done] = 0");
        }

        public Task<Note> GetItemAsync(int id)
        {
            return database.Table<Note>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Note item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Note item)
        {
            return database.DeleteAsync(item);
        }
    }
}
