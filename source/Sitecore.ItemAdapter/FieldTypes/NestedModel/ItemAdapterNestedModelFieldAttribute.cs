using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter.FieldTypes.NestedModel
{
    public abstract class ItemAdapterNestedModelFieldAttribute : ItemAdapterFieldAttribute
    {
        public Type NestedModelType { get; private set; }
 
        protected IItemAdapter Adapter { get; set; } 

        protected ItemAdapterNestedModelFieldAttribute(string fieldId, Type nestedModelType) : base(fieldId)
        {
            NestedModelType = nestedModelType;
        }

        public void InitItemAdapter(IItemAdapter itemAdapter)
        {
            Adapter = itemAdapter;
        }

        protected virtual IItemAdapterModel GetModel(Item item, Type propertyType)
        {
            return Adapter.GetModel(item);
        }
        
        public override bool CheckType(Type propertyType)
        {
            if (propertyType.GetInterfaces().Any(i => i == ExpectedInterface())
                && propertyType == ExpectedType())
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
            return NestedModelType;
        }

        public virtual Type ExpectedInterface()
        {
            return typeof(IItemAdapterModel);
        }


    }
}
