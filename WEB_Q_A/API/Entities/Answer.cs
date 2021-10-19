using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Answers")]
    public class Answer
    {
        public int Id { get; set; }
        public int Rank { get; set; } = 0;
        public bool IsBestAnswer { get; set; } = false;
        public int VoteCount { get; set; } = 0;
        public string AnswerDetails { get; set; }        
        public DateTime DatePosted { get; set; } = DateTime.Now;       
        public Question Question { get; set; }
        public int QuestionId { get; set; }       
        public AppUser AppUser  { get; set; }
        public int AppUserId { get; set; }
    }
}
