using E_Ticket_System.Models;
using E_Ticket_System.Repositries;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace E_Ticket_System.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class MovieDetailsController : Controller
    {
        public MovieRepository movieRepository = new MovieRepository();
        public ActorRepository actorRepository = new ActorRepository();

        public IActionResult Details(int movieid)
        {
            var data = movieRepository.GetOne(
                x => x.Id == movieid,
                includes: new Expression<Func<Movie, object>>[]
                {
                    e => e.ActorMovies,
                    e => e.Category,
                    e => e.Cinema
                });

            if (data == null)
            {
                return Redirect("/Customer/Home/ErrorPage"); 
            }

            var actors = actorRepository.Get(
                e => e.ActorMovies.Any(am => am.MovieId == movieid),
                new Expression<Func<Actor, object>>[] { e => e.ActorMovies }
            );

            ViewBag.Actors = actors.ToList();
            return View(data);
        }
        public IActionResult ActorDetails(int actorid) {
         
            var data = actorRepository.GetOne(e => e.Id == actorid);
            if (data == null)
            {
                return Redirect("/Customer/Home/ErrorPage");

            }
            ViewBag.Movies = movieRepository.Get(m => m.ActorMovies.Any(am => am.ActorId == actorid)).ToList();

            return View(data);
        }
    }
}