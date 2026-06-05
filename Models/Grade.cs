namespace GradesApp.Models;

public class Grade
{
    public int GradeID { get; set; }
    public int CategoryID { get; set; }
    public string? CourseName { get; set; }
    public string? CourseGrade { get; set; }
    public string? Comments { get; set; }
    public string? CourseDescription { get; set; }
}
