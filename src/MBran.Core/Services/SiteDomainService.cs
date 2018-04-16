using System;
using System.Globalization;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MBran.Core.Services
{
    public class SiteDomainService : ISiteDomainService
    {
        private readonly UmbracoHelper _umbracoHelper;

        public SiteDomainService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public IPublishedContent GetDomainRoot()
        {
            var domainId = GetDomainRootId();
            return domainId == 0 ? null : _umbracoHelper.TypedContent(domainId);
        }

        public int GetDomainRootId()
        {
            var domain = GetCurrentDomain();

            return domain
                       ?.RootContentId.GetValueOrDefault()
                   ?? _umbracoHelper.TypedContentAtRoot().FirstOrDefault()?.Id
                   ?? 0;
        }

        public IDomain GetCurrentDomain()
        {
            var domainService = ApplicationContext.Current.Services.DomainService;
            return domainService
                       ?.GetAll(true)
                       .FirstOrDefault(domain => domain.DomainName.Equals(GetDomainName(),
                           StringComparison.InvariantCultureIgnoreCase));
        }

        public CultureInfo GetSiteCulture()
        {
            var domain = GetCurrentDomain();
            var localizationService = ApplicationContext.Current.Services.LocalizationService;
            if (domain?.LanguageId == null)
            {
                return CultureInfo.CurrentCulture;
            }

            return localizationService.GetLanguageById((int)domain?.LanguageId).CultureInfo;
        }

        public string GetDomainName()
        {
            return HttpContext.Current.Request.Url.Host;
        }
    }
}
