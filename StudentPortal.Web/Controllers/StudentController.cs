using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Infrastructure;
using StudentPortal.MODEL;
using StudentPortal.Web.Models;

namespace StudentPortal.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationContext _context;

        public StudentController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel addStudentViewModel)
        {
            //should use automapper here..............to map the modellll...
            Student student = new Student
            {
                Name = addStudentViewModel.Name,
                Email = addStudentViewModel.Email,
                PhoneNumber = addStudentViewModel.PhoneNumber,
                Subscribed = addStudentViewModel.Subscribed,

            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", "Student");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await _context.Students.ToListAsync();

            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var student = await _context.Students.FindAsync(id);

            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            var existingStudent = await _context.Students.FindAsync(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.PhoneNumber = student.PhoneNumber;
                existingStudent.Subscribed = student.Subscribed;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("List", "Student");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var studentttToDelet = await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id ==id);
            if (studentttToDelet is not null)
            {
                _context.Students.Remove(studentttToDelet);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }
    }
}
