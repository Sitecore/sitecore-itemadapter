using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Extensions;

namespace Sitecore.ItemAdapter.FieldTypes
{
    public class ItemAdapterRichTextFieldAttribute : ItemAdapterTextFieldAttribute
    {
        public ItemAdapterRichTextFieldAttribute(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            return item.GetRichTextContent(FieldId);
        }
    }

}
