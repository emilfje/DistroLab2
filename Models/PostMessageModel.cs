using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Distro2.Models
{
    public class PostMessageModel
    {
        public SelectList users { get; set; }
        public string user { get; set; }
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }
        [StringLength(300, MinimumLength = 3)]
        [Required]
        public string Body { get; set; }
    }
}
