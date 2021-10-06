using Distro2.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Distro2.DAL
{
    //Repository interface handling communication with the user database. 
    public interface IUserRepository : IDisposable
    {
        //Returns all users in the user database.
        Task<List<Distro2User>> GetUsers();
        //Update a users number of logins and last login date in the database.
        void updateUser(string email);
        //Get last login  date for a user.
        Task<DateTime> GetUserLastLogin(string email);
        //Get monthly number of logins for a user. 
        Task<int> monthlyLogins(string email);
        //Update number of deletions for a user. 
        void updateDeletions(string? id);
        // Get nr of deletions for a user. 
        Task<int> NrOfDeletions(string email);

    }
}
