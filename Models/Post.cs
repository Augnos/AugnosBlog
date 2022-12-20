#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AugnosBlog.Enums;
using Microsoft.AspNetCore.Identity;

namespace AugnosBlog.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int BlogId { get; set; }
        public string AuthorId { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be at least {2}, and at most {1} characters.", MinimumLength = 2)]
        public string PostTitle { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be at least {2}, and at most {1} characters.", MinimumLength = 2)]
        public string Abstract { get; set; }

        [Required]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Updated Date")]
        public DateTime? UpdatedAt { get; set; }

        public ReadyStatus ReadyStatus {get;set;}
        public string Slug { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        // Navigation Properties
        public virtual Blog Blog { get; set; }
        public virtual IdentityUser Author { get; set; }
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}