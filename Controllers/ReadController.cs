using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Distro2.DAL;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Distro2.Controllers
{
    //Controller for reading received messages
    [Authorize]
    public class ReadController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public ReadController()
        {
            _messageRepository = new MessageRepository();
            _userRepository = new UserRepository();
        }

        //Read/Index
        // Updates view with users who have sent the user messages, aswell as displaying total messages, read messages and deleted messages. 
        public async Task<IActionResult> Index()
        {
            var id = User.Identity.Name;
            int totMessages = await _messageRepository.NumberOfTotalMessages(id);
            int unreadMessages = await _messageRepository.NumberOfUnreadMessages(id);
            ViewBag.totalMessages = totMessages;
            ViewBag.ReadMessages = totMessages - unreadMessages;
            ViewBag.NrOfDeletions = _userRepository.NrOfDeletions(id).Result;
            return View(await _messageRepository.getReceivedUsers(await _userRepository.GetUsers(), id));
        }

        // GET: Read/Inbox
        //Shows messages from sender with corresponding id
        public async Task<IActionResult> Inbox(string? id)
        {

            return View(await _messageRepository.GetMessagesForUser(id));
        }


        // GET: Read/Details/id
        //Details about a message with a id. 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            if (!CorrectUser(id))
            {
                return NotFound();
            }

            var message = await _messageRepository.GetDetails(id);
            ViewData["SendUser"] = message.Sender;

            if (message == null)
               return NotFound();

            return View(message);
        }


        // GET: Read/Delete/id
        //display delete template for a message with a certain id. 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            if (!CorrectUser(id))
            {
                return NotFound();
            }

            var message = await _messageRepository.GetDetails(id);
            ViewData["SendUser"] = message.Sender;

            if (message == null)
                return NotFound();

            return View(message);
        }

        // POST: Post/Delete/id
        //Delete a message from the database with a certain id. 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _messageRepository.DeleteMessage(id);
            _userRepository.updateDeletions(user);
            return RedirectToAction("Inbox", new { id = user });
        }


        private Boolean CorrectUser(int? id)
        {
            if (_messageRepository.GetDetails(id).Result.Receiver == User.Identity.Name)
                return true;
            return false;
        }
    }
}
