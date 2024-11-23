namespace Chirp.Razor
{
    public class Follower
    {
        public int FollowerId { get; set; }
        public Author FollowerAuthor { get; set; } = null!;

        public int FolloweeId { get; set; }
        public Author FolloweeAuthor { get; set; } = null!;
    }
}