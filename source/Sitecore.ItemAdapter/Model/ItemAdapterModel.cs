using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.ItemAdapter.FieldTypes;

namespace Sitecore.ItemAdapter.Model
{
    [Serializable]
    [ItemAdapterModel(typeof(ItemAdapterModel))]
    public class ItemAdapterModel : IItemAdapterModel
    {
        public ItemAdapterModel()
        {
            Version = 0;
            Id = Guid.Empty;
        }

        public void SetId(Guid id)
        {
            if (!Guid.Empty.Equals(Id)) { throw new InvalidOperationException(); }
            Id = id;
        }

        private bool _loaded = false;

        public void Loaded()
        {
            _loaded = true;
        }

        public bool IsLoaded()
        {
            return _loaded;
        }

        public const string DisplayNameFieldId = "{B5E02AD9-D56F-4C41-A065-A133DB87BDEB}";

        public Guid Id { get; protected set; }

        public string Name { get; set; }

        public Guid TemplateId { get; set; }

        public string TemplateName { get; set; }
        
        [ItemAdapterTextField(DisplayNameFieldId)]
        public string DisplayName { get; set; }

        public int Version { get; set; }

        public string Language { get; set; }

        [ItemAdapterExtendedProperty]
        [ItemAdapterCheckboxField("{39C4902E-9960-4469-AEEF-E878E9C8218F}")]
        public Nullable<bool> Hidden { get; set; }

        [ItemAdapterExtendedProperty]
        [ItemAdapterIntegerField("{BA3F86A2-4A1C-4D78-B63D-91C2779C1B5E}")]
        public Nullable<int> SortOrder { get; set; }
        
        [ItemAdapterExtendedProperty]
        public virtual IEnumerable<IItemAdapterModel> Children { get; set; }

        public virtual IGeneralLink CreateGeneralLink()
        {
            return new GeneralLink();
        }
    }
}
