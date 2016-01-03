using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemAdapter.Model
{
    public interface IItemAdapterModel
    {
        Guid Id { get; }

        void Initialize(Guid id);
        
        void Loaded(bool extended = false);

        bool IsLoaded(bool extended);

        string Name { get; set; }

        Guid TemplateId { get; set; }

        string TemplateName { get; set; }
        
        string DisplayName { get; set; }

        int Version { get; set; }

        string Language { get; set; }
        
        Nullable<bool> Hidden { get; set; }
        
        Nullable<int> SortOrder { get; set; }
        
        IEnumerable<IItemAdapterModel> Children { get; set; }

        IGeneralLink CreateGeneralLink();

    }
}
