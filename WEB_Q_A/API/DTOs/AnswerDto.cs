using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public bool IsBestAnswer { get; set; }  
        [Required]    
        public string AnswerDetails { get; set; }        
        public DateTime DatePosted { get; set; }  
        [Required]    
        public int QuestionId { get; set; }
        [Required] 
        public string Username { get; set; }
    }
}