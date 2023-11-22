using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mid.Models
{
    public class SignupModel
    {

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 60 characters.")]
        [RegularExpression(@"^[a-zA-Z. -]*$", ErrorMessage = "Name can only contain letters, spaces, dots, and dashes.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Phone number is required.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9]+@gmail.com$", ErrorMessage = "Email must follow the format '@gmail.com'")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=(?:[^a-zA-Z]*[a-zA-Z]){2})(?=[^0-9]*[0-9])(?=[^A-Z]*[A-Z])(?=[^\s]*\W).{6,}$", ErrorMessage = "least 2 alphabets, least 1 number, least 1 uppercase alphabet, 1 special character, least totall 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}