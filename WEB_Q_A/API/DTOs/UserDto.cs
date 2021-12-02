using System;
using System.Collections.Generic;
using API.Classes;

namespace API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public MemberSince MemberSince { get; set; }
        public DateTime LastActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public int QuestionsAnswered { get; set; } = 0;
        public int QuestionsPosted { get; set; } = 0;
        public ICollection<UserVoteDto> UserVotes {get; set;}
    }
}