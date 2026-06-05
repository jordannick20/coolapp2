using GradesApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GradesApp.ViewModels;

public class GradesViewModel
{
    public List<Grade> Grades { get; set; } = new List<Grade>();
    public List<SelectListItem> CategoryList { get; set; } = new List<SelectListItem>();
    public int CategoryID { get; set; }

    public GradesViewModel()
    {
    }
}
