namespace Auction.Web.ViewModels.User
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using global::Auction.Infrastructure.Mapping;

    using Models;

    public class UserViewModel : IMapFrom<User>
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

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string AvatarFileName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}