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
    public class ItemAdapterNameValueListFieldAttribute : ItemAdapterFieldAttribute
    {
        public ItemAdapterNameValueListFieldAttribute(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            SortedList<string,string> result = item.GetNameValueList(FieldId);
            return result;
        }

        public override bool CheckType(Type propertyType)
        {
            if (propertyType == ExpectedType())
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
            return typeof(SortedList<string, string>);
        }
    }
}
