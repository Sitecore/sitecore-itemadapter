using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.ItemAdapter.Extensions;
using Sitecore.Pipelines.PasswordRecovery;

namespace Sitecore.ItemAdapter.FieldTypes
{

    public class ItemAdapterTextFieldAttribute : ItemAdapterFieldAttribute
    {
        public ItemAdapterTextFieldAttribute(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            if (typeof(string) == propertyType)
            {
                return item.GetTextContent(FieldId);
            }
            else
            {
                return null;
            }
        }

        public override bool CheckType(Type propertyType)
        {
            Type expected = ExpectedType();
            if (propertyType == expected)
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
            return typeof (string);
        }
    }
    

    
}
