using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }  
        [Required]
        public string QuestionTitle { get; set; } 
        [Required] 
        public string QuestionDetails { get; set; }
        public DateTime DatePosted { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }
        [Required]
        public string Username { get; set; }
        public bool HasBestAnswer { get; set; }
    }
}
