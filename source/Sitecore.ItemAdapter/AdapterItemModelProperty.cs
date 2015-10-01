using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemAdapter.FieldTypes;

namespace Sitecore.ItemAdapter
{
    internal class AdapterItemModelProperty
    {
        public AdapterItemModelProperty(PropertyInfo property, AdapterFieldAttribute modelAttribute)
        {
            PropertyInfo = property;
            this.FieldModelAttribute = modelAttribute;
        }

        public PropertyInfo PropertyInfo
        {
            get; private set;
        }
        public AdapterFieldAttribute FieldModelAttribute { get; private set; }
    }
}
