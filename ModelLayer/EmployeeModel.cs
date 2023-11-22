using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage ="{0} input should be given")]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$",ErrorMessage="First Name is not valid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} input should be given")]
        public string ProfileImage { get; set; }

        [Required(ErrorMessage = "{0} input should be given")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "{0} input should be given")]
        public string Department { get; set; }

        [Required(ErrorMessage = "{0} input should be given")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "{0} input should be given")]
        public DateTime StartDate { get; set; }
        public string Notes { get; set; }
    }
}
