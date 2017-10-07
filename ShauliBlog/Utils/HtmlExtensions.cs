using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShauliBlog.Utils
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString DisplayImageFor<T, TValue>(this HtmlHelper<T> helper, Expression<System.Func<T, TValue>> prop, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(prop, helper.ViewData);

            string value = (string)metadata.Model;

            return new MvcHtmlString("<img src='" + value + "' />");
        }

        public static MvcHtmlString DisplayVideoFor<T, TValue>(this HtmlHelper<T> helper, Expression<System.Func<T, TValue>> prop, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(prop, helper.ViewData);
            string value = (string)metadata.Model;

            var dataStart = "<video controls = \"controls\" >";
            var dataNotSupported = "Your browser does not support the video tag.";
            var dataEnd = "</video>";

            string mediaType = "";

            if (value != null)
            {
                string[] splittedData = value.Split('.');

                mediaType = splittedData[splittedData.Length - 1];
            }
          
            var video = "<source src=\"" + value + "\" type = \"video/" + mediaType + "\">";
            return new MvcHtmlString(dataStart + video + dataNotSupported + dataEnd);
        }

        public static MvcHtmlString FileFor<T, TValue>(this HtmlHelper<T> helper, Expression<System.Func<T, TValue>> prop, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(prop, helper.ViewData);

            RouteValueDictionary dic = new RouteValueDictionary(htmlAttributes);

            return new MvcHtmlString("<input type=\"file\" name=\"" + metadata.PropertyName + "\" id=\"" + metadata.PropertyName + "\">");
        }
    }
}