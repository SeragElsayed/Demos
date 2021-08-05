using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.ViewModels
{
    public class UserVMParent
    {
        public string Id { get; set; }


        public List<Roles> Roles { get; set; } = new List<Roles>();
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        [Required]

        public string Email { get; set; }
    }
    public enum Roles
    {
        SuperAdmin, Admin, Finance, Sales, Manager, Employee
    }
}
