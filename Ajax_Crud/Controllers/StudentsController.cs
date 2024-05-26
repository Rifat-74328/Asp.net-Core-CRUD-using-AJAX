using Microsoft.AspNetCore.Mvc;
using Ajax_Crud.Models;
using Microsoft.AspNetCore.Cors;

namespace Ajax_Crud.Controllers
{
    
    [EnableCors("shop")]
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        
        public IActionResult ListOfStudent()
        {
            return View();
        }

        [HttpGet]
        // GET: Students
        public JsonResult ListOfStudents()
        {
            var data = _context.students.ToList();
            return Json(data);
        }

        
        public IActionResult PostStudent()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CreateStudent(Student student)
        {
            _context.students.Add(student);
            if (_context.SaveChanges() > 0)
            {
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult Delete(int id)
        {
            var data = _context.students.Where(x => x.id == id).FirstOrDefault();
            _context.students.Remove(data);
            if (_context.SaveChanges() > 0)
            {
                return Json("Deleted Successfully");
            }
            return Json("Cant Delete");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _context.students.FirstOrDefault(s => s.id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public JsonResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.students.Update(student);
                if (_context.SaveChanges() > 0)
                {
                    return Json(true);
                }
            }
            return Json(false);
        }


    }
}
