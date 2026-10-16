using DotNetEnv;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlConnector;

namespace GradesApp.Models;

public class CourseGrades
{
    private readonly string CONNECTION_STRING;

    public CourseGrades()
    {
        // getting connecting string from .env
        Env.Load();
        CONNECTION_STRING = Env.GetString("CONNECTION_STRING");
    }

    // ***** CHALLENGE SOLUTION *****
    private async Task<int> GetFirstCategoryIDAsync()
    {
        using var dbConnection = new MySqlConnection(CONNECTION_STRING);

        await dbConnection.OpenAsync();

        using var dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "SELECT categoryID FROM tblCategory ORDER BY categoryID LIMIT 1";

        var categoryID = await dbCommand.ExecuteScalarAsync();

        return Convert.ToInt32(categoryID);
    }

    public async Task<List<Grade>> GetGradesAsync(int? categoryID = null)
    {
        var grades = new List<Grade>();

        // ***** CHALLENGE SOLUTION *****
        if (!categoryID.HasValue)
        {
            categoryID = await GetFirstCategoryIDAsync();
        }

        // create our MySqlConnection object
        using var dbConnection = new MySqlConnection(CONNECTION_STRING);

        await dbConnection.OpenAsync();

        using var dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "SELECT * FROM tblGrades WHERE categoryID = " + categoryID;

        using var dbReader = await dbCommand.ExecuteReaderAsync();

        while (await dbReader.ReadAsync())
        {
            Grade grade = new Grade();

            grade.GradeID = Convert.ToInt32(dbReader["gradeID"]);
            grade.CategoryID = Convert.ToInt32(dbReader["categoryID"]);
            grade.CourseName = Convert.ToString(dbReader["courseName"]);
            grade.CourseGrade = Convert.ToString(dbReader["grade"]);
            grade.Comments = Convert.ToString(dbReader["comments"]);

            // add object to list
            grades.Add(grade);
        }

        return grades;
    }

    public async Task<List<SelectListItem>> GetCategoryListAsync()
    {
        var categoryList = new List<SelectListItem>();

        using var dbConnection = new MySqlConnection(CONNECTION_STRING);

        await dbConnection.OpenAsync();

        using var dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "SELECT * FROM tblCategory";

        using var dbReader = await dbCommand.ExecuteReaderAsync();

        // populate List
        while (await dbReader.ReadAsync())
        {
            SelectListItem item = new SelectListItem();

            item.Text = Convert.ToString(dbReader["categoryName"]);

            item.Value = Convert.ToString(dbReader["categoryID"]);

            categoryList.Add(item);
        }

        return categoryList;
    }

    public async Task<Grade> GetGradeByIDAsync(int gradeID)
    {
        using var dbConnection = new MySqlConnection(CONNECTION_STRING);

        await dbConnection.OpenAsync();

        using var dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "SELECT * FROM tblGrades WHERE gradeID = " + gradeID;

        using var dbReader = await dbCommand.ExecuteReaderAsync();

        await dbReader.ReadAsync();

        Grade grade = new Grade();

        grade.GradeID = Convert.ToInt32(dbReader["gradeID"]);
        grade.CategoryID = Convert.ToInt32(dbReader["categoryID"]);
        grade.CourseName = Convert.ToString(dbReader["courseName"]);
        grade.CourseGrade = Convert.ToString(dbReader["grade"]);
        grade.Comments = Convert.ToString(dbReader["comments"]);
        grade.CourseDescription = Convert.ToString(dbReader["courseDescription"]);

        return grade;
    }

    public async Task AddGradeAsync(Grade grade)
    {
        using var dbConnection = new MySqlConnection(CONNECTION_STRING);

        await dbConnection.OpenAsync();

        using var dbCommand = dbConnection.CreateCommand();

        // BAD APPROACH
        // dbCommand.CommandText = "INSERT INTO tblGrades (categoryID,courseName,courseDescription,grade,comments) VALUES (" + grade.CategoryID + ",'" + grade.CourseName + "','" + grade.CourseDescription + "'," + grade.CourseGrade + ",'" + grade.Comments + "')";

        // GOOD APPROACH - parameterized queries
        dbCommand.CommandText = "INSERT INTO tblGrades (categoryID,courseName,courseDescription,grade,comments) VALUES (@categoryID,@courseName,@courseDescription,@grade,@comments)";

        dbCommand.Parameters.AddWithValue("@categoryID", grade.CategoryID);
        dbCommand.Parameters.AddWithValue("@courseName", grade.CourseName);
        dbCommand.Parameters.AddWithValue("@courseDescription", grade.CourseDescription);
        dbCommand.Parameters.AddWithValue("@grade", grade.CourseGrade);
        dbCommand.Parameters.AddWithValue("@comments", grade.Comments);

        await dbCommand.ExecuteNonQueryAsync();
    }
}
