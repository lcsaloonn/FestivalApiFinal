using System;
using System.Collections.Generic;

namespace Infrastructure.SqlServer.Repositories.Hoodies
{
    public interface IHoodiesRepository
    {
        bool SaveChanges();
        IEnumerable<Domain.Hoodies> GetAllHoodies();
        Domain.Hoodies GetHoodieById(Guid id);
        void CreateHoodie(Domain.Hoodies hoodie);
        void UpdateHoodie(Domain.Hoodies hoodie);
        void DeleteHoodie(Domain.Hoodies hoodie);
    }
}