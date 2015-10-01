using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Extensions;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter.FieldTypes
{
    public class AdapterNameLookupListFieldAttribute : AdapterItemModelFieldAttribute
    {
        public AdapterNameLookupListFieldAttribute(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            SortedList<string,Item> items = item.GetNameLookupList(FieldId);
            SortedList<string,AdapterItem> result = new SortedList<string, AdapterItem>();
            foreach (var kvp in items)
            {
                result.Add(kvp.Key, (AdapterItem)base.GetModelValue(kvp.Value, typeof (AdapterItem)));
            }
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
            return typeof(SortedList<string, AdapterItem>);
        }
    }
}
