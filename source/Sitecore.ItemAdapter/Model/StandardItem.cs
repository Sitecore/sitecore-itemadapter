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
    [SitecoreItemModel]
    public class StandardItem
    {
        public const string DisplayNameFieldId = "{C27AC3EB-1145-433C-BED2-96F6931ECDE8}";

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid TemplateId { get; set; }

        public string TemplateName { get; set; }
        
        [SitecoreTextField(DisplayNameFieldId)]
        public string DisplayName { get; set; }
        
    }
}
