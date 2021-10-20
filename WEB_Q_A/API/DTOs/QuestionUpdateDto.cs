using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class QuestionUpdateDto
    {
        [Required]
        public string QuestionTitle { get; set; }
        [Required]
        public string QuestionDetails { get; set; }
    }
}