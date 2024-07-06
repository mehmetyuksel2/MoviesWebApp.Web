using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Models.Validators
{
    public class BirthDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime datetime = Convert.ToDateTime(value);

            return datetime >= DateTime.Now;
        }
    }
}
