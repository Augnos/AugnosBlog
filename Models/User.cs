#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AugnosBlog.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2}, and at most {1} characters.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2}, and at most {1} characters.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2}, and at most {1} characters.", MinimumLength = 2)]
        public string TwitterUrl { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2}, and at most {1} characters.", MinimumLength = 2)]
        public string InstagramUrl { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2}, and at most {1} characters.", MinimumLength = 2)]
        public string GitHubUrl { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2}, and at most {1} characters.", MinimumLength = 2)]
        public string LinkedInUrl { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        // Navigation Properties
        public virtual ICollection<Blog> Blogs {get;set;} = new HashSet<Blog>();
        public virtual ICollection<Post> Posts {get;set;} = new HashSet<Post>();
    }
}