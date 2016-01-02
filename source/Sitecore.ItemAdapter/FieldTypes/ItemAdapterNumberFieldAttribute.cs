using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Extensions;

namespace Sitecore.ItemAdapter.FieldTypes
{
    public class ItemAdapterNumberFieldAttribute : ItemAdapterFieldAttribute
    {
        public ItemAdapterNumberFieldAttribute(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            return new Nullable<Decimal>(item.GetDecimalFieldValue(FieldId));
        }

        public override bool CheckType(Type propertyType)
        {
            if (propertyType.IsEquivalentTo(typeof(Decimal))
                || propertyType.Equals(ExpectedType())
                || (propertyType.IsGenericType
                   && typeof(Decimal) == propertyType.GetGenericArguments().FirstOrDefault()))
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
            return typeof (Nullable<Decimal>);
        }

        internal override object SetFieldValue(Item item, Type propertyType, object propertyValue)
        {
            throw new NotImplementedException();
        }
    }
}
