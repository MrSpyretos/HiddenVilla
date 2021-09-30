using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9_.]+$",ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage ="Password is Required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password's do nto match.")]
        public string ConfirmPassword { get; set; }
    }
}
