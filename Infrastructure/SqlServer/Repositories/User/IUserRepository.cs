using System;
using System.Collections.Generic;

namespace Infrastructure.SqlServer.Repositories.User
{
    public interface IUserRepository
    {
        bool SaveChanges();
        IEnumerable<Domain.User> GetAll();
        Domain.User GetByUserId(Guid id);
        void CreateUser(Domain.User user);
        void UpdateUser(Domain.User user);
        void DeleteUser(Domain.User user);


    }
}