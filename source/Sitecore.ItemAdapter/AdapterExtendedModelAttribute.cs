using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemAdapter
{
    using Sitecore.Data;

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class AdapterExtendedModelAttribute : AdapterItemModelAttribute
    {
        public AdapterExtendedModelAttribute()
        {
        }

        public AdapterExtendedModelAttribute(string templateId) : base(templateId)
        {
        }
    }
}
