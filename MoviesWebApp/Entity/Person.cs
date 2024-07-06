namespace MoviesWebApp.Entity
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Imdb { get; set; }
        public string HomePage { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Biography { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }// foreign Key, Unique Key
    }

}
