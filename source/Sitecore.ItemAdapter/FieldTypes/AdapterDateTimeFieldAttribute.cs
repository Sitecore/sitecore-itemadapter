using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Extensions;

namespace Sitecore.ItemAdapter.FieldTypes
{
    public class AdapterDateTimeFieldAttribute : AdapterTextFieldAttribute
    {
        public AdapterDateTimeFieldAttribute(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            return item.GetDateTimeFieldValue(FieldId);
        }

        public override bool CheckType(Type propertyType)
        {
            if (propertyType.IsEquivalentTo(typeof(DateTime))
               || (propertyType.IsGenericType
                   && typeof(DateTime) == propertyType.GetGenericArguments().FirstOrDefault()))
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
            return typeof(Nullable<DateTime>);
        }

    }
}
