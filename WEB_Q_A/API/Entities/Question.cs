using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Questions")]
    public class Question
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }    
        public string QuestionDetails { get; set; }
        public int Rank { get; set; } = 0;
        public Boolean BestQuestion { get; set; } = false;
        public int VoteCount { get; set; } = 0;
        public int Views { get; set; } = 0;
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public ICollection<Answer> Answers { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}
