using System;
using System.Collections.Generic;
using EventManagementApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsRepository repository;

        public EventsController(IEventsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Event>))]
        public IActionResult GetAll() => Ok(repository.GetAll());

        [HttpGet("{id}", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        public IActionResult GetById(int id)
        {
            var existingEvent = repository.GetById(id);
            if (existingEvent == null) return NotFound();
            return Ok(existingEvent);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] Event newEvent)
        {
            if (newEvent.Id < 1) 
            {
                return BadRequest("Invalid id");
            }

            repository.Add(newEvent);
            return CreatedAtAction(nameof(GetById), new { id = newEvent.Id}, newEvent);
        }

        [HttpDelete]
        [Route("{eventToDeleteId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int eventToDeleteId)
        {
            try
            {
                repository.Delete(eventToDeleteId);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}