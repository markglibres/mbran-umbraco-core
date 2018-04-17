using System.Globalization;
using Umbraco.Core.Models;

namespace MBran.Core.Services
{
    public interface ISiteDomainService
    {
        int GetDomainRootId();
        IPublishedContent GetDomainRoot();
        IDomain GetCurrentDomain();
        CultureInfo GetSiteCulture();
        string GetDomainName();
    }
}