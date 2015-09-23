using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemAdapter.Model;

namespace Sitecore.ItemAdapter.Sample
{
    using Sitecore.Data;
    using Sitecore.ItemAdapter.FieldTypes;

    [Serializable]
    [SitecoreItemModel(SampleItemTemplateId)]
    public class SampleItem : StandardItem
    {
        public const string SampleItemTemplateId = "{DE2318C7-67B4-40EB-B98F-E09257ED1023}";
        public const string SampleItemTitleFieldId = "{A2C5293C-B8B2-42A8-8236-84BA099D6699}";
        public const string SampleItemBodyFieldId = "{3C1DF511-7F47-4168-BBA3-AEEBB9419949}";

        [SitecoreTextField(SampleItemTitleFieldId)]
        public string Title { get; set; }


        [SitecoreRichTextField(SampleItemBodyFieldId)]
        public string Body { get; set; }


    }
}
