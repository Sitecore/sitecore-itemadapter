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
        public Type ModelType { get; private set; }
 
        protected IItemAdapter Adapter { get; set; } 

        protected ItemAdapterNestedModelFieldAttribute(string fieldId, Type modelType) : base(fieldId)
        {
            ModelType = modelType;
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
            return typeof(IItemAdapterModel);
        }


    }
}
