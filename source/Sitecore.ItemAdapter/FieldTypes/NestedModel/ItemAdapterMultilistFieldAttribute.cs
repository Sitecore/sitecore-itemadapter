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
        public ItemAdapterMultilistFieldAttribute(string fieldId, Type nestedModelType) : base(fieldId, nestedModelType)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            Item[] items = item.GetListItems(FieldId).ToArray();
            List<IItemAdapterModel> result = items.Select(
                (child) => GetModel(child, propertyType)).ToList();
            if (propertyType.IsArray)
            {
                return result.ToArray();
            }
            return result;
        }

        public override bool CheckType(Type propertyType)
        {
            if (propertyType.IsArray)
            {
                var expectedTypeArray = ExpectedInterface().MakeArrayType();
                if (propertyType == expectedTypeArray)
                {
                    return true;
                }
                return false;
            }
            if (propertyType.IsGenericType)
            {
                var genericUnderlyingType = propertyType.GetGenericArguments().FirstOrDefault();
                if (genericUnderlyingType != null
                    && (genericUnderlyingType == ExpectedInterface() 
                        || genericUnderlyingType.GetInterfaces().Any(i => i == ExpectedInterface()))
                )
                {
                    return true;
                }
                return false;
            }

            //if (propertyType.IsEnum)
            //{
            //    var enumType = propertyType.GetEnumUnderlyingType();
            //    if ((enumType == ExpectedInterface() || enumType.IsSubclassOf(ExpectedInterface()))
            //         // && enumType.GetInterfaces().Any(i => i == ExpectedInterface())
            //    )
            //    {
            //        return true;
            //    }
            //    return false;
            //}

            return false;
        }

        internal override object SetFieldValue(Item item, Type propertyType, object propertyValue)
        {
            throw new NotImplementedException();
        }
    }
}
