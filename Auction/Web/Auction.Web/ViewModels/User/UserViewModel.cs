﻿namespace Auction.Web.ViewModels.User
{
    using System;
    using System.Linq.Expressions;

    using global::Auction.Models;

    public class UserViewModel
    {
        public static Expression<Func<User, UserViewModel>> FromUser
        {
            get
            {
                return user => new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AvatarLink = user.AvatarLink,
                    PhoneNumber = user.PhoneNumber
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarLink { get; set; }

        public string PhoneNumber { get; set; }
    }
}