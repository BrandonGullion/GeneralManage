using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem
{
    public class DataBaseHelper
    {
        // Get current program directory
        public static string CurrentDirectory { get; set; } = Directory.GetCurrentDirectory();
        // Static naming for user db
        public static string UserDatabaseName { get; set; } = "User.db";
        // Combine to save to current directory 
        public static string UserDatabase { get; set; } = Path.Combine(CurrentDirectory, UserDatabaseName);

        /// <summary>
        /// DB Used to access SQL server and return a list of users for login 
        /// </summary>
        /// <returns></returns>
        public static List<Users> ReadDB()
        {
            var UserList = new List<Users>();

            using (SQLiteConnection conn = new SQLiteConnection(UserDatabase))
            {
                // Create table if not currently there
                conn.CreateTable<Users>();

                // Store all management users into the user list 
                UserList = conn.Table<Users>().ToList();
            }

            // Return the list 
            return UserList;
        }

        public static void AddUser(string userName, string password, int authorityLevel = 1)
        {
            using (SQLiteConnection conn = new SQLiteConnection(UserDatabase))
            {
                // Create table if not currently there
                conn.CreateTable<Users>();

                Users NewUser = new Users();
                NewUser.UserName = userName;
                NewUser.Password = password;

                // Store all management users into the user list 
                conn.Insert(NewUser);
            }
        }

        public static void DeleteUser()
        {

        }

        public static bool ValidateUser(string userName, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(UserDatabase))
            {
                List<Users> users = new List<Users>();

                users = ReadDB();

                foreach(var User in users)
                {
                    if (User.UserName == userName && User.Password == password)
                        return true;

                    else
                        return false;
                }

                return false;
            }
        }
    }

}
