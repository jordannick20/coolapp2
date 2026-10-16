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
        gradesViewModel.CategoryID = Convert.ToInt32(gradesViewModel.CategoryList[0].Value);

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

    [Route("Grades/Add/{categoryID}")]
    [HttpGet]
    public IActionResult Add(int categoryID)
    {
        Grade grade = new Grade();
        grade.CategoryID = categoryID;

        return View(grade);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Grade grade)
    {
        CourseGrades courseGrades = new CourseGrades();
        // GradesViewModel gradesViewModel = new GradesViewModel();

        // add new grade
        await courseGrades.AddGradeAsync(grade);

        // gradesViewModel.Grades = await courseGrades.GetGradesAsync(grade.CategoryID);
        // gradesViewModel.CategoryList = await courseGrades.GetCategoryListAsync();
        // gradesViewModel.CategoryID = grade.CategoryID;

        // return View("Index", gradesViewModel);

        // PRG pattern
        // redirects to /Grades/Category/?CategoryID=2 (if grade.CategoryID is 2)
        return RedirectToAction("Category", new { grade.CategoryID });
    }
}