using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;

namespace Sitecore.ItemAdapter.FieldTypes
{
    using Sitecore.Data;
    using Sitecore.Pipelines.PasswordRecovery;

    public class SitecoreTextFieldAttribute : SitecoreFieldModelAttribute
    {
        public SitecoreTextFieldAttribute(string fieldId) : base(fieldId)
        {
        }

        protected override object GetValue(Item item, Type propertyType)
        {
            if (propertyType.IsEquivalentTo(typeof(string)))
            {
                return item[FieldId];
            }
            else
            {
                return null;
            }
        }
    }
    

    
}
