using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Distro2.Areas.Identity.Data;
using Distro2.Data;
using System.Linq;

namespace Distro2.DAL
{
    //Repository handling communication with the user database. 
    //Implements IUserRepository interface
    public class UserRepository : IUserRepository
    {

        private DbContextOptionsBuilder<UserContext> optionsBuilder;
        public UserRepository()
        {
            optionsBuilder = new DbContextOptionsBuilder<UserContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UserContext-1;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        //Update number of deletions for a user. 
        public async void updateDeletions(string? id)
        {
            using (var context = new UserContext(optionsBuilder.Options))
            {
                var user = await context.User.Where(e => e.Email == id).FirstOrDefaultAsync();
                try
                {
                    user.NrOfDeletions++;
                    context.Update(user);
                    context.SaveChanges();
                }
                catch (Exception e) { }
            }
        }

        // Get nr of deletions for a user. 
        public async Task<int> NrOfDeletions(string email)
        {
            using (var context = new UserContext(optionsBuilder.Options))
            {
                var user = await context.User.Where(e => e.Email == email).FirstOrDefaultAsync();
                return user.NrOfDeletions;
            }
        }

        //Get last login  date for a user.
        public async Task<DateTime> GetUserLastLogin(string email)
        {
            using (var context = new UserContext(optionsBuilder.Options))
            {
                var user = await context.User.Where(e => e.Email == email).FirstOrDefaultAsync();
                return user.LastLoginDate;
            }
        }

        //Get monthly number of logins for a user. 
        public async Task<int> monthlyLogins(string email)
        {
            using (var context = new UserContext(optionsBuilder.Options))
            {
                var user = await context.User.Where(e => e.Email == email).FirstOrDefaultAsync();
                return user.NrOfLogins;
            }
        }

        //Update a users number of logins and last login date in the database.
        public async void updateUser(string email)
        {
            using (var context = new UserContext(optionsBuilder.Options))
            {
                var user = await context.User.Where(e => e.Email == email).FirstOrDefaultAsync();
                var tmp = DateTime.Now;
                if ((tmp.Year.Equals(user.LastLoginDate.Year)) && (tmp.Month.Equals(user.LastLoginDate.Month)))
                {
                    user.NrOfLogins++;
                }
                else
                {
                    user.NrOfLogins = 1;
                }
                user.LastLoginDate = DateTime.Now;
                context.Update(user);
                context.SaveChanges();
            }
        }

        //Returns all users in the user database.
        public async Task<List<Distro2User>> GetUsers()
        {
            using (var context = new UserContext(optionsBuilder.Options))
            {
                return await context.Users.ToListAsync();
            }
        }

        private bool disposed = false;

        //Disposes database connection after retrieval/update/read. 
        protected virtual void Dispose(bool disposing)
        {
            using (var context = new UserContext(optionsBuilder.Options))
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        context.Dispose();
                    }
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
