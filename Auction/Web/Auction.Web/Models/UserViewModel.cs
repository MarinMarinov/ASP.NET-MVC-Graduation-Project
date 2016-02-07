using System;

namespace Auction.Web.Models
{
    using System.Linq.Expressions;
    using Auction.Models;

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
                    PhoneNumber = user.PhoneNumber
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }
    }
}