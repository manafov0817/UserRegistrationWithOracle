using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OracleTask;
using OracleTask.Data.Abstract;
using OracleTask.Entity.Entities;
using OracleTask.Models;

namespace OracleTask.Controllers
{
    public class UsersController : Controller
    {

        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,
                               IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            //List<User> users = _userRepository.GetAll().ToList();

            var usersDTO = new List<UserDTO>();

            //foreach (var user in users)
            //    usersDTO.Add(_mapper.Map<UserDTO>(user));


            return View(usersDTO);
        }

        [HttpGet]
        public IActionResult IndexJson()
        {
            List<User> users = _userRepository.GetAll().ToList();

            var usersDTO = new List<UserDTO2>();

            foreach (var user in users)
                usersDTO.Add(_mapper.Map<UserDTO2>(user));

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                    {
                        data = users
                    });

 
            return Ok(data);
        }



        public IActionResult Details(int? id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {

            return PartialView("_AddUserPartial", new UserDTO());
        }


        [HttpPost]
        public IActionResult Create(UserDTO userDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("", "Please Fill Correctly");
            //    return View();
            //}

            User user = _mapper.Map<User>(userDTO);

            _userRepository.Create(user);

            return PartialView("_AddUserPartial", new UserDTO());
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {

            User user = _userRepository.GetById(id);

            UserDTO userDTO = _mapper.Map<UserDTO>(user);

            return View(userDTO);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserDTO userDTO)
        {

            if (!_userRepository.ExistById(userDTO.Id))
            {
                ModelState.AddModelError("", "This user doesn't exists");
                return View(userDTO);
            }

            User user = _mapper.Map<User>(userDTO);


            _userRepository.Update(user);

            return RedirectToAction("Index", "Users");
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int id)
        {

            _userRepository.Delete(id);

            return RedirectToAction("Index", "Users");
        }


    }
}
