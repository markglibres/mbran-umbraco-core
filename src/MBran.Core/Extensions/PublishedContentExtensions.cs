using System;
using System.Collections.Generic;
using System.Linq;
using MBran.Core.Helpers;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

namespace MBran.Core.Extensions
{
    public static class PublishedContentExtensions
    {
        public static IEnumerable<T> MapEnumerable<T>(this IEnumerable<IPublishedContent> content)
            where T : class
        {
            if (content != null)
                return content
                    .Select(c => c.As<T>())
                    .Where(c => c != null);
            return new List<T>();
        }

        public static Type StronglyTyped(this IPublishedContent content)
        {
            var docTypeAlias = content.GetDocumentTypeAlias();
            return ModelsHelper.Instance.StronglyTypedPublishedContent(docTypeAlias);
        }

        public static string GetDocumentTypeAlias(this IPublishedContent content)
        {
            var docType = content.DocumentTypeAlias;
            return char.ToUpperInvariant(docType[0]) + docType.Substring(1);
        }
    }
}