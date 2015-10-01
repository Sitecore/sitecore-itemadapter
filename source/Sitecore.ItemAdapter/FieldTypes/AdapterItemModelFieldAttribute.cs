using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemAdapter.Extensions;

namespace Sitecore.ItemAdapter.FieldTypes
{
   
    public abstract class AdapterItemModelFieldAttribute : AdapterModelFieldAttribute
    {
        protected AdapterItemModelFieldAttribute(string fieldId) : base(fieldId)
        {

        }

        protected virtual object GetModelValue(Item item, Type propertyType)
        {
            AdapterItem result = GetModelValue<AdapterItem>(item, propertyType);
            return result;
        }
        
        public override TModel GetModelValue<TModel>(Item item, Type propertyType)
        {
            return StandardItemAdapter<TModel>.GetModel(item);
        }

        public override bool CheckType(Type propertyType)
        {
            if (propertyType == typeof(AdapterItem))
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
            return typeof(AdapterItem);
        }

    }
}
