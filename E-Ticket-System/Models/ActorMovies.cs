namespace E_Ticket_System.Models
{
    public class ActorMovies
    {
        public int ActorId { get; set; }
        public int MovieId { get; set; }
        public Actor actor { get; set; } 

        public Movie movie { get; set; } 


    }
}
