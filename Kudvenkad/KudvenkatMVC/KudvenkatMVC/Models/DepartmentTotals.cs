
namespace KudvenkatMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class DepartmentTotals
    {
        [Key]
        public string Name { get; set; }
        public int Total { get; set; }
    }
}
