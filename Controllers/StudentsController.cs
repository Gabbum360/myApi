using FirstApi_Project.DataBase;
using FirstApi_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FirstApi_Project.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace FirstApi_Project.Controllers
{
    // [Authorize]
    [EnableCors("CorsPolicy")]
    [Route("api/controllers")]
    [ApiController]
    public class StudentsController : ControllerBase //inherit StudentsController from the Controllers class.
    {
        private SchoolManagementDbContext SMDContext;   //SMDContext is an instance of the Database "SchoolManagementDbContext"
        //and "SchoolManagementDbContext" is an instance of "DbContext".
        public StudentsController(SchoolManagementDbContext SMDb)  //SMDb is Alias/another name i have given for "SchoolManagementDbContext".
        {
            SMDContext = SMDb;  //initiallising the "SMDb" explicitly.
        }

      
        [HttpGet("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            List<GetStudents> students = new List<GetStudents>();//created a list of "students" from the "GetStudents" class.
                                                                 //which will help pull all eachers when accessing this endpoint irrespective of the validations on the model.
            var studentsFromDb = await SMDContext.Students.ToListAsync();
            foreach (var item in studentsFromDb)
            {
                GetStudents std = new GetStudents();
                std.FirstName = item.FirstName;
                std.Age = item.Age;
                std.Country = item.Country;
                students.Add(std);
            }
            return Ok(students);
        }

        [HttpGet("get-student-with-full_Details/{id}")]
        public async Task<IActionResult> GetOneStudent(int id, Student student)
        {
            var pupil = await SMDContext.Students.Where(student => student.Id == id.ToString()).FirstOrDefaultAsync();
            return Ok(pupil);
        }

        [HttpGet("get-pupil-encrpted/{id}")]
        public async Task<IActionResult> GetOnestudentinfo(int id, [FromBody] GetStudentbyId stdnt)
        {
            var pupil = await SMDContext.Students.Where(s=>s.Id == id.ToString()).FirstOrDefaultAsync();
            stdnt.Id = pupil.Id;
            stdnt.SurName = pupil.SurName;
            stdnt.FirstName = pupil.FirstName;  
            return Ok(pupil);
        }

        /*public async Task<IActionResult> GetOneStudentL()
        {
            var stud = await SMDContext.Students.Include(s=>s.Country).FirstOrDefault(c=>c.Id).ToString();
        }*/

        [HttpPost("register-new-student")]
        public async Task<IActionResult> RegisterNewStudent()
        {
            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    Id = "103",
                    SurName = "Fadolamu",
                    FirstName = "Oluwafunmilayo",
                    Age = 25,
                    Sex = "Female",
                    ClassArmId = "1555",
                    Country = "Germany",
                    StudentNo = 20004,
                };
                SMDContext.Add(student);
                await SMDContext.SaveChangesAsync();
            }
                return Ok("student added!");
        }

        [HttpPost("insert-new-record")]
        public async Task<IActionResult> InsertNewRecord()
        {
            if (ModelState.IsValid)// using validation to validate user inputs as required from the Model.
            {
                var pupil = new Student()
                {
                    Id = "102",
                    FirstName = "Clement",
                    SurName = "Ochayi",
                    Age = 22,
                    Sex = "Male",
                    Country = "USA",
                    StudentNo = 20002
                };
                SMDContext.Add(pupil);
                await SMDContext.SaveChangesAsync();
            }
            return Ok("pupil added!");
        }

        [HttpPost("register-new-Std")]
        public async Task<IActionResult> RegisterStudent([FromBody] AddStudent student)//this method allows user input from the endpoint when called.
        {
            if (ModelState.IsValid)
            {
                var newStudent = new Student()
                {
                    Id = student.Id,
                    SurName = student.SurName,
                    FirstName = student.FirstName,
                    Age = student.Age,
                    Sex = student.Sex,
                  //  ClassArmId = student.ClassArmId,
                    Country = student.Country,
                    //StudentNo = student.StudentNo,
                };
                SMDContext.Add(newStudent);
                await SMDContext.SaveChangesAsync();
            }
            return Ok("student added!");
        }

        [HttpPatch("update-studentRecord/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student studnt)
        {
            var sted = SMDContext.Students.Where(student => student.Id == id.ToString()).Select(student => student).FirstOrDefault();
            /*sted.FirstName = studnt.FirstName;
            sted.Age = studnt.Age;
            sted.Sex = studnt.Sex;*/
            sted.Country = studnt.Country;
            //sted.StudentNo = studnt.StudentNo;
            sted.ClassArmId = studnt.ClassArmId;
            await SMDContext.SaveChangesAsync();
            return Ok("table updated");
        }

        [HttpPatch("change-few-data/{id}")]// this method allows the user to input see and edit only the required. base on the model created.
        public async Task<IActionResult> MakeChanges(int id, [FromBody] UpdateStudent pupil)
        {
            var sted = SMDContext.Students.Where(student => student.Id == id.ToString()).Select(student => student).FirstOrDefault();
            sted.Country = pupil.Country;
            await SMDContext.SaveChangesAsync();
            return Ok(sted);
        }

        [HttpDelete("remove-student-from-table")]
        public async Task<IActionResult> DeleteStudent()
        {
            var student = new Student()
            {
                Id = "103"
            };
            SMDContext.Remove(student);
            await SMDContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent(int id, [FromBody] Student student)
        {
            var pupil = SMDContext.Students.Where(p => p.Id == id.ToString()).Select(student => student).FirstOrDefault();
            SMDContext.Remove(pupil);
            await SMDContext.SaveChangesAsync();
            return Ok("delete successful");
        }
    }
}
