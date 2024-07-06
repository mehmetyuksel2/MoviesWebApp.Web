using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesWebApp.Entity;
using System.Collections.Generic;
using System.Linq;

namespace MoviesWebApp.Data//48. ders
{
    public static class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<MovieContext>();//moviecontex sınıfını contexe atıyoruz

            context.Database.Migrate();//veri tabanına şema güncellemelerini gönder // dotnet ef database update

            var genres = new List<Genre>()
                        {
                            new Genre { name="macera", Movies=new List<Movie>(){

                                new Movie {
                                        Title = "yeni film",
                                        Description = "özel taşıyıcılık yapan bir şöförün akıl almaz maceraları",
                                        ImageUrl = "1.jpg"
                                    },//ardından yeni türü filmi içinde tanımladı
                                new Movie {
                                        Title = "yeni film2",
                                        Description = "sihir okulunda yaşanan bir birinden heyecanlı olaylar serisi",
                                        ImageUrl = "2.jpg"
                                    }
                                }
                            },
                            new Genre { name="komedi" },
                            new Genre { name="romantik" },
                            new Genre { name="savaş" },
                            new Genre { name="bilim kurgu" }
                        };
            var movies = new List<Movie>()
                        {
                            new Movie {
                                Title = "Taşıyıcı",
                                Description = "özel taşıyıcılık yapan bir şöförün akıl almaz maceraları",
                                ImageUrl = "1.jpg",
                                Genres = new List<Genre>(){genres[0], new Genre() {name="yeni tür"}, genres[1] } //var olan türü filme atadı
                            },//ardından yeni türü filmi içinde tanımladı
                            new Movie {
                                Title = "harry potter",
                                Description = "sihir okulunda yaşanan bir birinden heyecanlı olaylar serisi",
                                ImageUrl = "2.jpg",
                                Genres = new List<Genre>(){genres[0], genres[2] }
                            },
                            new Movie {
                                Title = "Baba parası",
                                Description = "dedelerinden kalan mirasın şifresini ararken bin bir maceraya atılan bir topluluk",
                                ImageUrl = "3.jpg",
                                Genres = new List<Genre>(){genres[1], genres[3] }
                            },
                            new Movie {
                                Title = "recep ivedik",
                                Description = "yalnız ve arakadaşı olmayan kaba bir insanın komik yaşam öyküsü",
                                ImageUrl = "1.jpg",
                                Genres = new List<Genre>(){genres[2], genres[4] }
                            },
                            new Movie {
                                Title = "senede bir gün",
                                Description = "kavuşamayan iki sevgilinin senede bir kere buluştuğu dram filmi",
                                ImageUrl = "2.jpg",
                                Genres = new List<Genre>(){genres[1], genres[2] }
                            },
                            new Movie {
                                Title = "300 spartalı",
                                Description = "yalnız 300 askerin yüz binlerce düşman askerine karşı vermiş olduğu mücadeleyi anlatan film",
                                ImageUrl = "3.jpg",
                                Genres = new List<Genre>(){genres[0], genres[4] }
                            }
                       };
            var user = new List<User>()
            {

                new User() { UserName = "usera", Email="usera@mail.com", Password="1234",ImageUrl="person1.jpg"},
                new User() { UserName = "userb", Email="userb@mail.com", Password="1234",ImageUrl="person2.jpg"},
                new User() { UserName = "userc", Email="userc@mail.com", Password="1234",ImageUrl="person3.jpg",
                
                    

                
                
                },
                new User() { UserName = "userd", Email="userd@mail.com", Password="1234",ImageUrl="person3.jpg",
                
                    

                
                
                }
            };
            var people = new List<Person>()
            {
                new Person()
                {

                    Name = "Person2",
                    Biography = "tanıtım2",
                    User = user[0]


                },
                new Person(){

                        Name="Person1",
                        Biography="tanıtım",
                        User = user[1]

                },
            };
            var crew = new List<Crew>()
            {
                new Crew(){Movie=movies[0],Person=people[0],Job="Yönetmen"},
                new Crew(){Movie=movies[0],Person=people[1],Job="Yönetmen Yard."}
                

            };
            var cast = new List<Cast>()
            {

                new Cast(){Movie=movies[0],Person=people[0],Name="Oyuncu Adı",Character="karakter 1"},
                new Cast(){Movie=movies[0],Person=people[1],Name="Oyuncu Adı 1",Character="karakter 2"}
            };

            //var i = genres.Count();

            if (context.Database.GetPendingMigrations().Count() == 0)//bekleyen henüz uygulanmamış migrationları sayar ve sıfıra eşitse
            {
                if (context.Genres.Count() == 0)
                {
                    context.Genres.AddRange(genres);
                }
                if (context.MovieSet.Count() == 0)
                {
                    context.MovieSet.AddRange(movies);
                }
                if (context.Users.Count() == 0)
                {
                    context.Users.AddRange(user);
                }
                if (context.People.Count() == 0)
                {
                    context.People.AddRange(people);
                }
                if (context.Crews.Count() == 0)
                {
                    context.Crews.AddRange(crew);
                }
                if (context.Casts.Count() == 0)
                {
                    context.Casts.AddRange(cast);
                }


                context.SaveChanges();
            }
        }
    }
}
