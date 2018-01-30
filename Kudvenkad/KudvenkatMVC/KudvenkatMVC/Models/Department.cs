using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KudvenkatMVC.Models
{
    public partial class Department
    {
        public Department()
        {
            TblEmployee = new HashSet<Employee>();
        }
         
        public int? Id { get; set; }
        
        [Display(Name="Department")]
        public string Name { get; set; }

        public ICollection<Employee> TblEmployee { get; set; }
    }
}
