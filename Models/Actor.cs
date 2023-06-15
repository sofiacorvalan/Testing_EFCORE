using System;
using System.Collections.Generic;

namespace Testing.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Actor_name { get; set; }
        public DateTime Actor_birthday { get; set; }
        public string Actor_picture { get; set; }
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    }

}
