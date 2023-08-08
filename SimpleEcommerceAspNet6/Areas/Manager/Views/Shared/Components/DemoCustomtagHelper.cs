using Microsoft.AspNetCore.Razor.TagHelpers;
using SimpleEcommerceAspNet6.Data;
using SimpleEcommerceAspNet6.Models;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SimpleEcommerceAspNet6.Areas.Manager.Views.Shared.Components
{
    [HtmlTargetElement("DemoCustomtag")]
    public class DemoCustomtagHelper : TagHelper
    {
       

        public List<Category> Categories { get; set; }
        public string ListTitle { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            output.TagName = "ul";
            output.TagMode= TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class","DemoCustomtag");
            output.PreElement.AppendHtml($"<h2>{ListTitle }</h2>");

            
            
            var sb = new StringBuilder();

            foreach (var category in Categories)
            {
                sb.Append("<li>");
                sb.Append($@" <a href=""/Manager/ManageProducts?CategoryId={category.CategoryId}"">{category.CategoryName}</a>");
              
                sb.Append("</li>");
            }

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
