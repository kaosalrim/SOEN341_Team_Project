using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AnswerUpdateDto
    {
        public bool IsBestAnswer { get; set; }  
        [Required]    
        public string AnswerDetails { get; set; } 
    }
}