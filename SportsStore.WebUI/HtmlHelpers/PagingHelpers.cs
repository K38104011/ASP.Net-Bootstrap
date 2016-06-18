using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.WebUI.Models;
using System.Web.Mvc;
using System.Text;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPage; i++)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pageInfo.CurrentPage)
                    tag.AddCssClass("selected");
                li.InnerHtml += tag;
                result.Append(li.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}