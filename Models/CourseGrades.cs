using MySqlConnector;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetEnv;

namespace gradesApp.Models
{

    public class CourseGrades
    {

        // private const string CONNECTION_STRING = "Server=localhost;port=3306;Database=app_db;Uid=root;Pwd=secret;";
        private string CONNECTION_STRING;

        // database connectivity variables
        private MySqlConnection? dbConnection;
        private MySqlCommand? dbCommand;
        private MySqlDataReader? dbReader;

        // property variables
        private int _count;
        private List<Grade> _grades;
        private List<SelectListItem> _categoryList;
        private Grade _gradeDetails;

        public CourseGrades()
        {
            // initialization
            _count = 0;
            _grades = new List<Grade>();
            _categoryList = new List<SelectListItem>();
            _gradeDetails = new Grade();

            // getting connecting string from .env
            Env.Load();
            CONNECTION_STRING = Env.GetString("CONNECTION_STRING");
        }

        // ------------------------------------------------- gets/sets
        public int count
        {
            get
            {
                return _count;
            }
        }

        public List<Grade> grades
        {
            get
            {
                return _grades;
            }
        }

        public List<SelectListItem> categoryList
        {
            get
            {
                return _categoryList;
            }
        }

        public int categoryID { get; set; } = 0;

        public Grade gradeDetails
        {
            get
            {
                return _gradeDetails;
            }
        }

        // ------------------------------------------------- public methods
        public void setupMe()
        {
            getCategoryList();
            getGradeData();
        }

        // ------------------------------------------------- private methods
        private void getCategoryList()
        {
            using (dbConnection = new MySqlConnection(CONNECTION_STRING))
            {
                dbConnection.Open();
                using (dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "SELECT * FROM tblCategory";

                    using (dbReader = dbCommand.ExecuteReader())
                    {
                        // populate List
                        while (dbReader.Read())
                        {
                            SelectListItem item = new SelectListItem();

                            item.Text = dbReader.GetString("categoryName");

                            item.Value = Convert.ToString(dbReader["categoryID"]);

                            _categoryList.Add(item);
                        }
                    }
                }
            }
        }

        private void getGradeData()
        {
            using (dbConnection = new MySqlConnection(CONNECTION_STRING))
            {
                dbConnection.Open();

                using (dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "SELECT * FROM tblGrades WHERE categoryID = " + categoryID;
                    using (dbReader = dbCommand.ExecuteReader())
                    {
                        while (dbReader.Read())
                        {
                            Grade grade = new Grade();
                            grade.gradeID = dbReader.GetInt32("gradeID");
                            grade.categoryID = dbReader.GetInt32("categoryID");
                            grade.courseName = dbReader.GetString("courseName");
                            grade.grade = dbReader.GetString("grade");
                            grade.comments = dbReader.GetString("comments");
                            // add object to list
                            _grades.Add(grade);
                        }

                        _count = _grades.Count;
                    }
                }
            }
        }

        // -------------------------------------------- public methods
        public Grade getDetails(string grdID)
        {
            // need this to find the grade details data
            int gradeID = Convert.ToInt32(grdID);

            using (dbConnection = new MySqlConnection(CONNECTION_STRING))
            {
                dbConnection.Open();

                using (dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "SELECT * FROM tblGrades WHERE gradeID = " + gradeID;

                    using (dbReader = dbCommand.ExecuteReader())
                    {
                        dbReader.Read();

                        // populate Grade object properties
                        _gradeDetails.gradeID = dbReader.GetInt32("gradeID");
                        _gradeDetails.categoryID = dbReader.GetInt32("categoryID");
                        _gradeDetails.courseName = dbReader.GetString("courseName");
                        _gradeDetails.courseDescription = dbReader.GetString("courseDescription");
                        _gradeDetails.grade = dbReader.GetString("grade");
                        _gradeDetails.comments = dbReader.GetString("comments");
                    }
                }
            }

            return _gradeDetails;
        }

    }

}