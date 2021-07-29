using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using Quad_Shop.Models;
using System.Text;

namespace Quad_Shop.HtmlpageHelpers
{
    public static class htmlpagehelper
    {
        public static MvcHtmlString PageLinks (this HtmlHelper html,
                                                Pageinfo pageinfo,
                                                Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 1; i <= pageinfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if( i == pageinfo.CurrentPage)
                {
                    tag.AddCssClass("current_page_link");
                }
                else
                {
                    tag.AddCssClass("other_page_link");
                }
                
                result.Append(tag.ToString());
                
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}