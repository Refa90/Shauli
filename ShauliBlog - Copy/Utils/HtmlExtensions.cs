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
                                           Expression<System.Func<T, TValue>> prop)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(prop, helper.ViewData);
            byte[] value = (byte[])metadata.Model;


            var img = string.Format("data:image/jpg:base64, {0}", Convert.ToBase64String(value));
            return new MvcHtmlString("<img src='" + img + "' />");
        }

        public static MvcHtmlString VideoFor<T, TValue>(this HtmlHelper<T> helper,
                                           Expression<System.Func<T, TValue>> prop)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(prop, helper.ViewData);
            byte[] value = (byte[])metadata.Model;

            var dataStart = "<video controls = \"controls\" >";
            var dataNotSupported = "Your browser does not support the video tag.";
            var dataEnd = "</video>";
            var video = string.Format("data:video;base64, {0}", Convert.ToBase64String(value));
            return new MvcHtmlString(dataStart + video + dataNotSupported + dataEnd);
        }


        //<video controls = "controls" >
        //                    < source src="~/Content/images/shauli.mp4" type="video/mp4" />
        //                    Your browser does not support the video tag.
        //                </video>



        //public MvcHtmlString Video(string name, IEnumerable<sourcelistitem> sourceList, ObjectType objectType, string objectSource, object htmlAttributes)
        //{
        //    TagBuilder tagBuilder = new TagBuilder("video");
        //    if (htmlAttributes != null)
        //    {
        //        RouteValueDictionary routeValueDictionary = new RouteValueDictionary(htmlAttributes);
        //        tagBuilder.MergeAttributes(routeValueDictionary);
        //    }

        //    tagBuilder.MergeAttribute("id", name);
        //    StringBuilder sourceItemBuilder = new StringBuilder();
        //    sourceItemBuilder.AppendLine();
        //    foreach (var sourceItem in sourceList)
        //    {
        //        sourceItemBuilder.AppendLine(SourceItemToSource(sourceItem));
        //    }
        //    sourceItemBuilder.AppendLine();
        //    if (objectType == ObjectType.Flash)
        //    {
        //        sourceItemBuilder.AppendLine(CreateFlashObject
        //        (objectSource, htmlAttributes));
        //    }
        //    else
        //    {
        //        sourceItemBuilder.AppendLine(CreateSilverlightObject(sourceList, objectSource, htmlAttributes));
        //    }

        //    tagBuilder.InnerHtml = sourceItemBuilder.ToString();
        //    sourceItemBuilder.AppendLine();
        //    return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        //}

    }
}