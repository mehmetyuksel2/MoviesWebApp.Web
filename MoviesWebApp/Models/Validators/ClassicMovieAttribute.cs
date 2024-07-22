using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Models.Validators
{
    public class ClassicMovieAttribute : ValidationAttribute
    {
        public ClassicMovieAttribute(int year)
        {
            Year = year;
        }
        public int Year { get; set; }
        //ÖRNEK OLARAK YAŞ VERİSİNİN BELİRLİ KOŞULLARDA GİRİLMESİNİ SAĞLARIZ
        //VALİDATİONRESULT İLE CUSTOM BİR ISVALİD OLUŞTURDUK
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (AdminMovieCreateModel)validationContext.ObjectInstance;
            var releaseDate = ((DateTime)value).Year;
            if (movie.IsClassic && releaseDate>Year)
            {
                return new ValidationResult($"Klasik film için {Year} ve öncesi değer girmelisiniz");
            }
            return ValidationResult.Success;
        }
    }
}
