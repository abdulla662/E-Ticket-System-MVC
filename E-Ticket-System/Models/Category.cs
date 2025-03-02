namespace E_Ticket_System.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Movie> Movies { get; set; }

    }
}
