using MBran.Components.Extensions;
using MBran.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;

namespace MBran.Core.Services
{
    public class DocTypeService : IDocTypeService
    {
        private readonly IContentTypeService _contentTypeService;
        public DocTypeService(IContentTypeService contentTypeService)
        {
            _contentTypeService = contentTypeService;
        }

        public DocTypeDefinition GetDocTypeDefinition(string docTypeAlias)
        {
            var cacheName = string.Join("_", GetType().FullName, nameof(GetDocTypeDefinition), docTypeAlias);

            return (DocTypeDefinition)ApplicationContext.Current
                .ApplicationCache
                .RequestCache
                .GetCacheItem(cacheName, () =>
                {
                    var allDocTypes = _contentTypeService.GetAllContentTypes();
                    return allDocTypes
                        .Where(docType =>
                            string.Equals(docType.Alias, docTypeAlias, StringComparison.InvariantCultureIgnoreCase))
                        .Select(docType => new DocTypeDefinition
                        {
                            Id = docType.Id,
                            Name = docType.Name,
                            Value = docType.Alias
                        })
                        .FirstOrDefault();
                });
        }

        public IEnumerable<DocTypeDefinition> GetDocTypes()
        {
            var cacheName = string.Join("_", GetType().FullName, nameof(GetDocTypes));

            return (IEnumerable<DocTypeDefinition>)ApplicationContext.Current
                .ApplicationCache
                .RequestCache
                .GetCacheItem(cacheName, () =>
                {
                    var docTypes = _contentTypeService.GetAllContentTypes();
                    return docTypes.Select(docType => new DocTypeDefinition
                    {
                        Id = docType.Id,
                        Name = docType.Name,
                        Value = docType.Alias
                    });
                });
        }

        public IEnumerable<DocTypeDefinition> GetDocTypesDefinition(IEnumerable<string> docTypeAliases)
        {
            if (!docTypeAliases?.Any() ?? true) return new List<DocTypeDefinition>();

            return docTypeAliases.Select(docType => GetDocTypeDefinition(docType));
        }

        public Type GetStronglyTypedPoco(string modelTypeFullName)
        {
            return !string.IsNullOrWhiteSpace(modelTypeFullName)
                ? AppDomain.CurrentDomain.FindImplementation(modelTypeFullName)
                : null;
        }

        public Type GetStronglyTypedPocoByName(string typeName)
        {
            return !string.IsNullOrWhiteSpace(typeName)
                ? AppDomain.CurrentDomain.FindImplementations(typeName).FirstOrDefault()
                : null;
        }

        public Type GetStronglyTypedPublishedContent(string docTypeAlias)
        {
            return AppDomain.CurrentDomain
                .FindImplementations<PublishedContentModel>()
                .FirstOrDefault(model => model.Name.Equals(docTypeAlias, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
