using System;
using System.Collections.Generic;

namespace Infrastructure.SqlServer.Repositories.Schedules
{
    public interface IScheduleRepository
    {
        bool SaveChanges();
        IEnumerable<Domain.Schedules> GetAllSchedule();
        Domain.Schedules GetScheduleById(Guid id);
        void CreateSchedule(Domain.Schedules schedule);
        void UpdateSchedule(Domain.Schedules schedule);
        void DeleteSchedule(Domain.Schedules schedule);
    }
}