using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.ViewModels
{
    public class CreateUserVM: UserVMParent
    {
        [DataType(DataType.Password)]
        [Required]

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
    
}
