namespace Auction.Web.Controllers
{
    using Auction.Services.Web;
    using AutoMapper;
    using Infrastructure.Mapping;
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}