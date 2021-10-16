using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OracleTask.Data.Abstract;
using OracleTask.Entity.Entities;
using OracleTask.Models;
using System.Collections.Generic;
using System.Linq;

namespace OracleTask.Controllers
{
    public class CityController : Controller
    {
        private ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityController(ICityRepository cityRepository,
                               IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            List<City> cities = _cityRepository.GetAll().ToList();

            var citiesDTO = new List<CityDTO>();

            foreach (var city in cities)
                citiesDTO.Add(_mapper.Map<CityDTO>(city));


            return View(citiesDTO);
        }

        [HttpGet]
        public IActionResult IndexJson()
        {
            List<City> cities = _cityRepository.GetAll().ToList();

            var citiesDTO = new List<CityDTO>();

            foreach (var city in cities)
                citiesDTO.Add(_mapper.Map<CityDTO>(city));


            var data = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    data = citiesDTO
                });

            return Ok(data);
        }


        [HttpGet]
        public IActionResult GetNames(string search)
        {
            var cities = _cityRepository
                            .GetAll()
                            .Where(c => c.Name.ToLower().Contains(search))
                            .Select(x => new
                            {
                                id = x.Id,
                                text = x.Name
                            });
            return Json(cities);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new CityDTO());
        }

        [HttpPost]
        public IActionResult Create(CityDTO cityDTO)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (cityDTO.Id == 0)
                {
                    City city = _mapper.Map<City>(cityDTO);

                    _cityRepository.Create(city);

                }

                return Json(new { isValid = true });
            }

            return Json(new { isValid = false });
        }

        public IActionResult Edit(int id)
        {

            City city = _cityRepository.GetById(id);

            CityDTO cityDTO = _mapper.Map<CityDTO>(city);

            return View(cityDTO);
        }

        [HttpPost]
        public IActionResult Edit(CityDTO cityDTO)
        {

            if (!_cityRepository.ExistById(cityDTO.Id))
            {
                ModelState.AddModelError("", "This user doesn't exists");
                return Json(new { isValid = false });

            }

            City city = _mapper.Map<City>(cityDTO);

            _cityRepository.Update(city);

            return Json(new { isValid = true });
        }

        public IActionResult Delete(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }

            var city = _cityRepository.GetById(id);

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {

            _cityRepository.Delete(id);

            List<CityDTO> usersDTO = new List<CityDTO>();

            foreach (var user in _cityRepository.GetAll().ToList())
                usersDTO.Add(_mapper.Map<CityDTO>(user));

            return Json(new object());
        }

    }
}
