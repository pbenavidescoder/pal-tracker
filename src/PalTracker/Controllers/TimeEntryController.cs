using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace PalTracker
{
    [Route("time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private ITimeEntryRepository repo;

        public TimeEntryController(ITimeEntryRepository repository)
        {
                repo = repository;
        }

        [HttpGet("{id}", Name= "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            TimeEntry timeEntry;

           if (repo.Contains(id))
           {
               timeEntry=repo.Find(id);
               
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
			var listTimeEntries = new OkObjectResult(repo.List())
			{ StatusCode = (int)HttpStatusCode.OK };

			return listTimeEntries;
           
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            TimeEntry timeEntryCreated;

          
            timeEntryCreated=repo.Create(timeEntry);
            return CreatedAtRoute("GetTimeEntry", new {id = timeEntryCreated.Id}, timeEntryCreated);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
			TimeEntry updatedTimeEntry;
			if (!repo.Contains(id))
			{
				return NotFound();
			}
			updatedTimeEntry =repo.Update(id, timeEntry);
			return new OkObjectResult(updatedTimeEntry);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id==0)
			{
				return BadRequest();
			}
			if (repo.Contains(id))
			{
				repo.Delete(id);
				return NoContent();
			}
			else
			{
				return NotFound();
			}
				
        }

    }
}


    