using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Extensions;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter.FieldTypes.NestedModel
{
    public class ItemAdapterLinkFieldAttribute : ItemAdapterNestedModelFieldAttribute
    {
        public ItemAdapterLinkFieldAttribute(string fieldId, Type nestedModelType) : base(fieldId, nestedModelType)
        {
        }

        protected override object GetValue(Item item, Type propertyType, int depth)
        {
            Item link = item.GetInternalLinkItem(FieldId);
            if (link == null)
            {
                return null;
            }
            return GetModel(link, propertyType, depth);

        }

        protected override object SetValue(Item item, Type propertyType, object propertyValue)
        {
            throw new NotImplementedException();
        }
    }
}
