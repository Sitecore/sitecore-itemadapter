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
    public class AdapterMultilistFieldAttribute : AdapterItemModelFieldAttribute
    {
        public AdapterMultilistFieldAttribute(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            Item[] items = item.GetListItems(FieldId).ToArray();
            List<AdapterItem> result = items.Select(
                (child) => (AdapterItem) base.GetModelValue(child, typeof (AdapterItem))).ToList();
            return result;
        }

        public override bool CheckType(Type propertyType)
        {
            if (propertyType.Equals(ExpectedType())
                || (propertyType.IsGenericType
                   && propertyType.IsEnum
                   && typeof(AdapterItem) == propertyType.GetGenericArguments().FirstOrDefault()))
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
            return typeof(List<AdapterItem>);
        }
    }
}
