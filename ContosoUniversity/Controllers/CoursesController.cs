using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Data;

namespace ContosoUniversity.Controllers
{

    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Courses;
            return View(model);
        }
    }
}