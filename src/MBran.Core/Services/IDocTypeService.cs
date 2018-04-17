using System;
using System.Collections.Generic;
using MBran.Core.Models;

namespace MBran.Core.Services
{
    public interface IDocTypeService
    {
        Type GetStronglyTypedPublishedContent(string docTypeAlias);
        Type GetStronglyTypedPoco(string modelTypeFullName);
        Type GetStronglyTypedPocoByName(string typeName);
        IEnumerable<DocTypeDefinition> GetDocTypes();
        IEnumerable<DocTypeDefinition> GetDocTypesDefinition(IEnumerable<string> docTypeAliases);
        DocTypeDefinition GetDocTypeDefinition(string docTypeAlias);
    }
}