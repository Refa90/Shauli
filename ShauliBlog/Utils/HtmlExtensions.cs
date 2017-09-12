using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShauliBlog.Utils
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper html, byte[] image)
        {
            var img = string.Format("data:image/jpg:base64, {0}", Convert.ToBase64String(image));
            return new MvcHtmlString("<img src='" + img + "' />");
        }

        
        public static MvcHtmlString ImageFor<T, TValue>(this HtmlHelper<T> helper,
                                           Expression<System.Func<T, TValue>> prop, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(prop, helper.ViewData);
            byte[] value = (byte[])metadata.Model;
            
            var img = string.Format("data:image/jpg:base64, {0}", Convert.ToBase64String(value));
            return new MvcHtmlString("<img src='" + img + "' />");
        }   

        public static MvcHtmlString VideoFor<T, TValue>(this HtmlHelper<T> helper,
                                           Expression<System.Func<T, TValue>> prop, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(prop, helper.ViewData);
            byte[] value = (byte[])metadata.Model;

            var dataStart = "<video controls = \"controls\" >";
            var dataNotSupported = "Your browser does not support the video tag.";
            var dataEnd = "</video>";
            var video = string.Format("data:video;base64, {0}", Convert.ToBase64String(value));
            return new MvcHtmlString(dataStart + video + dataNotSupported + dataEnd);
        }

        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var builder = new TagBuilder("input");

            var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            builder.GenerateId(id);
            builder.MergeAttribute("name", id);
            builder.MergeAttribute("type", "file");

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}