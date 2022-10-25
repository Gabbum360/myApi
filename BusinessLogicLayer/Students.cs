using Core.Models;
using Core.ViewModel;
using Infrastructuress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;

namespace BusinessLogicLayer
{
    public class Students: IStudents
    {
        private  SchoolDbContext SMDContext;
        public List<GetStudents> allStudentsinSchool { get; set; }
        public Students(SchoolDbContext SMDb)
        {
            SMDContext = SMDb;
            allStudentsinSchool = new List<GetStudents>();
        }

        public Students()
        {
        }

/*        public async Task<Student> GetStudents()
        {
            List<GetStudents> students = new List<GetStudents>();//created a list of "students" from the "GetStudents" class.
                                                                 //which will help pull all Studentss when accessing this endpoint irrespective of the validations on the model.
            var studentsFromDb = await SMDContext.Students.ToListAsync();
            foreach (var item in studentsFromDb)
            {
                GetStudents std = new GetStudents();
                std.FirstName = item.FirstName;
                std.Age = item.Age;
                std.Country = item.Country;
                students.Add(std);
            }
            return students;
        }*/

        public async Task<Student> GetStudents(int id)
        {
            var pupil = await SMDContext.Students.Where(student => student.Id == id.ToString()).FirstOrDefaultAsync();
            return pupil;
        }

        public Student Regr()
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
            SMDContext.SaveChangesAsync();

            return student;
        }
        public async Task<Student> Regr(string SurName, string FirstName, int Age, string Sex, string ClassArmId, string Country, int StudentNo)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                SurName = SurName,
                FirstName = FirstName,
                Age = Age,
                Sex = Sex,
                ClassArmId = ClassArmId,
                Country = Country,
                StudentNo = StudentNo,
            };
            SMDContext.Add(student);
            await SMDContext.SaveChangesAsync();

            return student;
        }
        /* public Students GetAll()
         {
             throw new NotImplementedException();
         }*/
        public async Task<Student> GetS(string id)
        {
            var pupil = await SMDContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
            return pupil;
        }

        public async Task<Student> UpdateS(string id)
        {
            var stdnt = await SMDContext.Students.Where(x => x.Id == id).Select(v => v).FirstOrDefaultAsync();
            return stdnt;
        }

        public async Task<Student> DeleteS(string id)
        {
            var pupil = SMDContext.Students.Where(D => D.Id == id).Select(Student => Student).FirstOrDefault();
             SMDContext.Remove(pupil);
            await SMDContext.SaveChangesAsync();
            return pupil;
        }
    }
}
