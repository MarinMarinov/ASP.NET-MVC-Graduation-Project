namespace Auction.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class SeedData
    {
        private Random random = new Random();

        private PasswordHasher hasher = new PasswordHasher();

        private List<Auction> Auctions = new List<Auction>();

        private List<Item> Items = new List<Item>();

        private List<User> Users = new List<User>();

        public void SeedAdminRole(AuctionDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var role = new IdentityRole { Name = "Admin" };

            manager.Create(role);
        }

        public void SeedUsers(AuctionDbContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);

            User admin = new User
            {
                UserName = "admin@mail.bg",
                Email = "admin@mail.bg",
                FirstName = "Tosho",
                LastName = "Ivanov",
                PhoneNumber = "+3591234569",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = this.hasher.HashPassword("1"),
            };

            User pesho = new User
            {
                UserName = "pesho@mail.bg",
                Email = "pesho@mail.bg",
                FirstName = "Pesho",
                LastName = "Goshov",
                PhoneNumber = "+3591234567",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = this.hasher.HashPassword("1"),
            };

            User gosho = new User
            {
                UserName = "gosho@mail.bg",
                Email = "gosho@mail.bg",
                FirstName = "Gosho",
                LastName = "Toshov",
                PhoneNumber = "+3591234568",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = this.hasher.HashPassword("1"),
            };

            this.Users.Add(pesho);
            this.Users.Add(gosho);
            this.Users.Add(admin);
            context.Users.AddOrUpdate(this.Users.ToArray());
            context.SaveChanges();

            var adminId = context.Users.FirstOrDefault(u => u.UserName == "admin@mail.bg").Id;
            manager.AddToRole(adminId, "Admin");
            context.SaveChanges();
        }

        public void SeedItems(AuctionDbContext context)
        {
            Array enumValuesArray = Enum.GetValues(typeof(ItemType));

            for (var i = 0; i < 10; i++)
            {
                var rand = this.random.Next(0, 7);
                var item = new Item
                {
                    Title = string.Format("Item {0}", i),
                    Author = string.Format("Pablo{0} Picasso{1}", i, i),
                    Description = "Description" + i,
                    Type = (ItemType)enumValuesArray.GetValue(rand)
                };

                this.Items.Add(item);
            }

            context.Items.AddOrUpdate(this.Items.ToArray());
            context.SaveChanges();
        }

        public void SeedAuctions(AuctionDbContext context)
        {
            for (var i = 0; i < 10; i++)
            {
                var randUser = this.random.Next(0, 3);
                var items = new List<Item> { this.Items[i] };
                var bidders = this.Users;

                var auction = new Auction
                {
                    Name = "Auction " + i,
                    DateOfAuction = DateTime.UtcNow.AddHours(24 + (24 * i)),
                    Active = false,
                    InitialPrice = 10000 + 1000 * i,
                    BidStep = 500,
                    Items = items,
                    //Creator = this.Users[randUser],
                    Bidders = bidders
                };
                this.Auctions.Add(auction);
            }

            context.Auctions.AddOrUpdate(this.Auctions.ToArray());
            context.SaveChanges();
        }
    }
}