using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetCoreGeneratedDocument;

namespace PTPMQLMvc.Models
{
    [Table("Employees")]
    public class Employee : Person
    {
      
        public string EmployeeId { get; set; }
        public int Age { get; set; }
    }
}
