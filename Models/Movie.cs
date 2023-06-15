namespace Testing.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Movie_name { get; set; }
        public string Movie_genre { get; set; }
        public int Movie_duration { get; set; }
        public decimal Movie_budget { get; set; }

        public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();
    }

}
