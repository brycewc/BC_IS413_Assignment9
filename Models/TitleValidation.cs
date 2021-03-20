using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BC_IS413_Assignment9.Models
{
    public class TitleValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {

            string Message = string.Empty;

            if (Convert.ToString(value) == "Independence Day")

            {

                Message = "Independence Day is not as good as Rocky IV, and is not valid input";

                return new ValidationResult(Message);

            }

            return ValidationResult.Success;

        }

    }
}

