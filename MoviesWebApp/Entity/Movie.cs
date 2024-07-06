using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesWebApp.Entity
{
    public class Movie
    {

        //       [Key,DatabaseGenerated(DatabaseGeneratedOption.None)] id parametresi  otomatik artandan çıkar serbesteleşir
        public Movie()
        {
            Genres = new List<Genre>();
        }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
