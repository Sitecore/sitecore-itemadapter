using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter.FieldTypes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public abstract class ItemAdapterFieldAttribute : Attribute
    {
        private readonly ID _fieldId;

        protected ID FieldId
        {
            get { return _fieldId; }
        }

        protected ItemAdapterFieldAttribute(string fieldId)
        {
            _fieldId = new ID(new Guid(fieldId));
        }

        protected abstract object GetValue(Item item, Type propertyType);

        internal object GetFieldValue(Item item, Type propertyType)
        {
            return GetValue(item, propertyType);
        }
        
        public abstract bool CheckType(Type propertyType);

        public abstract Type ExpectedType();

    }

    

}