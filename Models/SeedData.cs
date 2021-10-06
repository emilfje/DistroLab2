using Distro2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Distro2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MessageContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MessageContext>>()))
            {
                if (context.Message.Any())
                {
                    return;   // DB has been seeded
                }

                context.Message.AddRange(
                    new Message
                    {
                        Title = "Test message 1",
                        IsRead = false,
                        Body = "sample body lalalalalla",
                        TimeStamp = DateTime.Parse("1989-2-12"),
                        Sender = "bert@kth.se",
                        Receiver = "jonas@kth.se"
                    },

                   new Message
                   {
                       Title = "Test message 2",
                       IsRead = false,
                       Body = "heeeeeeeeeeeeeeeeeej kompis",
                       TimeStamp = DateTime.Parse("2015-5-20"),
                       Sender = "jonas@kth.se",
                       Receiver = "bert@kth.se"
                   },
                   new Message
                   {
                       Title = "Test message 3",
                       IsRead = false,
                       Body = "oasdiadiasidiasdasd",
                       TimeStamp = DateTime.Now,
                       Sender = "jonas@kth.se",
                       Receiver = "bert@kth.se"
                   },
                   new Message
                   {
                       Title = "Test message 4",
                       IsRead = false,
                       Body = "lorem ipsum och grejor",
                       TimeStamp = DateTime.Now,
                       Sender = "bert@kth.se",
                       Receiver = "jonas@kth.se"
                   }, 
                   new Message
                   {
                       Title = "Test message 5",
                       IsRead = false,
                       Body = "hohohoho 1234124123",
                       TimeStamp = DateTime.Now,
                       Sender = "lisa@kth.se",
                       Receiver = "bert@kth.se"
                   }



                );
                context.SaveChanges();
            }
        }
    }
}
