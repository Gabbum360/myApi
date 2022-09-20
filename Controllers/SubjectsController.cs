using FirstApi_Project.DataBase;
using FirstApi_Project.Models;
using FirstApi_Project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi_Project.Controllers
{
    [Route("api/controllers")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private SchoolManagementDbContext SMDContext;
        public SubjectsController(SchoolManagementDbContext SMDb)
        {
            SMDContext = SMDb;
        }

        [HttpGet("get-all-subjects-encrpted")]// this methos displays all subjects from the viewModel.GetSubjects Model spec.
        public async Task<IActionResult> GetAllSubjects()
        {
            List<GetSubjects> subjects = new List<GetSubjects>(); //created a list of that classncalled "Getsubjects".
            var subjectsFromDB = await SMDContext.Subjects.ToListAsync();
            foreach (var item in subjectsFromDB)
            {
                GetSubjects subject = new GetSubjects(); //created an instance of the class here
                subject.Id = item.Id; 
                subject.Name = item.Name;
            }
            return Ok(subjects);
        }
        [HttpGet("get-all-subjects")]
        public async Task<IActionResult> GetAll()
        {
            var subjectsFromDb = await SMDContext.Subjects.ToListAsync();
            return Ok(subjectsFromDb); 
        }

        [HttpPost("Add-new-subject-encrpted")]
        public async Task<IActionResult> AddNewSubject([FromBody] AddSubject subjct)
        {
            var subject = new Subjects()
            {
                Name = subjct.Name,
            };
             SMDContext.Subjects.Add(subject);    
            await SMDContext.SaveChangesAsync();
            return Ok(subject); 
        }

        [HttpPatch("edit-subject/{id}")]
        public async Task<IActionResult> EditSubject(int id, Subjects sub)
        {
            var subjectFromDb = SMDContext.Subjects.Where(s => s.Id == id).Select(s => s).FirstOrDefault();
            subjectFromDb.Name = sub.Name;
            subjectFromDb.StudentId = sub.StudentId;
            SMDContext.Add(subjectFromDb);
            await SMDContext.SaveChangesAsync();
            return Ok(subjectFromDb);
        }

        [HttpDelete("delete-subject/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteQuery = SMDContext.Subjects.Where(s => s.Id == id).Select(s => s).FirstOrDefault();
            SMDContext.Remove(deleteQuery);
            await SMDContext.SaveChangesAsync();
            return Ok(Delete(id));
        }

    }
}
