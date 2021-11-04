namespace API.Entities
{
    public class UserVotes
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public Answer Answer { get; set; }
        public int AnswerId { get; set; }
    }
}