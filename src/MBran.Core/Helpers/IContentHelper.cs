using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace MBran.Core.Helpers
{
    public interface IContentHelper
    {
        IEnumerable<T> GetDescendants<T>(int startId) where T : class, IPublishedContent;
        IEnumerable<T> GetDescendantsOrSelf<T>(int startId) where T : class, IPublishedContent;
        T GetDescendant<T>(int startId) where T : class, IPublishedContent;
        T GetDescendantOrSelf<T>(int startId) where T : class, IPublishedContent;
        IEnumerable<T> GetAncestors<T>(int startId) where T : class, IPublishedContent;
        IEnumerable<T> GetAncestorsOrSelf<T>(int startId) where T : class, IPublishedContent;
        T GetAncestor<T>(int startId) where T : class, IPublishedContent;
        T GetAncestorOrSelf<T>(int startId) where T : class, IPublishedContent;
        T GetContent<T>(int nodeId) where T : PublishedContentModel;
        IPublishedContent GetRoot();
        int CurrentPageId();
        IPublishedContent CurrentPage();
        T CurrentPage<T>() where T : PublishedContentModel;
    }
}
