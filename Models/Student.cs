using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi_Project.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string ClassArmId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Country { get; set; }
        public int StudentNo { get; set; }

    }
}
