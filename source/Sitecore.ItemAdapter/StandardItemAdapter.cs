using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter
{
    using System.Reflection;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.ItemAdapter.FieldTypes;
    
    public static class StandardItemAdapter<TModel> 
        where TModel : AdapterItem, new()
    {
        private readonly static AdapterItemModelAttribute _modelAttribute;
        private readonly static AdapterItemModelProperty[] _properties;
        private readonly static AdapterItemModelProperty[] _extendedProperties;

        static StandardItemAdapter()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(TModel), false); 
            _modelAttribute = attrs.FirstOrDefault((attr) => attr is AdapterItemModelAttribute) as AdapterItemModelAttribute;
            if (_modelAttribute == null)
            {
                throw new Exception("Invalid StandardItemAdapter<TModel> model type. Implementing class must declare [AdapterItem] attribute.", null);
            }

            _properties = typeof(TModel).GetProperties().Where(
                    (prop) => Attribute.IsDefined(prop, typeof(AdapterFieldAttribute), true) 
                        && !Attribute.IsDefined(prop, typeof(AdapterExtendedPropertyAttribute), true)
                ).Select( 
                    (prop) => new AdapterItemModelProperty(
                        prop, 
                        Attribute.GetCustomAttribute(prop, typeof(AdapterFieldAttribute)) as AdapterFieldAttribute)
                ).ToArray();

            _extendedProperties = typeof(TModel).GetProperties().Where(
                    (prop) => Attribute.IsDefined(prop, typeof(AdapterFieldAttribute), true)
                        && Attribute.IsDefined(prop, typeof(AdapterExtendedPropertyAttribute), true)
                ).Select(
                    (prop) => new AdapterItemModelProperty(
                        prop,
                        Attribute.GetCustomAttribute(prop, typeof(AdapterFieldAttribute)) as AdapterFieldAttribute)
                ).ToArray();



            foreach (AdapterItemModelProperty property in _properties)
            {
                if (!property.FieldModelAttribute.CheckType(property.PropertyInfo.PropertyType))
                {
                    throw new Exception(string.Format(
                        "Invalid Adapter Property Attribute - Model: {0}, Property: {1}, Type: {2}, Expected Type: {3}", 
                        typeof(TModel).FullName,
                        property.PropertyInfo.Name,
                        property.PropertyInfo.PropertyType.FullName,
                        property.FieldModelAttribute.ExpectedType().FullName
                        ));
                }

            }

        }

        

        public static TModel GetModel(Item item)
        {
            if (_modelAttribute.TemplateId != (ID)null && !_modelAttribute.TemplateId.Equals(item.TemplateID))
            {
                return default(TModel);
            }

            TModel result = new TModel();

            SetStandardProperties(result, item);

            foreach (AdapterItemModelProperty property in _properties)
            {
                property.PropertyInfo.SetValue(result, 
                    property.FieldModelAttribute.GetFieldValue(
                        item, 
                        property.PropertyInfo.PropertyType), 
                    null); 
            }
            return result;
        }
        public static TModel GetExtendedModel(Item item)
        {
            TModel model = GetModel(item);
            GetExtendedModel(model, item);
            GetChildren(model, item);
            return model;
        }

        public static void GetExtendedModel(TModel model, Item item)
        {
            SetExtendedProperties(model, item);
        }

        public static void GetChildren(TModel model, Item item)
        {
            SetChildrenProperty(model, item);
        }

        private static void SetChildrenProperty(TModel result, Item item)
        {
            if (result == null)
            {
                return;
            }

            List<TModel> modelChildren = new List<TModel>();
            Item[] itemChildren = item.GetChildren().ToArray();
            foreach (Item child in itemChildren)
            {
                modelChildren.Add(GetModel(item));
            }

            result.Children = modelChildren;
        }

        private static void SetExtendedProperties(TModel result, Item item)
        {
            if (result == null)
            {
                return;
            }

            foreach (AdapterItemModelProperty property in _extendedProperties)
            {
                property.PropertyInfo.SetValue(result, property.FieldModelAttribute.GetFieldValue(item, property.PropertyInfo.PropertyType), null);
            }
        }

        private static void SetStandardProperties(TModel result, Item item)
        {
            result.Id = item.ID.ToGuid();
            result.Name = item.Name;
            result.TemplateId = item.TemplateID.ToGuid();
            result.TemplateName = item.TemplateName;
            result.DisplayName = item.Appearance.DisplayName;
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
