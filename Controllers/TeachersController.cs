using FirstApi_Project.DataBase;
using FirstApi_Project.Models;
using FirstApi_Project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi_Project.Controllers
{
    public class TeachersController : ControllerBase
    {
        private SchoolManagementDbContext SMDContext;
        public TeachersController(SchoolManagementDbContext SMDb)
        {
            SMDContext = SMDb;
        }

        [HttpGet("get-all-teachers")]
        public async Task<IActionResult> GetallTeachers()
        {
            List<GetTeachers> teachers = new List<GetTeachers>();   //created a list of "teachers" from the "Getteachers" class.
                                                                   //which will help pull all eachers when accessing this endpoint irrespective of the validations on the model.
            var staffFromDb = await SMDContext.Teachers.ToListAsync();
            foreach (var item in staffFromDb)
            {
                GetTeachers staff = new GetTeachers(); 
                staff.Id = item.Id;
                staff.Name = item.Name; 
                staff.Age = item.Age;   
                staff.Email = item.Email;
                staff.Country = item.Country;
                staff.Sex = item.Sex;
                staff.StaffNo = item.StaffNo; 
            }
            return Ok(teachers);
        }

        [HttpPost("add-new-teacher-to-database")]
        public async Task<IActionResult> CreateNewStaff([FromBody]Addteacher teacher)
        {
            if (ModelState.IsValid)
            {
                var newStaff = new Teacher()
                {
                    Id = teacher.Id,   
                    Name = teacher.Name,
                    Sex = teacher.Sex,
                    Age = teacher.Age,
                    Email = teacher.Email,
                    Country = teacher.Country,
                   // StaffNo = teacher.StaffNo,  
                };
                SMDContext.Add(newStaff);
                await SMDContext.SaveChangesAsync();
            }
            return Ok("added");
        }
        [HttpPost("regiser-new-student-from-codeBase")]
        public async Task<IActionResult> RegisterNewStaff()
        {
            var newStaff = new Teacher()
            {
                Id = 0,
                Name = "",
                Sex = "",
                Age = 0,
                Email = "",
                Country ="",
                StaffNo = 0
            };
            SMDContext.Add(newStaff);
            await SMDContext.SaveChangesAsync();
            return Ok(newStaff);
        }

        [HttpGet("get-one-teacher/{id}")]
        public async Task<IActionResult> GetOneStaff(int id)
        {
            var staff = await SMDContext.Teachers.Where(s => s.Id == id).Select(x => x).FirstAsync();
            return Ok(staff);
        }

        [HttpPut("update-record/{id}")]
        public async Task<IActionResult> EditRecord(int id, [FromBody] Teacher staff)
        {
            var sted = SMDContext.Teachers.Where(t => t.Id == id).Select(teacher => teacher).FirstOrDefault();
            sted.Age = staff.Age;
            sted.Name = staff.Name;
            sted.Sex = staff.Sex;
            await SMDContext.SaveChangesAsync();
            return Ok(sted);
        }

        [HttpPatch("update-only-Country/{id}")]
        public async Task<IActionResult> Update_Teacher(int id, [FromBody] UpdateTeacher staff)
        {
            var worker = SMDContext.Teachers.Where(T=>T.Id == id).Select(Teacher => Teacher).FirstOrDefault();
            if (worker == null) return BadRequest("Tacher don't exist");
            else
            {
                worker.Name = staff.Country;  
            }
            SMDContext.Add(worker);
            await SMDContext.SaveChangesAsync();
            return Ok("worker");
        }

    }

}
