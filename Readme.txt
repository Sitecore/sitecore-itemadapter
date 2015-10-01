David Morrison
twitter: @dmo_sc
2015-10-01
v0.1.0

Example Usage

	SampleItem ItemModel = StandardItemAdapter<SampleItem>.GetExtendedModel(Sitecore.Context.Item);

Example Model

	[Serializable]
	[AdapterItemModel(SampleItemTemplateId)]
	public class SampleItem : AdapterItem
	{
		public const string SampleItemTemplateId = "{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}";
		public const string SampleItemTitleFieldId = "{75577384-3C97-45DA-A847-81B00500E250}";
		public const string SampleItemBodyFieldId = "{A60ACD61-A6DB-4182-8329-C957982CEC74}";

		[AdapterTextField(SampleItemTitleFieldId)]
		public string Title { get; set; }

		[AdapterExtendedProperty]
		[AdapterRichTextField(SampleItemBodyFieldId)]
		public string Text { get; set; }
	}

