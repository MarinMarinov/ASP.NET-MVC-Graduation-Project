using System;
using System.Collections.Generic;

namespace Auction.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System.Data.Entity.Migrations;

    public class SeedData
    {
        public AuctionDbContext db = new AuctionDbContext();

        private Random random = new Random();

        private PasswordHasher hasher = new PasswordHasher();

        private List<Auction> Auctions = new List<Auction>();

        private List<Item> Items = new List<Item>();

        private List<User> Users = new List<User>();

        public SeedData()
        {
            User pesho = new User
            {
                UserName = "pesho@mail.bg",
                Email = "pesho@mail.bg",
                FirstName = "Pesho",
                LastName = "Goshov",
                PhoneNumber = "+3591234567",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = this.hasher.HashPassword("1")
            };

            User gosho = new User
            {
                UserName = "gosho@mail.bg",
                Email = "gosho@mail.bg",
                FirstName = "Gosho",
                LastName = "Toshov",
                PhoneNumber = "+3591234568",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = this.hasher.HashPassword("1")
            };

            User tosho = new User
            {
                UserName = "tosho@mail.bg",
                Email = "tosho@mail.bg",
                FirstName = "Tosho",
                LastName = "Ivanov",
                PhoneNumber = "+3591234569",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = this.hasher.HashPassword("1")
            };

            this.Users.Add(pesho);
            this.Users.Add(gosho);
            this.Users.Add(tosho);
            this.db.Users.AddOrUpdate(this.Users.ToArray());

            Array enumValuesArray = Enum.GetValues(typeof (ItemType));

            for (var i = 0; i < 10; i++)
            {
                var rand = this.random.Next(0, 7);
                var item = new Item
                {
                    Title = string.Format("Item {0}", i ),
                    Author = string.Format("Pablo{0} Picasso{1}", i, i),
                    Description = "Description" + i,
                    Type = (ItemType)enumValuesArray.GetValue(rand)
                };
                this.Items.Add(item);
            }
            this.db.Items.AddOrUpdate(this.Items.ToArray());

            for (var i = 0; i < 10; i++)
            {
                var randUser = this.random.Next(0, 3);
                var auction = new Auction
                {
                    Name = "Auction " + i,
                    DateOfCreation = DateTime.UtcNow,
                    DateOfAuction = DateTime.UtcNow.AddHours(24 + (24 * i)),
                    Item = this.Items[i],
                    Creator = this.Users[randUser]
                };
                this.Auctions.Add(auction);
            }
            this.db.Auctions.AddOrUpdate(this.Auctions.ToArray());
        }
    }
}