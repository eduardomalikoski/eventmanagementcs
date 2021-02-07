using System.Collections.Generic;

namespace EventManagementApi.Services
{
    public interface IEventsRepository
    {
        Event Add(Event newEvent);
        IEnumerable<Event> GetAll();
        Event GetById(int id);
        void Delete(int id);
    }
}