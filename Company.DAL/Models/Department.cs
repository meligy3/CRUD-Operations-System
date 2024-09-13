using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Department
    {
        public int Id { get; set; } // by convention => PK Identity

        [Required(ErrorMessage ="Code is Required !")]
        public string Code { get; set; } // Ref TYpe

        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(50)] 
        public string Name { get; set; }    

        public DateTime DateOfCreation { get; set; }   

       public IReadOnlyCollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        // Navigation property [ many ]

    }
}
