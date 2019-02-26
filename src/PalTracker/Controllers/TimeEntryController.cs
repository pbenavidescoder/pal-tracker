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
        private readonly IOperationCounter<TimeEntry> counter;

        public TimeEntryController(ITimeEntryRepository repository, IOperationCounter<TimeEntry> counter)
        {
                repo = repository;
                this.counter = counter;
        }

        [HttpGet("{id}", Name= "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            counter.Increment(TrackedOperation.Read);

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
            counter.Increment(TrackedOperation.List);
			var listTimeEntries = new OkObjectResult(repo.List())
			{ StatusCode = (int)HttpStatusCode.OK };

			return listTimeEntries;
           
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            counter.Increment(TrackedOperation.Create);
            TimeEntry timeEntryCreated;

          
            timeEntryCreated=repo.Create(timeEntry);
            return CreatedAtRoute("GetTimeEntry", new {id = timeEntryCreated.Id}, timeEntryCreated);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
            counter.Increment(TrackedOperation.Update);
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
            counter.Increment(TrackedOperation.Delete);
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


    