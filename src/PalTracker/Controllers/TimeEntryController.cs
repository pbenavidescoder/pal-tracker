using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PalTracker
{
    [Route("api/timeentries")]
    public class TimeEntryController : ControllerBase
    {
        private ITimeEntryRepository _inMemoryTimeEntryRepository;

        public TimeEntryController(ITimeEntryRepository inMemoryTimeEntryRepository)
        {
                _inMemoryTimeEntryRepository = inMemoryTimeEntryRepository;
        }

        [HttpGet("{id}")]
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
           /* IEnumerable<TimeEntry> listTimeEntries;
            listTimeEntries = _inMemoryTimeEntryRepository.List();
           if (listTimeEntries == null)
            {
                return NotFound();
            }

            return Ok(listTimeEntries);
            */
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            TimeEntry timeEntryCreated;

            if (timeEntry.Id==null)
            {
                return BadRequest();
            }
            timeEntryCreated=_inMemoryTimeEntryRepository.Create(timeEntry);
            return CreatedAtRoute("Read", new {Id = timeEntryCreated.Id}, timeEntryCreated);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            throw new NotImplementedException();
        }

    }
}


    