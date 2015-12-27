using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Sitecore.ItemAdapter.FieldTypes.NestedAdapter;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter
{
    using System.Reflection;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.ItemAdapter.FieldTypes;

    public interface IItemAdapter
    {
        IItemAdapterModel GetModel(Item item);
        //object GetExtendedModel(Item item);
        //void GetChildren(object model, Item item);
        //IEnumerable<object> GetEnumerator(Data.Items.Item[] items);
    }

    public class StandardItemAdapter<TModel> : IItemAdapter
        where TModel : IItemAdapterModel, new()
    {
        private StandardItemAdapter()
        {
        }
        
        public static StandardItemAdapter<TModel> CreateAdapterInstance()
        {
            return new StandardItemAdapter<TModel>();
        }

        public IItemAdapterModel GetModel(Item item)
        {
            return GetNewModel(item);
        }
        
        private static readonly ItemAdapterModelAttribute _modelAttribute;
        private static readonly ItemAdapterModelProperty[] _properties;
        private static readonly ItemAdapterModelProperty[] _extendedProperties;

        private static readonly IItemAdapter _childItemAdapter;


        static StandardItemAdapter()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(TModel), false); 
            _modelAttribute = attrs.FirstOrDefault((attr) => attr is ItemAdapterModelAttribute) as ItemAdapterModelAttribute;
            if (_modelAttribute == null)
            {
                throw new Exception("Invalid StandardItemAdapter<TModel> model type. Implementing class must declare [AdapterItemModel] attribute.", null);
            }

            if (_modelAttribute.ChildType == null)
            {
                throw new Exception("Invalid StandardItemAdapter<TModel> model type. Implementing class must declare [AdapterItemModel] attribute with Child Type.", null);
            }
            _childItemAdapter =
                GetChildItemAdapter();


            var modelProperties = typeof (TModel).GetProperties();

            var standardFieldProperties = modelProperties.Where(
                (prop) => Attribute.IsDefined(prop, typeof (ItemAdapterFieldAttribute), true)
                          && !Attribute.IsDefined(prop, typeof (ItemAdapterExtendedPropertyAttribute), true));

            var extendedFieldProperties = modelProperties.Where(
                (prop) => Attribute.IsDefined(prop, typeof (ItemAdapterFieldAttribute), true)
                          && Attribute.IsDefined(prop, typeof (ItemAdapterExtendedPropertyAttribute), true));

            _properties = standardFieldProperties.Select((prop) => new ItemAdapterModelProperty(
                prop, 
                Attribute.GetCustomAttribute(prop, typeof(ItemAdapterFieldAttribute)) as ItemAdapterFieldAttribute)).ToArray();

            _extendedProperties = extendedFieldProperties.Select((prop) => new ItemAdapterModelProperty(
                prop, 
                Attribute.GetCustomAttribute(prop, typeof(ItemAdapterFieldAttribute)) as ItemAdapterFieldAttribute)).ToArray();

            CheckProperties(_properties);
            CheckProperties(_extendedProperties);

            LoadPropertyNestedItemAdapters(_properties);
            LoadPropertyNestedItemAdapters(_extendedProperties);

        }

        private static IItemAdapter GetChildItemAdapter()
        {
            return (IItemAdapter)
                Activator.CreateInstance(typeof(StandardItemAdapter<>).MakeGenericType(_modelAttribute.ChildType));
        }

        private static void LoadPropertyNestedItemAdapters(ItemAdapterModelProperty[] properties)
        {
            foreach (ItemAdapterModelProperty property in properties)
            {
                ItemAdapterNestedAdapterFieldAttribute attribute = (ItemAdapterNestedAdapterFieldAttribute)property.FieldModelAttribute;
                if (attribute != null)
                {
                    attribute.InitItemAdapter(typeof (StandardItemAdapter<>));
                }
            }
        }

        private static void CheckProperties(ItemAdapterModelProperty[] propertyList)
        {
            foreach (ItemAdapterModelProperty property in propertyList)
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

        public static TModel GetNewModel(Item item)
        {
            if (_modelAttribute.TemplateId != (ID)null && !_modelAttribute.TemplateId.Equals(item.TemplateID))
            {
                return default(TModel);
            }

            TModel result = new TModel();
            result.SetId(item.ID.ToGuid());

            SetStandardProperties(result, item);

            foreach (ItemAdapterModelProperty property in _properties)
            {
                object fieldValue = property.FieldModelAttribute.GetFieldValue(
                    item,
                    property.PropertyInfo.PropertyType);

                property.PropertyInfo.SetValue(result, fieldValue, null); 
            }
            return result;
        }

        public static TModel GetExtendedModel(Item item)
        {
            TModel model = GetNewModel(item);
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

            List<IItemAdapterModel> modelChildren = new List<IItemAdapterModel>();
            Item[] itemChildren = item.GetChildren().ToArray();
            foreach (Item child in itemChildren)
            {
                modelChildren.Add(_childItemAdapter.GetModel(child));
            }

            result.Children = modelChildren;
        }


        private static void SetStandardProperties(TModel result, Item item)
        {
            result.Name = item.Name;
            result.TemplateId = item.TemplateID.ToGuid();
            result.TemplateName = item.TemplateName;
            result.DisplayName = item.Appearance.DisplayName;
        }

        private static void SetExtendedProperties(TModel result, Item item)
        {
            if (result == null)
            {
                return;
            }

            foreach (ItemAdapterModelProperty property in _extendedProperties)
            {
                property.PropertyInfo.SetValue(result, property.FieldModelAttribute.GetFieldValue(item, property.PropertyInfo.PropertyType), null);
            }
        }

        public static IEnumerable<TModel> GetEnumerator(Data.Items.Item[] items)
        {
            return items.Select(GetNewModel);
        }

    }

}
