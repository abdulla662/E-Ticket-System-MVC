using E_Ticket_System.Repositries;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticket_System.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CinemaController : Controller
    {
        public CinemaRepository CinemaRepository = new CinemaRepository();

        public IActionResult CinemaView ()
        {
            var data = CinemaRepository.Get();
            return View(data);
        }
        public IActionResult CinemaMovie(int id)
        {
            var cinema = CinemaRepository.GetOne(e => e.Id == id, includes: [e => e.Movies]);

            if (cinema == null)
            {
                return Redirect("/Customer/Home/ErrorPage");
            }

            return View(cinema);
        }

    }
}
