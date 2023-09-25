using System.ComponentModel.DataAnnotations;

namespace BlazorWallAreaCalculator.Data
{
    public class Project
    {
        [Required]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "User Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        [StringLength(50, ErrorMessage = "'User Email' has a maximum length of 50 characters.")]
        public string UserEmail { get; set; } = String.Empty;

        [Required(ErrorMessage = "Project Name is required")]
        [StringLength(50, ErrorMessage = "'Project Name' has a maximum length of 50 characters.")]
        public string ProjectName { get; set; } = String.Empty;
    }
}