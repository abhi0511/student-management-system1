 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using student_data_management.data;
using student_data_management.Models;
using student_data_management.Models.Domain;

namespace student_data_management.Controllers
{

    public class StudentController : Controller
    {
        private readonly MVCdemodbcontext mvcdemodbcontext;
        public StudentController(MVCdemodbcontext mvcdemodbcontext) {
            this.mvcdemodbcontext = mvcdemodbcontext;
        }
        //private int rollno;
        //private string address;
        //private DateTime dob;
        //private object mvcDemoDbContext;


        //public Guid Id { get; private set; }
        //public string Name { get; private set; }
        //public string Email { get; private set; }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
      var students =    await   mvcdemodbcontext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Add(AddStudentViewModel addStudentRequest)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = addStudentRequest.Name,
                Email = addStudentRequest.Email,
                rollno = addStudentRequest.rollno,
                address = addStudentRequest.address,
                dob = addStudentRequest.dob,

            };
         await   mvcdemodbcontext.Students.AddAsync(student);
         await   mvcdemodbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
           
        }
        [HttpGet]
    public async Task < IActionResult> View(Guid Id)
        {
       var student =    await  mvcdemodbcontext.Students.FirstOrDefaultAsync(x => x.Id == Id);
            if (student != null)
            {
                var viewModel = new UpdateStudentViewModel()

                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    rollno = student.rollno,
                    address = student.address,
                    dob = student.dob,
                };
                return  await Task.Run(()=>View("view",viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateStudentViewModel model)
        {
            var student = await mvcdemodbcontext.Students.FindAsync(model.Id);

            if(student!= null)
            {
                student.Name = model.Name;
                student.Email = model.Email;
                student.rollno = model.rollno;
                student.address = model.address;
                student.dob = model.dob;

                await mvcdemodbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateStudentViewModel model)
        {
            var student = await  mvcdemodbcontext.Students.FindAsync(model.Id);
            if(student != null)
            {
                mvcdemodbcontext.Students.Remove(student);
                await mvcdemodbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
