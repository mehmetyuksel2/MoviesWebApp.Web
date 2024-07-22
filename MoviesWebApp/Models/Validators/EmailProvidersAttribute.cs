using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Models.Validators
{
    public class EmailProvidersAttribute : ValidationAttribute
    {
        //ÖRNEK OLARAK YAŞ VERİSİNİN BELİRLİ KOŞULLARDA GİRİLMESİNİ SAĞLARIZ
        //VALİDATİONRESULT İLE CUSTOM BİR ISVALİD OLUŞTURDUK
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string email = "";
            if(value != null)
            {
                email = value.ToString();
            }
            if(email.EndsWith("@gmail.com") || email.EndsWith("@hotmail.com"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Hatalı eposta sunucu");
        }
    }
}
