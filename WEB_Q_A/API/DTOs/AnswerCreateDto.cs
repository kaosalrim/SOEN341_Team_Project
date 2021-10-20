using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AnswerCreateDto
    { 
        [Required]    
        public string AnswerDetails { get; set; } 
        [Required]    
        public int QuestionId { get; set; }
        [Required] 
        public string Username { get; set; }
        [Required]
        public int AppUserId { get; set; }
    }
}