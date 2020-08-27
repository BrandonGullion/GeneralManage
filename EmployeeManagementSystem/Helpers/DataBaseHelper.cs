using EmployeeManagementSystem.Helpers;
using EmployeeManagementSystem.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
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
        public static string EmployeeDatabaseName { get; set; } = "Employee.db";
        public static string PositionDatabaseName { get; set; } = "Position.db";

        // Combine to save to current directory 
        public static string UserDatabase { get; set; } = Path.Combine(CurrentDirectory, UserDatabaseName);
        public static string EmployeeDatabase { get; set; } = Path.Combine(CurrentDirectory, EmployeeDatabaseName);
        public static string PositionDatabase { get; set; } = Path.Combine(CurrentDirectory, PositionDatabaseName);

        #region Management User DB Methods 

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

        // Create User TODO:: Implement only when manager is logged in 
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

        // Delete the user that is selected 
        public static void DeleteUser(Users users)
        {
            using (SQLiteConnection conn = new SQLiteConnection(UserDatabase))
            {
                conn.Delete(users);
            }
        }

        // Checking DB upon login to make sure they match someone in the db
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
        #endregion

        #region EmployeeDB Methods 

        public static List<EmployeeModel> ReadEmployeeDB()
        {
            var EmployeeList = new List<EmployeeModel>();

            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Create table if not currently there
                conn.CreateTable<EmployeeModel>();

                // Store all employees into the user list 
                EmployeeList = conn.Table<EmployeeModel>().ToList();
            }

            // Return the list 
            return EmployeeList;
        }

        // Add employee to db 
        public static void AddEmplyee(string firstName, string lastName, int authorityLevel, 
            string employeeId, string phoneNumber, string position, string hourlyWage)
        {
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Create table if not currently there
                conn.CreateTable<EmployeeModel>();

                var NewEmployee = new EmployeeModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    AuthorityLevel = authorityLevel,
                    EmployeeId = employeeId,
                    PhoneNumber = phoneNumber,
                    Position = position,
                    HourlyWage = hourlyWage,
                    FirstLetter = firstName.First().ToString(),
                    RandomHex = RandomRGBHelper.GenerateRandomColor(),
                };

                // Store all management users into the user list 
                conn.Insert(NewEmployee);
            }
        }

        public static void DeleteEmployee(EmployeeModel employeeModel)
        {
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Make sure table is present 
                conn.CreateTable<EmployeeModel>();

                // Delete the passed in model 
                conn.Delete(employeeModel);
            }
        }

        public static void UpdateEmployee(EmployeeModel employeeModel)
        {
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // sends to db 
                conn.Update(employeeModel);
            }
        }

        // Read Database and return list 
        public static List<ShiftModel> ReadShiftDb()
        {
            List<ShiftModel> ShiftModelList = new List<ShiftModel>();

            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Creates table
                conn.CreateTable<ShiftModel>();

                // Reads db and generates the list 
                ShiftModelList = conn.Table<ShiftModel>().ToList();
            }
            return ShiftModelList;
        }

        public static void AddShift(EmployeeModel employeeModel, int startHourValue, int startMinValue, int endHourValue,
            int endMinValue, int day, int month, int year)
        {

            // Creates a new instance of a shift and adds it to the database 
            ShiftModel shiftModel = new ShiftModel()
            {
                EmployeeId = employeeModel.Id,
                StartHourValue = startHourValue,
                StartMinValue = startMinValue,
                EndtHourValue = endHourValue,
                EndMinValue = endMinValue,
                Day = day,
                Month = month,
                Year = year,
                Name = $"{employeeModel.FirstName} {employeeModel.LastName}",
                Hex = employeeModel.RandomHex,
                Width = (Math.Abs(startHourValue - endHourValue) * 60) + Math.Abs(startMinValue - endMinValue),
                Margin = (startHourValue * 60) + startMinValue,
            };

            using(SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Creates the table 
                conn.CreateTable<ShiftModel>();

                // Inserts the shift into the db 
                conn.Insert(shiftModel);
            }

        }

        public static void DeleteShift(ShiftModel shiftModel)
        {
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // make sure that the table is present 
                conn.CreateTable<ShiftModel>();

                // Delete the selected model 
                conn.Delete(shiftModel);
            }
        }

        public static void UpdateShift()
        {

        }


        #endregion

        #region PositionDB Methods

        // Read position db and store in list 
        public static List<PositionModel> ReadPositionDB()
        {
            var PositionList = new List<PositionModel>();

            using (SQLiteConnection conn = new SQLiteConnection(PositionDatabase))
            {
                // Create table if not currently there
                conn.CreateTable<PositionModel>();

                // Store all management users into the user list 
                PositionList = conn.Table<PositionModel>().ToList();
            }

            // Return the list 
            return PositionList;
        }

        // Insert a desired position 
        public static void AddPosition(string position)
        {
            using (SQLiteConnection conn = new SQLiteConnection(PositionDatabase))
            {
                // Create table if not currently there
                conn.CreateTable<PositionModel>();

                PositionModel positionModel = new PositionModel();
                positionModel.Position = position;

                // Store all management users into the user list 
                conn.Insert(positionModel);
            }
        }

        public static void DeletePosition(PositionModel positionModel)
        {
            using (SQLiteConnection conn = new SQLiteConnection(PositionDatabase))
            {
                conn.Delete(positionModel);
            }
        }

        public static void UpdatePosition(PositionModel positionModel, string updatedPositionName)
        {
            using (SQLiteConnection conn = new SQLiteConnection(PositionDatabase))
            {
                positionModel.Position = updatedPositionName;
                conn.Update(positionModel);
            }
        }
        #endregion

        #region Vacation Methods


        public static ObservableCollection<VacationModel> ReadVacatinDB()
        {
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Create table if not currently there
                conn.CreateTable<VacationModel>();

                // Store all management users into the user list 
                var VacationList = new ObservableCollection<VacationModel>(conn.Table<VacationModel>().OrderBy(v => v.Name).ToList());

                // Return the obsCollection 
                return VacationList;
            }
        }

        public static void DeleteVacation<T>(object value)
        {
            using(SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Creates the table depending on the generic class inserted 
                conn.CreateTable<T>();

                // Deletes the value passed in, after casting as 
                conn.Delete((T)value);
            }
        }

        public static void AddVacation(EmployeeModel employeeModel, DateTime startDate, DateTime endDate)
        {
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Creates the table depending on the generic class inserted 
                conn.CreateTable<VacationModel>();

                // Creates instance of Vacation model to be inserted 
                VacationModel vacationModel = new VacationModel()
                {
                    Name = $"{employeeModel.FirstName} {employeeModel.LastName}",
                    StartDate = startDate.ToString("MMMM dd, yyyy"),
                    EndDate = endDate.ToString("MMMM dd, yyyy"),
                    Hex = employeeModel.RandomHex,
                };

                // inserts the desired vacation model 
                conn.Insert(vacationModel);
            }
        }

        #endregion


    }

}
