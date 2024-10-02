using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Library_Managment.CustomHelpers
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Custom helper to create an input field with optional attributes.
        /// </summary>
        /// <param name="helper">HtmlHelper instance</param>
        /// <param name="labelText">Label for the input</param>
        /// <param name="inputType">Type of the input (text, password, email, textarea, etc.)</param>
        /// <param name="htmlAttributes">Additional HTML attributes</param>
        /// <returns>MvcHtmlString with combined label and input field</returns>
        public static MvcHtmlString Description(this HtmlHelper helper, string labelText, string inputType = "text", object htmlAttributes = null)
        {
            // Convert the htmlAttributes object to a RouteValueDictionary
            var attributes = new RouteValueDictionary(htmlAttributes);

            // Create a <label> tag
            TagBuilder labelTag = new TagBuilder("label");
            labelTag.SetInnerText(labelText);

            // Create an <input> or <textarea> tag based on the inputType parameter
            TagBuilder inputTag = inputType.ToLower() == "textarea" ? new TagBuilder("textarea") : new TagBuilder("input");

            // Set the input type attribute for <input> elements (not applicable for <textarea>)
            if (inputType.ToLower() != "textarea")
            {
                inputTag.Attributes["type"] = inputType; // e.g., "text", "password", "email"
            }

            // Merge the additional attributes into the input or textarea tag
            inputTag.MergeAttributes(attributes);

            // Combine the label and input/textarea tags
            string finalHtml = labelTag.ToString() + inputTag.ToString();

            // Return the combined HTML string
            return MvcHtmlString.Create(finalHtml);
        }
    }
}
