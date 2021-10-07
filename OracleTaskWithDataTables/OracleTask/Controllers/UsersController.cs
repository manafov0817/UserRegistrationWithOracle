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
            List<User> users = _userRepository.GetAll().ToList();

            var usersDTO = new List<UserDTO>();

            foreach (var user in users)
                usersDTO.Add(_mapper.Map<UserDTO>(user));


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

        [HttpGet]
        public IActionResult Create()
        {

            return View(new UserDTO());
        }


        [HttpPost]
        public IActionResult Create(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (userDTO.Id == 0)
                {
                    User user = _mapper.Map<User>(userDTO);

                    _userRepository.Create(user);

                }



                return Json(new { isValid = true });
            }
            return Json(new { isValid = false });

        }

         public IActionResult Edit(int id)
        {

            User user = _userRepository.GetById(id);

            UserDTO userDTO = _mapper.Map<UserDTO>(user);

            return View(userDTO);
        }

        [HttpPost]
        public IActionResult Edit(UserDTO userDTO)
        {

            if (!_userRepository.ExistById(userDTO.Id))
            {
                ModelState.AddModelError("", "This user doesn't exists");
                return Json(new { isValid = false });

            }

            User user = _mapper.Map<User>(userDTO);

            _userRepository.Update(user);

            return Json(new { isValid = true });
        }

        public IActionResult Delete(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }

            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {

            _userRepository.Delete(id);

            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach (var user in _userRepository.GetAll().ToList())
                usersDTO.Add(_mapper.Map<UserDTO>(user));

            return Json(new object());
        }
    }
}
