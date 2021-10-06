using System;
using Microsoft.EntityFrameworkCore;
using Distro2.Models;

namespace Distro2.Data
{
    public class MessageContext : DbContext
    {
        //Message database context
        public MessageContext(DbContextOptions<MessageContext> options)
           : base(options)
        {
        }

        public DbSet<Message> Message { get; set; }
    }
}
