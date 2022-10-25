using Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IGetAll
    {
        List<GetStudents> _allStudentsinSchool { get; set; }
        Students GetAll();
    }
}
