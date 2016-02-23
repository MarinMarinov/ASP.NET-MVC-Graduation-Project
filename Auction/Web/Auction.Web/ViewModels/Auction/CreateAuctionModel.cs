namespace Auction.Web.ViewModels.Auction
{
    using Infrastructure.Mapping;
    using Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CreateAuctionModel : IMapFrom<Auction>, IMapTo<Auction> //, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Item Title")]
        public string ItemTitle { get; set; }

        [Required]
        [DisplayName("Initial Price")]
        public int InitialPrice { get; set; }

        [Required]
        [DisplayName("Bid step")]
        public int BidStep { get; set; } 

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