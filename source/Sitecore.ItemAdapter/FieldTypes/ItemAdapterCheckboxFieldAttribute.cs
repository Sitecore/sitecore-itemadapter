using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Extensions;

namespace Sitecore.ItemAdapter.FieldTypes
{
    public class ItemAdapterCheckboxFieldAttribute : ItemAdapterTextFieldAttribute
    {
        public ItemAdapterCheckboxFieldAttribute(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            return new Nullable<bool>(item.GetCheckBoxFieldValue(FieldId));
        }

        public override bool CheckType(Type propertyType)
        {
            if (propertyType.IsEquivalentTo(typeof(bool))
                || propertyType.Equals(ExpectedType())
                || (propertyType.IsGenericType
                   && typeof(bool) == propertyType.GetGenericArguments().FirstOrDefault()))
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
            return typeof(Nullable<bool>);
        }
    }
}
