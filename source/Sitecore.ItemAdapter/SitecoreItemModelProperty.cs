using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemAdapter.FieldTypes;

namespace Sitecore.ItemAdapter
{
    public class SitecoreItemModelProperty
    {
        public SitecoreItemModelProperty(PropertyInfo property, SitecoreFieldModelAttribute modelAttribute)
        {
            PropertyInfo = property;
            this.FieldModelAttribute = modelAttribute;
        }

        public PropertyInfo PropertyInfo
        {
            get; private set;
        }
        public SitecoreFieldModelAttribute FieldModelAttribute { get; private set; }
    }
}
