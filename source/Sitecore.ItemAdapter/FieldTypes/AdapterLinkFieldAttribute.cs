using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Extensions;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter.FieldTypes
{
    public class AdapterLinkFieldAttribute : AdapterItemModelFieldAttribute
    {
        public AdapterLinkFieldAttribute(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            Item link = item.GetInternalLinkItem(FieldId);
            if (link == null)
            {
                return null;
            }
            return base.GetModelValue(link, propertyType);
        }
        
    }
}
