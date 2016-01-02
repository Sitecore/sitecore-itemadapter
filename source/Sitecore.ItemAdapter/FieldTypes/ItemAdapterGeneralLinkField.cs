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
    public class ItemAdapterGeneralLinkField : ItemAdapterFieldAttribute
    {
        public ItemAdapterGeneralLinkField(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            return item.GetGeneralLinkFieldValue(FieldId);
        }

        public override bool CheckType(Type propertyType)
        {
            if (propertyType == typeof(IGeneralLink))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Type ExpectedType()
        {
            return typeof(IGeneralLink);
        }

        internal override object SetFieldValue(Item item, Type propertyType, object propertyValue)
        {
            throw new NotImplementedException();
        }
    }
}
