using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OracleTask.Data.Abstract;
using OracleTask.Entity.Entities;
using OracleTask.Models;
using System.Collections.Generic;
using System.Linq;

namespace OracleTask.Controllers
{
    public class LocationController : Controller
    {
        private ILocationRepository _locationRepository;
        private ICityRepository _cityRepository;

        private readonly IMapper _mapper;

        public LocationController(ILocationRepository locationRepository,
                                  ICityRepository cityRepository,
                                  IMapper mapper)
        {
            _locationRepository = locationRepository;
            _cityRepository= cityRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Location> locations = _locationRepository.GetAll().ToList();

            var locationsDTO = new List<LocationDTO>();

            foreach (var location in locations)
                locationsDTO.Add(_mapper.Map<LocationDTO>(location));


            return View(locationsDTO);
        }

        public IActionResult IndexJson()
        {
            List<Location> locations = _locationRepository.GetAll().ToList();

            var locationsDTO = new List<LocationDTO>();

            foreach (var location in locations)
                locationsDTO.Add(_mapper.Map<LocationDTO>(location));


            var data = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    data = locations
                });

            return Ok(data);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    List<City> cities=_cityRepository.GetAll().ToList();

        //    ViewBag.Cities = cities;
            
        //    return View(new LocationDTO());
        //}

        //[HttpPost]
        //public IActionResult Create(LocationDTO locationDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Insert
        //        if (locationDTO.Id == 0)
        //        {
        //            Location location = _mapper.Map<Location>(locationDTO);

        //            _locationRepository.Create(location);

        //        }

        //        return Json(new { isValid = true });
        //    }

        //    return Json(new { isValid = false });
        //}


        [HttpGet]
        public IActionResult ChooseLocation()
        {
          

            return View();
        }

    }
}
