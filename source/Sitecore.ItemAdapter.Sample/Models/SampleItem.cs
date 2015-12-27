using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ItemAdapter.FieldTypes.NestedAdapter;

namespace Sitecore.ItemAdapter.Sample.Models
{
    using Sitecore.Data;
    using Sitecore.ItemAdapter.Model;
    using Sitecore.ItemAdapter.FieldTypes;

    [Serializable]
    [ItemAdapterModel(SampleItemTemplateId, typeof(SampleItem))]
    public class SampleItem : ItemAdapterModel
    {
        public const string SampleItemTemplateId = "{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}";
        public const string SampleItemTitleFieldId = "{75577384-3C97-45DA-A847-81B00500E250}";
        public const string SampleItemBodyFieldId = "{A60ACD61-A6DB-4182-8329-C957982CEC74}";

        [ItemAdapterTextField(SampleItemTitleFieldId)]
        public string Title { get; set; }
        
        [ItemAdapterExtendedProperty]
        [ItemAdapterRichTextField(SampleItemBodyFieldId)]
        public string Text { get; set; }

        [ItemAdapterDateTimeField("{19EB33FB-FDBB-4E44-9122-519B6A8FE84E}")]
        public Nullable<DateTime> Date { get; set; }

        [ItemAdapterDateTimeField("{B8ED96C2-3C85-4B6A-9563-D67D1E884AA2}")]
        public Nullable<DateTime> DateTime { get; set; }

        [ItemAdapterCheckboxField("{F5E51289-C58E-4CA6-A16C-521F1AC57FCC}")]
        public Nullable<bool> Checkbox { get; set; }

        [ItemAdapterImageUrlField("{BE649F47-3B2F-42A3-A910-8656A9775326}")]
        public string Image { get; set; }

        [ItemAdapterTextField("{B27FE2AF-2887-416A-B480-1765D8840006}")]
        public string MultiLineText { get; set; }

        [ItemAdapterIntegerField("{2960CA34-C7B3-4FC0-98E8-9B40CFA90D7E}")]
        public Nullable<int> Integer { get; set; }

        [ItemAdapterNumberField("{453665A9-47F9-4A0D-96BF-6837A80024F9}")]
        public Nullable<Decimal> Number { get; set; }

        [ItemAdapterGeneralLinkField("{E2DDCCB6-7D22-475D-87BC-C39B8B511BE6}")]
        public IGeneralLink GeneralLink { get; set; }

        [ItemAdapterExtendedProperty]
        [ItemAdapterLinkField("{340D1B43-ECA6-4567-8B02-500C01ADBE02}", typeof(ItemAdapterModel))]
        public IItemAdapterModel Link { get; set; }

        [ItemAdapterExtendedProperty]
        [ItemAdapterMultilistField("{44B8C8E0-D6E7-44BF-BC3F-3BDDEAD90A74}", typeof(ItemAdapterModel))]
        public List<IItemAdapterModel> MultiList { get; set; }

        [ItemAdapterExtendedProperty]
        [ItemAdapterMultilistField("{E5DE474C-04CB-41F8-B5F8-3C171EE5E8FE}", typeof(ItemAdapterModel))]
        public List<IItemAdapterModel> Checklist { get; set; }

        [ItemAdapterExtendedProperty]
        [ItemAdapterFileUrlField("{A71C0579-8E0D-4658-92C9-03A7A260F383}")]
        public string FileUrl { get; set; }
        
        [ItemAdapterExtendedProperty]
        [ItemAdapterNameValueListField("{16075611-9204-4F76-A25A-871E24323550}")]
        public SortedList<string,string> NameValueList { get; set; }
        
        //[ItemAdapterExtendedProperty]
        //[ItemAdapterNameLookupListField("{088D04CC-F860-41BE-8009-A51BB9037FB8}", typeof(IItemAdapter<ItemAdapterModel>))]
        //public SortedList<string, IItemAdapterModel> NameLookupList { get; set; }

    }
}
