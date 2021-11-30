using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.SqlServer.Data;

namespace Infrastructure.SqlServer.Repositories.Schedules
{
    public class ScheduleRepository:IScheduleRepository
    {
        private readonly Context _context;

        public ScheduleRepository(Context context)
        {
            _context = context;
        }
        
        
        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }

        public IEnumerable<Domain.Schedules> GetAllSchedule()
        {
            return _context.Schedules.ToList();
        }

        public Domain.Schedules GetScheduleById(Guid id)
        {
            return _context.Schedules.FirstOrDefault(p => p.Id == id);
        }

        public void CreateSchedule(Domain.Schedules schedule)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException(nameof(schedule));
            }

            _context.Schedules.Add(schedule);
        }

        public void UpdateSchedule(Domain.Schedules schedule)
        {
            
        }

        public void DeleteSchedule(Domain.Schedules schedule)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException(nameof(schedule));
            }

            _context.Schedules.Remove(schedule);
        }
    }
}