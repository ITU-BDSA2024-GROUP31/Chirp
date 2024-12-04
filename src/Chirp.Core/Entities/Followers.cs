namespace Chirp.Razor
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Chirp.Core.Entities;

    public class Follower
    {
        [Key]
        public int Id { get; set; } // Primary key for the follower record

        [ForeignKey("FollowerAuthor")]
        public int FollowerId { get; set; } // The ID of the user who is following
        public Author FollowerAuthor { get; set; } = null!; // Navigation property to the follower

        [ForeignKey("FolloweeAuthor")]
        public int FolloweeId { get; set; } // The ID of the user being followed
        public Author FolloweeAuthor { get; set; } = null!; // Navigation property to the followee
    }
}