using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Distro2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Distro2.Areas.Identity.Data;
using Distro2.DAL;

namespace Distro2.Controllers
{
    //Controller for the posting page
    [Authorize]
    public class PostController : Controller
    {
        private readonly UserManager<Distro2User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;


        public PostController(UserManager<Distro2User> userManager)
        {
            _userRepository = new UserRepository();
            _messageRepository = new MessageRepository();
            _userManager = userManager;
        }

        // GET
        // Gets information necessary for posting template and updates view. 
        public async Task<IActionResult> Index(string? id)
        {
            var users = await _userRepository.GetUsers();
            ViewData["Users"] = (users);
            var postmess = new PostMessageModel();
            postmess.users = new SelectList(users);
            if (id != "" && id != null)
                ViewBag.Confirm = id;
            else
                ViewBag.Confirm = null;
            return View(postmess);
        }



        // POST
        // Creates a post and updates the database. Updates view to confirm a message is sent. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Title, Body, user")] PostMessageModel Postmess)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _messageRepository.PostMessage(Postmess, _userManager.GetUserName(HttpContext.User)))
                    {
                        return RedirectToAction("Index", new { id = Postmess.user });
                    }
                }
                catch (Exception e) { }
            }
            return RedirectToAction("Index", new { id = "" });
        }
    }
}
