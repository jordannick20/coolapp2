using Microsoft.AspNetCore.Mvc;
using GradesApp.Models;
using GradesApp.ViewModels;

namespace GradesApp.Controllers;

public class GradesController : Controller
{
    public async Task<IActionResult> Index()
    {
        // first visit to app - construct model and pass to view
        CourseGrades courseGrades = new CourseGrades();
        GradesViewModel gradesViewModel = new GradesViewModel();

        gradesViewModel.Grades = await courseGrades.GetGradesAsync();
        gradesViewModel.CategoryList = await courseGrades.GetCategoryListAsync();

        return View(gradesViewModel);
    }

    public async Task<IActionResult> Category(int categoryID)
    {
        CourseGrades courseGrades = new CourseGrades();
        GradesViewModel gradesViewModel = new GradesViewModel();

        gradesViewModel.Grades = await courseGrades.GetGradesAsync(categoryID);
        gradesViewModel.CategoryList = await courseGrades.GetCategoryListAsync();
        gradesViewModel.CategoryID = categoryID;

        return View("Index", gradesViewModel);
    }

    [Route("Grades/Details/{gradeID}")]
    public async Task<IActionResult> Details(int gradeID)
    {
        CourseGrades courseGrades = new CourseGrades();
        Grade grade = await courseGrades.GetGradeByIDAsync(gradeID);
        return View(grade);
    }
}