namespace Chirp.Razor
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    

        public class Follower
        {
            [Key]
            public int Id { get; set; } // Primary key for the follower record

            [ForeignKey("FollowerAuthor")]
            public int FollowerId { get; set; } // The ID of the user who is following
            public Author FollowerAuthor { get; set; } // Navigation property to the follower

            [ForeignKey("FolloweeAuthor")]
            public int FolloweeId { get; set; } // The ID of the user being followed
            public Author FolloweeAuthor { get; set; } // Navigation property to the followee
        }

    }
