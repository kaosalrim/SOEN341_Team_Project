using System;
using System.ComponentModel.DataAnnotations;


namespace API.Entities
{
    public Answer
    {
        [Key]
        public int Id { get; set; }
        
        public String AnswerDetails { get; set; }
        
        public DateTime DatePosted { get; set; }
        
        [Required]
        public int QuestionId { get; set; }
        
        [Required]
        public int UserId { get; set; }

        public virtual Question Question { get; set; }
       
        public virtual AppUser User  { get; set; }
    }
}
