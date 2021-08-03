using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Data;

namespace WebDoctors.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PatientsController : Controller
    {
        private readonly UserManager<Person> _manager;
        public PatientsController(UserManager<Person> manager)
        {
            _manager = manager;
        }

        // GET: CustomersController
        public async Task<ActionResult> Index()
        {
            var users = await _manager.GetUsersInRoleAsync("Patient");
            return View(users);
        }

        // GET: CustomersController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var user = await _manager.FindByIdAsync(id);
            return View();
        }
    }
}
