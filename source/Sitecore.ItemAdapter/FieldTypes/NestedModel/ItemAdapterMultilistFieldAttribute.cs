using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Extensions;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter.FieldTypes.NestedModel
{
    public class ItemAdapterMultilistFieldAttribute : ItemAdapterNestedModelFieldAttribute
    {
        public ItemAdapterMultilistFieldAttribute(string fieldId, Type modelType) : base(fieldId, modelType)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            Item[] items = item.GetListItems(FieldId).ToArray();
            List<IItemAdapterModel> result = items.Select(
                (child) => GetModel(child, propertyType)).ToList();
            return result;
        }

        public override bool CheckType(Type propertyType)
        {
            if (propertyType.Equals(ExpectedType())
                || (propertyType.IsGenericType
                   && propertyType.IsEnum
                   && typeof(IItemAdapterModel) == propertyType.GetGenericArguments().FirstOrDefault()))
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
            return typeof(List<IItemAdapterModel>);
        }
    }
}
