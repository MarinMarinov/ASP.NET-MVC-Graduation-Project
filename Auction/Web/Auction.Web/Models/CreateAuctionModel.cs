namespace Auction.Web.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Auction.Infrastructure.Mapping;
    using Auction.Models;

    using AutoMapper;

    public class CreateAuctionModel : IMapTo<Auction> //, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Item Title")]
        public string ItemTitle { get; set; }

        [Required]
        [DisplayName("Auction Date and Time")]
        public DateTime DateOfAuction { get; set; }


        /* this.dataAuction.Add(new Auction
                        {
                            Name = auction.Name,
                            Items = items,
                            DateOfAuction = auction.DateOfAuction,
                            Creator = currentUser,
                        });*/
        /* public void CreateMappings(IConfiguration config)
                {
                    config.CreateMap<Comment, CommentViewModel>()
                        .ForMember(pr => pr.Author, opts => opts.MapFrom(pm => pm.Author.UserName));
                }*/

        /*public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Auction, CreateAuctionModel>()
                .ForMember(
                    createModelProperty => createModelProperty.ItemTitle,
                    opts => opts.MapFrom(mod => mod.Items.FirstOrDefault(item => item.Title == this.ItemTitle)));
        }*/

    }
}