using System;
using System.Collections.Generic;
using System.Linq;

namespace Chirp.Razor
{
    public static class DbInitializer
    {
        public static void SeedDatabase(ChatDbContext chirpContext)
        {
            if (!(chirpContext.Authors.Any() && chirpContext.Cheeps.Any()))
            {
                var a1 = new Author()
                {
                    Id = 1,
                    Name = "Roger Histand",
                    Email = "Roger+Histand@hotmail.com",
                    Cheeps = new List<Cheep>()
                };
                var a2 = new Author()
                {
                    Id = 2,
                    Name = "Luanna Muro",
                    Email = "Luanna-Muro@ku.dk",
                    Cheeps = new List<Cheep>()
                };
                var a3 = new Author()
                {
                    Id = 3,
                    Name = "Wendell Ballan",
                    Email = "Wendell-Ballan@gmail.com",
                    Cheeps = new List<Cheep>()
                };
                var a4 = new Author()
                {
                    Id = 4,
                    Name = "Nathan Sirmon",
                    Email = "Nathan+Sirmon@dtu.dk",
                    Cheeps = new List<Cheep>()
                };
                var a5 = new Author()
                {
                    Id = 5,
                    Name = "Quintin Sitts",
                    Email = "Quintin+Sitts@itu.dk",
                    Cheeps = new List<Cheep>()
                };
                var a6 = new Author()
                {
                    Id = 6,
                    Name = "Mellie Yost",
                    Email = "Mellie+Yost@ku.dk",
                    Cheeps = new List<Cheep>()
                };
                var a7 = new Author()
                {
                    Id = 7,
                    Name = "Malcolm Janski",
                    Email = "Malcolm-Janski@gmail.com",
                    Cheeps = new List<Cheep>()
                };
                var a8 = new Author()
                {
                    Id = 8,
                    Name = "Octavio Wagganer",
                    Email = "Octavio.Wagganer@dtu.dk",
                    Cheeps = new List<Cheep>()
                };
                var a9 = new Author()
                {
                    Id = 9,
                    Name = "Johnnie Calixto",
                    Email = "Johnnie+Calixto@itu.dk",
                    Cheeps = new List<Cheep>()
                };
                var a10 = new Author()
                {
                    Id = 10,
                    Name = "Jacqualine Gilcoine",
                    Email = "Jacqualine.Gilcoine@gmail.com",
                    Cheeps = new List<Cheep>()
                };
                var a11 = new Author()
                {
                    Id = 11,
                    Name = "Helge",
                    Email = "ropf@itu.dk",
                    Cheeps = new List<Cheep>()
                };
                var a12 = new Author()
                {
                    Id = 12,
                    Name = "Adrian",
                    Email = "adho@itu.dk",
                    Cheeps = new List<Cheep>()
                };

                var authors = new List<Author>() { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12 };

                var c1 = new Cheep()
                {
                    CheepId = 1,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "They were married in Chicago, with old Smith, and was expected aboard every day; meantime, the two went past me.",
                    Timestamp = DateTime.Parse("2023-08-01 13:14:37")
                };
                var c2 = new Cheep()
                {
                    CheepId = 2,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "And then, as he listened to all that''s left o'' twenty-one people.",
                    Timestamp = DateTime.Parse("2023-08-01 13:15:21")
                };
                var c3 = new Cheep()
                {
                    CheepId = 3,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "In various enchanted attitudes, like the Sperm Whale.",
                    Timestamp = DateTime.Parse("2023-08-01 13:14:58")
                };
                var c4 = new Cheep()
                {
                    CheepId = 4,
                    AuthorId = a5.Id,
                    Author = a5,
                    Text = "Unless we succeed in establishing ourselves in some monomaniac way whatever significance might lurk in them.",
                    Timestamp = DateTime.Parse("2023-08-01 13:16:00")
                };
                var c5 = new Cheep()
                {
                    CheepId = 5,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "At last we came back!",
                    Timestamp = DateTime.Parse("2023-08-01 13:17:00")
                };
                var c6 = new Cheep()
                {
                    CheepId = 6,
                    AuthorId = a3.Id,
                    Author = a3,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:18:00")
                };
                var c7 = new Cheep()
                {
                    CheepId = 7,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "The whale was now within a few yards of the ship.",
                    Timestamp = DateTime.Parse("2023-08-01 13:19:00")
                };
                var c8 = new Cheep()
                {
                    CheepId = 8,
                    AuthorId = a2.Id,
                    Author = a2,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:20:00")
                };
                var c9 = new Cheep()
                {
                    CheepId = 9,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "The folk on trust in me!",
                    Timestamp = DateTime.Parse("2023-08-01 13:21:00")
                };
                var c10 = new Cheep()
                {
                    CheepId = 10,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:22:00")
                };
                var c11 = new Cheep()
                {
                    CheepId = 11,
                    AuthorId = a4.Id,
                    Author = a4,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:23:00")
                };
                var c12 = new Cheep()
                {
                    CheepId = 12,
                    AuthorId = a5.Id,
                    Author = a5,
                    Text = "What did they take?",
                    Timestamp = DateTime.Parse("2023-08-01 13:24:00")
                };
                var c13 = new Cheep()
                {
                    CheepId = 13,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:25:00")
                };
                var c14 = new Cheep()
                {
                    CheepId = 14,
                    AuthorId = a1.Id,
                    Author = a1,
                    Text = "You are here for at all?",
                    Timestamp = DateTime.Parse("2023-08-01 13:26:00")
                };
                var c15 = new Cheep()
                {
                    CheepId = 15,
                    AuthorId = a5.Id,
                    Author = a5,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:27:00")
                };
                var c16 = new Cheep()
                {
                    CheepId = 16,
                    AuthorId = a1.Id,
                    Author = a1,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:28:00")
                };
                var c17 = new Cheep()
                {
                    CheepId = 17,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:29:00")
                };
                var c18 = new Cheep()
                {
                    CheepId = 18,
                    AuthorId = a5.Id,
                    Author = a5,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:30:00")
                };
                var c19 = new Cheep()
                {
                    CheepId = 19,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:31:00")
                };
                var c20 = new Cheep()
                {
                    CheepId = 20,
                    AuthorId = a1.Id,
                    Author = a1,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:32:00")
                };
                var c21 = new Cheep()
                {
                    CheepId = 21,
                    AuthorId = a10.Id,
                    Author = a10,
                    Text = "The ship was now kept away from the sea.",
                    Timestamp = DateTime.Parse("2023-08-01 13:33:00")
                };

                chirpContext.Authors.AddRange(authors);
                chirpContext.Cheeps.AddRange(new List<Cheep> { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21 });
                chirpContext.SaveChanges();
            }
        }
    }
}