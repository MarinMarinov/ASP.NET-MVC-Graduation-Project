namespace Auction.Web.ViewModels.User
{
    using System;
    using System.Linq.Expressions;
    using Models;

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
                    AvatarFileName = user.AvatarFileName,
                    PhoneNumber = user.PhoneNumber
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarFileName { get; set; }

        public string PhoneNumber { get; set; }
    }
}