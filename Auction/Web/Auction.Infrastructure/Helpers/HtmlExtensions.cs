namespace Auction.Infrastructure.Helpers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static MvcHtmlString Submit(this HtmlHelper helper, object htmlAttributes = null)
        {
            TagBuilder input = new TagBuilder("Input");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
            input.MergeAttributes(attributes);
            input.Attributes.Add("type", "submit");

            return new MvcHtmlString(input.ToString());
        }
    }
}