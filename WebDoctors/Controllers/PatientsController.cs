using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Data;
using WebDoctors.Models;

namespace WebDoctors.Controllers
{
    [Authorize(Roles = "Admin, Doctor")]
    public class PatientsController : Controller
    {
        private readonly UserManager<Person> _manager;
        private readonly IMapper _mapper;

        public PatientsController(UserManager<Person> manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        // GET: CustomersController
        public async Task<ActionResult> Index()
        {
            var users = await _manager.GetUsersInRoleAsync("Patient");
            var model = _mapper.Map<List<PersonVM>>(users);
            return View(model);
        }

        // GET: CustomersController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var user = await _manager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var model = _mapper.Map<PersonVM>(user);
            return View(model);
        }
    }
}
