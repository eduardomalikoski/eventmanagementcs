using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementApi.Services
{
    public record Event(int Id, DateTime date, string Location, string Description);

    public class EventsRepository : IEventsRepository
    {
        private List<Event> Events { get; } = new();

        public Event Add(Event newEvent)
        {
            Events.Add(newEvent);
            return newEvent;
        }

        public IEnumerable<Event> GetAll() => Events;

        public Event GetById(int id) => Events.FirstOrDefault(e => e.Id == id);

        public void Delete(int id)
        {
            var eventToDelete = GetById(id);
            if (eventToDelete == null)
            {
                throw new ArgumentException("No events exists with the given id", nameof(id));
            }

            Events.Remove(eventToDelete);
        }
    }
}