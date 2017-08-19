using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HtmlHelpers.Models;
namespace HtmlHelpers.Helpers
{
    public static class ListHelpers
    {
        public static MvcHtmlString CreateList(this HtmlHelper html,IEnumerable<Book> items)
        {

            //Свойство InnerHtml позволяет установить или получить содержимое тега в виде строки
            /*Метод MergeAttribute (string, string, bool) позволяет добавить
             *  к элементу один атрибут. 
             *  Для получения всех атрибутов можно использовать
             *   коллекцию Attributes
             *   Метод SetInnerText(string) устанавливает текстовое
             *    содержимое внутри элемента
             *    Метод AddCssClass(sting)
             *     добавляет класс css к элементу
             * */


            //создаем элемент ul
            TagBuilder ul = new TagBuilder("ul");
            //-------------добавляем элементы li в ul-----------------
            foreach(var item in items)
            {
                
                TagBuilder li = new TagBuilder("li");
                //Свойство Element.innerHTML устанавливает или получает разметку дочерних элементов
                li.SetInnerText(item.NameBook);
                ul.InnerHtml += li.ToString();
                li.SetInnerText(item.Author);
                ul.InnerHtml += li.ToString();
                li.SetInnerText(item.Year.ToString());
                ul.InnerHtml += li.ToString();
                li.SetInnerText(item.Price.ToString());
                //внутреннее наполнение в ui
                ul.InnerHtml += li.ToString();

            }
            //------------------------------
            return new MvcHtmlString(ul.ToString());
        }



        /*------------------------Helper для создания пагинации---------------------------------
         * public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int Currentpage { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems/ItemsPerPage);
            }
        }
    }
         * 
         *  public static MvcHtmlString PageLings(this HtmlHelper html,
                                               PageInfo pageinfo,
                                               Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for(int i=1;i<=pageinfo.TotalPages;i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if(i==pageinfo.Currentpage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");

                }

                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());

                
            }

            return MvcHtmlString.Create(result.ToString());

        }

          @Html.PageLings(Model.PageInfo,x=>Url.Action("List",new {page=x,category=Model.CurrentCategory }))
         * 
         * //----------------------------------------------------------------------------
         * 
         * --------------помощник изображения HTML. Помощник Image реализован внутри с помощью TagBuilder, который представляет тег HTML <img>.-----------------------
         *  public static class ImageHelper
    {
        public static string Image(this HtmlHelper helper, string id, string url, string alternateText)
        {
            return Image(helper, id, url, alternateText, null);
        }

        public static string Image(this HtmlHelper helper, string id, string url, string alternateText, object htmlAttributes)
        {
            // Create tag builder
            var builder = new TagBuilder("img");
            
            // Create valid id
            builder.GenerateId(id);

            // Add attributes
            builder.MergeAttribute("src", url);
            builder.MergeAttribute("alt", alternateText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return builder.ToString(TagRenderMode.SelfClosing);
        }

         <!-- Calling helper without HTML attributes -->
    <%= Html.Image("img1", ResolveUrl("~/Content/XBox.jpg"), "XBox Console") %>

    <!-- Calling helper with HTML attributes -->
    <%= Html.Image("img1", ResolveUrl("~/Content/XBox.jpg"), "XBox Console", new {border="4px"})%>
    }


        //---------------------------------------------------------------------------------------------------


         * 
         * */


    }
}