using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Extensions;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter.FieldTypes.NestedAdapter
{
    public class ItemAdapterLinkFieldAttribute : ItemAdapterNestedAdapterFieldAttribute
    {
        public ItemAdapterLinkFieldAttribute(string fieldId, Type modelType) : base(fieldId, modelType)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            Item link = item.GetInternalLinkItem(FieldId);
            if (link == null)
            {
                return null;
            }
            return GetModel(link, propertyType);

        }
        
    }
}
