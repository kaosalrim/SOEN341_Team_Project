using System;
using System.ComponentModel.DataAnnotations;


namespace API.Entities
{
    public class AnswerModel
    {
        [Key]
        public int Id { get; set; }
        
        public String AnswerDetails { get; set; }
        
        public DateTime DatePosted { get; set; }
        
        [Required]
        public int QuestionId { get; set; }
        
        [Required]
        public int UserId { get; set; }

        public QuestionModel Question { get; set; }
       
        public ApplicationUser User  { get; set; }
    }
}
