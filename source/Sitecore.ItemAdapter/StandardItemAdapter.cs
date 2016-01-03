using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Sitecore.ItemAdapter.FieldTypes.NestedModel;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter
{
    using System.Reflection;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.ItemAdapter.FieldTypes;

    
    internal static class StandardItemAdapter
    {
        public static IItemAdapter CreateInstance(Type modelType)
        {
            return (IItemAdapter) Activator.CreateInstance(typeof(StandardItemAdapter<>).MakeGenericType(modelType));
        }
    }

    public class StandardItemAdapter<TModel> : IItemAdapter
        where TModel : IItemAdapterModel, new()
    {
        public StandardItemAdapter()
        {
        }

        public StandardItemAdapter(int depth)
        {
            _depth = depth;
        }

        private readonly int  _depth = 1;
        
        public static StandardItemAdapter<TModel> CreateAdapterInstance()
        {
            return new StandardItemAdapter<TModel>();
        }
        
        public IItemAdapterModel LoadModel(Item item)
        {
            return LoadModel(item, _depth);
        }

        public IItemAdapterModel LoadModel(Item item, int depth)
        {
            if (depth < 0)
            {
                TModel result = new TModel();
                result.Initialize(item.ID.ToGuid());
            }
            return CreateModelInstance(item, depth);
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
            _childItemAdapter = CreateChildItemAdapter();
            
            var modelProperties = typeof (TModel).GetProperties();

            var standardFieldProperties = GetStandardFieldProperties(modelProperties);
            _properties = standardFieldProperties;

            var extendedFieldProperties = GetExtendedFieldProperties(modelProperties);
            _extendedProperties = extendedFieldProperties;

            CheckProperties(_properties);
            CheckProperties(_extendedProperties);

            LoadPropertyNestedItemAdapters(_properties);
            LoadPropertyNestedItemAdapters(_extendedProperties);

        }
        public static TModel CreateModelInstance(Item item, int depth)
        {
            if (_modelAttribute.TemplateId != (ID)null && !_modelAttribute.TemplateId.Equals(item.TemplateID))
            {
                return default(TModel);
            }

            TModel model = new TModel();
            model.Initialize(item.ID.ToGuid());

            SetStandardProperties(model, item);

            SetFieldProperties(item, depth, model);

            model.Loaded();
            return model;
        }


        public static TModel CreateExtendedModelInstance(Item item, int depth)
        {
            TModel model = CreateModelInstance(item, depth);
            SetExtendedModel(model, item, depth);
            SetModelChildren(model, item, depth);
            return model;
        }

        public static void SetExtendedModel(TModel model, Item item, int depth)
        {
            SetExtendedProperties(model, item, depth);
            model.Loaded(true);
        }

        public static void SetModelChildren(TModel model, Item item, int depth)
        {
            SetChildrenProperty(model, item, depth);
        }

        public static void SaveModel(TModel model, Item item)
        {
            item.Editing.BeginEdit();
            UpdateFieldValues(model, item);
            item.Editing.EndEdit();
        }


        public static IEnumerable<TModel> GetEnumerator(Data.Items.Item[] items, int depth)
        {
            return items.Select(c =>
            {
                int d = depth;
                return CreateModelInstance(c, depth);
            });
        }

        private static IItemAdapter CreateChildItemAdapter()
        {
            return StandardItemAdapter.CreateInstance(_modelAttribute.ChildType);
        }


        private static void SetFieldProperties(Item item, int depth, TModel result)
        {
            foreach (ItemAdapterModelProperty property in _properties)
            {
                object fieldValue = property.FieldModelAttribute.GetFieldValue(
                    item,
                    property.PropertyInfo.PropertyType,
                    depth);

                property.PropertyInfo.SetValue(result, fieldValue, null);
            }
        }


        private static ItemAdapterModelProperty[] GetExtendedFieldProperties(PropertyInfo[] modelProperties)
        {
            var extendedFieldAttributes = modelProperties.Where(
                (prop) => Attribute.IsDefined(prop, typeof (ItemAdapterFieldAttribute), true)
                          && Attribute.IsDefined(prop, typeof (ItemAdapterExtendedPropertyAttribute), true));
            var extendedFieldProperties = extendedFieldAttributes.Select(
                (prop) => new ItemAdapterModelProperty(
                    prop,
                    Attribute.GetCustomAttribute(prop, typeof (ItemAdapterFieldAttribute)) as ItemAdapterFieldAttribute)).ToArray();
            return extendedFieldProperties;
        }

        private static ItemAdapterModelProperty[] GetStandardFieldProperties(PropertyInfo[] modelProperties)
        {
            var standardFieldAttributes = modelProperties.Where(
                (prop) => Attribute.IsDefined(prop, typeof (ItemAdapterFieldAttribute), true)
                          && !Attribute.IsDefined(prop, typeof (ItemAdapterExtendedPropertyAttribute), true));
            var standardFieldProperties = standardFieldAttributes.Select(
                (prop) => new ItemAdapterModelProperty(
                    prop,
                    Attribute.GetCustomAttribute(prop, typeof (ItemAdapterFieldAttribute)) as ItemAdapterFieldAttribute)).ToArray();
            return standardFieldProperties;
        }

        private static void LoadPropertyNestedItemAdapters(ItemAdapterModelProperty[] properties)
        {
            var modelProperties = properties.Where(p => p.FieldModelAttribute is ItemAdapterNestedModelFieldAttribute);
            foreach (ItemAdapterModelProperty property in modelProperties)
            {
                ItemAdapterNestedModelFieldAttribute attribute = (ItemAdapterNestedModelFieldAttribute)property.FieldModelAttribute;
                if (attribute != null)
                {
                    attribute.InitItemAdapter(StandardItemAdapter.CreateInstance(attribute.NestedModelType));
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
                        "Invalid Adapter Property Attribute - Model: {0}, Model Type: {1}, Property Type: {2}, Expected Type: {3}",
                        typeof(TModel).FullName,
                        property.PropertyInfo.Name,
                        property.PropertyInfo.PropertyType.FullName,
                        property.FieldModelAttribute.ExpectedType().FullName
                        ));
                }
            }
        }
        
        private static void SetChildrenProperty(TModel result, Item item, int depth)
        {
            if (result == null)
            {
                return;
            }

            List<IItemAdapterModel> modelChildren = new List<IItemAdapterModel>();
            Item[] itemChildren = item.GetChildren().ToArray();
            foreach (Item child in itemChildren)
            {
                modelChildren.Add(_childItemAdapter.LoadModel(child, --depth));
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

        private static void SetExtendedProperties(TModel result, Item item, int depth)
        {
            if (result == null)
            {
                return;
            }

            foreach (ItemAdapterModelProperty property in _extendedProperties)
            {
                property.PropertyInfo.SetValue(result, property.FieldModelAttribute.GetFieldValue(item, property.PropertyInfo.PropertyType, depth), null);
            }
        }

        private static void UpdateFieldValues(TModel model, Item item)
        {
            foreach (ItemAdapterModelProperty property in _properties)
            {
                object propertyValue = property.PropertyInfo.GetValue(model, null);
                if (propertyValue != null)
                {
                    object fieldValue = property.FieldModelAttribute.SetFieldValue(
                        item,
                        property.PropertyInfo.PropertyType,
                        propertyValue);
                }
            }
        }
    }

}
