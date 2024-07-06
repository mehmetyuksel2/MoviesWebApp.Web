using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.Models.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = " UserName için 10 karakterden fazla olamaz")]
        [Remote(action: "VerifyUserName", controller:"User")]
        public string UserName { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = " {0} karakter uzunluğu {2}-{1} arasında olmalıdır", MinimumLength =3)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [EmailProviders]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]//input içerisinde yıldızlı gözükür
        [Compare(nameof(Password))]//password değişkeni ile karşılaştırıyoruz
        public string RePassword { get; set; }
        [Url]
        public string Url { get; set; }
        //[Range(1900,2010)]
        //public int BirthYear { get; set; }
        [BirthDate(ErrorMessage ="Doğum tarihiniz şimdiki yada sonraki tarih olamaz.")]//birthdateAttribute ile bağlantılı
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
    }
}
