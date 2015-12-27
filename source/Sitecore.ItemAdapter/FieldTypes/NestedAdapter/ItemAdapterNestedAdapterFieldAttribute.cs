using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter.FieldTypes.NestedAdapter
{
    public abstract class ItemAdapterNestedAdapterFieldAttribute : ItemAdapterFieldAttribute
    {
        protected Type AdapterType { get; private set; }
 
        protected IItemAdapter Adapter { get; set; } 

        protected ItemAdapterNestedAdapterFieldAttribute(string fieldId, Type adapterType) : base(fieldId)
        {
            AdapterType = adapterType;
        }

        public void InitItemAdapter(Type itemAdapter)
        {
            Adapter = (IItemAdapter)Activator.CreateInstance(itemAdapter.MakeGenericType(AdapterType));
        }

        protected virtual IItemAdapterModel GetModel(Item item, Type propertyType)
        {
            return Adapter.GetModel(item);
        }
        
        public override bool CheckType(Type propertyType)
        {
            if (propertyType == typeof(IItemAdapterModel))
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
            return typeof(ItemAdapterModel);
        }


    }
}
