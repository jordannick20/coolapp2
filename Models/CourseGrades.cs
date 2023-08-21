using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gradesApp.Models
{

    public class CourseGrades
    {

        private const string CONNECTION_STRING = "Server=localhost;port=3306;Database=sean_dotnetcoreSamples;Uid=user;Pwd=secret;SslMode=none;";

        // database connectivity variables
        private MySqlConnection dbConnection;
        private MySqlCommand dbCommand;
        private MySqlDataReader dbReader;

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

            // construct required DB objects for use in private methods
            dbConnection = new MySqlConnection(CONNECTION_STRING);
            dbCommand = new MySqlCommand("", dbConnection);
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
            try
            {
                dbConnection.Open();
                dbCommand.CommandText = "SELECT * FROM tblCategory";
                dbReader = dbCommand.ExecuteReader();

                // populate List
                while (dbReader.Read())
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = Convert.ToString(dbReader["categoryName"]);
                    item.Value = Convert.ToString(dbReader["categoryID"]);
                    _categoryList.Add(item);

                    // if the model was just constructed then categoryID will be initialized to 0 (and not set to something else through model binding) which means this is the first visit - in that case we set the categoryID of model to first category
                    // otherwise - we do nothing since it will have been set automatically to something else via model binding
                    if (categoryID == 0)
                    {
                        categoryID = Convert.ToInt32(dbReader["categoryID"]);
                    }
                }
                dbReader.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(">>> An error has occured with CategoryList");
                Console.WriteLine(">>> " + e.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void getGradeData()
        {
            try
            {
                dbConnection.Open();

                dbCommand.CommandText = "SELECT * FROM tblGrades WHERE categoryID = " + categoryID;
                dbReader = dbCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    Grade grade = new Grade();
                    grade.gradeID = Convert.ToInt32(dbReader["gradeID"]);
                    grade.categoryID = Convert.ToInt32(dbReader["categoryID"]);
                    grade.courseName = dbReader["courseName"].ToString();
                    grade.grade = dbReader["grade"].ToString();
                    grade.comments = dbReader["comments"].ToString();
                    // add object to list
                    _grades.Add(grade);
                }
                dbReader.Close();

                // accessing single value from db
                dbCommand.CommandText = "SELECT Count(*) FROM tblGrades WHERE categoryID = " + categoryID;
                _count = Convert.ToInt32(dbCommand.ExecuteScalar());

            }
            catch (Exception e)
            {
                Console.WriteLine(">>> An error has occurred with get Grades");
                Console.WriteLine(">>> " + e.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        // -------------------------------------------- public methods
        public Grade getDetails(string grdID)
        {
            // need this to find the grade details data
            int gradeID = Convert.ToInt32(grdID);

            try
            {
                dbConnection.Open();
                dbCommand.CommandText = "SELECT * FROM tblGrades WHERE gradeID = " + gradeID;
                dbReader = dbCommand.ExecuteReader();
                dbReader.Read();

                // populate Grade object properties
                _gradeDetails.gradeID = Convert.ToInt32(dbReader["gradeID"]);
                _gradeDetails.categoryID = Convert.ToInt32(dbReader["categoryID"]);
                _gradeDetails.courseName = dbReader["courseName"].ToString();
                _gradeDetails.courseDescription = dbReader["courseDescription"].ToString();
                _gradeDetails.grade = dbReader["grade"].ToString();
                _gradeDetails.comments = dbReader["comments"].ToString();

                dbReader.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(">>> An error has occured with get Grades");
                Console.WriteLine(">>> " + e.Message);
            }
            finally
            {
                dbConnection.Close();
            }

            return _gradeDetails;
        }

    }

}