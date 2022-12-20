#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AugnosBlog.Enums;
using Microsoft.AspNetCore.Identity;

namespace AugnosBlog.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string AuthorId { get; set; }
        public string ModeratorId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2}, and at most {1} characters.", MinimumLength = 2)]
        [Display(Name = "Comment")]
        public string CommentBody { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ModeratedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2}, and at most {1} characters.", MinimumLength = 2)]
        [Display(Name = "Moderated Comment")]
        public string ModeratedBody { get; set; }

        public bool IsReady { get; set; }
        public string Slug { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        // Navigation Properties
        public virtual Post Post { get; set; }
        public virtual IdentityUser Author { get; set; }
        public virtual IdentityUser Moderator { get; set; }
    }
}
