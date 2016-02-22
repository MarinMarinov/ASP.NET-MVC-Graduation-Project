namespace Auction.Web.ViewModels.Item
{
    using System;
    using System.Collections.Generic;

    using Infrastructure.Mapping;
    using global::Auction.Models;

    public class ItemViewModel : IMapFrom<Item>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ItemType Type { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}