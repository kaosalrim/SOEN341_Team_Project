using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class QuestionCreateDto
    {
        [Required]
        public string QuestionTitle { get; set; }
        [Required]
        public string QuestionDetails { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public int AppUserId { get; set; }
    }
}