using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OracleTask.Data.Abstract;
using OracleTask.Entity.Entities;
using OracleTask.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OracleTask.Controllers
{
    public class UsersController : Controller
    {

        private IUserRepository _userRepository;
        private IImageRepository _imageRepository;
        private ILocationRepository _locationRepository;


        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,
                               IImageRepository imageRepository,
                               ILocationRepository locationRepository,
                               IMapper mapper)
        {
            _userRepository = userRepository;
            _imageRepository = imageRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<User> users = _userRepository.GetAllWithProperties().ToList();

            var usersDTO = new List<UserDTOIndex>();

            foreach (var user in users)
                usersDTO.Add(_mapper.Map<UserDTOIndex>(user));


            return View(usersDTO);
        }

        [HttpGet]
        public IActionResult IndexJson()
        {
            List<User> users = _userRepository.GetAllWithProperties().ToList();

            var usersDTO = new List<UserDTOIndex>();

            foreach (var user in users)
                usersDTO.Add(_mapper.Map<UserDTOIndex>(user));

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    data = usersDTO
                });


            return Ok(data);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(new UserDTOCreate());
        }

        [HttpPost]
        public IActionResult Create(UserDTOCreate userDTO)
        {
            if (ModelState.IsValid)
            {
                User user = _mapper.Map<User>(userDTO);

                //if (_userRepository.GetIdByUsername(userDTO.Username)>0)
                //{
                //    ModelState.AddModelError("","This username already exists");
                //    return Json(new { isValid = false });
                //};


                _userRepository.Create(user);


                var extension = Path.GetExtension(userDTO.Image.FileName);

                var randomName = string.Format($"{Guid.NewGuid()}{extension}");

                string path = Path.Combine(Directory.GetCurrentDirectory()
                                                            , "wwwroot", "images"
                                                            , randomName);

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    userDTO.Image.CopyTo(fs);
                }

                int id = _userRepository.GetIdByUsername(userDTO.Username);

                Image image = new Image()
                {
                    Name = randomName,
                    UserId = id
                };

                _imageRepository.Create(image);

                Location location = new Location()
                {
                    Latitude = userDTO.Location.Latitude,
                    Longitude = userDTO.Location.Longitude,
                    MarkAs = userDTO.Location.MarkAs,
                    UserId = id
                };

                _locationRepository.Create(location);

                return Json(new { isValid = true });
            }
            return Json(new { isValid = false });

        }

        public IActionResult Edit(int id)
        {

            User user = _userRepository.GetWithPropertiesById(id);

            UserDTOIndex userDTO = _mapper.Map<UserDTOIndex>(user);

            return View(userDTO);
        }


        [HttpPost]
        public IActionResult Edit(UserDTOCreate userDTO)
        {

            if (!_userRepository.ExistById(userDTO.Id))
            {
                ModelState.AddModelError("", "This user doesn't exists");
                return Json(new { isValid = false });
            }

            User user = _mapper.Map<User>(userDTO);

            #region Image Edit
            Image image = _imageRepository.GetByUserId(user.Id);

            //XX
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(),
                                                                      "wwwroot", "images"
                                                                   , image.Name);

            FileInfo fi = new FileInfo(oldPath);
            if (fi != null)
            {
                System.IO.File.Delete(image.Name);
                fi.Delete();
            }
            //XXX

            var extension = Path.GetExtension(userDTO.Image.FileName);

            var randomName = string.Format($"{Guid.NewGuid()}{extension}");

            image.Name = randomName;

            string path = Path.Combine(Directory.GetCurrentDirectory()
                                                        , "wwwroot", "images"
                                                        , randomName);

            using (FileStream fs = new FileStream(path, FileMode.Create))
                userDTO.Image.CopyTo(fs);

            _imageRepository.Update(image);
            #endregion

            #region Location Edit

            Location location = _locationRepository.GetByUserId(user.Id);

            location.MarkAs = userDTO.Location.MarkAs;
            location.Latitude = userDTO.Location.Latitude;
            location.Longitude = userDTO.Location.Longitude;

            _locationRepository.Update(location);

            #endregion

            _userRepository.Update(user);

            return Json(new { isValid = true });
        }

        public IActionResult ViewLocation(SetLocationDTO dto)
        {
            SetLocationDTO location = new SetLocationDTO()
            {
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                MarkAs = dto.MarkAs
            };

            return View(location);
        }

        [HttpGet]
        public IActionResult SetLocation(string latitude, string longitude, string markAs)
        {
            SetLocationDTO location = new SetLocationDTO()
            {
                Latitude = latitude,
                Longitude = longitude,
                MarkAs = markAs
            };

            return View(location);
        }

        [HttpPost]
        public IActionResult SetLocation(SetLocationDTO dto)
        {
            SetLocationDTO location = new SetLocationDTO()
            {
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                MarkAs = dto.MarkAs
            };

            return Json(new { isValid = true, location });
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

            #region Image Delete
            Image image = _imageRepository.GetByUserId(id);

            var oldPath = Path.Combine(Directory.GetCurrentDirectory(),
                                                                     "wwwroot", "images"
                                                                  , image.Name);

            FileInfo fi = new FileInfo(oldPath);
            if (fi != null)
            {
                System.IO.File.Delete(image.Name);
                fi.Delete();
            }

            _imageRepository.Delete(image.Id);
            #endregion

            #region Location Delete

            Location location = _locationRepository.GetByUserId(id);

            _locationRepository.Delete(location.Id);

            #endregion

            _userRepository.Delete(id);             

            return Json(new { isValid = true });
        }
    }
}
