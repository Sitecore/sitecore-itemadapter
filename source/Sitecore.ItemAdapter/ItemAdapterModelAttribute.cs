using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemAdapter
{
    using Sitecore.Data;

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ItemAdapterModelAttribute : Attribute
    {
        private readonly ID _templateId;

        private readonly Type _childType;
        
        public ID TemplateId { get { return _templateId; } }

        public Type ChildType { get { return _childType; } }

        public ItemAdapterModelAttribute(Type childModelType) : this(string.Empty, childModelType)
        {
        }

        //public ItemAdapterModelAttribute(string templateId) : this(templateId, null)
        //{
        //}

        public ItemAdapterModelAttribute(string templateId, Type childModelType) : base()
        {
            if (!string.IsNullOrEmpty(templateId))
            {
                _templateId = new ID(new Guid(templateId));
            }
            else
            {
                _templateId = new ID(Guid.Empty);
            }
            _childType = childModelType;
        }

    }
}
