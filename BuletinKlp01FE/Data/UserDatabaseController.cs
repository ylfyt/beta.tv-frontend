using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BuletinKlp01FE.Models;

namespace BuletinKlp01FE.Data
{
    public class UserDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public UserDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<User>();
        }

        public User GetUser(string userName)
        {
            lock (locker)
            {
                return database.Table<User>().FirstOrDefault(dbUser => dbUser.Username == userName);
            }
        }

        public void SaveUser(User user)
        {
            lock (locker)
            {
                User dbUser = database.Table<User>().FirstOrDefault(dbu => dbu.Username == user.Username);

                if (dbUser == null)
                {
                    dbUser = user;

                    database.Insert(dbUser);
                }
                else
                {
                    database.Update(user);
                }
            }
        }

        public int DeleteUser(int id)
        {
            lock (locker)
            {
                return database.Delete<User>(id);
            }
        }
    }
}
