using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class QuestionDetailsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string QuestionDetails { get; set; }
    }
}
