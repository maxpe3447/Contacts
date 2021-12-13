using Contacts.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Repository
{
    public class Repository : IRepository
    {
        private Lazy<SQLiteAsyncConnection> database;
        public Repository()
        {
            database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contactbook.db3");
                var database = new SQLiteAsyncConnection(path);

                database.CreateTableAsync<UserModel>();
                database.CreateTableAsync<ProfileModel>();
                return database;
            });
        }
        public Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new()
        {
            return database.Value.DeleteAsync(entity);
        }

        public Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new()
        {
            return database.Value.Table<T>().ToListAsync();
        }

        public Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new()
        {
            return database.Value.InsertAsync(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            return database.Value.UpdateAsync(entity);
        }

        public async Task<bool> IsExist(UserModel entity)
        {
            var table = database.Value.FindAsync<UserModel>(entity);
            return await table != null;
        }

        public async void Delete<T>() where T : IEntityBase, new()
        {
            await database.Value.DeleteAllAsync<T>();
        }
    }
}
