using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            var model = _context.Students;
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var student = _context.Students
                 .Include(s => s.Enrollments)
                     .ThenInclude(e => e.Course)
                 .FirstOrDefault(m => m.ID == id);



            if (student == null)
            {

                return NotFound();

            }

            return View(student);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EnrollmentDate,FirstMidName,LastName")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DbUpdateException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(student);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }


            var student = _context.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Student student)
        {

            var studentToUpdate = _context.Students
                .FirstOrDefault(m => m.ID == student.ID);

            if (studentToUpdate == null)
            {
                return NotFound();
            }

            studentToUpdate.FirstMidName = student.FirstMidName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.EnrollmentDate = student.EnrollmentDate;
            _context.Entry<Student>(studentToUpdate).State = EntityState.Modified;


            try

            {

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }

            catch (DbUpdateException ex)
            {

                //Log the error (uncomment ex variable name and write a log.)

                ModelState.AddModelError("", ex.Message);

            }

            return View(studentToUpdate);

        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _context.Students
                .FirstOrDefault(m => m.ID == id);

            if (student == null)
            {

                return NotFound();

            }


            return View(student);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Students.Remove(student);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
    }
}