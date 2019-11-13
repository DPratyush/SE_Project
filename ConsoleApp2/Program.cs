using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ConsoleApp2
{
    public class sqlConnector
    {
        public MySqlConnection connection;
        private string server;
        private string uid;
        private string password;
        private string database;
        public sqlConnector()
        {
            Initialize();
        }

        public MySqlConnection Initialize()
        {
            server = "localhost";
            database = "dbmsproject";
            uid = "root";
            password = "mysql";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            return connection;
        }
        public bool OpenConnection()
        {
            try
            {
                Console.WriteLine("Checking");
                connection.Open();
                Console.WriteLine("Connected");
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid Username/Password");
                        break;
                }
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                Console.WriteLine("Closed");
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
        }

    }
    class login
    {
        string Username;
        string password;
        MySqlConnection connection;
        public login()
        {
            String UN, pword;
            Console.WriteLine("Enter Username");
            UN = Console.ReadLine();
            Console.WriteLine("Enter Password");
            pword = Console.ReadLine();
            Username = UN;
            password = pword;
            String server = "localhost";
            String database = "dbmsproject";
            String uid = "root";
            String passwordServer = "mysql";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + passwordServer + ";";
            connection = new MySqlConnection(connectionString);
            if (connection != null)
                Console.WriteLine("Not null");
            else
                Console.WriteLine("Null");
            loginCheck();

        }
        private void loginCheck()
        {
            String loginStatement = "SELECT ID, Type_ID FROM user WHERE username = @username AND password = @password";
            using (connection)
            {
                MySqlCommand login = new MySqlCommand(loginStatement, connection);
                login.Parameters.AddWithValue("@username", Username);
                login.Parameters.AddWithValue("@password", password);
                connection.Open();
                MySqlDataReader reader = login.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(string.Format("{0},{1}", reader["ID"], reader["Type_ID"]));
                        int type = Int32.Parse(reader["Type_ID"].ToString());
                        int temp_ID = Int32.Parse(reader["ID"].ToString());
                        if (type == 1)
                        {
                            Console.WriteLine("Student");
                            Student stuobject = new Student(temp_ID,connection);
                        }
                        else if (type == 3)
                        {
                            Console.WriteLine("Teacher");
                            Teacher teachobject = new Teacher(temp_ID,connection);
                        }
                        else if (type == 4)
                        {
                            Console.WriteLine("Staff");
                            Staff staffobject = new Staff(temp_ID,connection);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Username/Password!");
                        }



                    }
                }
                finally
                {
                    reader.Close();
                }


            }



        }


    }
    class Student
    {
        public Student(int ID,MySqlConnection connection)
        {

        }
    }
    class Teacher
    {
        public Teacher(int ID, MySqlConnection connection)
        {

        }
    }
    class Staff
    {
        public Staff(int ID, MySqlConnection connection)
        {
            Console.WriteLine("0 to insert/update/delete batches, 1. Allot slots, 2. Allot Batches");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 0)
                new AddUsers(connection);
            else if (choice == 1)
                new AddSlots(connection);
            else if (choice == 2)
                new AllotBatches(connection);

        }
    }
    class AddUsers
    {
        public AddUsers(MySqlConnection connection)
        {
            Console.WriteLine("1.Insert/Update 2. Delete");
            String checkforID = "select userExists(?)";
                        





        }
    }
    class AddSlots
    {
        public AddSlots(MySqlConnection connection)
        {

        }
    }
    class AllotBatches
    {
        public AllotBatches(MySqlConnection connection)
        {

        }
    }
      

    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            sqlConnector connection = new sqlConnector();
           //bool lol = connection.OpenConnection();
            login login1 = new login();
            Console.ReadKey();
            //bool lmao = connection.CloseConnection();
            

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
