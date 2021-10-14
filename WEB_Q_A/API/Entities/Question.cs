using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{

    public class Question
    {
        [Key]
        public int Id { get; set; }
    
        public String QuestionDetails { get; set; }

        public DateTime DatePosted { get; set; }

        [Required]
        [StringLength(20), ErrorMessage = "No User Id length is more than 20 characters!"]
        public int UserId { get; set; }
       
        public ApplicationUser User { get; set; }

    }
}
