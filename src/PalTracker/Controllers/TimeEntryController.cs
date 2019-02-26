using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace PalTracker
{
    [Route("time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private ITimeEntryRepository _inMemoryTimeEntryRepository;

        public TimeEntryController(ITimeEntryRepository inMemoryTimeEntryRepository)
        {
                _inMemoryTimeEntryRepository = inMemoryTimeEntryRepository;
        }

        [HttpGet("{id}", Name= "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            TimeEntry timeEntry;

           if (_inMemoryTimeEntryRepository.Contains(id))
           {
               timeEntry=_inMemoryTimeEntryRepository.Find(id);
               
               return Ok(timeEntry);
           }
           else
           {
               return NotFound();
           }
        }

        [HttpGet]
        public IActionResult List()
        {
			var listTimeEntries = new OkObjectResult(_inMemoryTimeEntryRepository.List())
			{ StatusCode = (int)HttpStatusCode.OK };

			return listTimeEntries;
           
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            TimeEntry timeEntryCreated;

          
            timeEntryCreated=_inMemoryTimeEntryRepository.Create(timeEntry);
            return CreatedAtRoute("GetTimeEntry", new {id = timeEntryCreated.Id}, timeEntryCreated);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
			TimeEntry updatedTimeEntry;
			if (!_inMemoryTimeEntryRepository.Contains(id))
			{
				return NotFound();
			}
			updatedTimeEntry =_inMemoryTimeEntryRepository.Update(id, timeEntry);
			return new OkObjectResult(updatedTimeEntry);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id==0)
			{
				return BadRequest();
			}
			if (_inMemoryTimeEntryRepository.Contains(id))
			{
				_inMemoryTimeEntryRepository.Delete(id);
				return NoContent();
			}
			else
			{
				return NotFound();
			}
				
        }

    }
}


    