using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemAdapter
{
    using Sitecore.Data;

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ItemAdapterExtendedModelAttribute : ItemAdapterModelAttribute
    {
        public ItemAdapterExtendedModelAttribute(Type childModelType) : this(null, childModelType)
        {
        }

        public ItemAdapterExtendedModelAttribute(string templateId, Type childModelType) : base(templateId, childModelType)
        {
        }
    }
}
