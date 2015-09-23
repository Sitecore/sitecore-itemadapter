using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemAdapter
{
    using Sitecore.Data;

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class SitecoreItemModelAttribute : Attribute
    {
        private readonly ID _templateId;

        public ID TemplateId { get { return _templateId; } }

        public SitecoreItemModelAttribute()
        {
        }

        public SitecoreItemModelAttribute(string templateId) : base()
        {
            _templateId = new ID(new Guid(templateId));
        }
    }
}
