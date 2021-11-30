using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.SqlServer.Data;

namespace Infrastructure.SqlServer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }
        
        
        
        public bool SaveChanges()
        {
            //all changes will be implemented in DB only if this method is called
            return (_context.SaveChanges()) >= 0;
        }

        public IEnumerable<Domain.User> GetAll()
        {
            return _context.Users.ToList();
        }

        public Domain.User GetByUserId(Guid id)
        {
            return _context.Users.FirstOrDefault(p => p.Id == id);
        }

        public void CreateUser(Domain.User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
        }

        public void UpdateUser(Domain.User user)
        {
            //nothing
        }

        public void DeleteUser(Domain.User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Remove(user);
        }
    }
}