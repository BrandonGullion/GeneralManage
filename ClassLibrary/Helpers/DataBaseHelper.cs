using ClassLibrary;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace ClassLibrary
{
    public class DataBaseHelper
    {
        // Get current program directory
        public static string CurrentDirectory { get; set; } = Directory.GetCurrentDirectory();
        public static string DatabaseSaveDirectory { get; set; } = Path.Combine(CurrentDirectory, "Databases");

        // Static naming for user db
        public static string UserDatabaseName { get; set; } = "User.db";
        public static string EmployeeDatabaseName { get; set; } = "Employee.db";
        public static string PositionDatabaseName { get; set; } = "Position.db";

        // Combine to save to current directory 
        public static string UserDatabase { get; set; } = Path.Combine(DatabaseSaveDirectory, UserDatabaseName);
        public static string EmployeeDatabase { get; set; } = Path.Combine(DatabaseSaveDirectory, EmployeeDatabaseName);
        public static string PositionDatabase { get; set; } = Path.Combine(DatabaseSaveDirectory, PositionDatabaseName);

        // Used to set up managers that are allowed onto the propgram 
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

        // Methods that affect employee models 
        #region EmployeeDB Methods 

        public static List<EmployeeModel> ReadEmployeeDB()
        {
            // Creates Directory for saving the databases 
            Directory.CreateDirectory(DatabaseSaveDirectory);

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
        public static EmployeeModel AddEmplyee(EmployeeModel employeeModel)
        {
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Create table if not currently there
                conn.CreateTable<EmployeeModel>();

                // Generates random color for the employee
                employeeModel.RandomHex = RandomRGBHelper.GenerateRandomColor();

                // Convert First Name to char array, using the first letter to populate the field 
                employeeModel.FirstLetter = employeeModel.FirstName.ToCharArray().FirstOrDefault().ToString();

                employeeModel.HourlyWage = Convert.ToDouble(employeeModel.StringWage);

                // Insert employee model into the 
                conn.Insert(employeeModel);
                /// Creates the generic availability for the employee
                /// this can be changed within the employee page 

                // Store all management users into the user list 

                AddAvailability(employeeModel);

                // Return to populate the selected employee field 
                return employeeModel;
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

        public static EmployeeModel UpdateEmployee(EmployeeModel employeeModel)
        {
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // **** Creating type safe value converter ****
                employeeModel.HourlyWage = StringToDoubleConverter.ConvertToDouble(employeeModel.StringWage);
                employeeModel.StringWage = employeeModel.HourlyWage.ToString();

                // sends to db 
                conn.Update(employeeModel);
            }

            return employeeModel;
        }

        // Read Database and return list 
        public static List<ShiftModel> ReadShiftDb()
        {
            // Creates Database save directory 
            Directory.CreateDirectory(DatabaseSaveDirectory);

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

        public static void AddShift(EmployeeModel employeeModel, int startHourValue, double startMinValue, int endHourValue,
            double endMinValue, int day, int month, int year, double totalHours)
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
                Width = Convert.ToInt32((Math.Abs(startHourValue - endHourValue) * 60) + (endMinValue - startMinValue)),
                Margin = Convert.ToInt32((startHourValue * 60) + startMinValue),
                Total = totalHours,
            };



            using(SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Creates the table 
                conn.CreateTable<ShiftModel>();
                conn.CreateTable<MetricModel>();

                // Inserts the shift into the db 
                conn.Insert(shiftModel);

                // Creates a metric model that uses the similar data 
                MetricModel metricModel = new MetricModel()
                {
                    Name = $"{shiftModel.Name}",
                    EmployeeId = employeeModel.Id,
                    Hours = totalHours,
                    Day = day,
                    Month = month,
                    Year = year,
                    Wage = employeeModel.HourlyWage,
                    ShiftId = shiftModel.Id,
                };

                // Inserts the metric model after the shift id has been assigned 
                conn.Insert(metricModel);
            }

        }

        public static void DeleteShift(ShiftModel shiftModel)
        {
            // Finds the metric model associated with the passed in shift model and deletes it 
            FindAndDeleteMetricModel(shiftModel.Id);

            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // make sure that the table is present 
                conn.CreateTable<ShiftModel>();
                conn.CreateTable<MetricModel>();

                // Delete the selected model 
                conn.Delete(shiftModel);
            }
        }

        public static void UpdateShift(ShiftModel shiftModel, int startHourValue, double startMinValue, int endHourValue,
            double endMinValue, int day, int month, int year, double totalHours)
        {
            // Updates the selected shift model 
            shiftModel.StartHourValue = startHourValue;
            shiftModel.StartMinValue = startMinValue;
            shiftModel.EndtHourValue = endHourValue;
            shiftModel.EndMinValue = endMinValue;
            shiftModel.Day = day;
            shiftModel.Month = month;
            shiftModel.Year = year;
            shiftModel.Width = Convert.ToInt32((Math.Abs(startHourValue - endHourValue) * 60) + (endMinValue - startMinValue));
            shiftModel.Margin = Convert.ToInt32((startHourValue * 60) + startMinValue);
            shiftModel.Total = totalHours;

            // Updates metric models that are associated with the shift 
            FindAndUpdateMetricModel(shiftModel);

            // Update within the DB
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                conn.Update(shiftModel);
            }

        }


        #endregion

        // Methods that include employement positions that can be associated with employees 
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

        // Vacation methods used when displaying associated employee scheduled vacation 
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

        // Adds vacation model to the db
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

        // Updates existing vacation model within db 
        public static void UpdateVacation(VacationModel vacationModel)
        {
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                conn.CreateTable<VacationModel>();

                // This is used to switch the start and end date if the inputs are incorrectly ordered 
                if (DateTime.Parse(vacationModel.StartDate) > DateTime.Parse(vacationModel.EndDate))
                {
                    DateTime _endDate = DateTime.Parse(vacationModel.StartDate);
                    DateTime _startDate = DateTime.Parse(vacationModel.EndDate);
                    vacationModel.StartDate = _startDate.ToString("MMMM dd, yyyy");
                    vacationModel.EndDate = _endDate.ToString("MMMM dd, yyyy");
                }

                conn.Update(vacationModel);
            }
        }

        #endregion

        // Availability methods that will display when the employee wishes to work 
        #region Availability Methods 

        public static AvailabilityModel ReadAvailability(EmployeeModel employeeModel)
        {
            // new instance of ava. model 
            AvailabilityModel availabilityModel = new AvailabilityModel();

            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                /// Reads the list, updates depending on the passed in selected Employee
                /// And returns a single instance of an availability model to be displayed
                availabilityModel = conn.Table<AvailabilityModel>().Where(a => a.EmployeeId == employeeModel.Id).ToList().FirstOrDefault();

            }
            return availabilityModel;
        }

        public static void AddAvailability(EmployeeModel employeeModel)
        {
            AvailabilityModel availabilityModel = new AvailabilityModel
            {
                // Creates the availability for that employee and sets all to false 
                EmployeeId = employeeModel.Id,
                SundayMorning = true,
                MondayMorning = true,
                TuesdayMorning = true,
                WednesdayMorning = true,
                ThursdayMorning = true,
                FridayMorning = true,
                SaturdayMorning = true,
                SundayAfternoon = true,
                MondayAfternoon = true,
                TuesdayAfternoon = true,
                WednesdayAfternoon = true,
                ThursdayAfternoon = true,
                FridayAfternoon = true,
                SaturdayAfternoon = true,
                SundayEvening = true,
                MondayEvening = true,
                TuesdayEvening = true,
                WednesdayEvening = true,
                ThursdayEvening = true,
                FridayEvening = true,
                SaturdayEvening = true,
                SundayNight = true,
                MondayNight = true,
                TuesdayNight = true,
                WednesdayNight = true,
                ThursdayNight = true,
                FridayNight = true,
                SaturdayNight = true,
            };

            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                // Creates the table to avoid breaks 
                conn.CreateTable<AvailabilityModel>();

                // Inserts the availability model 
                conn.Insert(availabilityModel);
            }
        }

        public static void UpdateAvailability(AvailabilityModel availabilityModel)
        {
            using (SQLiteConnection conn = new SQLiteConnection(EmployeeDatabase))
            {
                conn.CreateTable<AvailabilityModel>();
                conn.Update(availabilityModel);
            }
        }

        #endregion

        // Metric methods that find and associate with required shift or employee models 
        #region Metrics Methods 

        // Finds the metric model that was assigned the matching shift id
        public static void FindAndDeleteMetricModel(int shiftId)
        {
            // Creates the list that we should iterate over 
            ObservableCollection<MetricModel> metricModels = ReadAllDB<MetricModel>(EmployeeDatabase);

            // Iterates over the metric model list, checking if any match with the shift Id passed in, delete the according metric model 
            foreach (var metricModel in metricModels)
                if (metricModel.ShiftId == shiftId)
                    DataBaseHelper.DeleteModel<MetricModel>(metricModel, EmployeeDatabase);
        }

        // Uses 2 overrides depending on the model that is being updated 
        public static void FindAndUpdateMetricModel(EmployeeModel employeeModel)
        {
            // Fills required List 
            ObservableCollection<MetricModel> metricModels = ReadAllDB<MetricModel>(EmployeeDatabase);

            // Iterates, finds metric models with matching employee id, then updates the wage 
            foreach (var metricModel in metricModels)
            {
                if (metricModel.EmployeeId == employeeModel.Id)
                {
                    metricModel.Wage = employeeModel.HourlyWage;
                    metricModel.Name = $"{employeeModel.FirstName} {employeeModel.LastName}";
                }

                // Updates within the database 
                UpdateModel<MetricModel>(metricModel, EmployeeDatabase);
            }
        }
        public static void FindAndUpdateMetricModel(ShiftModel shiftModel)
        {
            // Fills the metric list to check if there are any matching when the shift model is changed 
            ObservableCollection<MetricModel> metricModels = ReadAllDB<MetricModel>(EmployeeDatabase);
            
            // Iterate over the collection 
            foreach(var metricModel in metricModels)
            {
                // If the shift model matches with a metric model, the metric model is then updated 
                if(metricModel.ShiftId == shiftModel.Id)
                {
                    metricModel.Day = shiftModel.Day;
                    metricModel.Month = shiftModel.Month;
                    metricModel.Year = shiftModel.Year;
                    metricModel.Hours = shiftModel.Total;

                    UpdateModel<MetricModel>(metricModel, EmployeeDatabase);
                }
            }
        }

        #endregion

        // Generic Methods that allow for any of the DB to be accessed easily and quickly 
        #region Generic Methods


        // Generic Method for deleting selected objects w/ null check
        public static void DeleteModel<T>(object model, string database)
        {
            if (model != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(database))
                {
                    conn.CreateTable<T>();

                    conn.Delete((T)model);
                }
            }
            else
                Console.WriteLine($"{model} was not cast and or has null values");
        }

        // Generic Method for adding objects to DB
        public static void AddModel<T>(object model, string database)
        {
            // Creates save directory if not currently present 
            Directory.CreateDirectory(DatabaseSaveDirectory);

            if (model != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(database))
                {
                    // Creates table depending on the type passed in 
                    conn.CreateTable<T>();

                    // inserts the object after being cast to required type 
                    conn.Insert((T)model);
                }
            }

            else
                Console.WriteLine($"{model} was not cast and or has null values");
        }

        // Generic Method for updating objects in db 
        public static void UpdateModel<T>(object model, string database)
        {
            if (model != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(database))
                {
                    // Creates table depending on the type passed in 
                    conn.CreateTable<T>();

                    // inserts the object after being cast to required type 
                    conn.Update((T)model);
                }
            }

            else
                Console.WriteLine($"{model} was not cast and or has null values");
        }

        public static ObservableCollection<T> ReadAllDB<T>(string database)
            where T : new()
        {
            using(SQLiteConnection conn = new SQLiteConnection(database))
            {
                // Creates the generic table 
                conn.CreateTable<T>();

                return new ObservableCollection<T>(conn.Table<T>().ToList());
            }
        }  

        #endregion


    }
}
