using Microsoft.AspNetCore.Mvc;
using gradesApp.Models;
using System;

namespace gradesApp.Controllers
{
    public class GradesController : Controller
    {
        public IActionResult Index() {
            // first visit to app - construct model and pass to view
            CourseGrades courseGrades = new CourseGrades();
            courseGrades.setupMe();
            return View(courseGrades);
        }

        [HttpPost]
        public IActionResult SelectCategory(CourseGrades courseGrades) {
            courseGrades.setupMe();
            return View("Index", courseGrades);
        }

        [Route("Grades/Details/{gradeID}")]
        public IActionResult Details(CourseGrades courseGrades, string gradeID) {         
            courseGrades.getDetails(gradeID);
            return View(courseGrades);
        }        

    }
}
