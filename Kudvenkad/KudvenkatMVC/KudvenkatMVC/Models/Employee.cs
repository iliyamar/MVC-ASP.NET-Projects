
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace KudvenkatMVC.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string City { get; set; }

        [Required]
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }


    }
}
