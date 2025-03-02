namespace E_Ticket_System.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Price { get; set; }

        public string ImgUrl { get; set; }

        public string TrailerUrl { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }



        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ActorMovies> ActorMovies { get; set; }

        public bool MovieStatus
        {
            get
            {
                if (DateTime.Now >= StartDate && DateTime.Now <= EndDate)
                {
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
        }
    }
}

