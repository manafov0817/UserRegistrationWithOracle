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
using static OracleTask.Helper;

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

            User user = _mapper.Map<User>(userDTO);

            _userRepository.Create(user);

            return PartialView("_AddUserPartial", new UserDTO());
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {


            return View(new User());
        }



        public IActionResult AddOrEdit(int id)
        {
            if (id == 0)
                return View(new UserDTO());
            else
            {
                var user = _userRepository.GetById(id);

                UserDTO userDTO = _mapper.Map<UserDTO>(user);


                if (user == null)
                {
                    return NotFound();
                }
                return View(userDTO);
            }
        }


        [HttpPost]
        public IActionResult AddOrEdit(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (userDTO.Id == 0)
                {
                    User user = _mapper.Map<User>(userDTO);

                    _userRepository.Create(user);

                }
                //Update
                else
                {
                    try
                    {
                        User user = _mapper.Map<User>(userDTO);

                        _userRepository.Update(user);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                }


                List<UserDTO> usersDTO = new List<UserDTO>();

                foreach (var user in _userRepository.GetAll().ToList())
                    usersDTO.Add(_mapper.Map<UserDTO>(user));

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", usersDTO) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", userDTO) });

        }


        // GET: Transaction/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var transactionModel = _userRepository.GetById(id);

            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            _userRepository.Delete(id);

            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach (var user in _userRepository.GetAll().ToList())
                usersDTO.Add(_mapper.Map<UserDTO>(user));

            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", usersDTO) });
        }
    }
}
