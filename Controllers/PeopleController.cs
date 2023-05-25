﻿using CoolAppInTheCloud.Data.Models;
using CoolAppInTheCloud.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolAppInTheCloud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet("getPersonByName/{name}")]
        public async Task<IActionResult> GetPersonByName(string name)
        {
            try
            {
                var person = _peopleService.GetPersonByName(name);
                return Ok(person);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getAllPeople")]
        public async Task<IActionResult> GetAllPeople()
        {
            try
            {
                var people = _peopleService.GetAllPeople();
                return Ok(people);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.Admin)]
        public async Task<IActionResult> AddPerson([FromBody] Person person)
        {
            try
            {
                bool added = _peopleService.AddPerson(person);
                if (added)
                {
                    return Ok(person);
                }
                return BadRequest($"Could not add person: {person.Name}.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationPolicies.Admin)]
        public async Task<IActionResult> UpdatePerson([FromBody] Person person)
        {
            try
            {
                bool updated = _peopleService.UpdatePerson(person);
                if (updated)
                {
                    return Ok(person);
                }
                return BadRequest($"Could not update person: {person.Name}.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("searchByFilter/{filter}")]
        public async Task<IActionResult> SearchByFilter(string filter)
        {
            try
            {
                var people = _peopleService.Filter(filter);
                return Ok(people);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
