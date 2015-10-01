using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.ItemAdapter.FieldTypes;

namespace Sitecore.ItemAdapter.Model
{
    [Serializable]
    [AdapterItemModel]
    public class AdapterItem
    {
        public AdapterItem()
        {
            Version = 0;
        }

        public const string DisplayNameFieldId = "{B5E02AD9-D56F-4C41-A065-A133DB87BDEB}";

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid TemplateId { get; set; }

        public string TemplateName { get; set; }
        
        [AdapterTextField(DisplayNameFieldId)]
        public string DisplayName { get; set; }

        public int Version { get; set; }

        public string Language { get; set; }

        [AdapterExtendedProperty]
        [AdapterCheckboxField("{39C4902E-9960-4469-AEEF-E878E9C8218F}")]
        public Nullable<bool> Hidden { get; set; }

        [AdapterExtendedProperty]
        [AdapterIntegerField("{BA3F86A2-4A1C-4D78-B63D-91C2779C1B5E}")]
        public Nullable<int> SortOrder { get; set; }
        
        [AdapterExtendedProperty]
        public virtual IEnumerable<AdapterItem> Children { get; set; }

    }
}
