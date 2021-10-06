using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Distro2.Models;
using Microsoft.AspNetCore.Authorization;
using Distro2.DAL;

namespace Distro2.Controllers
{

    //Homepage controller
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _messageRepository = new MessageRepository();
            _userRepository = new UserRepository();
        }

        // Homepage displaying unread messages, last login date and times of logins this month. 
        public async Task<IActionResult> Index()
        {
            @ViewBag.unreadMessages = await _messageRepository.NumberOfUnreadMessages(User.Identity.Name);
            @ViewBag.LastLogin =  _userRepository.GetUserLastLogin(User.Identity.Name).Result;
            @ViewBag.monthlyLogins =  _userRepository.monthlyLogins(User.Identity.Name).Result;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
