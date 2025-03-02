using E_Ticket_System.Models;
using E_Ticket_System.Repositries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_Ticket_System.Areas.Customer.Controllers
{
    [Area("Customer")]


    public class HomeController : Controller
    {
        public MovieRepository movieRepository = new MovieRepository();


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var data = movieRepository.Get(includes: [e => e.Cinema]);
            if (data != null) {
                return View(data.ToList());

            }
            return  RedirectToAction("ErrorPage");  
        }
        public IActionResult ErrorPage() { 
        return View();
        }
        public IActionResult search(string keyword)
        {
            var data = movieRepository.Get(e=> e.Name.Contains(keyword), includes:[e=> e.Cinema]).ToList();
            
                return View("Index", data.ToList());



        }



    }
}
