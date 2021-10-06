using Distro2.Areas.Identity.Data;
using Distro2.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Distro2.DAL
{
    //Interface for handling communication with the message database. 
    public interface IMessageRepository : IDisposable
    {
        //gets messages from a certain user, id is the specified users id.
        Task<List<Message>> GetMessagesForUser(string? id);

        //Delete a message with a certain id from the database.
        Task<string> DeleteMessage(int Id);

        //Get details about a message with a certain id. 
        Task<Message> GetDetails(int? Id);

        //Retrieves the users who have sent the logged in user messages.
        //users is a list of users, id is the id for the logged in user. 
        Task<List<Distro2User>> getReceivedUsers(List<Distro2User> users, string id);

        //Updates the database with a new message.
        //PostMessageModel containing user input for the new message, sender is the user sending the message. 
        Task<Boolean> PostMessage(PostMessageModel Postmess, string sender);

        //Returns number of unread messages for a user corresponding to their id. 
        Task<int> NumberOfUnreadMessages(string? id);

        //Returns number of messages for a user corresponding to their id. 
        Task<int> NumberOfTotalMessages(string? id);

    }
}
