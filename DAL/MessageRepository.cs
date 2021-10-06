using Distro2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Distro2.Data;
using Distro2.Areas.Identity.Data;
using System.Linq;

namespace Distro2.DAL
{
    //Repository handling communication with the message database. 
    //Implements IMessageRepository interface
    public class MessageRepository : IMessageRepository
    {

        private DbContextOptionsBuilder<MessageContext> optionsBuilder;
        public MessageRepository()
        {
            optionsBuilder = new DbContextOptionsBuilder<MessageContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MessageContext-1;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        //Returns number of messages for a user corresponding to their id. 
        public async Task<int> NumberOfTotalMessages(string? id)
        {
            using (var context = new MessageContext(optionsBuilder.Options))
            {
                var messages = await context.Message.Where(e => e.Receiver == id).ToListAsync();
                return messages.Count;
            }
        }

        //Returns number of unread messages for a user corresponding to their id. 
        public async Task<int> NumberOfUnreadMessages(string? id)
        {
            using (var context = new MessageContext(optionsBuilder.Options))
            {
                var messages = await context.Message.Where(e => e.Receiver == id && !e.IsRead).ToListAsync();
                return messages.Count;
            }
        }

        //Updates the database with a new message.
        //PostMessageModel containing user input for the new message, sender is the user sending the message. 
        public async Task<Boolean> PostMessage(PostMessageModel Postmess, string sender)
        {
            using (var context = new MessageContext(optionsBuilder.Options))
            {
                try
                {
                    var message = new Message();
                    message.Receiver = Postmess.user;
                    message.Body = Postmess.Body;
                    message.Title = Postmess.Title;
                    message.Id = Postmess.Id;
                    message.IsRead = false;
                    message.TimeStamp = DateTime.Now;
                    message.Sender = sender;

                    context.Add(message);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        //Retrieves the users who have sent the logged in user messages.
        //users is a list of users, id is the id for the logged in user. 
        public async Task<List<Distro2User>> getReceivedUsers(List<Distro2User> users, string id)
        {
            using (var context = new MessageContext(optionsBuilder.Options))
            {
                var messages = await context.Message.Where(e => e.Receiver == id).ToListAsync();
                var tmp = new List<Distro2User>();
                foreach (var user in users)
                {
                    foreach (var message in messages)
                    {
                        if (message.Sender == user.UserName && !tmp.Contains(user))
                        {
                            tmp.Add(user);
                            break;
                        }
                    }
                }
                return tmp;
            }
        }

        //gets messages from a certain user, id is the specified users id. 
        public async Task<List<Message>> GetMessagesForUser(string? id)
        {
            using (var context = new MessageContext(optionsBuilder.Options))
            {
                var messages = await context.Message.Where(e => e.Sender == id).ToListAsync();
                return messages;
            }
        }

        //Delete a message with a certain id from the database.
        public async Task<string> DeleteMessage(int Id)
        {
            using (var context = new MessageContext(optionsBuilder.Options))
            {
                Message msg = context.Message.FindAsync(Id).Result;
                context.Message.Remove(context.Message.Find(Id));
                await context.SaveChangesAsync();
                return msg.Sender;
            }
        }

        //Get details about a message with a certain id. 
        public async Task<Message> GetDetails(int? Id)
        {
                using (var context = new MessageContext(optionsBuilder.Options))
                {
                    Message msg = context.Message.Find(Id);
                    if (!msg.IsRead)
                    {
                        msg.IsRead = true;
                        context.Message.Update(msg);
                        context.SaveChanges();
                    }
                    return await context.Message
                        .FirstOrDefaultAsync(m => m.Id == Id);
                }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            using (var context = new MessageContext(optionsBuilder.Options))
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
