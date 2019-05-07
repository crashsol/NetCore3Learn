using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSample.Data;
using WebSample.Models;

namespace WebSample.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext _dbContext;
        public HomeController(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View();
           
        }

        public IActionResult Privacy()
        {
            _dbContext.Persons.AddRange(new List<Person>() {   new Person
                                 {
                                     Id = Guid.NewGuid(),
                                     Name = DateTime.Now.ToString("yyyy-MM-ddhhmmssfff"),
                                     IsDelete = false,

                                 },

                new Person
                {
                    Id = Guid.NewGuid(),
                    Name = DateTime.Now.ToString("yyyy-MM-ddhhmmssfff"),
                    IsDelete = true,

                } }


            );
            _dbContext.SaveChanges();
            return View();
        }

        public IActionResult Data()
        {
            return Ok(await _dbContext.Persons.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
