using Company.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Company.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; } // Pk = idetity

        [Required(ErrorMessage = "Name is Requires")]
        [MaxLength(50, ErrorMessage = "Max length is 50 chars")]
        [MinLength(5, ErrorMessage = "Max length is 5 chars")]
        public string Name { get; set; }

        [Range(22, 35, ErrorMessage = "Age must be in range from 22 to 35 ")]
        public int? Age { get; set; }

        [RegularExpression("123-street", ErrorMessage = "Address Must Be like 123-street ")]
        //[RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4-10]-[a-zA-Z]{5,10}$",
        //    ErrorMessage = "Address Must Be like 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }
        
        public IFormFile Image { get; set; }

        public string ImageName { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; } // FK
        // FK optional => ondelete : restrict
        // FK requierd => ondelete  : cascade
        public Department Department { get; set; } // Navigation property [ one ]

    }
}
