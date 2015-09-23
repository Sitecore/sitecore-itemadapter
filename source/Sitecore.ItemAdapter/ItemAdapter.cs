using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemAdapter
{
    using System.Reflection;
    using Sitecore.Data;
    using Sitecore.ItemAdapter.FieldTypes;


    

    public static class ItemAdapter<TModel> 
        where TModel : new()
    {
        private readonly static SitecoreItemModelAttribute _modelAttribute;
        private readonly static SitecoreItemModelProperty[] _properties;

        static ItemAdapter()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(TModel), false); 
            _modelAttribute = attrs.FirstOrDefault((attr) => attr is SitecoreItemModelAttribute) as SitecoreItemModelAttribute;
            if (_modelAttribute == null)
            {
                throw new Exception("Invalid ItemAdapter<TModel> model type. Implementing class must declare [StandardItem] modelAttribute.", null);
            }

            _properties = typeof(TModel).GetProperties().Where(
                    (prop) => Attribute.IsDefined(prop, typeof(SitecoreFieldModelAttribute), true)
                ).Select( 
                    (prop) => new SitecoreItemModelProperty(
                        prop, 
                        Attribute.GetCustomAttribute(prop, typeof(SitecoreFieldModelAttribute)) as SitecoreFieldModelAttribute)
                ).ToArray();

            //TODO: check types of properties against the types of the attributes

        }

        public static TModel GetModel(Sitecore.Data.Items.Item item)
        {
            if (_modelAttribute.TemplateId != ID.Null && !_modelAttribute.TemplateId.Equals(item.TemplateID))
            {
                return default(TModel);
            }

            TModel result = new TModel();
            foreach (SitecoreItemModelProperty property in _properties)
            {
                property.PropertyInfo.SetValue(result, property.FieldModelAttribute.GetFieldValue(item, property.PropertyInfo.PropertyType), null); 
            }
            return result;
        }

        public static IEnumerable<TModel> GetEnumerator(Data.Items.Item[] items)
        {
            return items.Select(GetModel);
        }
        
    }


    public static class ListItemAdapter<TModel> 
    {
        
    }

    

}
