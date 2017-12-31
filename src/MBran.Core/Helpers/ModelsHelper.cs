using MBran.Core.Extensions;
using System;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;

namespace MBran.Core.Helpers
{
    public class ModelsHelper
    {
        private static Lazy<ModelsHelper> _helper => new Lazy<ModelsHelper>(() => new ModelsHelper());
        public static ModelsHelper Instance => _helper.Value;

        private ModelsHelper()
        {

        }

        public Type StronglyTyped(string docTypeAlias)
        {
            return AppDomain.CurrentDomain.FindImplementations<PublishedContentModel>()
                .Where(model => model.Name.Equals(docTypeAlias, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
        }
    }
}
