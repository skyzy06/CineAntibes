using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CineAntibes.Interface;
using SQLite;
using Xamarin.Forms;

namespace CineAntibes.Database
{
    public class ApplicationDatabase<T> where T : class, new()
    {
        protected SQLiteConnection database;

        public ApplicationDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<T>();
        }

        /// <summary>
        /// Delete all the data in the selected table
        /// </summary>
        public void DropTable()
        {
            database.RunInTransaction(() =>
            {
                database.DeleteAll<T>();
            });
        }

        /// <summary>
        /// Get All the data in the table <see cref="T"/>
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> with all the row of the table</returns>
        public IEnumerable<T> GetAll()
        {
            return database.Table<T>().ToList();
        }

        /// <summary>
        /// Get the first or default element <see cref="T"/> that matches with <paramref name="expr"/>
        /// </summary>
        /// <param name="expr"><see cref="Expression"/> used to filter and find the element</param>
        /// <returns>A <see cref="T"/> or null</returns>
        public T Find(Expression<Func<T, bool>> expr)
        {
            return database.Table<T>().FirstOrDefault(expr);
        }

        /// <summary>
        /// Get the list of <see cref="T"/> that match with <paramref name="expr"/>
        /// </summary>
        /// <param name="expr"><see cref="Expression"/> used to filter the table</param>
        /// <returns></returns>
        public IEnumerable<T> Select(Expression<Func<T, bool>> expr)
        {
            return database.Table<T>().Where(expr);
        }

        /// <summary>
        /// Delete a single element in the table <see cref="T"/>
        /// </summary>
        /// <param name="expr"><see cref="Expression"/> used to filter and delete the element</param>
        public void Remove(Expression<Func<T, bool>> expr)
        {
            database.Table<T>().Delete(expr);
        }
    }
}
