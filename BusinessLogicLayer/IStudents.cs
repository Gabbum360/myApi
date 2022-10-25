using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IStudents  // this interface is been registered on the Startup.cs 
    {
         Student Regr();
        Task<Student> Regr(string SurName, string FirstName, int Age, string Sex, string ClassArmId, string Country, int StudentNo);
        Task<Student> GetS(string id);
        Task<Student> UpdateS(string id);
        //Task<Student> GetStudents();
        Task<Student> DeleteS(string id);

    }
}
