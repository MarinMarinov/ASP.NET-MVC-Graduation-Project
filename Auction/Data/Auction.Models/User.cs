namespace Auction.Models
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser, IBaseModel<string>
    {
        private ICollection<Auction> auctions;

        public User()
        {
            this.auctions = new HashSet<Auction>();
            this.CreatedOn = DateTime.UtcNow;
        }

        // TODO: Server side validations!!!
        public string FirstName { get; set; }

        public string LastName { get; set; }

        // TODO: validate only picture formats
        public string AvatarFileName { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Auction> Auctions
        {
            get { return this.auctions; }
            set { this.auctions = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
